using System;
using UIKit;
using RoadRunner.Shared.Classes;
using CoreGraphics;

namespace RoadRunnerNew.iOS
{
	partial class SavedAddressesDetailsViewController : UIViewController
	{
		public GetRecentPickUpAddressResponseItem Item {get; set;}

		public SavedAddressesDetailsViewController (IntPtr handle) : base (handle)
		{
			Title = "SAVED ADDRESSES DETAILS";

			var button = new UIButton(new CGRect(0, 0, 20, 20));
			button.SetImage(UIImage.FromBundle("back-ic"), UIControlState.Normal);
			button.TouchUpInside += (sender, e) => NavigationController.PopViewController (true);
			NavigationItem.LeftBarButtonItem = new UIBarButtonItem(button);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			lblPickUpAddress.Text = Item.PickUpAddress;
			lblStreet.Text = Item.Street;
			lblCity.Text = Item.City;
			lblZip.Text = Item.Zip;
			lblComment.Text = Item.Comment;
		}
	}
}
