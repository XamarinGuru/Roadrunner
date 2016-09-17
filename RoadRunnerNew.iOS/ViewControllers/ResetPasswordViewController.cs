using System;
using UIKit;
using RoadRunner.Shared.ViewModels;
using GalaSoft.MvvmLight.Helpers;
using System.Linq;
using RoadRunner.Shared;
using System.Globalization;
using RoadRunnerNew.iOS.Extension;

namespace RoadRunnerNew.iOS
{
	partial class ResetPasswordViewController : BaseTitleViewController
	{
		private ResetPasswordVM ViewModel { get; set; }

		public ResetPasswordViewController (IntPtr handle) : base (handle)
		{
			Title = "PASSWORD UPDATE";
			ViewModel = new ResetPasswordVM ();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();

			NavigationItem.Customize (NavigationController);

			btnSubmit.SetCustomButton ();
			btnCancelResetPassword.SetCustomButton ();

			edtOldPassword.ShouldReturn += TextFieldShouldReturn;
			edtNewPassword.ShouldReturn += TextFieldShouldReturn;
			edtConfNewPassword.ShouldReturn += TextFieldShouldReturn;

			var tap = new UITapGestureRecognizer(() => { View.EndEditing(true); });
			View.AddGestureRecognizer(tap);

			ViewModel.IsEmailLogin = false;

			LoginType currentLt;
			if (Enum.TryParse(AppSettings.LoginType.ToString(CultureInfo.InvariantCulture), out currentLt))
			{
				if (currentLt == LoginType.Email) {
					ViewModel.IsEmailLogin = true;
					ViewModel.CurrentEmail = AppSettings.UserEmail;
				}
			}

			this.SetBinding (
				() => edtOldPassword.Text,
				() => ViewModel.OldPassword,
				BindingMode.TwoWay)
				.UpdateSourceTrigger ("EditingChanged");

			this.SetBinding (
				() => edtNewPassword.Text,
				() => ViewModel.NewPassword,
				BindingMode.TwoWay)
				.UpdateSourceTrigger ("EditingChanged");

			this.SetBinding (
				() => edtConfNewPassword.Text,
				() => ViewModel.ConfirmNewPassword,
				BindingMode.TwoWay)
				.UpdateSourceTrigger ("EditingChanged");
			
			btnSubmit.SetCommand ("TouchUpInside", ViewModel.SubmitCommand);

			this.SetBinding (
				() => ViewModel.ValidaionError)
				.UpdateSourceTrigger ("ValidaionErrorChanges")
				.WhenSourceChanges (
					() => {
						if (ViewModel.ValidaionError != null && ViewModel.ValidaionError.Count > 0)
						{
							var delimeter = Environment.NewLine + "and" + Environment.NewLine;
							var message = String.Join(delimeter, ViewModel.ValidaionError.Select(r => r.ErrorMessage));
							InvokeOnMainThread (() => new UIAlertView (
								"Just a couple things left:", message, null, "Ok", null).Show ());
						}
					});

			this.SetBinding (
				() => ViewModel.AppExceptions)
				.UpdateSourceTrigger("AppExceptionsChanges")
				.WhenSourceChanges (
					() => {
						if (ViewModel.AppExceptions != null && ViewModel.AppExceptions.Count > 0) {
							var delimeter = Environment.NewLine + "and" + Environment.NewLine;
							var message = String.Join (delimeter, ViewModel.AppExceptions.Select (r => r.Message));
							InvokeOnMainThread(() => new UIAlertView(
								"Errors:", message, null, "Ok", null).Show());
						}
					});
			
            this.SetBinding(
                () => ViewModel.CanMoveForward)
                .UpdateSourceTrigger("CanMoveForwardChanges")
                .WhenSourceChanges(
                () =>
                {
                    if (ViewModel.CanMoveForward)
                    {
                        InvokeOnMainThread(() => new UIAlertView(
                            "Success", "Password has been changed", null, "Ok", null).Show());
                    }
                });
		}
	}
}
