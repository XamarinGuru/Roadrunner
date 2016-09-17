using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;

using Task = System.Threading.Tasks.Task;
using UIKit;
using Foundation;
using RoadRunner.Shared;
using RoadRunner.Shared.Classes;

using CoreLocation;
using ObjCRuntime;

using Google.OpenSource;
using Google.Plus;
using Facebook.CoreKit;
using Facebook.LoginKit;
using LinkedIn.SignIn;

using Plugin.Messaging;

using Newtonsoft.Json.Linq;

namespace RoadRunnerNew.iOS
{
	public partial class LoginViewController : BaseViewController, ISignInDelegate
	{
		private UINavigationController thisController { get; set; }

		private readonly List<string> _facebookReadPermissions = new List<string> {"public_profile", "user_about_me", "email"} ;

		private SignIn signIn;

		private MD5 md5Hash;

		static bool UserInterfaceIdiomIsPhone
		{
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}
		public LoginViewController() : base()
		{
		}
		public LoginViewController(IntPtr handle) : base(handle)
		{
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}
		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			NavigationController.SetNavigationBarHidden (false, false);
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			NavigationController.SetNavigationBarHidden (true, false);
		}

		public override void ViewDidLoad()
		{
			UserTrackingReporter.TrackUser(Constant.CATEGORY_LOGIN, "ViewDidLoad");

			base.ViewDidLoad();
			thisController = NavigationController;

			md5Hash = MD5.Create ();

			#if DEBUG
			usernameTextField.Text = "test1@test.com";
			passwordTextField.Text = "test1234";
			#endif

			var tap = new UITapGestureRecognizer(() => { View.EndEditing(true); });
			View.AddGestureRecognizer(tap);

			CLLocationManager locationManager;
			locationManager = new CLLocationManager();

			if (locationManager.RespondsToSelector(new Selector("requestWhenInUseAuthorization")))
			{
				locationManager.RequestWhenInUseAuthorization();
			}

			locationManager.DistanceFilter = CLLocationDistance.FilterNone;
			locationManager.DesiredAccuracy = CLLocation.AccurracyBestForNavigation;
			locationManager.StartUpdatingLocation();

			usernameTextField.ShouldReturn += TextFieldShouldReturn;
			passwordTextField.ShouldReturn += TextFieldShouldReturn;

			btnLogin.TouchUpInside += BtnLogin_TouchUpInside;
			btnCreateAccount.TouchUpInside += BtnCreateAccount_TouchUpInside;
			btnCallRoadrunner.TouchUpInside += BtnCallRoadrunner_TouchUpInside;

			#region Facebook

			_facebookLoginButton.TouchUpInside += BtnLogin_Facebook;

			#endregion

			#region Linkedin

			_linkedinLoginButton.TouchUpInside += BtnLogin_Linkedin;

			#endregion

			#region Google+

			signIn = SignIn.SharedInstance;
			signIn.ClientId = RoadRunner.Shared.Constant.GOOGLECLIENTID;
			signIn.Scopes = new[] { PlusConstants.AuthScopePlusLogin, PlusConstants.AuthScopePlusMe };
			signIn.ShouldFetchGoogleUserEmail = true;
			signIn.ShouldFetchGoogleUserId = true;
			signIn.Delegate = this;

			_googleLoginButton.TouchUpInside += BtnLogin_Google;

			#endregion

			#region AutoLogin
			// If the user is already logged in GooglePlus
			if (signIn.ClientId != null && signIn.TrySilentAuthentication) {

			}

			// If the user is already logged in Facebook
			if (Facebook.CoreKit.AccessToken.CurrentAccessToken != null)
			{
				FacebookLoggedIn(Facebook.CoreKit.AccessToken.CurrentAccessToken.UserID);
			}

			// If the user is already logged in Linkedin
			if(SessionManager.SharedInstance.Session.AccessToken != null)
			{
				LinkedInLoggedIn();
			}

			if(!String.IsNullOrEmpty(AppSettings.UserLogin) && !String.IsNullOrEmpty(AppSettings.UserPassword)){
				usernameTextField.Text = AppSettings.UserLogin;
				passwordTextField.Text = AppSettings.UserPassword;
				BtnLogin_TouchUpInside(new object(), new EventArgs());
			}
			#endregion				
		}

		#region EmailLogin
		private void BtnLogin_TouchUpInside (object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(usernameTextField.Text))
			{
				ShowMessageBox ("Request", "Please enter login.", "Ok", null, null);
				usernameTextField.BecomeFirstResponder();
				return;
			}
			if (string.IsNullOrEmpty(passwordTextField.Text))
			{
				ShowMessageBox ("Request", "Please enter password.", "Ok", null, null);
				passwordTextField.BecomeFirstResponder();
				return;
			}

			UserTrackingReporter.TrackUser(Constant.CATEGORY_LOGIN, "Login button clicked");

			ShowLoadingView("Signing in...");

			usernameTextField.ResignFirstResponder();
			passwordTextField.ResignFirstResponder();

			AppSettings.UserLogin = usernameTextField.Text;
			AppSettings.UserPassword = passwordTextField.Text;

			var dic = new Dictionary<String, String>
			{
				{Constant.CHECKLOGINFORANDROID_USERNAME, usernameTextField.Text},
				{Constant.CHECKLOGINFORANDROID_PASSWORD, passwordTextField.Text},
				{Constant.CHECKLOGINFORANDROID_TYPE, "0"},
				{Constant.CHECKLOGINFORANDROID_TOKEN, ""}
			} ;

			string result = String.Empty;

			try
			{
				UserTrackingReporter.TrackUser(Constant.CATEGORY_LOGIN, "Logging in");
				Task runSync = Task.Factory.StartNew(async () => { 
					result = await AppData.ApiCall(Constant.CHECKLOGINFORANDROID, dic);
					var tt = (CheckLoginForAndroidResponse) AppData.ParseResponse(Constant.CHECKLOGINFORANDROID, result);

					InvokeOnMainThread(delegate {					

						if (tt.Result.ToLower().Contains("error") || tt.Result.ToLower().Contains("failed"))
						{
							HideLoadingView();
							UserTrackingReporter.TrackUser(Constant.CATEGORY_LOGIN, "Login failed");
							ShowMessageBox ("Login Failed", "The username or password entered are not valid, please try again.");
							passwordTextField.BecomeFirstResponder();
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
					} );

				} ).Unwrap();
				//runSync.Wait();
			}
			catch (Exception ex)
			{
				AppSettings.UserLogin = "";
				AppSettings.UserPassword = "";

				HideLoadingView();
				ShowMessageBox ("Error", "An error occurred during login.\n" + ex.Message, "Ok", null, null);
				CrashReporter.Report(ex);
				return;
			}
			finally{
				//HideLoadingView();
			}
		}

		#endregion

		#region EmailCreateAccount
		private void BtnCreateAccount_TouchUpInside (object sender, EventArgs e)
		{
			AppSettings.LoginType = (int) LoginType.Email;
			AppSettings.UserToken = string.Empty;

			UIStoryboard sb = UIStoryboard.FromName ("MainStoryboard", null);
			UIViewController pvc = sb.InstantiateViewController ("RegisterViewController");
			thisController.PushViewController (pvc, true);
		}
		#endregion

		#region FacebookLogin
		private void BtnLogin_Facebook (object sender, EventArgs e)
		{
			var fbLoginManager = new LoginManager ();
			fbLoginManager.LoginBehavior = LoginBehavior.Native;

			fbLoginManager.LogInWithReadPermissions (_facebookReadPermissions.ToArray (), FacebookLoginRequestHandler);
		}

		private void FacebookLoginRequestHandler(LoginManagerLoginResult result, NSError error)
		{
			if (error != null)
			{
				//ShowMessageBox("Ups", error.Description, "Ok", null, null);
				ShowMessageBox("Login Failed", "The social network login failed for your account", "Ok", null, null);
				return;
			}
			if (result.IsCancelled)
			{
				return;
			}
			var userid = result.Token.UserID;
			FacebookLoggedIn(result.Token.UserID);
		}

		private void FacebookLoggedIn(string userId)
		{
			ShowLoadingView("Getting some user data...");

			var fields = "?fields=id,name,email,first_name,last_name,picture";
			var request = new GraphRequest ("/" + userId + fields, null, Facebook.CoreKit.AccessToken.CurrentAccessToken.TokenString, null, "GET");
			var requestConnection = new GraphRequestConnection ();
			requestConnection.AddRequest (request, (connection, result, error) => {

				if (error != null)
				{
					HideLoadingView();
					//new UIAlertView("Error...", error.Description, null, "Ok", null).Show();
					new UIAlertView("Login Failed", "The social network login failed for your account", null, "Ok", null).Show();
					return;
				}

				var userInfo = (NSDictionary) result;

				AppSettings.LoginType = (int) LoginType.Facebook;
				AppSettings.UserType = "";
				AppSettings.UserFirstName = userInfo["first_name"].ToString();
				AppSettings.UserLastName = userInfo["last_name"].ToString();
				AppSettings.UserEmail = userInfo["email"].ToString();

				var tmp1 = (NSDictionary)userInfo["picture"];
				var tmp2 = (NSDictionary)tmp1["data"];
				AppSettings.UserPhoto = tmp2["url"].ToString();

				AppSettings.UserToken = GetMd5Hash (md5Hash, userInfo["email"].ToString());

				////we got all the data we need at this point, FB login successful
				UserTrackingReporter.TrackUser(Constant.CATEGORY_LOGIN, "Facebook login completed");

				bool registerSuccessful = false;

				Task runSync = Task.Factory.StartNew(async () => { 
					registerSuccessful = await RegisterUser();
				} ).Unwrap();
				runSync.Wait();

				if (!registerSuccessful) {
					GoToCreateAccountScreen();
					return;
				}

				ShowHomeScreen();
			} );
			requestConnection.Start ();
		}

		#endregion

		#region LinkedinLogin
		private void BtnLogin_Linkedin (object sender, EventArgs e)
		{
			SessionManager.CreateSession (
				new []{ Permission.BasicProfile, Permission.EmailAddress, Permission.Share },
				"some state",
				true,
				returnState => {
					LinkedInLoggedIn();
				} ,
				error => {
					InvokeOnMainThread(delegate {
						//var okAlertController = UIAlertController.Create("error", error.Description, UIAlertControllerStyle.Alert);
						var okAlertController = UIAlertController.Create("Login Failed", "The social network login failed for your account", UIAlertControllerStyle.Alert);
						okAlertController.AddAction(UIAlertAction.Create("ok", UIAlertActionStyle.Default, null));
						PresentViewController(okAlertController, true, null);
					} );
				}
			);
		}

		private void LinkedInLoggedIn()
		{
			ApiHelper.SharedInstance.ApiRequest ("https://api.linkedin.com/v1/people/~:(first-name,last-name,picture-url,email-address)?format=json", "GET", NSData.FromString (string.Empty, NSStringEncoding.UTF8),
				response => {

					var result = JObject.Parse(response.Data);

					InvokeOnMainThread(delegate {					

						AppSettings.LoginType = (int) LoginType.LinkedIn;
						AppSettings.UserType = "";
						AppSettings.UserFirstName = result["firstName"].ToString();
						AppSettings.UserLastName = result["lastName"].ToString();
						AppSettings.UserEmail = result["emailAddress"].ToString();
						AppSettings.UserPhoto = result["pictureUrl"].ToString();

						AppSettings.UserToken = GetMd5Hash (md5Hash, result["emailAddress"].ToString());

						UserTrackingReporter.TrackUser(Constant.CATEGORY_LOGIN, "Linkedin login completed");

						bool registerSuccessful = false;

						Task runSync = Task.Factory.StartNew(async () => { 
							registerSuccessful = await RegisterUser();
						} ).Unwrap();
						runSync.Wait();

						if (!registerSuccessful) {
							GoToCreateAccountScreen();
							return;
						}

						ShowHomeScreen();
					} );
				} ,
				apiError => {
					Console.WriteLine ("Error called: " + apiError.Description);
				} );
		}

		#endregion

		#region GoogleLogin
		private void BtnLogin_Google (object sender, EventArgs e)
		{
			signIn.Authenticate ();
		}

		public void Finished(OAuth2Authentication auth, NSError error)
		{
			if (error != null)
			{
				//InvokeOnMainThread(() => new UIAlertView("Error", "Could not sign in.\nError: " + error.LocalizedDescription, null, "Ok", null).Show());
				InvokeOnMainThread(() => new UIAlertView("Login Failed", "The social network login failed for your account", null, "Ok", null).Show());
				HideLoadingView();
				CrashReporter.Report(new Exception(error.LocalizedDescription));
				return;
			}

			ShowLoadingView("Getting some user data...");

			UserTrackingReporter.TrackUser(Constant.CATEGORY_LOGIN, "Google login successful");           

			AppSettings.LoginType = (int)LoginType.Google;
			AppSettings.UserToken = GetMd5Hash (md5Hash, SignIn.SharedInstance.UserEmail);
			AppSettings.UserEmail = SignIn.SharedInstance.UserEmail;
			AppSettings.UserType = "";

			QueryPlus query = QueryPlus.QueryForPeopleGetWithUserId(SignIn.SharedInstance.UserId);
			SignIn.SharedInstance.PlusService.ExecuteQuery(query, GooglePlusPersonQueryCompleted);
		}

		private async void GooglePlusPersonQueryCompleted(ServiceTicket ticket, NSObject obj, NSError error)
		{
			if (error != null)
			{
				HideLoadingView();
				//InvokeOnMainThread(() => new UIAlertView("Error", "Could not get user information.\nError: " + error.LocalizedDescription, null, "Ok", null).Show());
				InvokeOnMainThread(() => new UIAlertView("Login Failed", "The social network login failed for your account", null, "Ok", null).Show());
				CrashReporter.Report(new Exception(error.LocalizedDescription));
				return;
			}

			var person = (PlusPerson)obj;
			AppSettings.UserEmail = SignIn.SharedInstance.Authentication.UserEmail;
			AppSettings.UserFirstName = person.Name.GivenName;
			AppSettings.UserLastName = person.Name.FamilyName;
			AppSettings.UserPhoto = person.Image.Url;

			bool registerSuccessful = await RegisterUser();

			if (!registerSuccessful) {
				GoToCreateAccountScreen ();
				return;
			}

			UserTrackingReporter.TrackUser(Constant.CATEGORY_LOGIN, "Google login completed");

			ShowHomeScreen();
		}

		#endregion

		private void ShowHomeScreen()
		{
			HideLoadingView();
			var dvc = (HomeViewController)Storyboard.InstantiateViewController("HomeViewController");
			PresentViewController(dvc, true, null);
		}

		private void GoToCreateAccountScreen()
		{
			HideLoadingView();
			LoginType currentLt;
			if (Enum.TryParse(AppSettings.LoginType.ToString(CultureInfo.InvariantCulture), out currentLt))
			{
				switch (currentLt)
				{
				case LoginType.Facebook:
					new LoginManager().LogOut();
					break;
				case LoginType.Google:
					SignIn.SharedInstance.SignOut();
					break;
				case LoginType.LinkedIn:
					SessionManager.ClearSession ();
					break;
				}
			}
			UIStoryboard sb = UIStoryboard.FromName ("MainStoryboard", null);
			UIViewController pvc = sb.InstantiateViewController ("RegisterViewController");
			thisController.PushViewController (pvc, true);
		}

		private async Task<bool> RegisterUser()
		{
			UserTrackingReporter.TrackUser (Constant.CATEGORY_LOGIN, "Checking if user is registered");

			var token = GetMd5Hash (md5Hash, AppSettings.UserEmail);

			var dic = new Dictionary<String, String>
			{
				{Constant.CHECKLOGINFORANDROID_USERNAME, AppSettings.UserEmail},
				{Constant.CHECKLOGINFORANDROID_PASSWORD, ""},
				{Constant.CHECKLOGINFORANDROID_TYPE, AppSettings.LoginType.ToString()},
				{Constant.CHECKLOGINFORANDROID_TOKEN, token}
			} ;

			string result = String.Empty;
			try {
				result = await AppData.ApiCall (Constant.CHECKLOGINFORANDROID, dic);
				var tt = (CheckLoginForAndroidResponse)AppData.ParseResponse (Constant.CHECKLOGINFORANDROID, result);

				if (tt.Result.ToLower ().Contains ("error") || tt.Result.ToLower ().Contains ("fail") || tt.Result.ToLower().Contains("failed")) {
					
					return false;

				}  else {
					
					AppSettings.UserID = tt.Customerid;

					dic = new Dictionary<String, String>
					{
						{Constant.GET_MY_PROFILE_FOR_ANDROID_API_CUSTOMERID, AppSettings.UserID}
					} ;

					result = String.Empty;
					try {
						result = await AppData.ApiCall (Constant.GET_MY_PROFILE_FOR_ANDROID_API, dic);
						var tt1 = (GetMyProfileForAndroidResponse)AppData.ParseResponse (Constant.GET_MY_PROFILE_FOR_ANDROID_API, result);

						if (tt.Result.ToLower ().Contains ("error") || tt.Result.ToLower ().Contains ("fail") || tt.Result.ToLower().Contains("failed")) {

							//new UIAlertView("Error...", "Login Failed", null, "Ok", null).Show();
							new UIAlertView("Login Failed", "The social network login failed for your account", null, "Ok", null).Show();
							return false;
						}
						AppSettings.UserEmail = tt1.Email;
						AppSettings.UserFirstName = tt1.FirstName;
						AppSettings.UserLastName = tt1.LastName;
						AppSettings.UserPhone = tt1.Phone;
						AppSettings.UserPassword = tt1.Password;
						AppSettings.IsSMS = bool.Parse(tt1.IsSMS);

						return true;

					}  catch (Exception e) {
					}

					return true;
				}
			}  catch (Exception e) {
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

		void BtnCallRoadrunner_TouchUpInside (object sender, EventArgs e)
		{
			var phoneCallTask = MessagingPlugin.PhoneDialer;
			if(phoneCallTask.CanMakePhoneCall)
			{
				phoneCallTask.MakePhoneCall("8002477919", "Roadrunner Support");
			}else{
				var okAlertController = UIAlertController.Create("error", "you can't make phone call on emulator", UIAlertControllerStyle.Alert);
				okAlertController.AddAction(UIAlertAction.Create("ok", UIAlertActionStyle.Default, null));
				PresentViewController(okAlertController, true, null);
			}
		}
	}
}
