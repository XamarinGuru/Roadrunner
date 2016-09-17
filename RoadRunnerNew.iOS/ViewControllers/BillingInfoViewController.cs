using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using CoreGraphics;
using RoadRunnerNew.iOS.Extension;

namespace RoadRunnerNew.iOS
{
	partial class BillingInfoViewController : BaseTitleViewController
	{
		private BillingInfoDataSource dataSource;

		public BillingInfoViewController (IntPtr handle) : base (handle)
		{
			Title = "PAYMENT INFO";
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad();
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			dataSource = new BillingInfoDataSource (this, DoUpdate, NavigationController);
			PaymentTableView.Source = dataSource;
			ShowLoadingView("Loading...");
		}

		public void DoUpdate(){
			InvokeOnMainThread(delegate {
				PaymentTableView.ReloadData ();
				this.View.SetNeedsDisplay();
				HideLoadingView();
			});

		}
	}
}
