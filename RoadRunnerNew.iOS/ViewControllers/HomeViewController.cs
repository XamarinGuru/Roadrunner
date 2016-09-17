using Foundation;
using System;
using UIKit;
using RoadRunner.Shared;
using System.Globalization;
using Facebook.LoginKit;
using Google.Plus;
using MonoTouch.Dialog;
using FlyoutNavigation;
using System.Collections.Generic;
using CoreGraphics;
using LinkedIn.SignIn;

namespace RoadRunnerNew.iOS
{
	partial class HomeViewController : UIViewController, IUITableViewDelegate
	{

		public HomeViewController () : base ()
		{
		}

		public HomeViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			AppDelegate.FlyoutNavigation = new FlyoutNavigationController ();
			AppDelegate.FlyoutNavigation.Position = FlyOutNavigationPosition.Left;
			AppDelegate.FlyoutNavigation.AlwaysShowLandscapeMenu = false;
			AppDelegate.FlyoutNavigation.ForceMenuOpen = false;
			AppDelegate.FlyoutNavigation.View.Frame = UIScreen.MainScreen.Bounds;

			AppDelegate.FlyoutNavigation.NavigationTableView.RowHeight = 60;
			AppDelegate.FlyoutNavigation.NavigationTableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			AppDelegate.FlyoutNavigation.NavigationTableView.SectionHeaderHeight = 0;
			AppDelegate.FlyoutNavigation.NavigationTableView.BackgroundColor = new UIColor ((nfloat)(0 / 255.0), (nfloat)(0 / 255.0), (nfloat)(0 / 255.0), (nfloat)0);

			View.AddSubview (AppDelegate.FlyoutNavigation.View);
			AddChildViewController (AppDelegate.FlyoutNavigation);


			var username = String.Format ("{0} {1}", AppSettings.UserFirstName, AppSettings.UserLastName);
			var userphoto = UIImage.FromBundle ("empty.png");

			var menu = new Dictionary<Section, Dictionary<ImageStringElement, UINavigationController>>()
		    {
				{
					new Section("Help"), new Dictionary<ImageStringElement, UINavigationController>
					{
						{new ImageStringElement(username, userphoto), null},
						{new ImageStringElement("Home", UIImage.FromFile("micon_user.png")), new UINavigationController(Storyboard.InstantiateViewController("MapViewController"))},
						{new ImageStringElement("Book a Ride", UIImage.FromFile("micon_user.png")), new UINavigationController(Storyboard.InstantiateViewController("ScheduleARideViewController"))},
						{new ImageStringElement("Payment", UIImage.FromFile("micon_payment.png")), new UINavigationController(Storyboard.InstantiateViewController("BillingInfoViewController"))},
						{new ImageStringElement("My Trips", UIImage.FromFile("micon_clock.png")), new UINavigationController(Storyboard.InstantiateViewController("ReservationsDetailsViewController"))},
						{new ImageStringElement("Support", UIImage.FromFile("micon_support.png")), new UINavigationController(Storyboard.InstantiateViewController("SupportViewController"))},
						{new ImageStringElement("Terms", UIImage.FromFile("micon_terms.png")), new UINavigationController(Storyboard.InstantiateViewController("TermsOFServiceViewController"))},
//						{new ImageStringElement("Profile", UIImage.FromFile("micon_user.png")), new UINavigationController(Storyboard.InstantiateViewController("ProfileViewController"))},
						{new ImageStringElement("Change Login", UIImage.FromFile("micon_terms.png")), new UINavigationController(Storyboard.InstantiateViewController("LoginViewController"))}
					}
				}
		    };

			var viewControllers = new List<UINavigationController>();
		    var rootElement = new RootElement("Task List");
		    foreach (var pair in menu)
		    {
		        var section = pair.Key;
		        foreach (var innerPair in pair.Value)
		        {
		            section.Add(innerPair.Key);
                    viewControllers.Add(innerPair.Value);
		        }
                rootElement.Add(section);
		    }

            // Create the menu:
            AppDelegate.FlyoutNavigation.NavigationRoot = rootElement;
            AppDelegate.FlyoutNavigation.ViewControllers = viewControllers.ToArray();
			AppDelegate.FlyoutNavigation.SelectedIndex = 1;
			AppDelegate.FlyoutNavigation.SelectedIndexChanged = new Action(delegate {
				int idx = AppDelegate.FlyoutNavigation.SelectedIndex;

				if (idx == 7) {
					LogoutProcess();
				}

				//if(idx != 1){
					Facade.Instance.ResetCurrentRide();

					for (int i=0; i < Facade.Instance.UIViewControllerList.Count; i++) {
						UIViewController vController = Facade.Instance.UIViewControllerList[i] as UIViewController;
						if(vController!=null){
							foreach(UIView view in vController.View.Subviews){
								UITextField textField = view as UITextField;
								if(textField!=null)
								{
									//textField.Text = "";	
								}
							}
						}
						vController.View.Dispose();
						vController.Dispose();
						vController.ViewDidLoad();
					}
				//}
			});


			foreach (var cell in AppDelegate.FlyoutNavigation.NavigationTableView.VisibleCells) 
			{
				cell.BackgroundColor = new UIColor ((nfloat)(0 / 255.0), (nfloat)(0 / 255.0), (nfloat)(0 / 255.0), (nfloat)0);
				cell.TextLabel.TextColor = UIColor.White;

				var seperator = new UIView (new CGRect(0.0f, cell.Frame.Height-2.0f, cell.Frame.Width, 2.0f));
				seperator.BackgroundColor = new UIColor ((nfloat)(171.0 / 255.0), (nfloat)(200.0 / 255.0), (nfloat)(226.0 / 255.0), (nfloat)1.0);
				cell.AddSubview (seperator);
			}

			UIImageView iView = new UIImageView ();
			if (AppSettings.LoginType == 0) {
				iView.Image = UIImage.FromBundle("icon_avatar.png");
			}else{
				iView.Image = UIImage.LoadFromData (NSData.FromUrl (new NSUrl (AppSettings.UserPhoto)));
				iView.Layer.CornerRadius = 25.0f;
			}

			iView.Frame = new CGRect (0, 0, 50, 50);

			UITableViewCell mCell = AppDelegate.FlyoutNavigation.NavigationTableView.VisibleCells [0];
			mCell.SelectionStyle = UITableViewCellSelectionStyle.None;
			mCell.ImageView.AddSubview (iView);

		    NavigationItem.LeftBarButtonItem = new UIBarButtonItem("Menu", UIBarButtonItemStyle.Plain, (sender, e) => { AppDelegate.FlyoutNavigation.ToggleMenu(); });
		}

		private void LogoutProcess()
		{
			UserTrackingReporter.TrackUser(Constant.LOGOUT, "Logout process started");

			LoginType currentLt;
			if (Enum.TryParse(AppSettings.LoginType.ToString(CultureInfo.InvariantCulture), out currentLt))
			{
				switch (currentLt)
				{
				case LoginType.Email:
					AppSettings.UserLogin = "";
					AppSettings.UserPassword = "";

					UserTrackingReporter.TrackUser(Constant.LOGOUT, "Email logout");
					break;
				case LoginType.Facebook:
					UserTrackingReporter.TrackUser (Constant.LOGOUT, "Facebook logout");
					new LoginManager().LogOut();
					break;
				case LoginType.Google:
					UserTrackingReporter.TrackUser(Constant.LOGOUT, "Google+ logout");
					SignIn.SharedInstance.SignOut();
					break;
				case LoginType.LinkedIn:
					SessionManager.ClearSession ();
					break;
				}
			}

			UserTrackingReporter.TrackUser(Constant.LOGOUT, "Logout completed, navigating to the login screen");
		}


		class TaskPageController : DialogViewController
		{
			public TaskPageController (string title) : base (null) // : base (title)
			{
				Root = new RootElement (title) {
					new Section {
						new CheckboxElement (title)
					}
				};

			    NavigationItem.LeftBarButtonItem = new UIBarButtonItem("Menu",
			        UIBarButtonItemStyle.Plain, (sender, e) => { AppDelegate.FlyoutNavigation.ToggleMenu(); });
			}

			private void BtnLogout_TouchUpInside (object sender, EventArgs e)
			{
				UserTrackingReporter.TrackUser(Constant.LOGOUT, "Logout process started");

				LoginType currentLt;
				if (Enum.TryParse(AppSettings.LoginType.ToString(CultureInfo.InvariantCulture), out currentLt))
				{
					switch (currentLt)
					{
					case LoginType.Email:
						AppSettings.UserLogin = "";
						AppSettings.UserPassword = "";

						UserTrackingReporter.TrackUser(Constant.LOGOUT, "Email logout");
						break;
					case LoginType.Facebook:
						UserTrackingReporter.TrackUser(Constant.LOGOUT, "Facebook logout");
						new LoginManager().LogOut();
						break;
					case LoginType.Google:
						UserTrackingReporter.TrackUser(Constant.LOGOUT, "Google+ logout");
						SignIn.SharedInstance.SignOut();
						break;
					case LoginType.LinkedIn:
						break;
					}
				}

				UserTrackingReporter.TrackUser(Constant.LOGOUT, "Logout completed, navigating to the login screen");
				var sb = UIStoryboard.FromName ("MainStoryboard", null);
				var vc = (LoginViewController)sb.InstantiateViewController("LoginViewController");
				this.PresentViewController(vc, true, null);
			}
		}
	}
}
