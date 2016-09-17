
using System;
using System.Collections.Generic;
using Java.Util;
using Java.Lang;

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
	class TripAdapter : BaseAdapter
	{

		Context mContext;
		List<GetMyBookedReservationsResponseReservation> mTripList;
		Activity mSuperActivity;
		string mType;

		public TripAdapter(Context context, List<GetMyBookedReservationsResponseReservation> tripList, Activity superActivity, string type)
		{
			mContext = context;
			mTripList = tripList;
			mSuperActivity = superActivity;
			mType = type;
		}

		public override int Count
		{
			get { return mTripList.Count; }
		}

		public override Java.Lang.Object GetItem(int position)
		{
			return null;
		}

		public GetMyBookedReservationsResponseReservation GetNavItem(int position)
		{
			return mTripList[position];
		}

		override public long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			
			var trip = GetNavItem(position);

			if (mType == "1")
			{
				convertView = LayoutInflater.From(mContext).Inflate(Resource.Layout.item_upcomingTrip, null);

				var txtTripDate = (TextView)convertView.FindViewById(Resource.Id.txtTripDate);
				var txtTripID = (TextView)convertView.FindViewById(Resource.Id.txtTripID);
				var txtPickupTime = (TextView)convertView.FindViewById(Resource.Id.txtPickupTime);
				var txtPickupLocation = (TextView)convertView.FindViewById(Resource.Id.txtPickupLocation);
				var txtDropoffTime = (TextView)convertView.FindViewById(Resource.Id.txtDropoffTime);
				var txtDropoffLocation = (TextView)convertView.FindViewById(Resource.Id.txtDropoffLocation);

				DateTime pDate = DateTime.Parse(trip.createdOn);
				string[] arrDate = pDate.GetDateTimeFormats();

				txtTripDate.Text = arrDate[8];

				txtTripID.Text = string.Format("{0} {1}", "  Reservation #: ", trip.ReservationID);

				if (trip.pickUpTime == "")
					pDate = DateTime.Now;
				else
					pDate = DateTime.Parse(trip.pickUpTime);
				arrDate = pDate.GetDateTimeFormats();

				txtPickupTime.Text = string.Format("{0} {1} {2}", "Pick up at ", arrDate[106], " from");

				var pCity = trip.Pickcity.IsMatchRegex
					? string.Format("{0} {1} {2} {3} {4} {5}", trip.Pickcity.stNum, trip.Pickcity.street, trip.Pickcity.complex, trip.Pickcity.unit, trip.Pickcity.city, trip.Pickcity.zip)
					: trip.Pickcity.raw;

				txtPickupLocation.Text = pCity;

				if (trip.dropOffTime == "")
					pDate = DateTime.Now;
				else
					pDate = DateTime.Parse(trip.dropOffTime);
				arrDate = pDate.GetDateTimeFormats();

				txtDropoffTime.Text = string.Format("{0} {1} {2}", "Take off at ", arrDate[106], " from");

				var dCity = trip.Dropoff.IsMatchRegex
					? string.Format("{0} {1} {2} {3} {4} {5}", trip.Dropoff.stNum, trip.Dropoff.street, trip.Dropoff.complex, trip.Dropoff.unit, trip.Dropoff.city, trip.Dropoff.zip)
					: trip.Dropoff.raw;

				txtDropoffLocation.Text = dCity;

				var btnEdit = (Button)convertView.FindViewById(Resource.Id.btnEdit);
				btnEdit.Tag = trip.ReservationID;
				var btnCancel = (Button)convertView.FindViewById(Resource.Id.btnCancel);
				btnCancel.Tag = trip.ReservationID;

				btnEdit.Click -= GoToEditReservationViewController;
				btnEdit.Click += GoToEditReservationViewController;
				btnCancel.Click -= GoToCancelReservationViewController;
				btnCancel.Click += GoToCancelReservationViewController;
			}
			else {
				convertView = LayoutInflater.From(mContext).Inflate(Resource.Layout.item_completedTrip, null);

				var txtTripDate = (TextView)convertView.FindViewById(Resource.Id.txtTripDate);
				var txtTripID = (TextView)convertView.FindViewById(Resource.Id.txtTripID);
				var txtPickupTime = (TextView)convertView.FindViewById(Resource.Id.txtPickupTime);
				var txtPickupLocation = (TextView)convertView.FindViewById(Resource.Id.txtPickupLocation);
				var txtDropoffTime = (TextView)convertView.FindViewById(Resource.Id.txtDropoffTime);
				var txtDropoffLocation = (TextView)convertView.FindViewById(Resource.Id.txtDropoffLocation);

				DateTime pDate = DateTime.Parse(trip.createdOn);
				string[] arrDate = pDate.GetDateTimeFormats();

				txtTripDate.Text = arrDate[8];

				txtTripID.Text = string.Format("{0} {1}", "  Reservation #: ", trip.ReservationID);

				if (trip.pickUpTime == "")
					pDate = DateTime.Now;
				else
					pDate = DateTime.Parse(trip.pickUpTime);
				arrDate = pDate.GetDateTimeFormats();

				txtPickupTime.Text = string.Format("{0} {1} {2}", "Pick up at ", arrDate[106], " from");

				var pCity = trip.Pickcity.IsMatchRegex
					? string.Format("{0} {1} {2} {3} {4} {5}", trip.Pickcity.stNum, trip.Pickcity.street, trip.Pickcity.complex, trip.Pickcity.unit, trip.Pickcity.city, trip.Pickcity.zip)
					: trip.Pickcity.raw;

				txtPickupLocation.Text = pCity;

				if (trip.dropOffTime == "")
					pDate = DateTime.Now;
				else
					pDate = DateTime.Parse(trip.dropOffTime);
				arrDate = pDate.GetDateTimeFormats();

				txtDropoffTime.Text = string.Format("{0} {1} {2}", "Take off at ", arrDate[106], " from");

				var dCity = trip.Dropoff.IsMatchRegex
					? string.Format("{0} {1} {2} {3} {4} {5}", trip.Dropoff.stNum, trip.Dropoff.street, trip.Dropoff.complex, trip.Dropoff.unit, trip.Dropoff.city, trip.Dropoff.zip)
					: trip.Dropoff.raw;

				txtDropoffLocation.Text = dCity;
			}

			return convertView;
		}

		internal void GoToEditReservationViewController(object sender, EventArgs ea)
		{
			var button = (Button)sender;
			var resID = button.Tag;

			for (var i = 0; i < mTripList.Count; i++)
			{
				var trip = mTripList[i];
				if (resID.ToString() == trip.ReservationID)
				{
					AppSettings.selectedTrip = trip;
				}
			}
			mSuperActivity.StartActivity(new Intent(mSuperActivity, typeof(EditReservationActivity)));
			mSuperActivity.OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
		}
		internal void GoToCancelReservationViewController(object sender, EventArgs ea)
		{
			var button = (Button)sender;
			var resID = button.Tag;

			for (var i = 0; i < mTripList.Count; i++)
			{
				var trip = mTripList[i];
				if (resID.ToString() == trip.ReservationID)
				{
					AppSettings.selectedTrip = trip;
				}
			}
			mSuperActivity.StartActivity(new Intent(mSuperActivity, typeof(CancelReservationActivity)));
			mSuperActivity.OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
		}
	}
}