
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

using RoadRunner.Shared;
using RoadRunner.Shared.Classes;

using GalaSoft.MvvmLight.Helpers;

namespace RoadRunner.Android
{
	[Activity (Label = "RideInformationActivity")]			
	public class RideInformationActivity : BaseActivity
	{
		private static bool IsFirstTime = true;

		TextView m_txtPickupLocation;
		TextView m_txtDropoffLocation;

		Spinner m_spinnerPassengers;
		Spinner m_spinnerHours;

		RadioGroup m_rgUpto;
		RadioGroup m_rgVehicle;

		TextView m_txtComment;

		Switch m_switchPH;

		private int m_selectedRideType = 1;
		private int m_selectedRideTypeValue = 1;

		private List<FareContainer> m_fares = new List<FareContainer>();

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView(Resource.Layout.RideInformatioin);

			AppSettings.currentActivity = this;
			//pickup location
			m_txtPickupLocation = (TextView)FindViewById(Resource.Id.txtPickupLocation);
			m_txtPickupLocation.Touch += PopupPickupLocation;
			m_txtPickupLocation.SetBinding(
				() => Facade.Instance.CurrentRide.PickUpLocation,
				() => m_txtPickupLocation.Text,
				BindingMode.TwoWay)
				.UpdateSourceTrigger("DropOffLocationChanges");
			
			//dropoff location
			m_txtDropoffLocation = (TextView)FindViewById(Resource.Id.txtDropoffLocation);
			m_txtDropoffLocation.Touch += PopupDropoffLocation;
			m_txtDropoffLocation.SetBinding(
				() => Facade.Instance.CurrentRide.DropOffLocation,
				() => m_txtDropoffLocation.Text,
				BindingMode.TwoWay)
				.UpdateSourceTrigger("DropOffLocationChanges");

			//passengers or hours
			m_switchPH = (Switch)FindViewById(Resource.Id.switchPorH);
			m_switchPH.CheckedChange += delegate (object sender, CompoundButton.CheckedChangeEventArgs e)
			{
				Facade.Instance.CurrentRide.ReservationType = e.IsChecked;
				m_spinnerHours.Enabled = e.IsChecked;
				GetFares();
			};

			//number of passengers/hours
			m_spinnerPassengers = (Spinner)FindViewById(Resource.Id.spPassengers);
			m_spinnerHours = (Spinner)FindViewById(Resource.Id.spHours);
			var adapter = new ArrayAdapter(this, global::Android.Resource.Layout.SimpleSpinnerItem);
			m_spinnerPassengers.Adapter = adapter;
			m_spinnerHours.Adapter = adapter;

			for (var i = 1; i <= 100; i++)
			{
				adapter.Add(i);
			}

			m_spinnerPassengers.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(OnPassengerChanged);
			m_spinnerHours.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(OnHourChanged);

			//upto of vehicle
			m_rgUpto = (RadioGroup)FindViewById(Resource.Id.rgUpto);
			m_rgUpto.CheckedChange += (s, e) =>
			{
				var indexView = m_rgUpto.FindViewById(e.CheckedId);
				var selectedIndex = m_rgUpto.IndexOfChild(indexView);
				m_selectedRideTypeValue = selectedIndex;
				SetFare();
			};

			//type of vehicle
			m_rgVehicle = (RadioGroup)FindViewById(Resource.Id.rgVehicle);
			m_rgVehicle.CheckedChange += (s, e) =>
			{
				var indexView = m_rgVehicle.FindViewById(e.CheckedId);
				var selectedIndex = m_rgVehicle.IndexOfChild(indexView);
				SetRideType(selectedIndex);
			};

			m_txtComment = (TextView)FindViewById(Resource.Id.txtComment);
			m_txtComment.Text = "Complete all fields above";

			var btnGotoLocation = (Button)FindViewById (Resource.Id.btn_GoToLocation);
			btnGotoLocation.SetCommand("Click", Facade.Instance.CurrentRide.GoToTheFlightInformation);

			var btnBack = (ImageButton)FindViewById (Resource.Id.btn_back);
			btnBack.Click += delegate(object sender , EventArgs e )
			{
				//OnBack();
				var preActivity = new Intent(this, typeof(MainActivity));
				StartActivity(preActivity);
				OverridePendingTransition(Resource.Animation.fromRight, Resource.Animation.toLeft);
			};

			if (AppSettings.isBindingFirst)
			{
				AppSettings.isBindingFirst = false;
				SetBindingsOnce();
			}

			GetFares();
		}

		protected override void OnResume()
		{
			base.OnResume();
			GetFares();
		}

		private void PopupPickupLocation(object sender, View.TouchEventArgs e)
		{
			if (e.Event.Action == MotionEventActions.Down)
			{
				var nextActivity = new Intent(this, typeof(PickupLocationActivity));
				nextActivity.PutExtra("IsPickupLocation", "true");
				StartActivity(nextActivity);
				OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
			}
		}

		private void PopupDropoffLocation(object sender, View.TouchEventArgs e)
		{
			if (e.Event.Action == MotionEventActions.Down)
			{
				var nextActivity = new Intent(this, typeof(PickupLocationActivity));
				nextActivity.PutExtra("IsPickupLocation", "false");
				StartActivity(nextActivity);
				OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
			}
		}

		private void OnPassengerChanged(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			var spinner = (Spinner)sender;
			Facade.Instance.CurrentRide.NumberOfPassangers = spinner.GetItemAtPosition(e.Position).ToString();
			GetFares();
		}
		private void OnHourChanged(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			var spinner = (Spinner)sender;
			Facade.Instance.CurrentRide.NumberOfHours = spinner.GetItemAtPosition(e.Position).ToString();
			GetFares();
		}

		private void SetFare()
		{
			Facade.Instance.CurrentRide.SelectedFare = null;
			m_txtComment.Text = "Please select a valid vehicle";

			if (m_fares.Count == 0 || m_selectedRideType == -1)
				return;

			var serviceID = -1;
			switch (m_selectedRideType)
			{
				case 0:
					serviceID = 0;
					break;
				case 1:
					if (m_selectedRideTypeValue == 0)
					{
						serviceID = 1;
					}
					else if (m_selectedRideTypeValue == 1)
					{
						serviceID = 11;
					}
					else if (m_selectedRideTypeValue == 2)
					{
						serviceID = 12;
					}
					break;
				case 2:
					serviceID = 2;
					break;
				case 3:
					serviceID = 4;
					break;
				case 4:
					if (m_selectedRideTypeValue == 0)
					{
						serviceID = 3;
					}
					else if (m_selectedRideTypeValue == 1)
					{
						serviceID = 6;
					}
					break;
				case 5:
					if (m_selectedRideTypeValue == 0)
					{
						serviceID = 13;
					}
					else if (m_selectedRideTypeValue == 1)
					{
						serviceID = 9;
					}
					break;
				case 6:
					serviceID = 5;
					break;
				default:
					break;
			}

			for (int i = 0; i < m_fares.Count; i++)
			{
				var fare = m_fares[i];
				if (Convert.ToInt32(fare.serviceid) == serviceID)
				{
					m_txtComment.Text = string.Format("Fare - ${0}", fare.amount);
					Facade.Instance.CurrentRide.SelectedFare = fare;
					Facade.Instance.CurrentRide.SelectedFareType = m_selectedRideType;
				}
			}
		}

		private void SetRideType(int selectedIndex)
		{
			m_selectedRideType = selectedIndex;

			var btnSwitch1 = (RadioButton)FindViewById(Resource.Id.rb1);
			var btnSwitch2 = (RadioButton)FindViewById(Resource.Id.rb2);
			var btnSwitch3 = (RadioButton)FindViewById(Resource.Id.rb3);

			switch (selectedIndex)
			{
				case 0:
					btnSwitch1.Enabled = false;
					btnSwitch3.Enabled = false;
					btnSwitch1.Text = "";
					btnSwitch1.Text = "";
					btnSwitch2.Text = "Up to 7";
					btnSwitch3.Text = "";
					break;
				case 1:
					btnSwitch1.Enabled = true;
					btnSwitch3.Enabled = true;
					btnSwitch1.Text = "Up to 7";
					btnSwitch2.Text = "Up to 9";
					btnSwitch3.Text = "Up to 14";
					break;
				case 2:
					btnSwitch1.Enabled = false;
					btnSwitch3.Enabled = false;
					btnSwitch1.Text = "";
					btnSwitch2.Text = "Up to 3";
					btnSwitch3.Text = "";
					break;
				case 3:
					btnSwitch1.Enabled = false;
					btnSwitch3.Enabled = false;
					btnSwitch1.Text = "";
					btnSwitch2.Text = "Up to 5";
					btnSwitch3.Text = "";
					break;
				case 4:
					btnSwitch1.Enabled = true;
					btnSwitch3.Enabled = false;
					btnSwitch1.Text = "Up to 6";
					btnSwitch2.Text = "Up to 13";
					btnSwitch3.Text = "";
					break;
				case 5:
					btnSwitch1.Enabled = true;
					btnSwitch3.Enabled = false;
					btnSwitch1.Text = "Up to 24";
					btnSwitch2.Text = "Up to 56";
					btnSwitch3.Text = "";
					break;
				case 6:
					btnSwitch1.Enabled = false;
					btnSwitch3.Enabled = false;
					btnSwitch1.Text = "";
					btnSwitch2.Text = "Up to 9";
					btnSwitch3.Text = "";
					break;
				default:
					break;
			}
			m_rgUpto.Check(Resource.Id.rb2);
			m_selectedRideTypeValue = 1;
			SetFare();
		}

		private async void GetFares()
		{

			if (m_txtDropoffLocation.Text == string.Empty || m_txtDropoffLocation.Text == string.Empty)
			{
				m_txtComment.Text = "Complete all fields above";
				return;
			}

			ShowLoadingView("Retrieving Available Vehicles...");

			if (m_switchPH.Checked)
			{
				m_fares = GetFareForReservationCharter();
			}
			else {
				m_fares = GetFareForReservationPoint();
			}

			if (m_fares.Count == 0)
			{
				UserTrackingReporter.TrackUser(Constant.CATEGORY_SCHEDULE_RIDE, "No fares returned");
				m_txtComment.Text = "Complete all fields above";
				ShowMessageBox("Fares Error", "No fares available for the selected pickup/dropoff. Please confirm they are correct and try again.");
				HideLoadingView();
				return;
			}

			UserTrackingReporter.TrackUser(Constant.CATEGORY_SCHEDULE_RIDE, "Fares retrieved successfully");

			SetFare();
			HideLoadingView();
			return;
		}

		private List<FareContainer> GetFareForReservationPoint()
		{

			List<FareContainer> fares = new List<FareContainer>();

			try
			{
				var result = string.Empty;
				var dic = new Dictionary<String, String>();

				//var dt = DateTime.Parse(Facade.Instance.CurrentRide.RequestedArrivalTimeAndDate);
				var dt = DateTime.Now.ToLocalTime();
				var dtStr = dt.ToString("MM/dd/yyyy");

				if (Facade.Instance.CurrentRide.IsPickUpLocationAirport)
				{
					dic = new Dictionary<String, String> {
						{ Constant.GETFARES_DEPAIRPORT,"" },
						{ Constant.GETFARES_DEPZIP, "" },
						{ Constant.GETFARES_DEPPESSANGER, "" },
						{ Constant.GETFARES_DEPDATE, "" },
						{ Constant.GETFARES_ARVAIRPORT, Facade.Instance.CurrentRide.PickUpLocation3CharacterAirportCode },
						{ Constant.GETFARES_ARVZIP, Facade.Instance.CurrentRide.DropOffLocationZip },
						{ Constant.GETFARES_ARVPESSANGER, Facade.Instance.CurrentRide.NumberOfPassangers },
						{ Constant.GETFARES_ARVDATE,  dtStr},
						{ Constant.GETFARES_QUERYSTRING,"" },
						{ Constant.GETFARES_DISCPROMOCODE, Facade.Instance.CurrentRide.PromoCode }
					};
				}
				else {
					if (Facade.Instance.CurrentRide.IsDropOffLocationAirport)
					{
						dic = new Dictionary<String, String> {
							{ Constant.GETFARES_DEPAIRPORT,Facade.Instance.CurrentRide.DropOffLocation3CharacterAirportCode },
							{ Constant.GETFARES_DEPZIP, Facade.Instance.CurrentRide.PickUpLocationZip },
							{ Constant.GETFARES_DEPPESSANGER, Facade.Instance.CurrentRide.NumberOfPassangers },
							{ Constant.GETFARES_DEPDATE, dtStr },
							{ Constant.GETFARES_ARVAIRPORT, "" },
							{ Constant.GETFARES_ARVZIP, "" },
							{ Constant.GETFARES_ARVPESSANGER, "" },
							{ Constant.GETFARES_ARVDATE,  ""},
							{ Constant.GETFARES_QUERYSTRING,"" },
							{ Constant.GETFARES_DISCPROMOCODE, Facade.Instance.CurrentRide.PromoCode }
						};
					}
					else {
						dic = new Dictionary<String, String>
						{
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERHOURS, "1" },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERARVCITY, string.Format("whatever,{0}", Facade.Instance.CurrentRide.DropOffLocationZip) },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERDEPCITY, string.Format("whatever,{0}", Facade.Instance.CurrentRide.PickUpLocationZip) },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERPASSENGER, Facade.Instance.CurrentRide.NumberOfPassangers  },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERTODATE, dtStr },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERFROMDATE, dtStr },
							{ Constant.GETFAREFORRESERVATIONCHARTER_SERVICEID, "" },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CLIENTID, "" },
							{ Constant.GETFAREFORRESERVATIONCHARTER_LOG, "" },
							{ Constant.GETFAREFORRESERVATIONCHARTER_ASSIGNID, "" },
							{ Constant.GETFAREFORRESERVATIONCHARTER_DISCPROMOCODE, Facade.Instance.CurrentRide.PromoCode }
						};

						Task runSync1 = Task.Factory.StartNew(async () =>
						{
							result = await AppData.ApiCall(Constant.GETFAREFORRESERVATIONCHARTER, dic);
						}).Unwrap();
						runSync1.Wait();

						var tt1 = AppData.ParseResponse(Constant.GETFAREFORRESERVATIONCHARTER, result);
						fares = (tt1 as GetFareForReservationCharterResponse).Fares;

						return fares;
					}
				}

				Task runSync = Task.Factory.StartNew(async () =>
				{
					result = await AppData.ApiCall(Constant.GETFARES, dic);
				}).Unwrap();
				runSync.Wait();

				var tt = (GetFaresResponse)AppData.ParseResponse(Constant.GETFARES, result);
				fares = tt.Fares;

			}
			catch (Exception ex)
			{
				HideLoadingView();
				ShowMessageBox("Error", "An error occurred getting fares. \n\nError: " + ex.Message);
				CrashReporter.Report(ex);
				return null;
			}

			return fares;
		}

		private List<FareContainer> GetFareForReservationCharter()
		{

			List<FareContainer> fares = new List<FareContainer>();

			try
			{

				var result = string.Empty;

				var dic = new Dictionary<String, String>();

				var dt = DateTime.Now.ToLocalTime();
				var dtStr = dt.ToString("MM/dd/yyyy");

				if (Facade.Instance.CurrentRide.IsPickUpLocationAirport)
				{
					dic = new Dictionary<String, String>
					{
						{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERHOURS, Facade.Instance.CurrentRide.NumberOfHours },
						{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERARVCITY, string.Format("whatever,{0}", Facade.Instance.CurrentRide.DropOffLocationZip) },
						{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERDEPCITY, string.Format("whatever,{0}", Facade.Instance.CurrentRide.PickUpLocationZip) },
						{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERPASSENGER, Facade.Instance.CurrentRide.NumberOfPassangers  },
						{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERTODATE, dtStr },
						{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERFROMDATE, dtStr },
						{ Constant.GETFAREFORRESERVATIONCHARTER_SERVICEID, "" },
						{ Constant.GETFAREFORRESERVATIONCHARTER_CLIENTID, "" },
						{ Constant.GETFAREFORRESERVATIONCHARTER_LOG, "" },
						{ Constant.GETFAREFORRESERVATIONCHARTER_ASSIGNID, "" },
						{ Constant.GETFAREFORRESERVATIONCHARTER_DISCPROMOCODE, Facade.Instance.CurrentRide.PromoCode }
					};
				}
				else {
					if (Facade.Instance.CurrentRide.IsDropOffLocationAirport)
					{
						dic = new Dictionary<String, String>
						{
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERHOURS, Facade.Instance.CurrentRide.NumberOfHours },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERARVCITY, string.Format("whatever,{0}", Facade.Instance.CurrentRide.DropOffLocationZip) },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERDEPCITY, string.Format("whatever,{0}", Facade.Instance.CurrentRide.PickUpLocationZip) },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERPASSENGER, Facade.Instance.CurrentRide.NumberOfPassangers  },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERTODATE, dtStr },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERFROMDATE, dtStr },
							{ Constant.GETFAREFORRESERVATIONCHARTER_SERVICEID, "" },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CLIENTID, "" },
							{ Constant.GETFAREFORRESERVATIONCHARTER_LOG, "" },
							{ Constant.GETFAREFORRESERVATIONCHARTER_ASSIGNID, "" },
							{ Constant.GETFAREFORRESERVATIONCHARTER_DISCPROMOCODE, Facade.Instance.CurrentRide.PromoCode }
						};
					}
					else {
						dic = new Dictionary<String, String>
						{
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERHOURS, Facade.Instance.CurrentRide.NumberOfHours },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERARVCITY, string.Format("whatever,{0}", Facade.Instance.CurrentRide.DropOffLocationZip) },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERDEPCITY, string.Format("whatever,{0}", Facade.Instance.CurrentRide.PickUpLocationZip) },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERPASSENGER, Facade.Instance.CurrentRide.NumberOfPassangers  },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERTODATE, dtStr },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERFROMDATE, dtStr },
							{ Constant.GETFAREFORRESERVATIONCHARTER_SERVICEID, "" },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CLIENTID, "" },
							{ Constant.GETFAREFORRESERVATIONCHARTER_LOG, "" },
							{ Constant.GETFAREFORRESERVATIONCHARTER_ASSIGNID, "" },
							{ Constant.GETFAREFORRESERVATIONCHARTER_DISCPROMOCODE, Facade.Instance.CurrentRide.PromoCode }
						};
					}
				}

				Task runSync = Task.Factory.StartNew(async () =>
				{
					result = await AppData.ApiCall(Constant.GETFAREFORRESERVATIONCHARTER, dic);
				}).Unwrap();
				runSync.Wait();

				var tt = AppData.ParseResponse(Constant.GETFAREFORRESERVATIONCHARTER, result);
				fares = (tt as GetFareForReservationCharterResponse).Fares;

			}
			catch (Exception ex)
			{
				HideLoadingView();
				ShowMessageBox("Error", "An error occurred getting fares. \n\nError: " + ex.Message);
				CrashReporter.Report(ex);
				return null;
			}
			return fares;
		}

		private void SetBindingsOnce()
		{

			this.SetBinding(
				() => Facade.Instance.CurrentRide.CanGoToTheFlightInformation)
				.UpdateSourceTrigger("CanGoToTheFlightInformationChanges")
				.WhenSourceChanges(
					async () =>
					{
						if (Facade.Instance.CurrentRide.CanGoToTheFlightInformation)
						{

							if (Facade.Instance.CurrentRide.IsPickUpLocationAirport)
							{
								AppSettings.currentActivity.StartActivity(new Intent(this, typeof(LocationPickupActivity)));
								AppSettings.currentActivity.OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
							}
							else if (Facade.Instance.CurrentRide.IsDropOffLocationAirport)
							{
								AppSettings.currentActivity.StartActivity(new Intent(this, typeof(LocationDropoffActivity)));
								AppSettings.currentActivity.OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
							}
							else {
								AppSettings.currentActivity.StartActivity(new Intent(this, typeof(LocationZipActivity)));
								AppSettings.currentActivity.OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
							}
						}
						else {
							if (Facade.Instance.CurrentRide.ValidaionError != null && Facade.Instance.CurrentRide.ValidaionError.Count > 0)
							{
								string header = Facade.Instance.CurrentRide.ValidaionError.Count > 1
									? "Just a couple things left:"
									: "Please correct the last thing:";

								var delimeter = System.Environment.NewLine + System.Environment.NewLine;
								var message = String.Join(delimeter, Facade.Instance.CurrentRide.ValidaionError.Select(r => r.ErrorMessage));
								AppSettings.currentActivity.ShowMessageBox(header, message);
							}
						}
					});
		}
	}
}

