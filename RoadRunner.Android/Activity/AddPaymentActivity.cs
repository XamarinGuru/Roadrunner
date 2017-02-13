
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using RoadRunner.Shared;
using RoadRunner.Shared.Classes;

using Card.IO;

namespace RoadRunner.Android
{
	[Activity (Label = "AddPaymentActivity")]			
	public class AddPaymentActivity : NavigationActivity
	{
		private EditText edtCardData;
		private ImageView cardImage;
		private EditText edtCCV;
		private EditText edtZIP;
		private EditText edtCardName;
		private EditText edtCardDate;

		private CreditCard creditCardInfo = null;

		private RoadRunner.Shared.CardType crditCardType = RoadRunner.Shared.CardType.Unknown;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView(Resource.Layout.AddPayment);

			edtCardData = (EditText)FindViewById(Resource.Id.edtCardData);
			cardImage = (ImageView)FindViewById(Resource.Id.cardImage);
			edtCCV = (EditText)FindViewById(Resource.Id.edtCCV);
			edtZIP = (EditText)FindViewById(Resource.Id.edtZIP);
			edtCardName = (EditText)FindViewById(Resource.Id.edtCardName);
			edtCardDate = (EditText)FindViewById(Resource.Id.edtCardDate);

			edtCardData.Touch += PopupCardIO;

			var btnScanCard = FindViewById<Button>(Resource.Id.btnScanCard);
			btnScanCard.Click += delegate
			{
				ScanCard(true);
			};

			var btnSubmit = FindViewById<Button>(Resource.Id.btnSubmit);
			btnSubmit.Click += delegate
			{
				SubmitCard();
			};

			ImageButton btnBack = (ImageButton)FindViewById(Resource.Id.btn_back);
			btnBack.Click += delegate (object sender, EventArgs e)
			{
				OnBack();
			};
		}

		private void PopupCardIO(object sender, View.TouchEventArgs e)
		{
			if (e.Event.Action == MotionEventActions.Down)
			{
				ScanCard(false);
			}
		}

		private void ScanCard(bool isScan)
		{
			var intent = new Intent(this, typeof(CardIOActivity));
			intent.PutExtra(CardIOActivity.ExtraRequireExpiry, true); 
			intent.PutExtra(CardIOActivity.ExtraRequireCvv, true); 		
			intent.PutExtra(CardIOActivity.ExtraRequirePostalCode, false); 
			intent.PutExtra(CardIOActivity.ExtraUseCardioLogo, true);
			intent.PutExtra(CardIOActivity.ExtraNoCamera, !isScan);

			StartActivityForResult (intent, 101);
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);

			if (data != null) {

				creditCardInfo = data.GetParcelableExtra (CardIOActivity.ExtraScanResult).JavaCast<CreditCard> ();

				edtCardData.Text = creditCardInfo.RedactedCardNumber;
				edtCCV.Text = creditCardInfo.Cvv;
				edtCardDate.Text = String.Format("{0}/{1}", creditCardInfo.ExpiryMonth.ToString(), creditCardInfo.ExpiryYear.ToString());

				switch (creditCardInfo.CardType.Name)
				{
					case "MasterCard":
						crditCardType = RoadRunner.Shared.CardType.Mastercard;
						cardImage.SetImageResource(Resource.Drawable.card_master);
						break;
					case "Visa":
						crditCardType = RoadRunner.Shared.CardType.Visa;
						cardImage.SetImageResource(Resource.Drawable.card_visa);
						break;
					case "AmEx":
						crditCardType = RoadRunner.Shared.CardType.AmericanExpress;
						cardImage.SetImageResource(Resource.Drawable.card_amer);
						break;
					case "Discover":
						crditCardType = RoadRunner.Shared.CardType.Discover;
						cardImage.SetImageResource(Resource.Drawable.card_discover);
						break;
					case "DinersClub":
						crditCardType = RoadRunner.Shared.CardType.DinersClub;
						cardImage.SetImageResource(Resource.Drawable.card_diner);
						break;
					default:
						crditCardType = RoadRunner.Shared.CardType.Unknown;
						cardImage.SetImageResource(Resource.Drawable.icon_card);
						break;
				}
			}
		}

		private async void SubmitCard()
		{
			ShowLoadingView("Loading...");
			var dic = new Dictionary<String, String>
			{
				{Constant.INSERTCREDITCARDDETAILSFORPHONE_CCNAME, edtCardName.Text },
				{Constant.INSERTCREDITCARDDETAILSFORPHONE_CCNUM, creditCardInfo.CardNumber},
				{Constant.INSERTCREDITCARDDETAILSFORPHONE_CCTYPE, crditCardType.ToString()},
				{Constant.INSERTCREDITCARDDETAILSFORPHONE_CID, creditCardInfo.Cvv },
				{Constant.INSERTCREDITCARDDETAILSFORPHONE_CUSTOMERID, AppSettings.UserID},
				{Constant.INSERTCREDITCARDDETAILSFORPHONE_EXPDATE, creditCardInfo.ExpiryMonth + creditCardInfo.ExpiryYear.ToString ().Substring (2, 2) },
				{Constant.INSERTCREDITCARDDETAILSFORPHONE_LOGINTYPE, "0" }, // do not use the actual login types
				{Constant.INSERTCREDITCARDDETAILSFORPHONE_TOKENID, string.Empty }, // do not use the actual token
				{Constant.INSERTCREDITCARDDETAILSFORPHONE_ZIP, edtZIP.Text }
			};

			var result = string.Empty;

			try
			{
				result = await AppData.ApiCall(Constant.INSERTCREDITCARDDETAILSFORPHONE, dic);
			}
			catch (Exception ex)
			{
				RunOnUiThread(() =>
				{
					HideLoadingView();
					ShowMessageBox("Exception:", ex.Message);
				});

				CrashReporter.Report(ex);
				return;
			}

			HideLoadingView();

			var tt = (InsertCreditCardDetailsForPhoneResponse)AppData.ParseResponse(Constant.INSERTCREDITCARDDETAILSFORPHONE, result);

			AlertDialog.Builder alert = new AlertDialog.Builder(this);

			alert.SetTitle(tt.Result);
			alert.SetMessage(tt.Msg);
			alert.SetPositiveButton("OK", (senderAlert, args) =>
			{
				if (tt.Result == "Success" || tt.Result == "Sucess")
				{
					string fromWhere = Intent.GetStringExtra("fromWhere") ?? "any";
					OnBack();
					//if (fromWhere == string.Empty) return;

					if (fromWhere == "signup")
					{
						var mainActivity = new Intent(this, typeof(MainActivity));
						StartActivity(mainActivity);
					} else if(fromWhere == "any") {
						OnBack();
					}
				}
			});

			RunOnUiThread(() =>
			{
				alert.Show();
			});
		}
	}
}

