
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Android.Support.V4.App;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using RoadRunner.Shared;
using RoadRunner.Shared.Classes;

namespace RoadRunner.Android
{
	public class MyTripsFragment : Fragment
	{
		MainActivity mSuperActivity;
		ListView mDrawerList;
		List<GetMyBookedReservationsResponseReservation> mTripList = new List<GetMyBookedReservationsResponseReservation>();
		//List<Menu_Item> mNavItems = new List<Menu_Item>();

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.fragment_myTrips, container, false);

			mSuperActivity = this.Activity as MainActivity;
			mDrawerList = (ListView)view.FindViewById(Resource.Id.myTripList);

			var tripTypeGroup = (RadioGroup)view.FindViewById(Resource.Id.selectTripType);
			tripTypeGroup.CheckedChange += (s, e) =>
			{
				switch (e.CheckedId)
				{
					case Resource.Id.typeUpcoming:
						getTripByType("1");
						break;
					case Resource.Id.typeCompleted:
						getTripByType("0");
						break;
						//other case statements if more radio buttons
				}
			};

			getTripByType("1");

			return view;
		}

		private void getTripByType(string type)
		{
			mSuperActivity.ShowLoadingView("Loading...");

			var dic = new Dictionary<String, String> {
				{ Constant.GETMYBOOKEDRESERVATIONS_CUSTOMERID, AppSettings.UserID },
				{ Constant.GETMYBOOKEDRESERVATIONS_ISFUTURERES, type },
				{ Constant.GETMYBOOKEDRESERVATIONS_LASTSYNCON, "5/7/2014" }
			};

			String result = String.Empty;

			Task runSync = Task.Factory.StartNew(async () =>
			{
				result = await AppData.ApiCall(Constant.GETMYBOOKEDRESERVATIONS, dic);
				GetMyBookedReservationsResponse response = new GetMyBookedReservationsResponse
				{
					MyReservations = new List<GetMyBookedReservationsResponseReservation>()
				};
				response = (GetMyBookedReservationsResponse)AppData.ParseResponse(Constant.GETMYBOOKEDRESERVATIONS, result);
				mTripList = response.MyReservations;
				TripAdapter adapter = new TripAdapter(this.Activity, mTripList, mSuperActivity, type);
				this.Activity.RunOnUiThread(() =>
				{
					mDrawerList.Adapter = adapter;
					adapter.NotifyDataSetChanged();
					mSuperActivity.HideLoadingView();
				});
			}).Unwrap();
		}
	}
}

