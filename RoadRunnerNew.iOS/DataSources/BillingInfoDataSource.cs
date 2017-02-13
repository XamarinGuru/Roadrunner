using System;
using UIKit;
using System.Collections.Generic;
using RoadRunner.Shared;
using System.Threading.Tasks;
using RoadRunner.Shared.Classes;
using Foundation;
using System.Drawing;

namespace RoadRunnerNew.iOS
{
	public class BillingInfoDataSource : UITableViewSource
	{
		private Action _DoUpdate;
		private UINavigationController _NavigationController;
		private GetCreditCardDetailsNewForPhoneResponse response = new GetCreditCardDetailsNewForPhoneResponse {
			CardList = new List<GetCreditCardDetailsNewForPhoneResponseItem> ()
		};

		private UIViewController owner;

		public BillingInfoDataSource(UIViewController owner,  Action DoUpdate, UINavigationController NavigationController)
		{
			this.owner = owner;
			_DoUpdate = DoUpdate;
			_NavigationController = NavigationController;
			LoadData ();
		}

		private void LoadData(){
			var dic = new Dictionary<String, String>
			{
				{Constant.GETCREDITCARDDETAILSNEWFORPHONE_CUSTOMERID, AppSettings.UserID},
				{Constant.GETCREDITCARDDETAILSNEWFORPHONE_LOGINTYPE, "-1"},
				{Constant.GETCREDITCARDDETAILSNEWFORPHONE_TOKENID, string.Empty } // do not use the actual token
			};

			String result = String.Empty;

			Task runSync = Task.Factory.StartNew(async () => {
				result = await AppData.ApiCall(Constant.GETCREDITCARDDETAILSNEWFORPHONE, dic);
				response = (GetCreditCardDetailsNewForPhoneResponse) AppData.ParseResponse(Constant.GETCREDITCARDDETAILSNEWFORPHONE, result);
				if (_DoUpdate != null)
					_DoUpdate();
			}).Unwrap();
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			UserTrackingReporter.TrackUser (Constant.CATEGORY_PAYMENT, "Card selected");
			var card = response.CardList[indexPath.Row];
			AppSettings.SelectedCard = card.Id;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			if (indexPath.Row == response.CardList.Count) {
				BillAddCell cell = tableView.DequeueReusableCell("BillAddCell") as BillAddCell;
				cell.SetCell (_NavigationController);
				cell.BackgroundColor = cell.ContentView.BackgroundColor;
				return cell;
			} else {
				BillCell cell = tableView.DequeueReusableCell("BillCell") as BillCell;
				cell.SetCell (response.CardList[indexPath.Row]);
				cell.BackgroundColor = cell.ContentView.BackgroundColor;
				return cell;
			}
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad) {
				if (indexPath.Row == response.CardList.Count) {
					return new nfloat (120.0f);
				} else {
					return new nfloat (140.0f);
				}
			} else {
				if (indexPath.Row == response.CardList.Count) {
					return new nfloat (80.0f);
				} else {
					return new nfloat (90.0f);
				}
			}
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return response.CardList.Count+1;
		}

		public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
		{
			if (indexPath.Row == response.CardList.Count) {
				return false;
			} else {
				return true;
			}
		}

		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			if (editingStyle == UITableViewCellEditingStyle.Delete) 
			{
				var alert = new UIAlertView ("Delete Card?", "Are you sure you want to delete this card?", null, "Yes", "No");
				alert.Clicked +=  (object sender, UIButtonEventArgs e) => 
				{
					if (e.ButtonIndex == 0)
					{
						var card = response.CardList[indexPath.Row];
						response.CardList.RemoveAt (indexPath.Row);
						deleteCard (card);
						LoadData();
					}
				};

				alert.Show ();
			}
		}

		public async void deleteCard(GetCreditCardDetailsNewForPhoneResponseItem card)
		{
			UserTrackingReporter.TrackUser (Constant.CATEGORY_PAYMENT, "Deleting card");

			var dic = new Dictionary<String, String>
			{
				{Constant.DELETECREDITCARDNEW_CUSTOMERID, AppSettings.UserID},
				{Constant.DELETECREDITCARDNEW_INFOID, card.Id},
				{Constant.DELETECREDITCARDNEW_LOGINTYPE, AppSettings.LoginType.ToString ()},
				{Constant.DELETECREDITCARDNEW_TOKENID, AppSettings.UserToken}
			};
			try
			{
				var result = await AppData.ApiCall (Constant.DELETECREDITCARDNEW, dic);
				var deleteCardResponse = (DeleteCreditCardNewResponse)AppData.ParseResponse (Constant.DELETECREDITCARDNEW, result);

				InvokeOnMainThread (() => new UIAlertView (
					deleteCardResponse.Result, deleteCardResponse.Message, null, "Ok", null).Show ());
			}
			catch(Exception ex)
			{
				InvokeOnMainThread (() => new UIAlertView (
					"Exception:", ex.Message, null, "Ok", null).Show ());
				CrashReporter.Report (ex);
				return;
			}

		}
	}
}

