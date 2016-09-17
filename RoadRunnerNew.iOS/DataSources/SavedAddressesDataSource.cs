using System;
using UIKit;
using RoadRunner.Shared;
using System.Collections.Generic;
using RoadRunner.Shared.Classes;
using System.Threading.Tasks;
using Foundation;

namespace RoadRunnerNew.iOS
{
	public class SavedAddressesDataSource : UITableViewSource
	{
		private GetRecentPickUpAddressResponse response = new GetRecentPickUpAddressResponse {
			Items = new List<GetRecentPickUpAddressResponseItem> ()
		};

		private UIViewController owner;

		public SavedAddressesDataSource(UIViewController owner, Action DoUpdate)
		{
			this.owner = owner;

			var dic = new Dictionary<String, String>
			{
				{Constant.GETRECENTPICKUPADDRESS_CUSTOMERID, AppSettings.UserID},
				{Constant.GETRECENTPICKUPADDRESS_RESNO, "0"},
				{Constant.GETRECENTPICKUPADDRESS_ISDARPROFILE, "-1"}
			};

			String result = String.Empty;

			Task runSync = Task.Factory.StartNew(async () => {
				result = await AppData.ApiCall(Constant.GETRECENTPICKUPADDRESS, dic);
				response = (GetRecentPickUpAddressResponse)AppData.ParseResponse(Constant.GETRECENTPICKUPADDRESS, result);
				if (DoUpdate != null)
					DoUpdate();
			}).Unwrap();
//			runSync.Wait();

		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var sb = UIStoryboard.FromName ("MainStoryboard", null);
			var details = (SavedAddressesDetailsViewController)sb.InstantiateViewController ("SavedAddressesDetailsViewController");
			GetRecentPickUpAddressResponseItem item = response.Items[indexPath.Row];
			details.Item = item;
			owner.NavigationController.PushViewController (details, true);
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = new UITableViewCell(UITableViewCellStyle.Subtitle, null);
			var item = response.Items[indexPath.Row];

			cell.TextLabel.Font = UIFont.FromName("Helvetica Light", 14);
			cell.DetailTextLabel.Font = UIFont.FromName("Helvetica Light", 12);
			cell.DetailTextLabel.TextColor = UIColor.LightGray;

			var fullAddress = String.Format ("{0} {1}, {2}, {3}", item.PickUpAddress, item.Street, item.City, item.Zip);

			cell.TextLabel.Text = String.Format ("{0}", fullAddress);
			cell.DetailTextLabel.Text = String.Format ("{0}", item.Comment);

			cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

			if (indexPath.Row % 2 == 0) {
				cell.BackgroundColor = new UIColor (217.0f / 255.0f, 217.0f / 255.0f, 217.0f / 255.0f, 1.0f);
			}

			return cell;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return response.Items.Count;
		}
	}
}

