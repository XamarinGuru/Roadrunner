
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
	[Activity(Label = "LocationZipActivity")]
	public class LocationZipActivity : BaseActivity
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

			SetContentView(Resource.Layout.LocationZip);

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
			Facade.Instance.CurrentRide.PickUpFlightTime = m_txtFlightTime.Text;
		}

		protected override Dialog OnCreateDialog(int id)
		{
			if (id == TIME_DIALOG_ID)
				return new TimePickerDialog(this, TimePickerCallback, 0, 0, true);

			return null;
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

