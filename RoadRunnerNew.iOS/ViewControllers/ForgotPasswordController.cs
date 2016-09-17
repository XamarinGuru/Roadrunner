using System;
using RoadRunner.Shared;
using System.Collections.Generic;
using RoadRunner.Shared.Classes;
using RoadRunnerNew.iOS.Extension;
using UIKit;

namespace RoadRunnerNew.iOS
{
	partial class ForgotPasswordController : BaseViewController
	{
		public ForgotPasswordController (IntPtr handle) : base (handle)
		{
			Title = "FORGOT PASSWORD";
		}

	    public override void ViewDidLoad()
	    {
			UserTrackingReporter.TrackUser(Constant.CATEGORY_FORGOT_PASSWORD, "ViewDidLoad");

	        base.ViewDidLoad();	        

			NavigationItem.Customize (NavigationController);

			btnRestorePassword.SetCustomButton ();

			edtEmailAdress.ShouldReturn += TextFieldShouldReturn;

			var tap = new UITapGestureRecognizer(() => { View.EndEditing(true); });
			View.AddGestureRecognizer(tap);

			#if DEBUG
	        edtEmailAdress.Text = "test1@test.com";
            #endif

	        btnRestorePassword.TouchUpInside += async delegate(object sender, EventArgs e)
	        {
	            UserTrackingReporter.TrackUser(Constant.CATEGORY_FORGOT_PASSWORD, "Restore Password clicked");

	            var txt = edtEmailAdress.Text;

				if (!AppData.IsValidEmail(txt))
	            {
					ShowMessageBox("Error", "Please enter valid email address.", "Ok", null, null);
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
	                    fpr = (ForgotPasswordResponse) AppData.ParseResponse(Constant.FORGOTPASSWORDAPI, result);
	                    HideLoadingView();
	                }
	                catch (Exception ex)
	                {
	                    HideLoadingView();
	                    var msg = String.Format("An error occurred requesting password reset.{0}{0}Error: {1}", Environment.NewLine, ex.Message);
	                    ShowMessageBox("Error", msg, "Ok", null, null);
	                    CrashReporter.Report(ex);
	                    return;
	                }

                    if (fpr!=null && fpr.Result.ToLower().Contains("success"))
	                {
	                    ShowMessageBox("Success", "E-mail to reset password has been sent", "Ok", null, null);
	                }
	                else
	                {
	                    ShowMessageBox("Error", "Something goes wrong, please contact our support", "Ok", null, null);
	                }

	                var justVC = Storyboard.InstantiateViewController("LoginViewController");
					this.NavigationController.PushViewController(justVC, true);
	            }

	        };

	    }
	}
}
