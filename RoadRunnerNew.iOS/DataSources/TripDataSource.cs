using System;
using RoadRunner.Shared.Classes;
using UIKit;
using RoadRunner.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;
using Foundation;

namespace RoadRunnerNew.iOS
{
	public class TripDataSource : UITableViewSource {

		private GetMyBookedReservationsResponse response = new GetMyBookedReservationsResponse {
			MyReservations = new List<GetMyBookedReservationsResponseReservation>()
		};

		private UIViewController owner;
		string cellIdentifier = "TripCell";

		private string m_isActiveReservations;

		public TripDataSource (UIViewController owner, string isActiveReservations, Action DoUpdate)
		{
			this.owner = owner;

			m_isActiveReservations = isActiveReservations;

			var dic = new Dictionary<String, String> {
				{ Constant.GETMYBOOKEDRESERVATIONS_CUSTOMERID, AppSettings.UserID },
				{ Constant.GETMYBOOKEDRESERVATIONS_ISFUTURERES, isActiveReservations },
				{ Constant.GETMYBOOKEDRESERVATIONS_LASTSYNCON, "5/7/2014" }
			};

			String result = String.Empty;

			Task runSync = Task.Factory.StartNew (async () => {

				result = await AppData.ApiCall (Constant.GETMYBOOKEDRESERVATIONS, dic);

				response = (GetMyBookedReservationsResponse)AppData.ParseResponse(Constant.GETMYBOOKEDRESERVATIONS, result);
				if(DoUpdate != null)
					DoUpdate();

			}).Unwrap ();
		}
		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return response.MyReservations.Count;
		}
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			if (m_isActiveReservations == "1") {
				TripCell cell = tableView.DequeueReusableCell("TripCell") as TripCell;
				cell.SetCell (response.MyReservations[indexPath.Row], owner);
				cell.BackgroundColor = cell.ContentView.BackgroundColor;
				return cell;
			} else {
				CompletedTripCell cell = tableView.DequeueReusableCell("CompletedTripCell") as CompletedTripCell;
				cell.SetCell (response.MyReservations[indexPath.Row], owner);
				cell.BackgroundColor = cell.ContentView.BackgroundColor;
				return cell;
			}
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad) {
				if (m_isActiveReservations == "1") {
					return new nfloat(330.0f);
				} else {
					return new nfloat(300.0f);
				}
			} else {
				if (m_isActiveReservations == "1") {
					return new nfloat(260.0f);
				} else {
					return new nfloat(230.0f);
				}
			}
		}
	}
}

