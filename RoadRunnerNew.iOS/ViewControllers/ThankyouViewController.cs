using Foundation;
using System;
using UIKit;
using CoreGraphics;
using RoadRunner.Shared;
using RoadRunner.Shared.Classes;
using RoadRunnerNew.iOS.Extension;

using System.Collections.Generic;
using System.Threading.Tasks;

using Facebook.ShareKit;

namespace RoadRunnerNew.iOS
{
	partial class ThankyouViewController : BaseViewController, Facebook.ShareKit.ISharingDelegate
	{
		private float scroll_amount = 0.0f;    // amount to scroll 
		private bool moveViewUp = false;  

		private UINavigationController thisController { get; set; }

		public ThankyouViewController (IntPtr handle) : base (handle)
		{
			Title = "THANK YOU!";
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			//thisController = NavigationController;
			AppSettings.CurrentNavigation = NavigationController;
			this.NavigationItem.SetHidesBackButton (true, false);

			GetConfirmText();

			if(UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone)
			{
				if (UIScreen.MainScreen.Bounds.Size.Height < 568)
				{
					lblReservation.Font = UIFont.SystemFontOfSize(8.0f);
					lblDepartureInstructions.Font = UIFont.FromName("Helvetica-Bold", 8.0f);
				}
			}


			// Keyboard popup
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.DidShowNotification,KeyBoardUpNotification);

			// Keyboard Down
			NSNotificationCenter.DefaultCenter.AddObserver
			(UIKeyboard.WillHideNotification,KeyBoardDownNotification);



			btnReturnTrip.SetCustomButton ();
			btnDone.SetCustomButton ();

			edtThankEmail.ShouldReturn += TextFieldShouldReturn;

			var tap = new UITapGestureRecognizer(() => { View.EndEditing(true); });
			View.AddGestureRecognizer(tap);

			btnFBShare.TouchUpInside += (object sender, EventArgs e) => {

				ShareLinkContent content = new ShareLinkContent();

				content.SetContentUrl( NSUrl.FromString("http://www.rrshuttle.com/index.html"));
				content.ImageURL = NSUrl.FromString("http://upload.wikimedia.org/wikipedia/commons/thumb/9/95/Facebook_Headquarters_Menlo_Park.jpg/2880px-Facebook_Headquarters_Menlo_Park.jpg");
				content.ContentTitle = "Thank you for choosing Roadrunner Shuttle & Limousine Service for your upcoming trip on January 26th, 2016!";
				content.ContentDescription = "www.rrshuttle.com";

				Facebook.ShareKit.ShareDialog.Show(this, content, this);
			};

			btnLIShare.TouchUpInside += (object sender, EventArgs e) => {	
				//http://www.sharelinkgenerator.com/
				UIApplication.SharedApplication.OpenUrl(new NSUrl("https://www.linkedin.com/shareArticle?mini=true&url=https%3A//www.rrshuttle.com&title=Roadrunner%20Shuttle%20and%20Limousine%20Service&summary=Check%20out%20my%20ride!&source="));
			};

			btnGPShare.TouchUpInside += (object sender, EventArgs e) => {
				//http://www.sharelinkgenerator.com/
				UIApplication.SharedApplication.OpenUrl(new NSUrl("https://plus.google.com/share?url=https%3A//www.rrshuttle.com"));
			};

			btnDone.TouchUpInside += (object sender, EventArgs e) => {	
				Facade.Instance.CurrentRide.ResetVM();
				NavigationController.PopToRootViewController(true);
				AppDelegate.FlyoutNavigation.SelectedIndex = 1;
//				if (edtThankEmail.Text == string.Empty)
//				{
//					var okAlertController = UIAlertController.Create("error", "please specify email address.", UIAlertControllerStyle.Alert);
//					okAlertController.AddAction(UIAlertAction.Create("ok", UIAlertActionStyle.Default, null));
//					PresentViewController(okAlertController, true, null);
//					return;
//				}

//				var emailTask = MessagingPlugin.EmailMessenger;
//				if(emailTask.CanSendEmail)
//				{
//					emailTask.SendEmail("erlend0720@hotmail.com", "roadrunner support team", "this is email");
//				}else{
//					var okAlertController = UIAlertController.Create("error", "you can't email on emulator", UIAlertControllerStyle.Alert);
//					okAlertController.AddAction(UIAlertAction.Create("ok", UIAlertActionStyle.Default, null));
//					PresentViewController(okAlertController, true, null);
//				}

			};

			btnReturnTrip.TouchUpInside += (object sender, EventArgs e) => {	
			
				UIViewController[] viewControllers = NavigationController.ViewControllers;
				for ( int i = 0; i < viewControllers.Length; i ++){
					UIViewController controller = viewControllers[i];
					if(controller is RideInformationViewController){
						ReverseTripInfo();
						//NavigationController.PopToViewController(controller, true);
						AppSettings.CurrentNavigation.PopToViewController(controller, true);
						return;
					}
				}
			};
		}

		private void ReverseTripInfo()
		{
			var PickUpLocation = Facade.Instance.CurrentRide.PickUpLocation;
			var PickUpLocationName = Facade.Instance.CurrentRide.PickUpLocationName;
			var PickUpLocationId = Facade.Instance.CurrentRide.PickUpLocationId;
			var PickUpLocationLatitude = Facade.Instance.CurrentRide.PickUpLocationLatitude;
			var PickUpLocationLongitude = Facade.Instance.CurrentRide.PickUpLocationLongitude;
			var PickUpAirlines = Facade.Instance.CurrentRide.PickUpAirlines;
			var PickUpAirlinesId = Facade.Instance.CurrentRide.PickUpAirlinesId;
			var PickUpFlightNumber = Facade.Instance.CurrentRide.PickUpFlightNumber;
			var PickUpFlightTime = Facade.Instance.CurrentRide.PickUpFlightTime;
			var PickUpFlightTypeIsDomestic = Facade.Instance.CurrentRide.PickUpFlightTypeIsDomestic;
			var PickUpLocation3CharacterAirportCode = Facade.Instance.CurrentRide.PickUpLocation3CharacterAirportCode;
			var PickUpLocationZip = Facade.Instance.CurrentRide.PickUpLocationZip;
			var IsPickUpLocationAirport = Facade.Instance.CurrentRide.IsPickUpLocationAirport;

			Facade.Instance.CurrentRide.PickUpLocation = Facade.Instance.CurrentRide.DropOffLocation;
			Facade.Instance.CurrentRide.PickUpLocationName = Facade.Instance.CurrentRide.DropOffLocationName;
			Facade.Instance.CurrentRide.PickUpLocationId = Facade.Instance.CurrentRide.DropOffLocationId;
			Facade.Instance.CurrentRide.PickUpLocationLatitude = Facade.Instance.CurrentRide.DropOffLocationLatitude;
			Facade.Instance.CurrentRide.PickUpLocationLongitude = Facade.Instance.CurrentRide.DropOffLocationLongitude;
			Facade.Instance.CurrentRide.PickUpAirlines = Facade.Instance.CurrentRide.DropOffAirlines;
			Facade.Instance.CurrentRide.PickUpAirlinesId = Facade.Instance.CurrentRide.DropOffAirlinesId;
			Facade.Instance.CurrentRide.PickUpFlightNumber = Facade.Instance.CurrentRide.DropOffFlightNumber;
			Facade.Instance.CurrentRide.PickUpFlightTime = Facade.Instance.CurrentRide.DropOffFlightTime;
			Facade.Instance.CurrentRide.PickUpFlightTypeIsDomestic = Facade.Instance.CurrentRide.DropOffFlightTypeIsDomestic;
			Facade.Instance.CurrentRide.PickUpLocation3CharacterAirportCode = Facade.Instance.CurrentRide.DropOffLocation3CharacterAirportCode;
			Facade.Instance.CurrentRide.PickUpLocationZip = Facade.Instance.CurrentRide.DropOffLocationZip;
			Facade.Instance.CurrentRide.IsPickUpLocationAirport = Facade.Instance.CurrentRide.IsDropOffLocationAirport;

			Facade.Instance.CurrentRide.DropOffLocation = PickUpLocation;
			Facade.Instance.CurrentRide.DropOffLocationName = PickUpLocationName;
			Facade.Instance.CurrentRide.DropOffLocationId = PickUpLocationId;
			Facade.Instance.CurrentRide.DropOffLocationLatitude = PickUpLocationLatitude;
			Facade.Instance.CurrentRide.DropOffLocationLongitude = PickUpLocationLongitude;
			Facade.Instance.CurrentRide.DropOffAirlines = PickUpAirlines;
			Facade.Instance.CurrentRide.DropOffAirlinesId = PickUpAirlinesId;
			Facade.Instance.CurrentRide.DropOffFlightNumber = PickUpFlightNumber;
			Facade.Instance.CurrentRide.DropOffFlightTime = PickUpFlightTime;
			Facade.Instance.CurrentRide.DropOffFlightTypeIsDomestic = PickUpFlightTypeIsDomestic;
			Facade.Instance.CurrentRide.DropOffLocation3CharacterAirportCode = PickUpLocation3CharacterAirportCode;
			Facade.Instance.CurrentRide.DropOffLocationZip = PickUpLocationZip;
			Facade.Instance.CurrentRide.IsDropOffLocationAirport = IsPickUpLocationAirport;
		}

		public void DidCancel (ISharing sharer){

		}

		public void DidComplete (ISharing sharer, NSDictionary results){

		}

		public void DidFail (ISharing sharer, NSError error){

		}

		private void KeyBoardUpNotification(NSNotification notification)
		{
			CGRect r = UIKeyboard.BoundsFromNotification (notification);

			scroll_amount = (float)r.Height - ((float)emailView.Frame.Y - (float)emailView.Frame.Size.Height) ;

			if (scroll_amount > 0) {
				moveViewUp = true;
				ScrollTheView (moveViewUp);
			} else {
				moveViewUp = false;
			}

		}
		private void KeyBoardDownNotification(NSNotification notification)
		{
			if(moveViewUp){ScrollTheView(false);}
		}
		private void ScrollTheView(bool move)
		{

			// scroll the view up or down
			UIView.BeginAnimations (string.Empty, System.IntPtr.Zero);
			UIView.SetAnimationDuration (0.3);

			CGRect frame = emailView.Frame;

			if (move) {
				frame.Y -= scroll_amount;
			} else {
				frame.Y += scroll_amount;
				scroll_amount = 0;
			}

			emailView.Frame = frame;
			UIView.CommitAnimations();
		}

		private void GetConfirmText()
		{
			ShowLoadingView("Loading data...");

			String result = String.Empty;

			System.Threading.Tasks.Task runSync = System.Threading.Tasks.Task.Factory.StartNew(async () =>
			{
				var dic = new Dictionary<String, String>
				{
					{Constant.GETCONFIRMATIONTEXT_RESID, String.IsNullOrEmpty(Facade.Instance.CurrentRide.ReservationID) ? "1536724" : Facade.Instance.CurrentRide.ReservationID}
				};

				result = await AppData.ApiCall(Constant.GETCONFIRMATIONTEXT, dic);

				var tt = (GetConfirmationTextResponse)AppData.ParseResponse(Constant.GETCONFIRMATIONTEXT, result);

				InvokeOnMainThread(() =>
				{
					var aaa = tt.ConfList;
				});

				HideLoadingView();
			}).Unwrap();
		}
	}
}
