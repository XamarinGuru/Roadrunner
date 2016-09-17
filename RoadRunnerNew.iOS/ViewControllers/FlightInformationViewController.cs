using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using CoreGraphics;
using RoadRunnerNew.iOS.Extension;
using System.Collections.Generic;
using RoadRunner.Shared;
using RoadRunner.Shared.Classes;
using System.Drawing;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Helpers;
using System.Linq;

namespace RoadRunnerNew.iOS
{
	partial class FlightInformationViewController : BaseViewController
	{
		private  static bool IsFirstTime = true;

		private UINavigationController thisController { get; set; }

		public FlightInformationViewController (IntPtr handle) : base (handle)
		{
			Title = "FLIGHT INFORMATION";
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			//thisController = NavigationController;
			AppSettings.CurrentNavigation = NavigationController;
			NavigationItem.Customize (NavigationController);

			var tap = new UITapGestureRecognizer(() => { View.EndEditing(true); });
			View.AddGestureRecognizer(tap);

			edtDropOffFlight.ShouldReturn += TextFieldShouldReturn;

			btnScheduleARide3.SetCustomButton ();

			SetBindings ();
			if (Facade.Instance.CurrentRide.IsFirstTime) {
				Facade.Instance.CurrentRide.IsFirstTime = false;
				SetBindingsOnce ();
			}
		}

		private void SetBindings(){
			
			#region //BEGIN Set up Airlines 
			var AirlineResponseItemList = new List<GetAirlineResponseItem> ();

			Task runSync = Task.Factory.StartNew (async () => {
				AirlineResponseItemList = await GetAirline ();
			}).Unwrap ();
			runSync.Wait ();

			var KvpAirlines = new List<KeyValuePair<object, string>> ();
			foreach (var item in AirlineResponseItemList) {
				KvpAirlines.Add (new KeyValuePair<object, string> (item.id, item.Airline));
			}

			SetupAirlines (edtDropOffAirlines, KvpAirlines, 2);

			#endregion 

			SetupTimePicker (edtRequestedArrivalTimeAndDate, UIDatePickerMode.DateAndTime, "{0: MM/dd/yyyy hh:mm tt}", futureDatesOnly: true);
			SetupTimePicker (edtDropOffFlightTime, UIDatePickerMode.Time, "{0: hh:mm tt}");

			SetupFlightType (edtDropOffFlightType, false);

			this.SetBinding (
				() => edtRequestedArrivalTimeAndDate.Text,
				() => Facade.Instance.CurrentRide.RequestedArrivalTimeAndDate,
				BindingMode.TwoWay)
				.UpdateSourceTrigger ("ValueChanged");

			#region PickUp elements

			this.SetBinding (
				() => Facade.Instance.CurrentRide.PickUpLocation,
				() => edtAirportPickUpLocation.Text,
				BindingMode.TwoWay)
				.UpdateSourceTrigger ("PickUpLocationChanges");
			
			#endregion

			#region Dropoff elements

			this.SetBinding (
				() => Facade.Instance.CurrentRide.DropOffLocation,
				() => editAirportDropOffLocation.Text,
				BindingMode.TwoWay)
				.UpdateSourceTrigger ("DropOffLocationChanges");
			
			this.SetBinding (
				() => edtDropOffFlight.Text,
				() => Facade.Instance.CurrentRide.DropOffFlightNumber,
				BindingMode.TwoWay)
				.UpdateSourceTrigger ("EditingChanged");

			this.SetBinding (
				() => edtDropOffFlightTime.Text,
				() => Facade.Instance.CurrentRide.DropOffFlightTime,
				BindingMode.TwoWay)
				.UpdateSourceTrigger ("ValueChanged");

			#endregion

			edtAirportPickUpLocation.EditingDidBegin += (object sender, EventArgs e) => {

				UIStoryboard sb = UIStoryboard.FromName ("MainStoryboard", null);
				PickUpViewController pvc = (PickUpViewController)sb.InstantiateViewController ("PickUpViewController");

				pvc.IsPickUpLocation = true;

				edtAirportPickUpLocation.ResignFirstResponder ();
				NavigationController.PushViewController (pvc, true);
			};

			editAirportDropOffLocation.EditingDidBegin += (object sender, EventArgs e) => {

				UIStoryboard sb = UIStoryboard.FromName ("MainStoryboard", null);
				PickUpViewController pvc = (PickUpViewController)sb.InstantiateViewController ("PickUpViewController");

				pvc.IsPickUpLocation = false;

				editAirportDropOffLocation.ResignFirstResponder ();
				NavigationController.PushViewController (pvc, true);
			};

			btnScheduleARide3.SetCommand ("TouchUpInside", Facade.Instance.CurrentRide.GoToThePaymentInformation);
		}

		private void SetBindingsOnce(){
			this.SetBinding (
				() => Facade.Instance.CurrentRide.CanGoToThePaymentInformation)
				.UpdateSourceTrigger ("CanGoToThePaymentInformationChanges")
				.WhenSourceChanges (
					async () => {
						if (Facade.Instance.CurrentRide.CanGoToThePaymentInformation) {
							UIStoryboard sb = UIStoryboard.FromName ("MainStoryboard", null);
							PaymentInformationViewController pvc = (PaymentInformationViewController)sb.InstantiateViewController("ScheduleARideViewController3");
							//thisController.PushViewController (pvc, true);
							AppSettings.CurrentNavigation.PushViewController(pvc, true);
						} else {
							if (Facade.Instance.CurrentRide.ValidaionError != null && Facade.Instance.CurrentRide.ValidaionError.Count > 0) {
								string header = Facade.Instance.CurrentRide.ValidaionError.Count > 1
									? "Just a couple things left:"
									: "Please correct the last thing:";

								var delimeter = Environment.NewLine + Environment.NewLine;
								var message = String.Join (delimeter, Facade.Instance.CurrentRide.ValidaionError.Select (r => r.ErrorMessage));
								InvokeOnMainThread (() => new UIAlertView (header, message, null, "Ok", null).Show ());
							}
						}
					}
				);

			this.SetBinding (
				() => Facade.Instance.CurrentRide.Dummy)
				.UpdateSourceTrigger ("ViewModelReset")
				.WhenSourceChanges (
					() => {
						edtDropOffAirlines.Text = "";
						edtDropOffFlightType.Text = "";
					});
		}

		private async Task<List<GetAirlineResponseItem>> GetAirline()
		{
			List<GetAirlineResponseItem> list = null;
			try {
				UserTrackingReporter.TrackUser (Constant.CATEGORY_SCHEDULE_RIDE, "Getting airlines");

				var dic = new Dictionary<String, String> { { Constant.GETAIRLINE_PREFIX, "" } };
				var result = await AppData.ApiCall (Constant.GETAIRLINE, dic);
				var tt = (GetAirlineResponse)AppData.ParseResponse (Constant.GETAIRLINE, result);
				list = tt.List;
				UserTrackingReporter.TrackUser (Constant.CATEGORY_SCHEDULE_RIDE, "Airlines retrieved successfully");
			} catch (Exception ex) {
				HideLoadingView ();
				ShowMessageBox ("Error", "An error occurred getting airlines. \n\nError: " + ex.Message);
				CrashReporter.Report (ex);
			}
			return list ?? new List<GetAirlineResponseItem> ();
		}

	}
}
