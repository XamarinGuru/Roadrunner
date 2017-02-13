using System;

using Foundation;
using UIKit;
using RoadRunnerNew.iOS.Extension;

namespace RoadRunnerNew.iOS
{
	public partial class BillAddCell : UITableViewCell
	{
		private UINavigationController _NavigationController;
		public static readonly NSString Key = new NSString ("BillAddCell");
		public static readonly UINib Nib;

		static BillAddCell ()
		{
			Nib = UINib.FromName ("BillAddCell", NSBundle.MainBundle);
		}

		public BillAddCell (IntPtr handle) : base (handle)
		{
		}

		public void SetCell(UINavigationController NavigationController)
		{
			_NavigationController = NavigationController;

			btnAddBill.SetCustomButton ();
			btnAddBill.TouchUpInside -= AddCreditCard;
			btnAddBill.TouchUpInside += AddCreditCard;
		}

		private void AddCreditCard(object sender, EventArgs args){
			UIStoryboard sb = UIStoryboard.FromName ("MainStoryboard", null);
			AddCreditCardViewController pvc = (AddCreditCardViewController)sb.InstantiateViewController ("AddCreditCardViewController");
			pvc.fromWhere = "bill";
			_NavigationController.PushViewController (pvc, true);
		}
	}
}
