using System;
using UIKit;
using RoadRunner.Shared;
using RoadRunnerNew.iOS.Extension;

namespace RoadRunnerNew.iOS
{
	partial class MyTripsViewController : BaseTitleViewController
	{
		public GetMyBookedReservationsResponseReservation Item{ get; set;}

		public MyTripsViewController (IntPtr handle) : base (handle, "MY TRIPS")
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			viewSelectTripType.SetCustomView ();

			btnUpcomingTrips.Selected = true;
			btnUpcomingTrips.BackgroundColor = new UIColor ((nfloat)(21 / 255.0), (nfloat)(100 / 255.0), (nfloat)(179 / 255.0), (nfloat)1.0); 

			btnCompletedTrips.Selected = false;
			btnCompletedTrips.BackgroundColor = new UIColor ((nfloat)(116 / 255.0), (nfloat)(143 / 255.0), (nfloat)(175 / 255.0), (nfloat)1.0); 

			TripTableView.BackgroundView = new UIView ();
			TripTableView.BackgroundView.BackgroundColor = UIColor.Clear;
			TripTableView.Source = new TripDataSource(this, "1", DoUpdate);
			ShowLoadingView ("Loading...");

			btnUpcomingTrips.TouchUpInside += (sender, e) => {
				SetMyTrips("1");
			};

			btnCompletedTrips.TouchUpInside += (sender, e) => {
				SetMyTrips("0");
			};
	    }

		public void SetMyTrips(string type)
		{
			if (type == "1" && !btnUpcomingTrips.Selected) {
				btnUpcomingTrips.Selected = true;
				btnUpcomingTrips.BackgroundColor = new UIColor ((nfloat)(21 / 255.0), (nfloat)(100 / 255.0), (nfloat)(179 / 255.0), (nfloat)1.0); 

				btnCompletedTrips.Selected = false;
				btnCompletedTrips.BackgroundColor = new UIColor ((nfloat)(116 / 255.0), (nfloat)(143 / 255.0), (nfloat)(175 / 255.0), (nfloat)1.0); 

				TripTableView.Source = new TripDataSource (this, type, DoUpdate);
				ShowLoadingView ("Loading...");

			} else if (type == "0" && !btnCompletedTrips.Selected) {
				btnUpcomingTrips.Selected = false;
				btnUpcomingTrips.BackgroundColor = new UIColor ((nfloat)(116 / 255.0), (nfloat)(143 / 255.0), (nfloat)(175 / 255.0), (nfloat)1.0);

				btnCompletedTrips.Selected = true;
				btnCompletedTrips.BackgroundColor = new UIColor ((nfloat)(21 / 255.0), (nfloat)(100 / 255.0), (nfloat)(179 / 255.0), (nfloat)1.0); 

				TripTableView.Source = new TripDataSource (this, type, DoUpdate);
				ShowLoadingView ("Loading...");
			}
		}

		public void DoUpdate(){
			InvokeOnMainThread (delegate {
				TripTableView.ReloadData ();
				this.View.SetNeedsDisplay ();
				HideLoadingView ();
			});
		}
	}
}
