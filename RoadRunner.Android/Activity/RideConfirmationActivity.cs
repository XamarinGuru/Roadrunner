
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using RoadRunner.Shared;
using RoadRunner.Shared.Classes;

namespace RoadRunner.Android
{
	[Activity (Label = "RideConfirmationActivity")]			
	public class RideConfirmationActivity : BaseActivity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView(Resource.Layout.RideConfirmation);

			SetLocationPart();
			SetFarePart();

			var btnGotoThankyou = (Button)FindViewById (Resource.Id.btn_GoToThankyou);
			btnGotoThankyou.Click += delegate(object sender , EventArgs e )
			{
				ConfirmRideInformation();
			};

			var btnBack = (ImageButton)FindViewById (Resource.Id.btn_back);
			btnBack.Click += delegate(object sender , EventArgs e )
			{
				OnBack();
			};
		}

		private void SetLocationPart()
		{
			var lblPickupDate = (TextView)FindViewById(Resource.Id.lblPickupDate);
			var lblPickUpAddress = (TextView)FindViewById(Resource.Id.lblPickUpAddress);
			var lblDropoffDate = (TextView)FindViewById(Resource.Id.lblDropoffDate);
			var lblDropOffAddress = (TextView)FindViewById(Resource.Id.lblDropOffAddress);
			var lblRideType1 = (TextView)FindViewById(Resource.Id.lblRideType1);
			var lblRideType2 = (TextView)FindViewById(Resource.Id.lblRideType2);

			var iconPickup = (ImageView)FindViewById(Resource.Id.iconPickup);
			var iconDropoff = (ImageView)FindViewById(Resource.Id.iconDropoff);
			var iconVehicle = (ImageView)FindViewById(Resource.Id.iconVehicle);

			if (Facade.Instance.CurrentRide.IsPickUpLocationAirport)
			{
				lblPickupDate.Text = string.Format("Pickup at {0} from", Facade.Instance.CurrentRide.PickUpFlightTime);
				lblPickUpAddress.Text = Facade.Instance.CurrentRide.PickUpLocationName;
				lblDropoffDate.Text = Facade.Instance.CurrentRide.RequestedArrivalTimeAndDate;
				lblDropOffAddress.Text = Facade.Instance.CurrentRide.IsDropOffLocationAirport ? Facade.Instance.CurrentRide.DropOffLocationName : Facade.Instance.CurrentRide.DropOffLocation;
			}
			else if (Facade.Instance.CurrentRide.IsDropOffLocationAirport)
			{
				lblPickupDate.Text = Facade.Instance.CurrentRide.RequestedArrivalTimeAndDate;
				lblPickUpAddress.Text = Facade.Instance.CurrentRide.PickUpLocation;
				lblDropoffDate.Text = string.Format("Dropoff at {0} to", Facade.Instance.CurrentRide.DropOffFlightTime);
				lblDropOffAddress.Text = Facade.Instance.CurrentRide.DropOffLocationName;
			}
			else {
				lblPickupDate.Text = Facade.Instance.CurrentRide.RequestedArrivalTimeAndDate;
				lblPickUpAddress.Text = Facade.Instance.CurrentRide.PickUpLocation;
				lblDropoffDate.Text = Facade.Instance.CurrentRide.RequestedArrivalTimeAndDate;
				lblDropOffAddress.Text = Facade.Instance.CurrentRide.DropOffLocation;
			}
			iconPickup.SetImageResource(Facade.Instance.CurrentRide.IsPickUpLocationAirport ? Resource.Drawable.icon_default_flight : Resource.Drawable.icon_home);
			iconDropoff.SetImageResource(Facade.Instance.CurrentRide.IsPickUpLocationAirport ? Resource.Drawable.icon_default_flight : Resource.Drawable.icon_home);

			switch (Facade.Instance.CurrentRide.SelectedFareType)
			{
				case 0:
					iconVehicle.SetImageResource(Resource.Drawable.icon_car);//UIImage.FromBundle(@"IconCar");
					break;
				case 1:
					iconVehicle.SetImageResource(Resource.Drawable.vehicle_black);//UIImage.FromBundle(@"IconVan");
					break;
				case 2:
					iconVehicle.SetImageResource(Resource.Drawable.icon_car);//UIImage.FromBundle(@"IconCar");
					break;
				case 3:
					iconVehicle.SetImageResource(Resource.Drawable.vehicle_suv);//UIImage.FromBundle(@"IconSUV");
					break;
				case 4:
					iconVehicle.SetImageResource(Resource.Drawable.vehicle_limo);//UIImage.FromBundle(@"IconLimo");
					break;
				case 5:
					iconVehicle.SetImageResource(Resource.Drawable.vehicle_bus);//UIImage.FromBundle(@"IconBus");
					break;
				case 6:
					iconVehicle.SetImageResource(Resource.Drawable.vehicle_black);//UIImage.FromBundle(@"IconVan");
					break;
				default:
					break;
			}

			var serviceName1 = "";
			var serviceName2 = "";
			switch (Facade.Instance.CurrentRide.SelectedFare.serviceid)
			{
				case "0":
					serviceName1 = "Ride Share";
					serviceName2 = "(Up To Passengers 7)";
					break;
				case "1":
					serviceName1 = "Private Van";
					serviceName2 = "(Up To Passengers 7)";
					break;
				case "11":
					serviceName1 = "Private Van";
					serviceName2 = "(Up To Passengers 9)";
					break;
				case "12":
					serviceName1 = "Private Van";
					serviceName2 = "(Up To Passengers 14)";
					break;
				case "2":
					serviceName1 = "Black Car";
					serviceName2 = "(Up To Passengers 3)";
					break;
				case "4":
					serviceName1 = "SUB";
					serviceName2 = "(Up To Passengers 5)";
					break;
				case "3":
					serviceName1 = "Limousine";
					serviceName2 = "(Up To Passengers 6)";
					break;
				case "6":
					serviceName1 = "Limousine";
					serviceName2 = "(Up To Passengers 13)";
					break;
				case "13":
					serviceName1 = "Bus";
					serviceName2 = "(Up To Passengers 24)";
					break;
				case "9":
					serviceName1 = "Bus";
					serviceName2 = "(Up To Passengers 56)";
					break;
				case "5":
					serviceName1 = "Wheelchair";
					serviceName2 = "(Up To Passengers 9)";
					break;
			}

			lblRideType1.Text = serviceName1;
			lblRideType2.Text = serviceName2;
		}

		private void SetFarePart()
		{
			GetGasSurcharge();

			var lblRideFee = (TextView)FindViewById(Resource.Id.lblRideFee);
			var lblGasSurcharges = (TextView)FindViewById(Resource.Id.lblGasSurcharges);
			var lblGratuity = (TextView)FindViewById(Resource.Id.lblGratuity);
			var lblMeetAndGreetFee = (TextView)FindViewById(Resource.Id.lblMeetAndGreetFee);
			var lblExtraBags = (TextView)FindViewById(Resource.Id.lblExtraBags);
			var lblTotal = (TextView)FindViewById(Resource.Id.lblTotal);

			double RideFee = double.Parse(Facade.Instance.CurrentRide.SelectedFare.amount);
			lblRideFee.Text = string.Format("{0:C}", RideFee);

			double Surcharge = Facade.Instance.CurrentRide.Surcharge;
			lblGasSurcharges.Text = string.Format("{0:C}", Surcharge);

			double pGratuity = 0;// = Facade.Instance.CurrentRide.Gratuity == "" ? "0" : Facade.Instance.CurrentRide.Gratuity;

			if (Facade.Instance.CurrentRide.Gratuity == "0%")
			{
				pGratuity = 0;
			}
			else if (Facade.Instance.CurrentRide.Gratuity == "10%")
			{
				pGratuity = 0.1;
			}
			else if (Facade.Instance.CurrentRide.Gratuity == "15%")
			{
				pGratuity = 0.15;
			}
			else if (Facade.Instance.CurrentRide.Gratuity == "20%")
			{
				pGratuity = 0.2;
			}

			double Gratuity = RideFee * pGratuity;
			lblGratuity.Text = string.Format("{0:C}", Gratuity);

			double MeetandGreetFee = Facade.Instance.CurrentRide.IsMeetandGreet ? Facade.Instance.CurrentRide.MeetandGreetFee : 0;
			lblMeetAndGreetFee.Text = string.Format("{0:C}", MeetandGreetFee);

			double ExtraBagsTotalFee = Facade.Instance.CurrentRide.ExtraBags == "" ? 0 : Facade.Instance.CurrentRide.ExtraBagsFee * int.Parse(Facade.Instance.CurrentRide.ExtraBags);
			lblExtraBags.Text = string.Format("{0:C}", ExtraBagsTotalFee);

			double Total = RideFee + Surcharge + Gratuity + MeetandGreetFee + ExtraBagsTotalFee;
			lblTotal.Text = string.Format("{0:C}", Total);
		}

		private void GetGasSurcharge()
		{
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

			Task runSync = Task.Factory.StartNew(async () =>
			{
				result = await AppData.ApiCall(Constant.GETGASSURCHARGE, dic);

				RunOnUiThread(() =>
				{
					var tt = (GetGasSurchargeResponse)AppData.ParseResponse(Constant.GETGASSURCHARGE, result);
					Facade.Instance.CurrentRide.Surcharge = double.Parse(tt.Surcharge);
					var lblGasSurcharges = (TextView)FindViewById(Resource.Id.lblGasSurcharges);
					lblGasSurcharges.Text = string.Format("{0:C}", double.Parse(tt.Surcharge));
				});
			});
			//try
			//{
			//	var serviceID = Facade.Instance.CurrentRide.SelectedFare.serviceid;
			//	bool IsArrival = Facade.Instance.CurrentRide.IsPickUpLocationAirport;
			//	var dtStr = Facade.Instance.CurrentRide.RequestedArrivalTimeAndDate;

			//	var dic = new Dictionary<String, String>
			//	{
			//		{Constant.GETGASSURCHARGE_SERVICEID, serviceID},
			//		{Constant.GETGASSURCHARGE_TRAVELDATE, dtStr},
			//		{Constant.GETGASSURCHARGE_ARVDEP, IsArrival ? "1" : "0"}
			//	};

			//	var result = string.Empty;
			//	Task runSync = Task.Factory.StartNew(async () =>
			//	{
			//		result = await AppData.ApiCall(Constant.GETGASSURCHARGE, dic);
			//	});
			//	runSync.Wait();

			//	var tt = (GetGasSurchargeResponse)AppData.ParseResponse(Constant.GETGASSURCHARGE, result);
			//	Facade.Instance.CurrentRide.Surcharge = double.Parse(tt.Surcharge);
			//	var lblGasSurcharges = (TextView)FindViewById(Resource.Id.lblGasSurcharges);
			//	lblGasSurcharges.Text = string.Format("{0:C}", double.Parse(tt.Surcharge));

			//}
			//catch (Exception ex)
			//{
			//	HideLoadingView();
			//	ShowMessageBox("Error", "An error occurred getting gas surcharge. \n\nError: " + ex.Message);
			//	CrashReporter.Report(ex);
			//}
		}

		private void ConfirmRideInformation()
		{
			var result = string.Empty;

			try
			{
				Task runSync = Task.Factory.StartNew(async () =>
				{
					var ride = new Ride();
					result = await ride.GetConfirmation(AppSettings.UserID);
					Facade.Instance.CurrentRide.ReservationID = result;
				}).Unwrap();
				runSync.Wait();

			}
			catch (Exception ex)
			{
				HideLoadingView();
				ShowMessageBox("Error", "Reservation confirmation error. \n\nError: " + ex.Message);
				CrashReporter.Report(ex);
				return;
			}

			StartActivity(new Intent(this, typeof(ThankYouActivity)));
			OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
		}
	}
}

