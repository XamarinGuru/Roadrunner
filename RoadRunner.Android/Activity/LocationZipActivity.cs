
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace RoadRunner.Android
{
	[Activity(Label = "LocationZipActivity")]
	public class LocationZipActivity : NavigationActivity
	{
		TextView txtFlightTime;

		private int hour;
		private int minute;

		const int TIME_DIALOG_ID = 0;


		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.LocationZip);

			TextView txtSearchLocation = (TextView)FindViewById(Resource.Id.txtRequestPickupDate);
			txtSearchLocation.Touch += PopupSetDateTime;

			txtFlightTime = (TextView)FindViewById(Resource.Id.txtFlightTime);
			txtFlightTime.Click += (o, e) => ShowDialog(TIME_DIALOG_ID);

			Button btnGotoRidePayment = (Button)FindViewById(Resource.Id.btn_GoToRidePayment);
			btnGotoRidePayment.Click += delegate (object sender, EventArgs e)
			{
				StartActivity(new Intent(this, typeof(RidePaymentActivity)));
				OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
			};

			ImageButton btnBack = (ImageButton)FindViewById(Resource.Id.btn_back);
			btnBack.Click += delegate (object sender, EventArgs e)
			{
				OnBack();
			};
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

		private void UpdateDisplay()
		{
			string time = string.Format("{0}:{1}", hour, minute.ToString().PadLeft(2, '0'));
			txtFlightTime.Text = time;
		}

		private void TimePickerCallback(object sender, TimePickerDialog.TimeSetEventArgs e)
		{
			hour = e.HourOfDay;
			minute = e.Minute;
			UpdateDisplay();
		}

		protected override Dialog OnCreateDialog(int id)
		{
			if (id == TIME_DIALOG_ID)
				return new TimePickerDialog(this, TimePickerCallback, hour, minute, false);

			return null;
		}
	}
}

