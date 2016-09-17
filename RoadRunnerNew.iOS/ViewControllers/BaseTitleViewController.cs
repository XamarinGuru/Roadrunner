using System;
using UIKit;
using RoadRunner.Shared;
using System.Globalization;
using RoadRunner.Shared.Classes;
using Facebook.LoginKit;
using Facebook.CoreKit;
using Google.Plus;
using CoreGraphics;
using Foundation;
using LinkedIn.SignIn;

namespace RoadRunnerNew.iOS
{
	public class BaseTitleViewController : BaseViewController
	{
		public BaseTitleViewController(String title = "") : base()
		{
			Init(title);
		}

		public BaseTitleViewController(IntPtr handle, String title = "") : base(handle)
		{
			Init(title);
		}

		protected virtual void Init(String title){
			if (!String.IsNullOrEmpty (title)) {
				Title = title;
			}
				
			var rightButton = new UIButton(new CGRect(0, 0, 40, 40));
			rightButton.SetImage(UIImage.FromFile("icon_mark.png"), UIControlState.Normal);
			//rightButton.TouchUpInside += BtnLogout_TouchUpInside;
			NavigationItem.RightBarButtonItem = new UIBarButtonItem(rightButton);

			var buttonMenu = new UIButton(new CGRect(0, 0, 25, 20));
			buttonMenu.SetImage(UIImage.FromBundle("icon_menu"), UIControlState.Normal);
			buttonMenu.TouchUpInside += (object sender, EventArgs e) => {
				AppDelegate.FlyoutNavigation.ToggleMenu ();
			};
			NavigationItem.LeftBarButtonItem = new UIBarButtonItem(buttonMenu);
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
			UIStoryboard sb = UIStoryboard.FromName ("MainStoryboard", null);
			var vc = (LoginViewController)sb.InstantiateViewController("LoginViewController");
			var nav = new UINavigationController (vc);
			this.PresentViewController(nav, true, null);
		}
	}
}

