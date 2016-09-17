using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using CoreGraphics;

namespace RoadRunnerNew.iOS
{
	partial class PreviousReservationsViewController : BaseTitleViewController
	{
		private UITableView tableView;
		private ReservationsDataSource dataSource;
		private NSLayoutConstraint topConstraint; 

		public PreviousReservationsViewController (IntPtr handle) : base (handle)
		{
			Title = "PREVIOUS RESERVATIONS";
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.


			Add(tableView = new UITableView(View.Frame));


			tableView.TranslatesAutoresizingMaskIntoConstraints = false;
			tableView.ContentInset = new UIEdgeInsets(20, 0, 0, 0);

			topConstraint = NSLayoutConstraint.Create (tableView, NSLayoutAttribute.Top,
				NSLayoutRelation.LessThanOrEqual, View, NSLayoutAttribute.TopMargin, 1, NavigationController.NavigationBar.Frame.Bottom);
			View.AddConstraint(topConstraint);
			View.AddConstraint(NSLayoutConstraint.Create(tableView, NSLayoutAttribute.Left,
				NSLayoutRelation.Equal, View, NSLayoutAttribute.Left, 1, 0));
			View.AddConstraint(NSLayoutConstraint.Create(tableView, NSLayoutAttribute.Width,
				NSLayoutRelation.Equal, View, NSLayoutAttribute.Width, 1, 0));
			View.AddConstraint(NSLayoutConstraint.Create(tableView, NSLayoutAttribute.Height,
				NSLayoutRelation.Equal, View, NSLayoutAttribute.Height, 1, 0));

			dataSource = new ReservationsDataSource (this, false, DoUpdate);
			tableView.Source = dataSource;
			ShowLoadingView("Loading...");
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);


		}

		public void DoUpdate(){
			InvokeOnMainThread(delegate {
				tableView.ReloadData ();
				this.View.SetNeedsDisplay();
				HideLoadingView();
			});

		}
	}
}
