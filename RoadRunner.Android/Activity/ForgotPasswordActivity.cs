
using System;
using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Widget;

using RoadRunner.Shared;
using RoadRunner.Shared.Classes;

namespace RoadRunner.Android
{
	[Activity(Label = "ForgotPasswordActivity")]
	public class ForgotPasswordActivity : NavigationActivity
	{
		private static bool IsFirstTime = true;

		EditText mTxtEmail;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.ForgotPassword);

			//pickup location
			mTxtEmail = (EditText)FindViewById(Resource.Id.txtEmail);

			#if DEBUG
			mTxtEmail.Text = "test1@test.com";
			#endif

			var btnReset = (Button)FindViewById(Resource.Id.btnReset);
			btnReset.Click += delegate (object sender, EventArgs e)
			{
				ResetPassword();
			};

			var btnBack = (ImageButton)FindViewById(Resource.Id.btn_back);
			btnBack.Click += delegate (object sender, EventArgs e)
			{
				OnBack();
			};
		}

		private async void ResetPassword()
		{
			UserTrackingReporter.TrackUser(Constant.CATEGORY_FORGOT_PASSWORD, "Restore Password clicked");

			var txt = mTxtEmail.Text;

			if (!AppData.IsValidEmail(txt))
			{
				ShowMessageBox("Error", "Please enter valid email address.");
			}
			else
			{
				UserTrackingReporter.TrackUser(Constant.CATEGORY_FORGOT_PASSWORD, "Resetting password");
				ShowLoadingView("Password Resetting...");

				Dictionary<string, string> dic = new Dictionary<string, string>()
					{
						{Constant.FORGOTPASSWORDAPIUSEREMAIL, txt}
					};

				ForgotPasswordResponse fpr;
				try
				{
					var result = await AppData.ApiCall(Constant.FORGOTPASSWORDAPI, dic);
					fpr = (ForgotPasswordResponse)AppData.ParseResponse(Constant.FORGOTPASSWORDAPI, result);
					HideLoadingView();
				}
				catch (Exception ex)
				{
					HideLoadingView();
					var msg = String.Format("An error occurred requesting password reset.{0}{0}Error: {1}", System.Environment.NewLine, ex.Message);
					ShowMessageBox("Error", msg);
					CrashReporter.Report(ex);
					return;
				}

				if (fpr != null && fpr.Result.ToLower().Contains("success"))
				{
					//ShowMessageBox("Success", "E-mail to reset password has been sent");
					OnBack();
				}
				else
				{
					ShowMessageBox("Error", "Something goes wrong, please contact our support");
				}
			}
		}
	}
}

