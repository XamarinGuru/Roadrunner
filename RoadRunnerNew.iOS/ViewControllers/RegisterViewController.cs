using System;
using GalaSoft.MvvmLight.Helpers;
using UIKit;
using System.Linq;
using RoadRunner.Shared;
using RoadRunner.Shared.ViewModels;
using RoadRunner.Shared.Classes;
using RoadRunnerNew.iOS.Extension;
using Foundation;

using System.Collections.Generic;

using Task = System.Threading.Tasks.Task;
using System.Threading.Tasks;

namespace RoadRunnerNew.iOS
{
    partial class RegisterViewController : BaseViewController
    {
        public RegisterViewController(IntPtr handle) : base(handle)
        {
            ViewModel = new RegisterVM();
        }
        private RegisterVM ViewModel { get; set; }

		private UINavigationController thisController { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			thisController = NavigationController;

			Title = "SIGN UP";

			NavigationItem.Customize (NavigationController);

			btnConfirmRegistration.SetCustomButton ();

			edtFirstName.ShouldReturn += TextFieldShouldReturn;
			edtLastName.ShouldReturn += TextFieldShouldReturn;
			edtMobileNumber.ShouldReturn += TextFieldShouldReturn;
			edtEmailAddress.ShouldReturn += TextFieldShouldReturn;
			edtPassword.ShouldReturn += TextFieldShouldReturn;
			edtRepeatPassword.ShouldReturn += TextFieldShouldReturn;

			var tap = new UITapGestureRecognizer(() => { View.EndEditing(true); });
			View.AddGestureRecognizer(tap);


			#if DEBUG
			edtFirstName.Text = "John";
			edtLastName.Text = "Dow";
			edtMobileNumber.Text = "8058081234";
			edtEmailAddress.Text = "johndow@mailinator.com";
			edtPassword.Text = "123456";
			edtRepeatPassword.Text = "123456";
			#endif

			if (AppSettings.LoginType > 0) {
				edtFirstName.Text = AppSettings.UserFirstName;
				edtLastName.Text = AppSettings.UserLastName;
				edtMobileNumber.Text = AppSettings.UserPhone;
				edtEmailAddress.Text = AppSettings.UserEmail;
				edtPassword.Text = "";
				edtRepeatPassword.Text = "";
			}

			this.SetBinding(
				() => edtFirstName.Text,
				()=> ViewModel.FirstName,
				BindingMode.TwoWay)
				.UpdateSourceTrigger("EditingChanged");

            this.SetBinding(
                () => edtLastName.Text,
				()=> ViewModel.LastName,
				BindingMode.TwoWay)
				.UpdateSourceTrigger("EditingChanged");
			
			this.SetBinding (
				() => edtMobileNumber.Text,
				() => ViewModel.MobileNumber,
				BindingMode.TwoWay)
				.UpdateSourceTrigger ("EditingChanged");

			this.SetBinding (
				() => edtEmailAddress.Text,
				() => ViewModel.EmailAdress,
				BindingMode.TwoWay)
				.UpdateSourceTrigger ("EditingChanged");

			this.SetBinding (
				() => edtPassword.Text,
				() => ViewModel.Password,
				BindingMode.TwoWay)
				.UpdateSourceTrigger ("EditingChanged");

			this.SetBinding (
				() => edtRepeatPassword.Text,
				() => ViewModel.RepeatPassword,
				BindingMode.TwoWay)
				.UpdateSourceTrigger ("EditingChanged");
			
			this.SetBinding (
				() => chkIAgree.On,
				() => ViewModel.IsAgree,
				BindingMode.TwoWay)
				.UpdateSourceTrigger ("ValueChanged");

			ViewModel.LoginType = AppSettings.LoginType.ToString();
			ViewModel.Token = AppSettings.UserToken;
			
			btnConfirmRegistration.SetCommand ("TouchUpInside", ViewModel.IncrementCommand);

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
				() => ViewModel.CanMoveForward)
                .UpdateSourceTrigger ("CanMoveForwardChanges")
                .WhenSourceChanges (
				() => {
					if (ViewModel.CanMoveForward) {	
						AppSettings.LoginType = (int) LoginType.Email;
						AppSettings.UserToken = string.Empty;
						AppSettings.UserEmail = edtEmailAddress.Text;
						AppSettings.UserPassword = edtPassword.Text;
						AppSettings.UserFirstName = edtFirstName.Text;
						AppSettings.UserLastName = edtLastName.Text;
						AppSettings.UserPhone = edtMobileNumber.Text;
						AppSettings.UserPhoto = string.Empty;

						var dic = new Dictionary<String, String>
						{
							{Constant.LOGINAPI_USERNAME, edtEmailAddress.Text},
							{Constant.LOGINAPI_PASSWORD, edtPassword.Text}
						};

						string result = String.Empty;

						try
						{
							Task runSync = Task.Factory.StartNew(async () => { 
								result = await AppData.ApiCall(Constant.LOGINAPI, dic);
								var tt = (LoginResponse) AppData.ParseResponse(Constant.LOGINAPI, result);

								AppSettings.UserID = tt.Customerid;

							}).Unwrap();
							//runSync.Wait();
						}
						catch (Exception ex)
						{
						}
						finally{
							//HideLoadingView();
						}

						UIStoryboard sb = UIStoryboard.FromName ("MainStoryboard", null);
						AddCreditCardViewController pvc = (AddCreditCardViewController)sb.InstantiateViewController ("AddCreditCardViewController");
						pvc.fromWhere = "signup";
						thisController.PushViewController (pvc, true);
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
