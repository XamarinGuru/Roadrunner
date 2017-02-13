
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Text;

using RoadRunner.Shared;
using RoadRunner.Shared.Classes;
using RoadRunner.Shared.ViewModels;

using GalaSoft.MvvmLight.Helpers;

namespace RoadRunner.Android
{
	[Activity(Label = "SignUpActivity")]
	public class SignUpActivity : NavigationActivity
	{
		private RegisterVM ViewModel { get; set; }

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.SignUp);

			ViewModel = new RegisterVM();

			var edtFirstName = (TextView)FindViewById(Resource.Id.edtFirstName);
			var edtLastName = (TextView)FindViewById(Resource.Id.edtLastName);
			var edtEmailAddress = (TextView)FindViewById(Resource.Id.edtEmailAddress);
			var edtPassword = (TextView)FindViewById(Resource.Id.edtPassword);
			var edtRepeatPassword = (TextView)FindViewById(Resource.Id.edtRepeatPassword);
			var edtMobileNumber = (TextView)FindViewById(Resource.Id.edtMobileNumber);

			#if DEBUG
			edtFirstName.Text = "John";
			edtLastName.Text = "Dow";
			edtMobileNumber.Text = "8058081234";
			edtEmailAddress.Text = "johndow@mailinator.com";
			edtPassword.Text = "123456";
			edtRepeatPassword.Text = "123456";
			#endif

			if (AppSettings.LoginType > 0)
			{
				edtFirstName.Text = AppSettings.UserFirstName;
				edtLastName.Text = AppSettings.UserLastName;
				edtMobileNumber.Text = AppSettings.UserPhone;
				edtEmailAddress.Text = AppSettings.UserEmail;
				edtPassword.Text = "";
				edtRepeatPassword.Text = "";
			}
			ViewModel.FirstName = edtFirstName.Text;
			ViewModel.LastName = edtLastName.Text;
			ViewModel.MobileNumber = edtMobileNumber.Text;
			ViewModel.EmailAdress = edtEmailAddress.Text;
			ViewModel.Password = edtPassword.Text;
			ViewModel.RepeatPassword = edtRepeatPassword.Text;

			edtFirstName.TextChanged += OnFirstNameChanged;
			edtLastName.TextChanged += OnLastNameChanged;
			edtMobileNumber.TextChanged += OnMobileNumberChanged;
			edtEmailAddress.TextChanged += OnEmailChanged;
			edtPassword.TextChanged += OnPasswordChanged;
			edtRepeatPassword.TextChanged += OnRepeatPasswordChanged;

			//passengers or hours
			var switchAgree = (Switch)FindViewById(Resource.Id.switchAgree);
			switchAgree.CheckedChange += delegate (object sender, CompoundButton.CheckedChangeEventArgs e)
			{
				ViewModel.IsAgree = e.IsChecked;
			};

			var btnTerms = (TextView)FindViewById(Resource.Id.btnTerms);
			btnTerms.Touch += PopupTermsOfService;
				
			ViewModel.LoginType = AppSettings.LoginType.ToString();
			ViewModel.Token = AppSettings.UserToken;

			var btnConfirmRegistration = (Button)FindViewById(Resource.Id.btnConfirmRegistration);
			btnConfirmRegistration.SetCommand("Click", ViewModel.IncrementCommand);

			var btnBack = (ImageButton)FindViewById(Resource.Id.btn_back);
			btnBack.Click += delegate (object sender, EventArgs e)
			{
				OnBack();
			};

			this.SetBinding(
				() => ViewModel.ValidaionError)
				.UpdateSourceTrigger("ValidaionErrorChanges")
				.WhenSourceChanges(
				() =>
				{
					if (ViewModel.ValidaionError != null && ViewModel.ValidaionError.Count > 0)
					{
						var delimeter = System.Environment.NewLine + "and" + System.Environment.NewLine;
						var message = String.Join(delimeter, ViewModel.ValidaionError.Select(r => r.ErrorMessage));
						RunOnUiThread(() =>
						{
							ShowMessageBox("Just a couple things left:", message);
						});
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
						AppSettings.LoginType = (int)LoginType.Email;
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
							Task runSync = Task.Factory.StartNew(async () =>
							{
								result = await AppData.ApiCall(Constant.LOGINAPI, dic);
								var tt = (LoginResponse)AppData.ParseResponse(Constant.LOGINAPI, result);

								AppSettings.UserID = tt.Customerid;

							}).Unwrap();
							//runSync.Wait();
						}
						catch (Exception ex)
						{
						}
						finally
						{
							//HideLoadingView();
						}

						var addPaymentActivity = new Intent(this, typeof(AddPaymentActivity));
						addPaymentActivity.PutExtra("fromWhere", "signup");
						StartActivity(addPaymentActivity);
						OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
					}
				});

			this.SetBinding(
				() => ViewModel.AppExceptions)
				.UpdateSourceTrigger("AppExceptionsChanges")
				.WhenSourceChanges(
				() =>
				{
					if (ViewModel.AppExceptions != null && ViewModel.AppExceptions.Count > 0)
					{
						var delimeter = System.Environment.NewLine + "and" + System.Environment.NewLine;
						var message = String.Join(delimeter, ViewModel.AppExceptions.Select(r => r.Message));
						RunOnUiThread(() =>
						{
							ShowMessageBox("Error", message);
						});
					}
				});
		}
		private void OnFirstNameChanged(object sender, TextChangedEventArgs e)
		{
			ViewModel.FirstName = e.Text.ToString();
		}
		private void OnLastNameChanged(object sender, TextChangedEventArgs e)
		{
			ViewModel.LastName = e.Text.ToString();
		}
		private void OnMobileNumberChanged(object sender, TextChangedEventArgs e)
		{
			ViewModel.MobileNumber = e.Text.ToString();
		}
		private void OnEmailChanged(object sender, TextChangedEventArgs e)
		{
			ViewModel.EmailAdress = e.Text.ToString();
		}
		private void OnPasswordChanged(object sender, TextChangedEventArgs e)
		{
			ViewModel.Password = e.Text.ToString();
		}
		private void OnRepeatPasswordChanged(object sender, TextChangedEventArgs e)
		{
			ViewModel.RepeatPassword = e.Text.ToString();
		}

		private void PopupTermsOfService(object sender, View.TouchEventArgs e)
		{
			if (e.Event.Action == MotionEventActions.Down)
			{
				StartActivity(new Intent(this, typeof(TermsActivity)));
				OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
			}
		}
	}
}

