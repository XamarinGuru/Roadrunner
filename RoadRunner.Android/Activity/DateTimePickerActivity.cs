
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

using RoadRunner.Shared;

namespace RoadRunner.Android
{
	[Activity(Label = "DateTimePickerActivity")]
	public class DateTimePickerActivity : NavigationActivity
	{
		DatePicker m_datePicker;
		TimePicker m_timePicker;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.DateTimePicker);

			m_datePicker = (DatePicker)FindViewById(Resource.Id.datePicker);
			var origin = new DateTime(1970, 1, 1);
			m_datePicker.MinDate = (long)(DateTime.Now.Date.AddYears(-8) - origin).TotalMilliseconds;

			m_timePicker = (TimePicker)FindViewById(Resource.Id.timePicker);

			var btnSetDateTime = (Button)FindViewById(Resource.Id.btnSetDateTime);
			btnSetDateTime.Click += delegate (object sender, EventArgs e)
			{
				var date = m_datePicker.DateTime;
				var arrDate = date.GetDateTimeFormats();
				var formatedDate = arrDate[3];

				String AM_PM = m_timePicker.CurrentHour.IntValue() < 12 ? "AM" : "PM";
				var time = m_timePicker.CurrentHour + ":" + m_timePicker.CurrentMinute + " " + AM_PM;

				Facade.Instance.CurrentRide.RequestedArrivalTimeAndDate = formatedDate + " " + time;
				OnBack();
			};

			var btnBack = (ImageButton)FindViewById(Resource.Id.btn_back);
			btnBack.Click += delegate (object sender, EventArgs e)
			{
				OnBack();
			};
		}
	}
}

