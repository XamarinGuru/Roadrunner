using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Globalization;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Auth0.SDK;

using Newtonsoft.Json.Linq;

using RoadRunner.Shared;
using RoadRunner.Shared.Classes;


namespace RoadRunner.Android
{
	[Activity (Label = "RoadRunner.Android", MainLauncher = true, Theme = "@android:style/Theme.NoTitleBar")]
	public class LoginActivity : BaseActivity
	{
		private Auth0.SDK.Auth0Client client = new Auth0.SDK.Auth0Client(
			"erlend.eu.auth0.com",
			"LnLnPaFL5cKqKJHEJaQeMwA7ouSsC52t");

		private MD5 md5Hash;
		
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Login);

			md5Hash = MD5.Create();

			Xamarin.Insights.Initialize ("37f8347b1aa3979ce023e220274aa739d6ea73ba", Application.Context);

			Xamarin.Insights.HasPendingCrashReport += (sender, isStartupCrash) => {
				if (isStartupCrash) {
					Xamarin.Insights.PurgePendingCrashReports ().Wait ();
				}
			};

			var username = FindViewById<EditText>(Resource.Id.login_username);
			var password = FindViewById<EditText>(Resource.Id.login_password);

			#if DEBUG
			username.Text = "test1@test.com";
			password.Text = "test1234";
			//username.Text = "newapiuser@mail.com";
			//password.Text = "newapi";
			#endif

			//signup
			var btnSignUp = FindViewById<ImageButton>(Resource.Id.btnSignUp);
			btnSignUp.Click += delegate
			{
				StartActivity(new Intent(this, typeof(SignUpActivity)));
				OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
			};

			if (!String.IsNullOrEmpty(AppSettings.UserLogin) && !String.IsNullOrEmpty(AppSettings.UserPassword))
			{
				username.Text = AppSettings.UserLogin;
				password.Text = AppSettings.UserPassword;
				loginProcess();
			}

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
				loginWithSocial("facebook");
			};

			//login with linkedin
			var btnLinkedin = FindViewById<ImageButton>(Resource.Id.btnLinkedin);
			btnLinkedin.Click += delegate
			{
				loginWithSocial("linkedin");
			};

			//login with google
			var btnGoogle = FindViewById<ImageButton>(Resource.Id.btnGoogle);
			btnGoogle.Click += delegate
			{
				loginWithSocial("google-oauth2");
			};
		}

		private async void loginWithSocial(string type)
		{
			ShowLoadingView("Getting some user data...");

			try
			{
				var user = await this.client.LoginAsync(this, type);
				this.ShowResult(user);
				HideLoadingView();
			}
			catch (AggregateException e)
			{
				HideLoadingView();
				ShowMessageBox("Oops!", e.Flatten().Message);
			}
			catch (Exception e)
			{
				HideLoadingView();
				ShowMessageBox("Oops!", e.Message);
			}
		}

		private void ShowResult(Auth0User user)
		{
			var userInfo = user.Profile;

			AppSettings.LoginType = (int)LoginType.Facebook;
			AppSettings.UserType = "";
			AppSettings.UserFirstName = userInfo["given_name"].ToString();
			AppSettings.UserLastName = userInfo["family_name"].ToString();
			AppSettings.UserEmail = userInfo["email"].ToString();

			AppSettings.UserPhoto = userInfo["picture"].ToString();

			AppSettings.UserToken = GetMd5Hash(md5Hash, userInfo["email"].ToString());

			////we got all the data we need at this point, FB login successful
			UserTrackingReporter.TrackUser(Constant.CATEGORY_LOGIN, "Facebook login completed");

			bool registerSuccessful = false;

			Task runSync = Task.Factory.StartNew(async () =>
			{
				registerSuccessful = await RegisterUser();
			}).Unwrap();
			runSync.Wait();

			if (!registerSuccessful)
			{
				GoToCreateAccountScreen();
				return;
			}

			ShowHomeScreen();
		}

		private async void loginProcess()
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

				//Task runSync = Task.Factory.StartNew(async () => { 
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

				//} );
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
		}

		private void ShowHomeScreen()
		{
			var mainActivity = new Intent(this, typeof(MainActivity));
			StartActivity(mainActivity);
		}

		private void GoToCreateAccountScreen()
		{
			StartActivity(new Intent(this, typeof(SignUpActivity)));
			OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
		}

		private async Task<bool> RegisterUser()
		{
			UserTrackingReporter.TrackUser(Constant.CATEGORY_LOGIN, "Checking if user is registered");

			var token = GetMd5Hash(md5Hash, AppSettings.UserEmail);

			var dic = new Dictionary<String, String>
			{
				{Constant.CHECKLOGINFORANDROID_USERNAME, AppSettings.UserEmail},
				{Constant.CHECKLOGINFORANDROID_PASSWORD, ""},
				{Constant.CHECKLOGINFORANDROID_TYPE, AppSettings.LoginType.ToString()},
				{Constant.CHECKLOGINFORANDROID_TOKEN, token}
			};

			string result = String.Empty;
			try
			{
				result = await AppData.ApiCall(Constant.CHECKLOGINFORANDROID, dic);
				var tt = (CheckLoginForAndroidResponse)AppData.ParseResponse(Constant.CHECKLOGINFORANDROID, result);

				if (tt.Result.ToLower().Contains("error") || tt.Result.ToLower().Contains("fail") || tt.Result.ToLower().Contains("failed"))
				{

					return false;

				}
				else {

					AppSettings.UserID = tt.Customerid;

					dic = new Dictionary<String, String>
					{
						{Constant.GET_MY_PROFILE_FOR_ANDROID_API_CUSTOMERID, AppSettings.UserID}
					};

					result = String.Empty;
					try
					{
						result = await AppData.ApiCall(Constant.GET_MY_PROFILE_FOR_ANDROID_API, dic);
						var tt1 = (GetMyProfileForAndroidResponse)AppData.ParseResponse(Constant.GET_MY_PROFILE_FOR_ANDROID_API, result);

						if (tt.Result.ToLower().Contains("error") || tt.Result.ToLower().Contains("fail") || tt.Result.ToLower().Contains("failed"))
						{
							ShowMessageBox("Login Failed", "The social network login failed for your account");
							return false;
						}
						AppSettings.UserEmail = tt1.Email;
						AppSettings.UserFirstName = tt1.FirstName;
						AppSettings.UserLastName = tt1.LastName;
						AppSettings.UserPhone = tt1.Phone;
						AppSettings.UserPassword = tt1.Password;
						AppSettings.IsSMS = bool.Parse(tt1.IsSMS);

						return true;

					}
					catch (Exception e)
					{
					}

					return true;
				}
			}
			catch (Exception e)
			{
			}
			return false;
		}

		private string GetMd5Hash(MD5 md5Hash, string input)
		{
			byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
			StringBuilder sBuilder = new StringBuilder();

			for (int i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}

			return sBuilder.ToString();
		}

		private bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
		{
			string hashOfInput = GetMd5Hash(md5Hash, input);
			StringComparer comparer = StringComparer.OrdinalIgnoreCase;

			if (0 == comparer.Compare(hashOfInput, hash))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}


