using System;
using RoadRunner.Shared.Classes;
using UIKit;
using RoadRunner.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;
using Foundation;

namespace RoadRunnerNew.iOS
{
	public class ReservationsDataSource : UITableViewSource
	{
		private GetMyBookedReservationsResponse response = new GetMyBookedReservationsResponse {
			MyReservations = new List<GetMyBookedReservationsResponseReservation>()
		};

		private UIViewController owner;

		public ReservationsDataSource(UIViewController owner, bool IsActiveReservations, Action DoUpdate)
		{
			this.owner = owner;

			var dic = new Dictionary<String, String>
			{
				{Constant.GETMYBOOKEDRESERVATIONS_CUSTOMERID, AppSettings.UserID}, //"11111"},
				{Constant.GETMYBOOKEDRESERVATIONS_ISFUTURERES, IsActiveReservations?"1":"0"},
				{Constant.GETMYBOOKEDRESERVATIONS_LASTSYNCON, "6/7/2014"}
			};

			String result = String.Empty;

			Task runSync = Task.Factory.StartNew(async () => {
				result = await AppData.ApiCall(Constant.GETMYBOOKEDRESERVATIONS, dic);
				response = (GetMyBookedReservationsResponse)AppData.ParseResponse(Constant.GETMYBOOKEDRESERVATIONS, result);
				if (DoUpdate != null)
					DoUpdate();
			}).Unwrap();
			//runSync.Wait();

		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var sb = UIStoryboard.FromName ("MainStoryboard", null);
			var reservationsDetails = (MyTripsViewController)sb.InstantiateViewController ("ReservationsDetailsViewController");
			GetMyBookedReservationsResponseReservation item = response.MyReservations[indexPath.Row];
			reservationsDetails.Item = item;
			owner.NavigationController.PushViewController (reservationsDetails, true);
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = new UITableViewCell(UITableViewCellStyle.Subtitle, null);
			var item = response.MyReservations[indexPath.Row];

			cell.TextLabel.Font = UIFont.FromName("Helvetica Light", 14);
			cell.DetailTextLabel.Font = UIFont.FromName("Helvetica Light", 12);
			cell.DetailTextLabel.TextColor = UIColor.LightGray;

			cell.TextLabel.Text = String.Format ("{0} ({1})", item.travelDate, item.ReservationID);
			cell.DetailTextLabel.Text = String.Format ("#passengers {0} pick-up time: {1}", item.noOfPassengers, item.pickTime);

			cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

			if (indexPath.Row % 2 == 0) {
				cell.BackgroundColor = new UIColor (217.0f / 255.0f, 217.0f / 255.0f, 217.0f / 255.0f, 1.0f);
			}

			return cell;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return response.MyReservations.Count;
		}
	}
}

