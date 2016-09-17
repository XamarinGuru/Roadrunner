using Foundation;
using System;
using UIKit;
using RoadRunner.Shared;
using RoadRunner.Shared.Classes;
using System.Threading.Tasks;
using System.Collections.Generic;
using RoadRunnerNew.iOS.Extension;

namespace RoadRunnerNew.iOS
{
	partial class ProfileViewController : BaseTitleViewController
	{
		public ProfileViewController (IntPtr handle) : base (handle, "MY PROFILE")
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var tap = new UITapGestureRecognizer(() => { View.EndEditing(true); });
			View.AddGestureRecognizer(tap);

			edtFirstName.Text = AppSettings.UserFirstName;
			edtFirstName.ShouldReturn += TextFieldShouldReturn;

			edtLastName.Text = AppSettings.UserLastName;
			edtLastName.ShouldReturn += TextFieldShouldReturn;

			edtMobileNumber.Text = AppSettings.UserPhone;
			edtMobileNumber.ShouldReturn += TextFieldShouldReturn;

			edtEmailAddress.Text = AppSettings.UserEmail;
			edtEmailAddress.ShouldReturn += TextFieldShouldReturn;

			btnChangePassword.SetCustomButton ();
			btnConfirmRegistration.SetCustomButton ();

			btnConfirmRegistration.TouchUpInside += UpdateProfile;
		}

		private void UpdateProfile (object sender, EventArgs e)
		{
			try {
				var result = string.Empty;
				var dic = new Dictionary<String, String>();

				dic = new Dictionary<String, String> {
					{ Constant.UPDATE_CONTACT_FOR_ANDROID_API_CUSTOMERID, AppSettings.UserID },
					{ Constant.UPDATE_CONTACT_FOR_ANDROID_API_FIRSTNAME, edtFirstName.Text },
					{ Constant.UPDATE_CONTACT_FOR_ANDROID_API_LASTNAME, edtLastName.Text },
					{ Constant.UPDATE_CONTACT_FOR_ANDROID_API_CONTACT, edtMobileNumber.Text },
					{ Constant.UPDATE_CONTACT_FOR_ANDROID_API_ISSMS, isSMS.On.ToString() },
				};

				Task runSync = Task.Factory.StartNew (async () => {
					result = await AppData.ApiCall (Constant.UPDATE_CONTACT_FOR_ANDROID_API, dic); 
				}).Unwrap ();
				runSync.Wait ();

				UserTrackingReporter.TrackUser(Constant.CATEGORY_UPDATE_PROFILE, "Updating user profile");
				var tt = (UpdateContactResponseForAndroid)AppData.ParseResponse (Constant.UPDATE_CONTACT_FOR_ANDROID_API, result);

				if (tt.Result.ToLower().Contains("error"))
				{
					HideLoadingView();
					UserTrackingReporter.TrackUser(Constant.CATEGORY_UPDATE_PROFILE, "Updating failed");
					ShowMessageBox (tt.Result, tt.Msg);
					return;
				}

				UserTrackingReporter.TrackUser(Constant.CATEGORY_UPDATE_PROFILE, "Updating successed");
				ShowMessageBox ("Success", "User profile updated successfully!");

			} catch (Exception ex) {
				HideLoadingView ();
				ShowMessageBox ("Error", "An error occurred getting fares. \n\nError: " + ex.Message);
				CrashReporter.Report (ex);
			}

		}
		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);

			var callHistoryContoller = segue.DestinationViewController as TermsOFServiceViewController;

			if (callHistoryContoller != null) {
				callHistoryContoller.isFromMenu = false;
			}
		}
	}
}
