// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace RoadRunnerNew.iOS
{
	[Register ("ReservationsDetailsViewController")]
	partial class MyTripsViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnCompletedTrips { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnUpcomingTrips { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView TripTableView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView viewSelectTripType { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnCompletedTrips != null) {
				btnCompletedTrips.Dispose ();
				btnCompletedTrips = null;
			}
			if (btnUpcomingTrips != null) {
				btnUpcomingTrips.Dispose ();
				btnUpcomingTrips = null;
			}
			if (TripTableView != null) {
				TripTableView.Dispose ();
				TripTableView = null;
			}
			if (viewSelectTripType != null) {
				viewSelectTripType.Dispose ();
				viewSelectTripType = null;
			}
		}
	}
}
