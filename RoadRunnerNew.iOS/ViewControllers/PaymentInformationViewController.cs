using System;
using UIKit;
using RoadRunnerNew.iOS.Extension;
using RoadRunner.Shared.Classes;
using System.Collections.Generic;
using RoadRunner.Shared;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Helpers;
using System.Linq;

namespace RoadRunnerNew.iOS
{
	partial class PaymentInformationViewController : BaseViewController
	{
		private  static bool IsFirstTime = true;

		private UINavigationController thisController { get; set; }

		public PaymentInformationViewController (IntPtr handle) : base (handle)
		{
			Title = "RIDE PAYMENT";
		}
			
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			//thisController = NavigationController;
			AppSettings.CurrentNavigation = NavigationController;
			NavigationItem.Customize (NavigationController);

			var tap = new UITapGestureRecognizer(() => { View.EndEditing(true); });
			View.AddGestureRecognizer(tap);

			edtGratuity.ShouldReturn += TextFieldShouldReturn;
			edtPromoCode.ShouldReturn += TextFieldShouldReturn;
			edtExtraBags.ShouldReturn += TextFieldShouldReturn;

			switchMandG.SetState (Facade.Instance.CurrentRide.IsMeetandGreet, false);

			SetupExtraBags (edtExtraBags);
			SetupGratuity (edtGratuity);

			switchMandG.ValueChanged += (sender, e) => {
				Facade.Instance.CurrentRide.IsMeetandGreet = switchMandG.On;
			};

			if (Facade.Instance.CurrentRide.SelectedFare.serviceid == "0") {
				edtGratuity.Enabled = false;
			}

			btnScheduleARide4.SetCustomButton ();

			GetSpecialServices ();

			SetBindings ();
			if (IsFirstTime) {
				IsFirstTime = false;
				SetBindingsOnce ();
			}
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			LoadCreditCards ();
		}

		private void SetBindings()
		{
			this.SetBinding (
				() => Facade.Instance.CurrentRide.ExtraBags,
				() => edtExtraBags.Text,
				BindingMode.TwoWay)
				.UpdateTargetTrigger ("ValueChanged");
			
			this.SetBinding (
				() => Facade.Instance.CurrentRide.Gratuity,
				() => edtGratuity.Text,
				BindingMode.TwoWay)
				.UpdateTargetTrigger ("ValueChanged");

			this.SetBinding (
				() => edtPromoCode.Text,
				() => Facade.Instance.CurrentRide.PromoCode,
				BindingMode.TwoWay)
				.UpdateSourceTrigger ("EditingChanged");
			
			this.SetBinding (
				() => Facade.Instance.CurrentRide.CreditCard,
				() => edtSelectCard.Text,
				BindingMode.TwoWay)
				.UpdateTargetTrigger ("ValueChanged");
			
			btnAddCreditCard.TouchUpInside+= (object sender, EventArgs e) => {
				UIStoryboard sb = UIStoryboard.FromName ("MainStoryboard", null);
				var pvc = (AddCreditCardViewController)sb.InstantiateViewController("AddCreditCardViewController");
				pvc.fromWhere = "payment";
				pvc.callback = LoadCreditCards;
				NavigationController.PushViewController (pvc, true);
			};

			btnScheduleARide4.SetCommand ("TouchUpInside", Facade.Instance.CurrentRide.GoToTheRideConfirmation);
		}
		private void SetBindingsOnce(){
			
			this.SetBinding (
				() => Facade.Instance.CurrentRide.CanGoToTheRideConfirmation)
				.UpdateSourceTrigger ("CanGoToTheRideConfirmationChanges")
				.WhenSourceChanges (
					async () => {
						if (Facade.Instance.CurrentRide.CanGoToTheRideConfirmation) {
							UIStoryboard sb = UIStoryboard.FromName ("MainStoryboard", null);
							RideConfirmViewController pvc = (RideConfirmViewController)sb.InstantiateViewController ("ScheduleARideViewController4");
							//thisController.PushViewController (pvc, true);
							AppSettings.CurrentNavigation.PushViewController(pvc, true);
						} else {
							if (Facade.Instance.CurrentRide.ValidaionError != null && Facade.Instance.CurrentRide.ValidaionError.Count > 0) {
								string header = Facade.Instance.CurrentRide.ValidaionError.Count > 1
									? "Just a couple things left:"
									: "Please correct the last thing:";

								var delimeter = Environment.NewLine + Environment.NewLine;
								var message = String.Join (delimeter, Facade.Instance.CurrentRide.ValidaionError.Select (r => r.ErrorMessage));
								InvokeOnMainThread (() => new UIAlertView (header, message, null, "Ok", null).Show ());
							}
						}
					});
		}

		private void LoadCreditCards(){

			ShowLoadingView ("Loading data...");

			var dic = new Dictionary<String, String>
			{
				{Constant.GETCREDITCARDDETAILSNEWFORPHONE_CUSTOMERID, AppSettings.UserID},
				{Constant.GETCREDITCARDDETAILSNEWFORPHONE_LOGINTYPE, "-1"},
				{Constant.GETCREDITCARDDETAILSNEWFORPHONE_TOKENID, string.Empty } // do not use the actual token
			};

			String result = String.Empty;

			Task runSync = Task.Factory.StartNew(async () => {
				result = await AppData.ApiCall(Constant.GETCREDITCARDDETAILSNEWFORPHONE, dic);

				var tt = (GetCreditCardDetailsNewForPhoneResponse) AppData.ParseResponse(Constant.GETCREDITCARDDETAILSNEWFORPHONE, result);
				var listCreditCards = new List<KeyValuePair<object, string>>();
				var listCreditCardImages = new List<KeyValuePair<object, object>>();

				InvokeOnMainThread(() => { 
					foreach(var card in  tt.CardList){

						UIImage cardImage;

						CardType cardType = CardType.Unknown; 

						if (!string.IsNullOrEmpty (card.CardType)) {
							cardType = (CardType)int.Parse (card.CardType);
						}

						switch (cardType)
						{
						case CardType.Mastercard:
							cardImage = UIImage.FromBundle (@"fa-cc-mastercard");
							break;
						case CardType.Visa:				
							cardImage = UIImage.FromBundle (@"fa-cc-visa");
							break;
						case CardType.AmericanExpress:
							cardImage = UIImage.FromBundle (@"fa-cc-amex");
							break;
						case CardType.Discover:
							cardImage = UIImage.FromBundle (@"fa-cc-discover");
							break;
						case CardType.DinersClub:
							cardImage = UIImage.FromBundle (@"fa-cc-diners-club");
							break;
						default:
							cardImage = UIImage.FromBundle (@"oi-credit-card");
							break;
						}

						if(card.Id == AppSettings.SelectedCard)
						{
							edtSelectCard.Text = card.CardNumber;
							imgCard.Image = cardImage;
							Facade.Instance.CurrentRide.CreditCardId = card.Id;
						}
						listCreditCards.Add(new KeyValuePair<object, string>(card.Id, card.CardNumber));
						listCreditCardImages.Add(new KeyValuePair<object, object>(card.Id, cardImage));

					}
					SetupCreditCards (edtSelectCard, imgCard, listCreditCards, listCreditCardImages);
				});

				HideLoadingView();
			}).Unwrap();
		}

		public async void GetSpecialServices()
		{
			ShowLoadingView ("Loading data...");

			Ride ride = new Ride ();
			var specialServices = await ride.GetSpecialService ();
			var arrSpcialServices = specialServices.SpecialServices;

			switchMandG.Enabled = specialServices.IsMeetAndGreetAvailable;

			Facade.Instance.CurrentRide.MeetandGreetFee = 0;
			Facade.Instance.CurrentRide.ExtraBagsFee = 0;
			for (int i = 0; i < arrSpcialServices.Count; i++) {
				if (specialServices.MeetAndGreetProductID == arrSpcialServices [i].ProductID) {
					Facade.Instance.CurrentRide.MeetandGreetFee = double.Parse(arrSpcialServices [i].Price);
				}
				if (specialServices.AdditionalBaggageProductID == arrSpcialServices [i].ProductID) {
					Facade.Instance.CurrentRide.ExtraBagsFee = double.Parse(arrSpcialServices [i].Price);
				}
			}

			//double extraBagsFee = double.Parse (specialServices.AdditionalBaggageCost);
			lblExtraBags.Text = string.Format ("* ${0} per extra bag", specialServices.AdditionalBaggageCost.ToString());

			//double meetandGreetFee = double.Parse (specialServices.MeetAndGreetCost);
			lblMeetandGreetFee.Text = string.Format ("Yes ({0:C} fee)", specialServices.MeetAndGreetCost);

			HideLoadingView();
		}
	}
}
