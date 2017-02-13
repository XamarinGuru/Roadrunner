
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Text;

using GalaSoft.MvvmLight.Helpers;

using RoadRunner.Shared;
using RoadRunner.Shared.Classes;

namespace RoadRunner.Android
{
	public class HomeFragement : Fragment
	{
		Spinner spSelectPayment;
		EditText mPromoCode;
		MainActivity mActivity;
		List<KeyValuePair<object, string>> m_listCreditCards;

		private static bool IsFirstTime = true;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{

			var view = inflater.Inflate(Resource.Layout.fragment_home, container, false);

			mActivity = this.Activity as MainActivity;

			var txtSearchLocation = (EditText)view.FindViewById (Resource.Id.txtSearchLocation);
			txtSearchLocation.Touch += PopupSearchLocation;
			txtSearchLocation.SetBinding (
				() => Facade.Instance.CurrentRide.PickUpLocation,
				() => txtSearchLocation.Text,
				BindingMode.TwoWay)
				.UpdateSourceTrigger ("PickUpLocationChanges");

			var btnAddPayment = (Button)view.FindViewById(Resource.Id.btnAddPayment);
			btnAddPayment.Click += delegate (object sender, EventArgs e)
			{
				StartActivity(new Intent(mActivity, typeof(AddPaymentActivity)));
				this.Activity.OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
			};

			spSelectPayment = (Spinner)view.FindViewById(Resource.Id.spSelectPayment);

			mPromoCode = (EditText)view.FindViewById(Resource.Id.txtPromoCode);
			mPromoCode.Text = Facade.Instance.CurrentRide.PromoCode;
			mPromoCode.TextChanged += OnPromoCodeChanged;

			var btnVerifyPromo = (Button)view.FindViewById(Resource.Id.btnVerifyPromo);
			btnVerifyPromo.Click += delegate (object sender, EventArgs e)
			{
				VerifyPromoCode();
			};

			var btnBookARide = (Button)view.FindViewById(Resource.Id.btnBookARide);
			btnBookARide.SetCommand("Click", Facade.Instance.CurrentRide.GoToTheRideInformation);

			if (IsFirstTime)
			{
				IsFirstTime = false;
				SetBindingOnce();
			}

			return view;
		}

		public override void OnResume()
		{
			base.OnResume();
			LoadCreditCards();
		}

		private void SetBindingOnce()
		{
			this.SetBinding(
				() => Facade.Instance.CurrentRide.CanGoToTheRideInformation)
				.UpdateSourceTrigger("CanGoToTheRideInformationChanges")
				.WhenSourceChanges(
					async () =>
					{
						if (Facade.Instance.CurrentRide.CanGoToTheRideInformation)
						{
							StartActivity(new Intent(this.Activity, typeof(RideInformationActivity)));
							this.Activity.OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
						}
						else {
							if (Facade.Instance.CurrentRide.ValidaionError != null && Facade.Instance.CurrentRide.ValidaionError.Count > 0)
							{
								string header = Facade.Instance.CurrentRide.ValidaionError.Count > 1
									? "Just a couple things left:"
									: "Please correct the last thing:";

								var delimeter = System.Environment.NewLine + System.Environment.NewLine;
								var message = String.Join(delimeter, Facade.Instance.CurrentRide.ValidaionError.Select(r => r.ErrorMessage));
								mActivity.RunOnUiThread(() =>
								{
									mActivity.ShowMessageBox(header, message);
								});
							}
						}
					});
		}

		private void LoadCreditCards()
		{
			mActivity.ShowLoadingView("Loading data...");

			var dic = new Dictionary<String, String>
			{
				{Constant.GETCREDITCARDDETAILSNEWFORPHONE_CUSTOMERID, AppSettings.UserID},
				{Constant.GETCREDITCARDDETAILSNEWFORPHONE_LOGINTYPE, "-1"},
				{Constant.GETCREDITCARDDETAILSNEWFORPHONE_TOKENID, string.Empty } // do not use the actual token
			};

			String result = String.Empty;

			Task runSync = Task.Factory.StartNew(async () =>
			{
				result = await AppData.ApiCall(Constant.GETCREDITCARDDETAILSNEWFORPHONE, dic);

				var tt = (GetCreditCardDetailsNewForPhoneResponse)AppData.ParseResponse(Constant.GETCREDITCARDDETAILSNEWFORPHONE, result);
				m_listCreditCards = new List<KeyValuePair<object, string>>();
				var listCreditCardImages = new List<KeyValuePair<object, object>>();

				mActivity.RunOnUiThread(() =>
				{
					var adapter = new ArrayAdapter(mActivity, Resource.Layout.item_spinner);
					spSelectPayment.Adapter = adapter;
					foreach (var card in tt.CardList)
					{
						m_listCreditCards.Add(new KeyValuePair<object, string>(card.Id, card.CardNumber));
						adapter.Add(card.CardNumber);
					}
					spSelectPayment.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(OnPaymentChanged);
				});

				mActivity.HideLoadingView();
			});
		}

		private void OnPaymentChanged(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Facade.Instance.CurrentRide.CreditCardId = m_listCreditCards[e.Position].Key.ToString();
			AppSettings.SelectedCard = m_listCreditCards[e.Position].Key.ToString();
		}

		private void PopupSearchLocation(object sender, View.TouchEventArgs e)
		{
			if (e.Event.Action == MotionEventActions.Down) {
				var nextActivity = new Intent (this.Activity, typeof(PickupLocationActivity));
				nextActivity.PutExtra ("IsPickupLocation", "true");
				StartActivity(nextActivity);
				this.Activity.OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
			}
		}

		private void OnPromoCodeChanged(object sender, TextChangedEventArgs e)
		{
			Facade.Instance.CurrentRide.PromoCode = e.Text.ToString();
		}

		private void VerifyPromoCode()
		{
			mActivity.ShowLoadingView("Veryfying promo code");
			var dic = new Dictionary<String, String> {
				{ Constant.VALIDATEDISCOUNTCOUPON_CODE, mPromoCode.Text },
				{ Constant.VALIDATEDISCOUNTCOUPON_CUSTOMERID, "-1" },
				{ Constant.VALIDATEDISCOUNTCOUPON_EMAIL, "" },
				{ Constant.VALIDATEDISCOUNTCOUPON_SERVICEID, "-1" },
				{ Constant.VALIDATEDISCOUNTCOUPON_TRAVELDATE, "" },
				{ Constant.VALIDATEDISCOUNTCOUPON_VALIDATIONTYPE, "1" }
			};

			string result;
			ValidateDiscountCouponResponse tt = null;
			try
			{
				UserTrackingReporter.TrackUser(Constant.CATEGORY_PROMO_CODE, "Validating promo code");

				Task runSync = Task.Factory.StartNew(async () =>
				{
					result = await AppData.ApiCall(Constant.VALIDATEDISCOUNTCOUPON, dic);
					tt = (ValidateDiscountCouponResponse)AppData.ParseResponse(Constant.VALIDATEDISCOUNTCOUPON, result);
				}).Unwrap();
				runSync.Wait();

			}
			catch (Exception ex)
			{
				CrashReporter.Report(ex);
				mActivity.HideLoadingView();
				mActivity.RunOnUiThread(() =>
				{
					mActivity.ShowMessageBox("Failed", "Invalid promo code entered.");
				});
			}
			if (tt == null || String.IsNullOrEmpty(tt.Result) || tt.Result.ToLower().Contains("failed"))
			{
				UserTrackingReporter.TrackUser(Constant.CATEGORY_PROMO_CODE, "Invalid promo code entered");
				mActivity.RunOnUiThread(() =>
				{
					mActivity.ShowMessageBox("Failed", "Invalid promo code entered.");
				});
			}
			else {
				mActivity.RunOnUiThread(() =>
				{
					mActivity.ShowMessageBox("Success", "Valid promo code entered.");
				});
			}
			mActivity.HideLoadingView();
		}
	}
}

