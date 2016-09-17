using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using CoreLocation;
using Facebook.CoreKit;
using Facebook.LoginKit;
using CoreGraphics;
using Foundation;
using LinkedIn.SignIn;
using Google.Maps;
using Google.Plus;
using RoadRunner.Shared;
using UIKit;
using FlyoutNavigation;
using System.Threading;
using Google.Core;
using Google.SignIn;

namespace RoadRunnerNew.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
		const string fbscheme = "fb" + Constant.FacebookAppId;
		const string lischeme = "li" + Constant.LinkedInAppId;
		const string gplusscheme = "com.roadrunner.roadrunner";

		public static LocationHelper MyLocationHelper = new LocationHelper();

		public static MainNavigationController MNController;
		public static UIWindow _window;

		public static FlyoutNavigationController FlyoutNavigation;

        // class-level declarations
        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
			NSThread.SleepFor (1);

            _window = new UIWindow(UIScreen.MainScreen.Bounds);

            MapServices.ProvideAPIKey(Constant.GOOGLE_PLACE_APIKEY);
            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, true);

			UINavigationBar.Appearance.BarTintColor =  new UIColor ((nfloat)(60.0 / 255.0), (nfloat)(90.0 / 255.0), (nfloat)(126.0 / 255.0), (nfloat)1.0);
			UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes()
			{
				ForegroundColor = UIColor.White
			};

            #region Facebook login

            Profile.EnableUpdatesOnAccessTokenChange(true);
            Settings.AppID = Constant.FacebookAppId;
            Settings.DisplayName = Constant.DisplayName;

//			NSError configureError;
//			Context.SharedInstance.Configure (out configureError);
//			if (configureError != null) {
//				Console.WriteLine ("Error configuring the Google context: {0}", configureError);
//			}
//
//			Invite.ApplicationDidFinishLaunching ();
//
//			return true;

            // This method verifies if you have been logged into the app before, and keep you logged in after you reopen or kill your app.
            return ApplicationDelegate.SharedInstance.FinishedLaunching(application, launchOptions);
            #endregion
        }

		public override bool OpenUrl (UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
		{
//			var invite = Invite.HandleUrl (url, sourceApplication, annotation);
//			if (invite != null) {
//				var message =string.Format ("Deep link from {0} \nInvite ID: {1}\nApp URL: {2}",
//					sourceApplication, invite.InviteId, invite.DeepLink);
//				new UIAlertView (@"Deep-link Data", message, null, "OK").Show ();
//
//				return true;
//			}

			switch (url.Scheme) {
			//FB
			case fbscheme:
				return ApplicationDelegate.SharedInstance.OpenUrl (application, url, sourceApplication, annotation);
			//G+
			case gplusscheme:
				return UrlHandler.HandleUrl (url, sourceApplication, annotation);

			case lischeme:
				return CallbackHandler.OpenUrl (application, url, sourceApplication, new NSString("linkedin"));
			
			}
			return true;
		}

        // This method is invoked when the application is about to move from active to inactive state.
        // OpenGL applications should use this method to pause.
        public override void OnResignActivation(UIApplication application)
        {
        }
        // This method should be used to release shared resources and it should store the application state.
        // If your application supports background exection this method is called instead of WillTerminate
        // when the user quits.
        public override void DidEnterBackground(UIApplication application)
        {
        }
        // This method is called as part of the transiton from background to active state.
        public override void WillEnterForeground(UIApplication application)
        {
        }
        // This method is called when the application is about to terminate. Save data, if needed.
        public override void WillTerminate(UIApplication application)
        {
        }
    }
}
