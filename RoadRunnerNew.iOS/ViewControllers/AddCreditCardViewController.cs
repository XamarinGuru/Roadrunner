using System;
using UIKit;
using Card.IO;
using RoadRunner.Shared;
using System.Collections.Generic;
using RoadRunnerNew.iOS.Extension;

namespace RoadRunnerNew.iOS
{
	partial class AddCreditCardViewController : BaseViewController, ICardIOPaymentViewControllerDelegate
	{
		private CardIOPaymentViewController paymentViewController;

		private UINavigationController thisController { get; set; }

		private CreditCardInfo creditCardInfo = null;

		private CardType crditCardType = CardType.Unknown;

		public string fromWhere = string.Empty;

		public AddCreditCardViewController (IntPtr handle) : base (handle)
		{
			Title = "ADD PAYMENT";
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			thisController = NavigationController;
			NavigationItem.Customize (NavigationController);

			btnSubmit.SetCustomButton ();
			btnScaneCard.SetCustomButton ();

			edtCCV.ShouldReturn += TextFieldShouldReturn;
			edtZipCode.ShouldReturn += TextFieldShouldReturn;
			edtCreditDate.ShouldReturn += TextFieldShouldReturn;
			edtCreditCardData.ShouldReturn += TextFieldShouldReturn;
			edtCardHolderName.ShouldReturn += TextFieldShouldReturn;

			var tap = new UITapGestureRecognizer(() => { View.EndEditing(true); });
			View.AddGestureRecognizer(tap);

			edtCreditCardData.EditingDidBegin += (object sender, EventArgs e) => {
				paymentViewController = new CardIOPaymentViewController (this, false);
				NavigationController.PresentViewController(paymentViewController, true, null);
			};

			btnSubmit.TouchUpInside += async (object sender, EventArgs e) => {

				var dic = new Dictionary<String, String>
				{
					{Constant.INSERTCREDITCARDDETAILSFORPHONE_CCNAME, edtCardHolderName.Text },
					{Constant.INSERTCREDITCARDDETAILSFORPHONE_CCNUM, creditCardInfo.CardNumber},
					{Constant.INSERTCREDITCARDDETAILSFORPHONE_CCTYPE, crditCardType.ToString()},
					{Constant.INSERTCREDITCARDDETAILSFORPHONE_CID, creditCardInfo.Cvv },
					{Constant.INSERTCREDITCARDDETAILSFORPHONE_CUSTOMERID, AppSettings.UserID},
					{Constant.INSERTCREDITCARDDETAILSFORPHONE_EXPDATE, creditCardInfo.ExpiryMonth + creditCardInfo.ExpiryYear.ToString ().Substring (2, 2) },
					{Constant.INSERTCREDITCARDDETAILSFORPHONE_LOGINTYPE, "0" }, // do not use the actual login types
					{Constant.INSERTCREDITCARDDETAILSFORPHONE_TOKENID, string.Empty }, // do not use the actual token
					{Constant.INSERTCREDITCARDDETAILSFORPHONE_ZIP, edtZipCode.Text }
				};

				var result = string.Empty;

				try
				{
					result = await AppData.ApiCall(Constant.INSERTCREDITCARDDETAILSFORPHONE, dic);
				}
				catch(Exception ex)
				{
					InvokeOnMainThread (() => new UIAlertView (
						"Exception:", ex.Message, null, "Ok", null).Show ());					
					CrashReporter.Report (ex);
					return;
				}
					
				var tt = (InsertCreditCardDetailsForPhoneResponse) AppData.ParseResponse(Constant.INSERTCREDITCARDDETAILSFORPHONE, result);

				UIAlertView alert = new UIAlertView (tt.Result, tt.Msg, null, "Ok", null);
				alert.Clicked += (object senderObj, UIButtonEventArgs buttonArgs) => 
				{
					if (buttonArgs.ButtonIndex == 0 && (tt.Result == "Success" || tt.Result == "Sucess")) 
					{
						if(fromWhere == string.Empty) return;

						if(fromWhere == "signup")
						{
							var dvc = (HomeViewController)Storyboard.InstantiateViewController("HomeViewController");
							PresentViewController(dvc, true, null);
						}else{
							thisController.PopViewController(true);
						}
					}
					else
					{
						//negativeCallback();
					}
				};
					
				InvokeOnMainThread (alert.Show);
			};

			btnScaneCard.TouchUpInside += async (object sender, EventArgs e) => {
				paymentViewController = new CardIOPaymentViewController (this);
				NavigationController.PresentViewController(paymentViewController, true, null);
			};
		}

		public void UserDidCancelPaymentViewController (CardIOPaymentViewController paymentViewController)
		{
			paymentViewController.DismissViewController(true, null);
		}

		public void UserDidProvideCreditCardInfo (CreditCardInfo cardInfo, CardIOPaymentViewController paymentViewController)
		{
			creditCardInfo = cardInfo;


			edtCreditCardData.Text = cardInfo.RedactedCardNumber;// String.Format ("#### #### #### {0} {1}/{2}", cardInfo.RedactedCardNumber.Substring (l, 4).ToString (), cardInfo.ExpiryMonth, cardInfo.ExpiryYear.ToString ().Substring (2, 2));
			edtCCV.Text = cardInfo.Cvv;
			edtCreditDate.Text = String.Format("{0}/{1}", cardInfo.ExpiryMonth.ToString(), cardInfo.ExpiryYear.ToString ());

			switch(creditCardInfo.CardType)
			{
			case CreditCardType.Mastercard:
				imgAddCard.Image = UIImage.FromBundle (@"fa-cc-mastercard");
					crditCardType = CardType.Mastercard;
				break;
			case CreditCardType.Visa:				
				imgAddCard.Image = UIImage.FromBundle (@"fa-cc-visa");
					crditCardType = CardType.Visa;
				break;
			case CreditCardType.Amex:
				imgAddCard.Image = UIImage.FromBundle (@"fa-cc-amex");
					crditCardType = CardType.AmericanExpress;
				break;
			case CreditCardType.Discover:
				imgAddCard.Image = UIImage.FromBundle (@"fa-cc-discover");
					crditCardType = CardType.Discover;
				break;
			case CreditCardType.JCB:
				imgAddCard.Image = UIImage.FromBundle (@"fa-cc-diners-club");
					crditCardType = CardType.DinersClub;
				break;
			default:
				imgAddCard.Image = UIImage.FromBundle (@"oi-credit-card");
					crditCardType = CardType.Unknown;
				break;
			}

			paymentViewController.DismissViewController(true, null);        
		}
	}
}
