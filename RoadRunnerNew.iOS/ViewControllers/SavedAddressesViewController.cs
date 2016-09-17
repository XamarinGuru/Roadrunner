using System;
using UIKit;

namespace RoadRunnerNew.iOS
{
	partial class SavedAddressesViewController : BaseTitleViewController
	{
		private UITableView tableView;
		private SavedAddressesDataSource dataSource;

		private NSLayoutConstraint topConstraint; 


		public SavedAddressesViewController (IntPtr handle) : base (handle)
		{
			Title = "SAVED ADDRESSES";
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			Add(tableView = new UITableView(this.View.Frame));

			tableView.TranslatesAutoresizingMaskIntoConstraints = false;
			tableView.ContentInset = new UIEdgeInsets(20, 0, 0, 0);

			topConstraint = NSLayoutConstraint.Create (tableView, NSLayoutAttribute.Top,
				NSLayoutRelation.Equal, View, NSLayoutAttribute.TopMargin, 1, 0);
			View.AddConstraint(topConstraint);
			View.AddConstraint(NSLayoutConstraint.Create(tableView, NSLayoutAttribute.Left,
				NSLayoutRelation.Equal, View, NSLayoutAttribute.Left, 1, 0));
			View.AddConstraint(NSLayoutConstraint.Create(tableView, NSLayoutAttribute.Width,
				NSLayoutRelation.Equal, View, NSLayoutAttribute.Width, 1, 0));
			View.AddConstraint(NSLayoutConstraint.Create(tableView, NSLayoutAttribute.Height,
				NSLayoutRelation.Equal, View, NSLayoutAttribute.Height, 1, 0));

			dataSource = new SavedAddressesDataSource (this, DoUpdate);
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
