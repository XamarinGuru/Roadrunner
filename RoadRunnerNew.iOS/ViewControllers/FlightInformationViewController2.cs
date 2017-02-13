using System;
using UIKit;
using RoadRunnerNew.iOS.Extension;
using System.Collections.Generic;
using RoadRunner.Shared;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Helpers;
using System.Linq;

namespace RoadRunnerNew.iOS
{
	partial class FlightInformationViewController2 : BaseViewController
	{
		private UINavigationController thisController { get; set; }

		public FlightInformationViewController2 (IntPtr handle) : base (handle)
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

			btnScheduleARide3.SetCustomButton ();

			SetBindings ();
			if (Facade.Instance.CurrentRide.IsFirstTime) {
				Facade.Instance.CurrentRide.IsFirstTime = false;
				SetBindingsOnce ();
			}
		}

		private void SetBindings(){

			SetupTimePicker (edtRequestedArrivalTimeAndDate, UIDatePickerMode.DateAndTime, "{0: MM/dd/yyyy hh:mm tt}", futureDatesOnly: true);

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

			#endregion

			edtAirportPickUpLocation.EditingDidBegin += (object sender, EventArgs e) => {

				UIStoryboard sb = UIStoryboard.FromName ("MainStoryboard", null);
				PickUpViewController pvc = (PickUpViewController)sb.InstantiateViewController ("PickUpViewController");

				pvc.IsPickUpLocation = true;

				edtAirportPickUpLocation.ResignFirstResponder ();
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
