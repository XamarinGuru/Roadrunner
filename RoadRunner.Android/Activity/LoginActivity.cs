using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using RoadRunner.Shared;
using RoadRunner.Shared.Classes;


namespace RoadRunner.Android
{
	[Activity (Label = "RoadRunner.Android", MainLauncher = true, Theme = "@android:style/Theme.NoTitleBar")]
	public class LoginActivity : BaseActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Login);

			Xamarin.Insights.Initialize ("37f8347b1aa3979ce023e220274aa739d6ea73ba", Application.Context);

			Xamarin.Insights.HasPendingCrashReport += (sender, isStartupCrash) => {
				if (isStartupCrash) {
					Xamarin.Insights.PurgePendingCrashReports ().Wait ();
				}
			};

			#if DEBUG
			var username = FindViewById<EditText> (Resource.Id.login_username);
			var password = FindViewById<EditText> (Resource.Id.login_password);
			username.Text = "test1@test.com";
			password.Text = "test1234";
			#endif

			//signup
			var btnSignUp = FindViewById<ImageButton>(Resource.Id.btnSignUp);
			btnSignUp.Click += delegate
			{
				StartActivity(new Intent(this, typeof(SignUpActivity)));
				OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
			};

			//login with email, pw 
			var btnLogin = FindViewById<ImageButton> (Resource.Id.btnLogin);
			btnLogin.Click += delegate {
				loginProcess();
			};

			//forgot password
			var btnForgotPassword = FindViewById<Button>(Resource.Id.btnForgot);
			btnForgotPassword.Click += delegate
			{
				StartActivity(new Intent(this, typeof(ForgotPasswordActivity)));
				OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
			};

			//login with facebook
			var btnFacebook = FindViewById<ImageButton>(Resource.Id.btnFacebook);
			btnFacebook.Click += delegate
			{
				loginProcess();
			};

			//login with linkedin
			var btnLinkedin = FindViewById<ImageButton>(Resource.Id.btnLinkedin);
			btnLinkedin.Click += delegate
			{
				loginProcess();
			};

			//login with google
			var btnGoogle = FindViewById<ImageButton>(Resource.Id.btnGoogle);
			btnGoogle.Click += delegate
			{
				loginProcess();
			};
		}

		private void loginProcess()
		{
			var username = FindViewById<EditText> (Resource.Id.login_username);
			var password = FindViewById<EditText> (Resource.Id.login_password);
			if (string.IsNullOrEmpty(username.Text))
			{
				ShowMessageBox ("Request", "Please enter login.");
				return;
			}
			if (string.IsNullOrEmpty(password.Text))
			{
				ShowMessageBox ("Request", "Please enter password.");
				return;
			}

			AppSettings.UserLogin = username.Text;
			AppSettings.UserPassword = password.Text;

			var dic = new Dictionary<String, String>
			{
				{Constant.CHECKLOGINFORANDROID_USERNAME, username.Text},
				{Constant.CHECKLOGINFORANDROID_PASSWORD, password.Text},
				{Constant.CHECKLOGINFORANDROID_TYPE, "0"},
				{Constant.CHECKLOGINFORANDROID_TOKEN, ""}
			} ;

			string result = String.Empty;

			try
			{
				UserTrackingReporter.TrackUser(Constant.CATEGORY_LOGIN, "Logging in");

				ShowLoadingView("Signing in...");

				Task runSync = Task.Factory.StartNew(async () => { 
					result = await AppData.ApiCall(Constant.CHECKLOGINFORANDROID, dic);
					var tt = (CheckLoginForAndroidResponse) AppData.ParseResponse(Constant.CHECKLOGINFORANDROID, result);

					if (tt.Result.ToLower().Contains("error") || tt.Result.ToLower().Contains("failed"))
					{
						HideLoadingView();
						UserTrackingReporter.TrackUser(Constant.CATEGORY_LOGIN, "Login failed");
						ShowMessageBox ("Login Failed", "The username or password entered are not valid, please try again.");
						return;
					}

					AppSettings.LoginType = (int) LoginType.Email;
					AppSettings.UserToken = string.Empty;
					AppSettings.UserType = tt.CustomerType;
					AppSettings.UserID = tt.Customerid;
					AppSettings.UserEmail = tt.Email;
					AppSettings.UserFirstName = tt.FName;
					AppSettings.UserLastName = tt.LName;
					AppSettings.UserPhone = tt.PH;

					UserTrackingReporter.TrackUser(Constant.CATEGORY_LOGIN, "Email login successful");

					ShowHomeScreen();

				} ).Unwrap();
				//runSync.Wait();
			}
			catch (Exception ex)
			{
				AppSettings.UserLogin = "";
				AppSettings.UserPassword = "";

				HideLoadingView();
				ShowMessageBox ("Error", "An error occurred during login.\n" + ex.Message);
				CrashReporter.Report(ex);
				return;
			}
			finally{
				//HideLoadingView();
			}
		}

		private void ShowHomeScreen()
		{
			var mainActivity = new Intent(this, typeof(MainActivity));
			StartActivity(mainActivity);
		}
	}
}


