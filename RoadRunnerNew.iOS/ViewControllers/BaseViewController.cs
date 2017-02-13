using System;
using UIKit;
using BigTed;
using System.Threading.Tasks;
using System.Net.Http;
using Foundation;
using System.Collections.Generic;
using RoadRunner.Shared;
using CoreGraphics;

namespace RoadRunnerNew.iOS
{

	public class BaseViewController : UIViewController
	{
		public BaseViewController() : base()
		{

		}
		public BaseViewController(IntPtr handle) : base(handle)
		{
		}

		protected void ShowLoadingView(string title)
		{
			InvokeOnMainThread(() => { BTProgressHUD.Show(title, -1, ProgressHUD.MaskType.Black); });
		}

		protected void HideLoadingView()
		{
			InvokeOnMainThread(() => { BTProgressHUD.Dismiss(); });
		}


		// Show the alert view
		protected void ShowMessageBox(string title, string message, string cancelButton, string[] otherButtons, Action successHandler)
		{
			var alertView = new UIAlertView(title, message, null, cancelButton, otherButtons);
			alertView.Clicked += (sender, e) =>
			{
				if (e.ButtonIndex == 0)
				{
					return;
				}
				if (successHandler != null)
				{
					successHandler();
				}
			};
			alertView.Show();
		}

		//overloaded method
		protected void ShowMessageBox(string title, string message)
		{
			ShowMessageBox(title, message, "Ok", null, null);
		}

		protected bool TextFieldShouldReturn(UITextField textField)
		{
			textField.ResignFirstResponder();
			return true;
		}

		protected UIImage LoadImageSync(string imageUrl)
		{
			byte[] contents = new byte[0];

			Task runSync = Task.Factory.StartNew(async () => {
				var httpClient = new HttpClient();
				contents = await httpClient.GetByteArrayAsync (imageUrl);
			}).Unwrap();
			runSync.Wait();

			return UIImage.LoadFromData (NSData.FromArray (contents));
		}

		protected async Task<UIImage> LoadImage (string imageUrl)
		{
			var httpClient = new HttpClient();
			var contents = await httpClient.GetByteArrayAsync (imageUrl);
			return UIImage.LoadFromData (NSData.FromArray (contents));
		}

		protected static DateTime NSDateToDateTime (NSDate date)
		{
			DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime (new DateTime (2001, 1, 1, 0, 0, 0));
			reference = reference.AddSeconds (date.SecondsSinceReferenceDate);
			if (reference.IsDaylightSavingTime ()) {
				reference = reference.AddHours (1);
			}
			return reference;
		}

		//From Xamarin
		public static DateTime ToDateTime (NSDate date)
		{
			DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime( 
				new DateTime(2001, 1, 1, 0, 0, 0) );
			return reference.AddSeconds(date.SecondsSinceReferenceDate);
		}

		//From Xamarin
		public static NSDate ToNSDate(DateTime date)
		{
			DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(
				new DateTime(2001, 1, 1, 0, 0, 0) );
			return NSDate.FromTimeIntervalSinceReferenceDate(
				(date - reference).TotalSeconds);
		}


		protected void SetupFlightType (UITextField field, bool IsPickUp)
		{
			List<KeyValuePair<object, string>> list = new List<KeyValuePair<object, string>> ();
			list.Add(new KeyValuePair<object, string>(true, String.Format("Domestic")));
			list.Add(new KeyValuePair<object, string>(false, String.Format("International")));
			//SetupGenericSelector (field, list);

			if (IsPickUp == true) {
				Facade.Instance.CurrentRide.PickUpFlightTypeIsDomestic = null;
				//field.Text = list [0].Value;
			}
			if (IsPickUp == false) {
				Facade.Instance.CurrentRide.DropOffFlightTypeIsDomestic = null;
				//field.Text = list [0].Value;
			}

			MyPickerModel model = new MyPickerModel (list);
			model.PickerChanged += (sender, e) => {
				field.Text = e.SelectedValue;

				bool outResult;
				if (bool.TryParse (e.SelectedKey.ToString (), out outResult)) {
					if (IsPickUp) {
						Facade.Instance.CurrentRide.PickUpFlightTypeIsDomestic = outResult;	
					} else {
						Facade.Instance.CurrentRide.DropOffFlightTypeIsDomestic = outResult;	
					}
				} else {
					if (list.Count > 0) {
						field.Text = list [0].Value;

						if (IsPickUp) {
							Facade.Instance.CurrentRide.PickUpFlightTypeIsDomestic = (bool)list [0].Key;
						} else {
							Facade.Instance.CurrentRide.PickUpFlightTypeIsDomestic = (bool)list [0].Key;
						}
					}
				}

				field.SendActionForControlEvents (UIControlEvent.ValueChanged);
			};

			UIPickerView picker = new UIPickerView ();
			picker.ShowSelectionIndicator = true;
			picker.Model = model;
			picker.BackgroundColor = new UIColor ((nfloat)(19.0 / 255.0), (nfloat)(36.0 / 255.0), (nfloat)(65.0 / 255.0), (nfloat)1.0);

			// Setup the toolbar
			UIToolbar toolbar = new UIToolbar ();
			toolbar.BarStyle = UIBarStyle.Black;
			toolbar.Translucent = true;
			toolbar.SizeToFit ();

			// Create a 'done' button for the toolbar and add it to the toolbar
			UIBarButtonItem doneButton = new UIBarButtonItem ("Done", UIBarButtonItemStyle.Done, (s, e) => {

				//Default selection if nothing was selected
				if (IsPickUp == true && Facade.Instance.CurrentRide.PickUpFlightTypeIsDomestic == null && list.Count > 0) {
					Facade.Instance.CurrentRide.PickUpFlightTypeIsDomestic = (bool)list [0].Key;
					field.Text = list [0].Value;
				}
				if (IsPickUp == false && Facade.Instance.CurrentRide.DropOffFlightTypeIsDomestic == null && list.Count > 0) {
					Facade.Instance.CurrentRide.DropOffFlightTypeIsDomestic = (bool)list [0].Key;
					field.Text = list [0].Value;
				}			

				field.ResignFirstResponder ();
			});

			toolbar.SetItems (new UIBarButtonItem[]{ doneButton }, true);

			field.InputView = picker;
			field.InputAccessoryView = toolbar;

			field.ShouldChangeCharacters = new UITextFieldChange (delegate (UITextField textField, NSRange range, string replacementString) {
				return false;
			});
		}

		protected void SetupPassengers (UITextField field)
		{
			List<KeyValuePair<object, string>> list = new List<KeyValuePair<object, string>> ();
			for (int i = 1; i <= 100; i++) {
				list.Add (new KeyValuePair<object, string> (i, String.Format ("{0}", i)));
			}
			SetupGenericSelector (field, list);
		}

		protected void SetupExtraBags (UITextField field)
		{
			List<KeyValuePair<object, string>> list = new List<KeyValuePair<object, string>> ();
			for (int i = 1; i < 10; i++) {
				list.Add (new KeyValuePair<object, string> (i, String.Format ("{0}", i)));
			}
			SetupGenericSelector (field, list);
		}

		protected void SetupGratuity (UITextField field)
		{
			List<KeyValuePair<object, string>> list = new List<KeyValuePair<object, string>> ();
			list.Add (new KeyValuePair<object, string> (0, String.Format ("0%")));
			list.Add (new KeyValuePair<object, string> (0.1, String.Format ("10%")));
			list.Add (new KeyValuePair<object, string> (0.15, String.Format ("15%")));
			list.Add (new KeyValuePair<object, string> (0.2, String.Format ("20%")));
			SetupGenericSelector (field, list);
		}

		protected void SetupCreditCards (UITextField field, UIImageView image, List<KeyValuePair<object, string>> list, List<KeyValuePair<object, object>> list1)
		{
			// Setup the picker and model
			MyPickerModel model = new MyPickerModel (list);

			model.PickerChanged += (sender, e) => {				
				Facade.Instance.CurrentRide.CreditCardId = e.SelectedKey.ToString();
				field.Text = e.SelectedValue;
				field.SendActionForControlEvents (UIControlEvent.ValueChanged);

				for(int i = 0; i < list1.Count; i++)
				{
					if(list1[i].Key.ToString() == e.SelectedKey.ToString())
					{
						image.Image = (UIImage)list1[i].Value;
					}
				}

				AppSettings.SelectedCard = e.SelectedKey.ToString();
			};

			UIPickerView picker = new UIPickerView ();
			picker.ShowSelectionIndicator = true;
			picker.Model = model;
			picker.BackgroundColor = new UIColor ((nfloat)(19.0 / 255.0), (nfloat)(36.0 / 255.0), (nfloat)(65.0 / 255.0), (nfloat)1.0);

			// Setup the toolbar
			UIToolbar toolbar = new UIToolbar ();
			toolbar.BarStyle = UIBarStyle.Black;
			toolbar.Translucent = true;
			toolbar.SizeToFit ();

			// Create a 'done' button for the toolbar and add it to the toolbar
			UIBarButtonItem doneButton = new UIBarButtonItem ("Done", UIBarButtonItemStyle.Done, (s, e) => {
				if (String.IsNullOrEmpty (Facade.Instance.CurrentRide.CreditCardId) && list.Count > 0) {
					Facade.Instance.CurrentRide.CreditCardId = list [0].Key.ToString ();
					field.Text = list [0].Value.ToString ();

					field.SendActionForControlEvents (UIControlEvent.ValueChanged);

					AppSettings.SelectedCard = list [0].Key.ToString ();
				}
				field.ResignFirstResponder ();
			});

			toolbar.SetItems (new UIBarButtonItem[]{ doneButton }, true);

			field.InputView = picker;
			field.InputAccessoryView = toolbar;

			field.ShouldChangeCharacters = new UITextFieldChange (delegate (UITextField textField, NSRange range, string replacementString) {
				return false;
			});
		}


		protected void SetupReadyPickup (UITextField field, List<KeyValuePair<object, string>> list, Action<string> callback)
		{
			// Setup the picker and model
			MapPickerModel model = new MapPickerModel (list);

			model.PickerChanged += (sender, e) => {				
				Facade.Instance.CurrentRide.ReadyReservationId = e.SelectedKey.ToString();
				field.Text = e.SelectedValue;
				field.SendActionForControlEvents (UIControlEvent.ValueChanged);
			};

			UIPickerView picker = new UIPickerView ();
			picker.ShowSelectionIndicator = true;
			picker.Model = model;
			picker.BackgroundColor = new UIColor ((nfloat)(19.0 / 255.0), (nfloat)(36.0 / 255.0), (nfloat)(65.0 / 255.0), (nfloat)1.0);
			picker.TintColor = UIColor.White;

			// Setup the toolbar
			UIToolbar toolbar = new UIToolbar ();
			toolbar.BarStyle = UIBarStyle.Black;
			toolbar.Translucent = true;
			toolbar.SizeToFit ();

			// Create a 'done' button for the toolbar and add it to the toolbar
			UIBarButtonItem doneButton = new UIBarButtonItem ("Done", UIBarButtonItemStyle.Done, (s, e) => {
				if (String.IsNullOrEmpty (Facade.Instance.CurrentRide.CreditCardId) && list.Count > 0) {
					Facade.Instance.CurrentRide.ReadyReservationId = list [0].Key.ToString ();
					field.Text = list [0].Value.ToString ();

					field.SendActionForControlEvents (UIControlEvent.ValueChanged);

					AppSettings.SelectedCard = list [0].Key.ToString ();
				}
				field.ResignFirstResponder ();
			});

			toolbar.SetItems (new UIBarButtonItem[]{ doneButton }, true);

			UIView topView = new UIView (toolbar.Frame);
			UIButton btnDone = new UIButton (new CGRect(0, 0, toolbar.Frame.Width, toolbar.Frame.Height));
			btnDone.SetTitle ("SELECT RIDE READY FOR PICKUP", UIControlState.Normal);
			btnDone.BackgroundColor = new UIColor ((nfloat)(21.0 / 255.0), (nfloat)(100.0 / 255.0), (nfloat)(179.0 / 255.0), (nfloat)1.0);
			btnDone.TouchUpInside += (object sender, EventArgs e) => {
				var reservationID = Facade.Instance.CurrentRide.ReadyReservationId;
				callback(reservationID);
				field.ResignFirstResponder ();
			};
			topView.AddSubview (btnDone);

			field.InputView = picker;
			field.InputAccessoryView = topView;

			field.ShouldChangeCharacters = new UITextFieldChange (delegate (UITextField textField, NSRange range, string replacementString) {
				return false;
			});
		}


		/// <summary>
		/// Setups the airlines.
		/// </summary>
		/// <param name="field">Field.</param>
		/// <param name="list">List.</param>
		/// <param name="direction">Direction 1 - Pick-Up and 2 - Drop-off </param>
		protected void SetupAirlines(UITextField field, List<KeyValuePair<object, string>> list, int direction=0)
		{
			// Setup the picker and model
			MyPickerModel model = new MyPickerModel (list);

			if (direction == 1) {
				Facade.Instance.CurrentRide.PickUpAirlinesId = string.Empty;
				Facade.Instance.CurrentRide.PickUpAirlines = string.Empty;
			}
			if (direction == 2) {
				Facade.Instance.CurrentRide.DropOffAirlinesId = string.Empty;
				Facade.Instance.CurrentRide.DropOffAirlines = string.Empty;
			}	

			model.PickerChanged += (sender, e) => {
				switch (direction) {
				case 1:
					Facade.Instance.CurrentRide.PickUpAirlinesId = e.SelectedKey.ToString();
					Facade.Instance.CurrentRide.PickUpAirlines = e.SelectedValue.ToString();
					break;
				case 2:
					Facade.Instance.CurrentRide.DropOffAirlinesId = e.SelectedKey.ToString();
					Facade.Instance.CurrentRide.DropOffAirlines = e.SelectedValue.ToString();
					break;
				default:
					throw new NotImplementedException ();
				}
				field.Text = e.SelectedValue;
				field.SendActionForControlEvents (UIControlEvent.ValueChanged);
			};

			UIPickerView picker = new UIPickerView ();
			picker.ShowSelectionIndicator = true;
			picker.Model = model;
			picker.BackgroundColor = new UIColor ((nfloat)(19.0 / 255.0), (nfloat)(36.0 / 255.0), (nfloat)(65.0 / 255.0), (nfloat)1.0);

			// Setup the toolbar
			UIToolbar toolbar = new UIToolbar ();
			toolbar.BarStyle = UIBarStyle.Black;
			toolbar.Translucent = true;
			toolbar.SizeToFit ();

			// Create a 'done' button for the toolbar and add it to the toolbar
			UIBarButtonItem doneButton = new UIBarButtonItem ("Done", UIBarButtonItemStyle.Done, (s, e) => {

				//Default selection if nothing was selected
				if (direction == 1 && String.IsNullOrEmpty (Facade.Instance.CurrentRide.PickUpAirlinesId) && list.Count > 0) {
					Facade.Instance.CurrentRide.PickUpAirlinesId = list [0].Key.ToString ();
					Facade.Instance.CurrentRide.PickUpAirlines = list [0].Value.ToString ();

					field.Text = list [0].Value;
				}
				if (direction == 2 && String.IsNullOrEmpty (Facade.Instance.CurrentRide.DropOffAirlinesId) && list.Count > 0) {
					Facade.Instance.CurrentRide.DropOffAirlinesId = list [0].Key.ToString ();
					Facade.Instance.CurrentRide.DropOffAirlines = list [0].Value.ToString ();

					field.Text = list [0].Value;
				}		

				field.ResignFirstResponder ();
			});

			toolbar.SetItems (new UIBarButtonItem[]{ doneButton }, true);

			field.InputView = picker;
			field.InputAccessoryView = toolbar;

			field.ShouldChangeCharacters = new UITextFieldChange (delegate (UITextField textField, NSRange range, string replacementString) {
				return false;
			});
		}

		protected void SetupGenericSelector(UITextField field, List<KeyValuePair<object, string>> list)
		{
			// Setup the picker and model
			MyPickerModel model = new MyPickerModel (list);
			model.PickerChanged += (sender, e) => {
				field.Text = e.SelectedValue;
				field.SendActionForControlEvents (UIControlEvent.ValueChanged);
			};

			UIPickerView picker = new UIPickerView ();
			picker.ShowSelectionIndicator = true;
			picker.Model = model;
			picker.BackgroundColor = new UIColor ((nfloat)(19.0 / 255.0), (nfloat)(36.0 / 255.0), (nfloat)(65.0 / 255.0), (nfloat)1.0);

			// Setup the toolbar
			UIToolbar toolbar = new UIToolbar ();
			toolbar.BarStyle = UIBarStyle.Black;
			toolbar.Translucent = true;
			toolbar.SizeToFit ();

			// Create a 'done' button for the toolbar and add it to the toolbar
			UIBarButtonItem doneButton = new UIBarButtonItem ("Done", UIBarButtonItemStyle.Done, (s, e) => {
				
				//Nothing was selected, just press done
				if (String.IsNullOrEmpty (field.Text) && list.Count > 0) {
					field.Text = list [0].Value;
					field.SendActionForControlEvents (UIControlEvent.ValueChanged);
				}
				field.ResignFirstResponder ();
			});

			toolbar.SetItems (new UIBarButtonItem[]{ doneButton }, true);

			field.InputView = picker;
			field.InputAccessoryView = toolbar;

			field.ShouldChangeCharacters = new UITextFieldChange (delegate (UITextField textField, NSRange range, string replacementString) {
				return false;
			});
		}
			
		/// <summary>
		/// Setups the time picker.
		/// </summary>
		/// <param name="field">Field.</param>
		/// <param name="mode">Mode.</param>
		/// <param name="format">Format. Like "{0: hh:mm tt}" or "{0: MM/dd/yyyy hh:mm tt}" </param>
		/// <param name="changeOnEdit">If set to <c>true</c> change on edit.</param>
		protected void SetupTimePicker (UITextField field, UIDatePickerMode mode = UIDatePickerMode.Time, String format = "{0: hh:mm tt}", bool futureDatesOnly = false, DateTime? minimumDateTime=null, bool changeOnEdit=false)
		{
			UIDatePicker picker = new UIDatePicker ();
			picker.Mode = mode;

			if(minimumDateTime!=null)
			{ 				
				NSDate nsMinDateTime = ToNSDate((DateTime)minimumDateTime);
				picker.MinimumDate = nsMinDateTime;
			}
			if (futureDatesOnly) {
				NSDate nsMinDateTime = ToNSDate(DateTime.Now);
				picker.MinimumDate = nsMinDateTime;
			}

			picker.ValueChanged += (object s, EventArgs e) => {
				if (futureDatesOnly) {
					NSDate nsMinDateTime = ToNSDate(DateTime.Now);
					picker.MinimumDate = nsMinDateTime;
				}
				if (changeOnEdit) {
					updateSetupDateTimePicker (field, picker.Date, format, s, e);
				}
			};

			// Setup the toolbar
			UIToolbar toolbar = new UIToolbar ();
			toolbar.BarStyle = UIBarStyle.Black;
			toolbar.Translucent = true;
			toolbar.SizeToFit ();

			// Create a 'done' button for the toolbar and add it to the toolbar
			UIBarButtonItem doneButton = new UIBarButtonItem ("Done", UIBarButtonItemStyle.Done, (s, e) => {
				updateSetupDateTimePicker (field, picker.Date, format, s, e, true);
			});

			toolbar.SetItems (new UIBarButtonItem[]{ doneButton }, true);

			field.InputView = picker;
			field.InputAccessoryView = toolbar;

			field.ShouldChangeCharacters = new UITextFieldChange (delegate (UITextField textField, NSRange range, string replacementString) {
				return false;
			});
		}

		private void updateSetupDateTimePicker(UITextField field, NSDate date, String format, object sender, EventArgs e, bool done=false){
			var newDate = NSDateToDateTime (date);
			var str = String.Format (format, newDate);

			field.Text = str;
			field.SendActionForControlEvents (UIControlEvent.ValueChanged);
			if (done) {
				field.ResignFirstResponder ();
			}
		}

		protected string getMapVehicleIconByServiceID(string serviceID){
			string returnName = "suv_map";
			switch(serviceID)
			{
			case "0":
				//returnName = 0;
				break;
			case "1":
			case "11":
			case "12":
				returnName = "Shuttle-Icon.png";
				break;
			case "2":
				returnName = "Towncar-Icon.png";
				break;
			case "4":
				returnName = "SUV-Icon.png";
				break;
			case "3":
			case "6":
				returnName = "Limo-Icon.png";
				break;
			case "9":
			case "13":
				returnName = "Bus-Icon.png";
				break;
			case "5":
				//serviceID = 5;
				break;
			default:
				break;
			}

			return returnName;
		}

		protected UIImage rotateImage(UIImage sourceImage, float rotate)
		{
			float rads = (float)Math.PI * rotate / 180;
			UIImage returnImage;

			using (CGImage imageRef = sourceImage.CGImage)
			{
				UIView rotatedViewBox = new UIView(new CGRect(0,0, imageRef.Width, imageRef.Height));

				CGAffineTransform t = CGAffineTransform.MakeIdentity();
				t.Rotate(rads);
				rotatedViewBox.Transform = t;
				CGSize rotatedSize = rotatedViewBox.Frame.Size;


				UIGraphics.BeginImageContextWithOptions(rotatedSize, false, sourceImage.CurrentScale);
				var context = UIGraphics.GetCurrentContext();
				context.TranslateCTM(rotatedSize.Width / 2, rotatedSize.Height / 2);
				context.RotateCTM(rads);
				context.ScaleCTM(1.0f, -1.0f);
				var rect = new CGRect(-sourceImage.Size.Width / 2, -sourceImage.Size.Height / 2, sourceImage.Size.Width, sourceImage.Size.Height);
				context.DrawImage(rect, sourceImage.CGImage);
				//context.FillRect(rect);

				returnImage = UIGraphics.GetImageFromCurrentImageContext();
				UIGraphics.EndImageContext ();
			}

			return returnImage;
		}
	}
}