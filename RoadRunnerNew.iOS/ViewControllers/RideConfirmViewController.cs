using System;
using UIKit;
using RoadRunner.Shared;
using RoadRunnerNew.iOS.Extension;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoadRunnerNew.iOS
{
	partial class RideConfirmViewController : BaseViewController
	{
		private UINavigationController thisController { get; set; }

		public RideConfirmViewController (IntPtr handle) : base (handle)
		{
			Title = "RIDE CONFIRMATION";
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			//thisController = NavigationController;
			AppSettings.CurrentNavigation = NavigationController;
			NavigationItem.Customize (NavigationController);

			btnScheduleARide5.SetCustomButton ();

			SetLocationPart ();
			SetFarePart ();

			btnScheduleARide5.TouchUpInside += (object sender, EventArgs e) => {

				ConfirmRideInformation();
			};
		}

		private void SetLocationPart()
		{
			if (Facade.Instance.CurrentRide.IsPickUpLocationAirport) {
				lblPickupDate.Text = string.Format ("Pickup at {0} from", Facade.Instance.CurrentRide.PickUpFlightTime);
				lblPickUpAddress.Text = Facade.Instance.CurrentRide.PickUpLocationName;
				lblDropoffDate.Text = Facade.Instance.CurrentRide.RequestedArrivalTimeAndDate;
				lblDropOffAddress.Text = Facade.Instance.CurrentRide.IsDropOffLocationAirport ? Facade.Instance.CurrentRide.DropOffLocationName : Facade.Instance.CurrentRide.DropOffLocation;
			} else if (Facade.Instance.CurrentRide.IsDropOffLocationAirport) {
				lblPickupDate.Text = Facade.Instance.CurrentRide.RequestedArrivalTimeAndDate;
				lblPickUpAddress.Text = Facade.Instance.CurrentRide.PickUpLocation;
				lblDropoffDate.Text = string.Format ("Dropoff at {0} to", Facade.Instance.CurrentRide.DropOffFlightTime);
				lblDropOffAddress.Text = Facade.Instance.CurrentRide.DropOffLocationName;
			} else {
				lblPickupDate.Text = Facade.Instance.CurrentRide.RequestedArrivalTimeAndDate;
				lblPickUpAddress.Text = Facade.Instance.CurrentRide.PickUpLocation;
				lblDropoffDate.Text = Facade.Instance.CurrentRide.RequestedArrivalTimeAndDate;
				lblDropOffAddress.Text = Facade.Instance.CurrentRide.DropOffLocation;
			}
			iconPickup.Image = Facade.Instance.CurrentRide.IsPickUpLocationAirport ? UIImage.FromBundle (@"icon_default_flight.png") : UIImage.FromBundle (@"icon_home.png");
			iconDropoff.Image = Facade.Instance.CurrentRide.IsDropOffLocationAirport ? UIImage.FromBundle (@"icon_default_flight.png") : UIImage.FromBundle (@"icon_home.png");

			switch(Facade.Instance.CurrentRide.SelectedFareType)
			{
			case 0:
				iconVehicle.Image = UIImage.FromBundle (@"IconShare");
				break;
			case 1:				
				iconVehicle.Image = UIImage.FromBundle (@"IconVan");
				break;
			case 2:
				iconVehicle.Image = UIImage.FromBundle (@"IconCar");
				break;
			case 3:
				iconVehicle.Image = UIImage.FromBundle (@"IconSUV");
				break;
			case 4:
				iconVehicle.Image = UIImage.FromBundle (@"IconLimo");
				break;
			case 5:
				iconVehicle.Image = UIImage.FromBundle (@"IconBus");
				break;
			case 6:
				iconVehicle.Image = UIImage.FromBundle (@"IconVan");
				break;
			default:
				break;
			}

			var serviceName = "";
			switch(Facade.Instance.CurrentRide.SelectedFare.serviceid)
			{
			case "0":
				serviceName = "Ride Share\r (Up To Passengers 7)";
				break;
			case "1":
				serviceName = "Private Van\r (Up To Passengers 7)";
				break;
			case "11":
				serviceName = "Private Van\r (Up To Passengers 9)";
				break;
			case "12":
				serviceName = "Private Van\r (Up To Passengers 14)";
				break;
			case "2":
				serviceName = "Black Car\r (Up To Passengers 3)";
				break;
			case "4":
				serviceName = "SUB\r (Up To Passengers 5)";
				break;
			case "3":
				serviceName = "Limousine\r (Up To Passengers 6)";
				break;
			case "6":
				serviceName = "Limousine\r (Up To Passengers 13)";
				break;
			case "13":
				serviceName = "Bus\r (Up To Passengers 24)";
				break;
			case "9":
				serviceName = "Bus\r (Up To Passengers 56)";
				break;
			case "5":
				serviceName = "Wheelchair\r (Up To Passengers 9)";
				break;
			}

			lblRideType.Text = serviceName;
		}

		private void SetFarePart()
		{
			GetGasSurcharge ();

			double RideFee = double.Parse (Facade.Instance.CurrentRide.SelectedFare.amount);
			lblRideFee.Text = string.Format ("{0:C}", RideFee);

			double Surcharge = Facade.Instance.CurrentRide.Surcharge;
			lblGasSurcharges.Text = string.Format ("{0:C}", Surcharge);

			double pGratuity = 0;// = Facade.Instance.CurrentRide.Gratuity == "" ? "0" : Facade.Instance.CurrentRide.Gratuity;

			if (Facade.Instance.CurrentRide.Gratuity == "0%") {
				pGratuity = 0;
			} else if (Facade.Instance.CurrentRide.Gratuity == "10%") {
				pGratuity = 0.1;
			} else if (Facade.Instance.CurrentRide.Gratuity == "15%") {
				pGratuity = 0.15;
			}else if (Facade.Instance.CurrentRide.Gratuity == "20%") {
				pGratuity = 0.2;
			}

			double Gratuity = RideFee * pGratuity;
			lblGratuity.Text = string.Format ("{0:C}", Gratuity);

			double MeetandGreetFee = Facade.Instance.CurrentRide.IsMeetandGreet ? Facade.Instance.CurrentRide.MeetandGreetFee : 0;
			lblMeetAndGreetFee.Text = string.Format ("{0:C}", MeetandGreetFee);

			double ExtraBagsTotalFee = Facade.Instance.CurrentRide.ExtraBags == "" ? 0 : Facade.Instance.CurrentRide.ExtraBagsFee * int.Parse(Facade.Instance.CurrentRide.ExtraBags);
			lblExtraBags.Text = string.Format ("{0:C}", ExtraBagsTotalFee);

			double Total = RideFee + Surcharge + Gratuity + MeetandGreetFee + ExtraBagsTotalFee;
			lblTotal.Text = string.Format ("{0:C}", Total);
		}

		private void GetGasSurcharge()
		{
			try {
				var serviceID = Facade.Instance.CurrentRide.SelectedFare.serviceid;
				bool IsArrival = Facade.Instance.CurrentRide.IsPickUpLocationAirport;
				var dtStr = Facade.Instance.CurrentRide.RequestedArrivalTimeAndDate;

				var dic = new Dictionary<String, String>
				{
					{Constant.GETGASSURCHARGE_SERVICEID, serviceID},
					{Constant.GETGASSURCHARGE_TRAVELDATE, dtStr},
					{Constant.GETGASSURCHARGE_ARVDEP, IsArrival ? "1" : "0"}
				};

				var result = string.Empty;
				Task runSync = Task.Factory.StartNew (async () => {
					result = await AppData.ApiCall (Constant.GETGASSURCHARGE, dic);
				}).Unwrap ();
				runSync.Wait ();

				var tt = (GetGasSurchargeResponse)AppData.ParseResponse (Constant.GETGASSURCHARGE, result);
				Facade.Instance.CurrentRide.Surcharge = double.Parse(tt.Surcharge);
				lblGasSurcharges.Text = string.Format ("{0:C}", double.Parse(tt.Surcharge));

			} catch (Exception ex) {
				HideLoadingView ();
				ShowMessageBox ("Error", "An error occurred getting gas surcharge. \n\nError: " + ex.Message);
				CrashReporter.Report (ex);
			}
		}

		private void ConfirmRideInformation()
		{
			var result = string.Empty;

			try {
				Task runSync = Task.Factory.StartNew (async () => {
					Ride ride = new Ride();
					result = await ride.GetConfirmation(AppSettings.UserID);
					Facade.Instance.CurrentRide.ReservationID = result;
				}).Unwrap ();
				runSync.Wait ();

			} catch (Exception ex) {
				HideLoadingView ();
				ShowMessageBox ("Error", "Reservation confirmation error. \n\nError: " + ex.Message);
				CrashReporter.Report (ex);
				return;
			}

			UIStoryboard sb = UIStoryboard.FromName ("MainStoryboard", null);
			ThankyouViewController pvc = (ThankyouViewController)sb.InstantiateViewController ("ScheduleARideViewController6");
			AppSettings.CurrentNavigation.PushViewController(pvc, true);
		}
	}
}
