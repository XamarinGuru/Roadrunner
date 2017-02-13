
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Text;

using RoadRunner.Shared;
using RoadRunner.Shared.Classes;

using GalaSoft.MvvmLight.Helpers;

namespace RoadRunner.Android
{
	[Activity (Label = "RidePaymentActivity")]			
	public class RidePaymentActivity : NavigationActivity
	{
		private static bool IsFirstTime = true;

		ImageView m_imgPayment;
		Spinner m_spPayment;
		Switch m_switchMeetGreet;

		List<KeyValuePair<object, string>> m_listCreditCards;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView(Resource.Layout.RidePayment);

			m_imgPayment = (ImageView)FindViewById(Resource.Id.imgPayment);

			var btnAddPayment = (ImageView)FindViewById(Resource.Id.btnAddPayment);
			btnAddPayment.Click += (object sender, EventArgs e) =>
			{
				StartActivity(new Intent(this, typeof(AddPaymentActivity)));
				OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
			};

			m_spPayment = (Spinner)FindViewById(Resource.Id.spPayment);

			var txtPromoCode = (EditText)FindViewById(Resource.Id.txtPromoCode);
			txtPromoCode.TextChanged += OnPromoCodeChanged;

			var spGratuity = (Spinner)FindViewById(Resource.Id.spGratuity);
			if (Facade.Instance.CurrentRide.SelectedFare.serviceid == "0")
			{
				spGratuity.Enabled = false;
			}
			var adapter = new ArrayAdapter(this, Resource.Layout.item_spinner);
			spGratuity.Adapter = adapter;
			adapter.Add("10%");
			adapter.Add("15%");
			adapter.Add("20%");

			spGratuity.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(OnGratuityChanged);

			m_switchMeetGreet = (Switch)FindViewById(Resource.Id.switchMeetGreet);
			m_switchMeetGreet.CheckedChange += delegate (object sender, CompoundButton.CheckedChangeEventArgs e)
			{
				Facade.Instance.CurrentRide.IsMeetandGreet = e.IsChecked;
			};

			var spExtraBags = (Spinner)FindViewById(Resource.Id.spExtraBags);
			var adapter1 = new ArrayAdapter(this, Resource.Layout.item_spinner);
			spExtraBags.Adapter = adapter1;
			for (var i = 1; i < 11; i++)
			{
				adapter1.Add(i);
			}
			spExtraBags.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(OnExtraBagsChanged);

			if (IsFirstTime)
			{
				IsFirstTime = false;
				SetBindingsOnce();
			}

			var btnGotoRideConfirmation = (Button)FindViewById (Resource.Id.btn_GoToRideConfirmation);
			btnGotoRideConfirmation.SetCommand("Click", Facade.Instance.CurrentRide.GoToTheRideConfirmation);

			var btnBack = (ImageButton)FindViewById (Resource.Id.btn_back);
			btnBack.Click += delegate(object sender , EventArgs e )
			{
				OnBack();
			};

			GetSpecialServices();
		}

		protected override void OnResume()
		{
			base.OnResume();
			LoadCreditCards();
		}

		private void LoadCreditCards()
		{
			ShowLoadingView("Loading data...");

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

				RunOnUiThread(() =>
				{
					var adapter = new ArrayAdapter(this, Resource.Layout.item_spinner);
					m_spPayment.Adapter = adapter;
					foreach (var card in tt.CardList)
					{
						m_listCreditCards.Add(new KeyValuePair<object, string>(card.Id, card.CardNumber));
						adapter.Add(card.CardNumber);
					}
					m_spPayment.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(OnPaymentChanged);
				 });

				HideLoadingView();
			});
		}

		private void OnPaymentChanged(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Facade.Instance.CurrentRide.CreditCardId = m_listCreditCards[e.Position].Key.ToString();
			AppSettings.SelectedCard = m_listCreditCards[e.Position].Key.ToString();
		}

		private void OnPromoCodeChanged(object sender, TextChangedEventArgs e)
		{
			Facade.Instance.CurrentRide.PromoCode = e.Text.ToString();
		}

		private void OnGratuityChanged(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			if (e.Position == 0)
			{
				Facade.Instance.CurrentRide.Gratuity = "0.1";
			}
			else if (e.Position == 1)
			{
				Facade.Instance.CurrentRide.Gratuity = "0.15";
			}
			else if (e.Position == 2)
			{
				Facade.Instance.CurrentRide.Gratuity = "0.2";
			}
		}
		private void OnExtraBagsChanged(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Facade.Instance.CurrentRide.ExtraBags = (e.Position + 1).ToString();
		}

		public async void GetSpecialServices()
		{
			ShowLoadingView("Loading data...");

			var ride = new Ride();
			var specialServices = await ride.GetSpecialService();
			var arrSpcialServices = specialServices.SpecialServices;

			m_switchMeetGreet.Enabled = specialServices.IsMeetAndGreetAvailable;

			Facade.Instance.CurrentRide.MeetandGreetFee = 0;
			Facade.Instance.CurrentRide.ExtraBagsFee = 0;
			for (int i = 0; i < arrSpcialServices.Count; i++)
			{
				if (specialServices.MeetAndGreetProductID == arrSpcialServices[i].ProductID)
				{
					Facade.Instance.CurrentRide.MeetandGreetFee = double.Parse(arrSpcialServices[i].Price);
				}
				if (specialServices.AdditionalBaggageProductID == arrSpcialServices[i].ProductID)
				{
					Facade.Instance.CurrentRide.ExtraBagsFee = double.Parse(arrSpcialServices[i].Price);
				}
			}

			var lblExtraBags = (TextView)FindViewById(Resource.Id.lblExtraBags);
			lblExtraBags.Text = string.Format("* ${0} per extra bag", specialServices.AdditionalBaggageCost.ToString());

			var lblMeetandGreetFee = (TextView)FindViewById(Resource.Id.lblMeetandGreetFee);
			lblMeetandGreetFee.Text = string.Format("Yes ({0:C} fee)", specialServices.MeetAndGreetCost);

			HideLoadingView();
		}

		private void SetBindingsOnce()
		{

			this.SetBinding(
				() => Facade.Instance.CurrentRide.CanGoToTheRideConfirmation)
				.UpdateSourceTrigger("CanGoToTheRideConfirmationChanges")
				.WhenSourceChanges(
					async () =>
					{
						if (Facade.Instance.CurrentRide.CanGoToTheRideConfirmation)
						{
							StartActivity(new Intent(this, typeof(RideConfirmationActivity)));
							OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
						}
						else {
							if (Facade.Instance.CurrentRide.ValidaionError != null && Facade.Instance.CurrentRide.ValidaionError.Count > 0)
							{
								string header = Facade.Instance.CurrentRide.ValidaionError.Count > 1
									? "Just a couple things left:"
									: "Please correct the last thing:";

								var delimeter = System.Environment.NewLine + System.Environment.NewLine;
								var message = String.Join(delimeter, Facade.Instance.CurrentRide.ValidaionError.Select(r => r.ErrorMessage));

								RunOnUiThread(() =>
								{
									ShowMessageBox(header, message);
								});
							}
						}
						//StartActivity(new Intent(this, typeof(RideConfirmationActivity)));
						//OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
					});
		}
	}
}

