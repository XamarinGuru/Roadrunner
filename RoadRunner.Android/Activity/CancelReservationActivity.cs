
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

using Plugin.Messaging;

namespace RoadRunner.Android
{
	[Activity(Label = "CancelReservationActivity")]
	public class CancelReservationActivity : BaseActivity
	{
		private GetMyBookedReservationsResponseReservation item { get; set; }

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.CancelReservation);

			item = AppSettings.selectedTrip;

			DateTime pDate = DateTime.Parse(item.createdOn);
			string[] arrDate = pDate.GetDateTimeFormats();

			var lblDetailTripTitle = (TextView)FindViewById(Resource.Id.lblDetailTripTitle);
			lblDetailTripTitle.Text = arrDate[8];

			var btnDetailReservationID = (TextView)FindViewById(Resource.Id.btnDetailReservationID);
			string strReservationID = String.Format("{1} {2}", System.Environment.NewLine, "  Reservation #: ", item.ReservationID);
			btnDetailReservationID.Text = strReservationID;

			var btnCancelReservation = (Button)FindViewById(Resource.Id.btnCancelReservation);
			btnCancelReservation.Click += (object sender, EventArgs e) =>
			{
				var phoneCallTask = MessagingPlugin.PhoneDialer;
				if (phoneCallTask.CanMakePhoneCall)
				{
					phoneCallTask.MakePhoneCall("8002477919", "Roadrunner Support");
				}
				else {
					AlertDialog.Builder alert = new AlertDialog.Builder(this);
					alert.SetTitle("error");
					alert.SetMessage("You can't make phone call on emulator");
					alert.SetPositiveButton("OK", (senderAlert, args) => { });
					alert.Show();
				}
			};

			if (item.pickUpTime == "")
				pDate = DateTime.Now;
			else
				pDate = DateTime.Parse(item.pickUpTime);
			arrDate = pDate.GetDateTimeFormats();

			var lblDetailPickupDate = (TextView)FindViewById(Resource.Id.lblDetailPickupDate);
			lblDetailPickupDate.Text = String.Format("{1} {2} {3}", System.Environment.NewLine, "Pick up at ", arrDate[106], " from");

			var pCity = item.Pickcity.IsMatchRegex
				? String.Format("{1} {2} {3} {4} {5} {6}", System.Environment.NewLine, item.Pickcity.stNum, item.Pickcity.street, item.Pickcity.complex, item.Pickcity.unit,
					item.Pickcity.city, item.Pickcity.zip)
				: String.Format("{1}", System.Environment.NewLine, item.Pickcity.raw);

			var lblDetailPickupLocation = (TextView)FindViewById(Resource.Id.lblDetailPickupLocation);
			lblDetailPickupLocation.Text = pCity;

			if (item.dropOffTime == "")
				pDate = DateTime.Now;
			else
				pDate = DateTime.Parse(item.dropOffTime);
			arrDate = pDate.GetDateTimeFormats();

			var lblDetailDropoffDate = (TextView)FindViewById(Resource.Id.lblDetailDropoffDate);
			lblDetailDropoffDate.Text = String.Format("{1} {2} {3}", System.Environment.NewLine, "Take off at ", arrDate[106], " from");

			var dCity = item.Dropoff.IsMatchRegex
				? String.Format("{1} {2} {3} {4} {5} {6}", System.Environment.NewLine, item.Dropoff.stNum, item.Dropoff.street, item.Dropoff.complex, item.Dropoff.unit, item.Dropoff.city,
					item.Dropoff.zip)
				: String.Format("{1}", System.Environment.NewLine, item.Dropoff.raw);

			var lblDetailDropoffLocation = (TextView)FindViewById(Resource.Id.lblDetailDropoffLocation);
			lblDetailDropoffLocation.Text = dCity;

			var btnBack = (ImageButton)FindViewById(Resource.Id.btn_back);
			btnBack.Click += delegate (object sender, EventArgs e)
			{
				OnBack();
			};
		}
	}
}

