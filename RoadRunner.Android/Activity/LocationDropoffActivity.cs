
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Text;

using GalaSoft.MvvmLight.Helpers;

using RoadRunner.Shared;

namespace RoadRunner.Android
{
	[Activity(Label = "LocationDropoffActivity")]
	public class LocationDropoffActivity : BaseActivity
	{
		const int TIME_DIALOG_ID = 0;

		TextView m_txtPickupLocation;
		TextView m_txtDropoffLocation;

		TextView m_txtRequestPickupDate;
		Spinner m_spinerAirline;
		Spinner m_spinerFlightType;
		TextView m_txtFlightTime;
		EditText m_txtFlightNumber;

		List<KeyValuePair<object, string>> m_KvpAirlines = new List<KeyValuePair<object, string>>();


		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.LocationDropoff);

			AppSettings.currentActivity = this;
			//pickup location
			m_txtPickupLocation = (TextView)FindViewById(Resource.Id.txtPickupLocation);
			m_txtPickupLocation.Text = Facade.Instance.CurrentRide.PickUpLocation;
			m_txtPickupLocation.SetBinding(
				() => Facade.Instance.CurrentRide.PickUpLocation,
				() => m_txtPickupLocation.Text,
				BindingMode.TwoWay)
				.UpdateSourceTrigger("PickUpLocationChanges");

			//dropoff location
			m_txtDropoffLocation = (TextView)FindViewById(Resource.Id.txtDropoffLocation);
			m_txtDropoffLocation.Text = Facade.Instance.CurrentRide.DropOffLocation;
			m_txtDropoffLocation.SetBinding(
				() => Facade.Instance.CurrentRide.DropOffLocation,
				() => m_txtDropoffLocation.Text,
				BindingMode.TwoWay)
				.UpdateSourceTrigger("DropOffLocationChanges");

			//request pickup date and time
			m_txtRequestPickupDate = (TextView)FindViewById(Resource.Id.txtRequestPickupDate);
			m_txtRequestPickupDate.Text = Facade.Instance.CurrentRide.RequestedArrivalTimeAndDate;
			m_txtRequestPickupDate.Touch += PopupSetDateTime;

			//airline

			m_spinerAirline = (Spinner)FindViewById(Resource.Id.spinnerAirline);
			var adapterAirline = new ArrayAdapter(this, Resource.Layout.item_spinner);
			m_spinerAirline.Adapter = adapterAirline;

			var AirlineResponseItemList = new List<GetAirlineResponseItem>();
			Task runSync = Task.Factory.StartNew(async () =>
			{
				AirlineResponseItemList = await GetAirline();
			}).Unwrap();
			runSync.Wait();

			m_KvpAirlines = new List<KeyValuePair<object, string>>();
			foreach (var item in AirlineResponseItemList)
			{
				m_KvpAirlines.Add(new KeyValuePair<object, string>(item.id, item.Airline));
				adapterAirline.Add(item.Airline);
			}
			m_spinerAirline.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(OnAirlineChanged);

			//flight type
			m_spinerFlightType = (Spinner)FindViewById(Resource.Id.spinnerFlightType);
			var adapterFlightType = new ArrayAdapter(this, Resource.Layout.item_spinner);
			m_spinerFlightType.Adapter = adapterFlightType;
			adapterFlightType.Add("Domestic");
			adapterFlightType.Add("International");
			m_spinerFlightType.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(OnFlightTypeChanged);

			//flight number
			m_txtFlightNumber = (EditText)FindViewById(Resource.Id.txtFlightNumber);
			m_txtFlightNumber.TextChanged += OnFlightNumberChanged;

			//flight time
			m_txtFlightTime = (TextView)FindViewById(Resource.Id.txtFlightTime);
			m_txtFlightTime.Text = Facade.Instance.CurrentRide.DropOffFlightTime;
			m_txtFlightTime.Click += (o, e) => ShowDialog(TIME_DIALOG_ID);

			var btnGotoRidePayment = (Button)FindViewById(Resource.Id.btn_GoToRidePayment);
			btnGotoRidePayment.SetCommand("Click", Facade.Instance.CurrentRide.GoToThePaymentInformation);

			var btnBack = (ImageButton)FindViewById(Resource.Id.btn_back);
			btnBack.Click += delegate (object sender, EventArgs e)
			{
				OnBack();
			};

			if (Facade.Instance.CurrentRide.IsFirstTime)
			{
				Facade.Instance.CurrentRide.IsFirstTime = false;
				SetBindingsOnce();
			}
		}

		protected override void OnResume()
		{
			base.OnResume();
			m_txtRequestPickupDate.Text = Facade.Instance.CurrentRide.RequestedArrivalTimeAndDate;

			m_txtPickupLocation.Text = Facade.Instance.CurrentRide.PickUpLocation;
			m_txtDropoffLocation.Text = Facade.Instance.CurrentRide.DropOffLocation;
			m_txtFlightNumber.Text = Facade.Instance.CurrentRide.DropOffFlightNumber;
		}

		private void PopupSetDateTime(object sender, View.TouchEventArgs e)
		{
			if (e.Event.Action == MotionEventActions.Down)
			{
				var nextActivity = new Intent(this, typeof(DateTimePickerActivity));
				StartActivity(nextActivity);
				OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
			}
		}

		private void TimePickerCallback(object sender, TimePickerDialog.TimeSetEventArgs e)
		{
			String AM_PM = e.HourOfDay < 12 ? "AM" : "PM";
			m_txtFlightTime.Text = e.HourOfDay + ":" + e.Minute + " " + AM_PM;
			Facade.Instance.CurrentRide.DropOffFlightTime = m_txtFlightTime.Text;
		}

		protected override Dialog OnCreateDialog(int id)
		{
			if (id == TIME_DIALOG_ID)
				return new TimePickerDialog(this, TimePickerCallback, 0, 0, true);

			return null;
		}

		private async Task<List<GetAirlineResponseItem>> GetAirline()
		{
			List<GetAirlineResponseItem> list = null;
			try
			{
				UserTrackingReporter.TrackUser(Constant.CATEGORY_SCHEDULE_RIDE, "Getting airlines");

				var dic = new Dictionary<String, String> { { Constant.GETAIRLINE_PREFIX, "" } };
				var result = await AppData.ApiCall(Constant.GETAIRLINE, dic);
				var tt = (GetAirlineResponse)AppData.ParseResponse(Constant.GETAIRLINE, result);
				list = tt.List;
				UserTrackingReporter.TrackUser(Constant.CATEGORY_SCHEDULE_RIDE, "Airlines retrieved successfully");
			}
			catch (Exception ex)
			{
				HideLoadingView();
				ShowMessageBox("Error", "An error occurred getting airlines. \n\nError: " + ex.Message);
				CrashReporter.Report(ex);
			}
			return list ?? new List<GetAirlineResponseItem>();
		}
		private void OnAirlineChanged(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			var item = m_KvpAirlines[e.Position];
			Facade.Instance.CurrentRide.DropOffAirlines = item.Value;
			Facade.Instance.CurrentRide.DropOffAirlinesId = item.Key.ToString();
		}
		private void OnFlightTypeChanged(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Facade.Instance.CurrentRide.DropOffFlightTypeIsDomestic = e.Position == 0 ? true : false;
		}
		private void OnFlightNumberChanged(object sender, TextChangedEventArgs e)
		{
			Facade.Instance.CurrentRide.DropOffFlightNumber = e.Text.ToString();
		}

		private void SetBindingsOnce()
		{
			this.SetBinding(
				() => Facade.Instance.CurrentRide.CanGoToThePaymentInformation)
				.UpdateSourceTrigger("CanGoToThePaymentInformationChanges")
				.WhenSourceChanges(
					async () =>
					{
						if (Facade.Instance.CurrentRide.CanGoToThePaymentInformation)
						{
							AppSettings.currentActivity.StartActivity(new Intent(this, typeof(RidePaymentActivity)));
							AppSettings.currentActivity.OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
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
					}
				);

			this.SetBinding(
				() => Facade.Instance.CurrentRide.Dummy)
				.UpdateSourceTrigger("ViewModelReset")
				.WhenSourceChanges(
					() =>
					{
						m_txtPickupLocation.Text = "";
						m_txtDropoffLocation.Text = "";
					});
		}
	}
}

