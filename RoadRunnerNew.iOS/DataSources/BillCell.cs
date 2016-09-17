using System;

using Foundation;
using UIKit;
using RoadRunner.Shared;
using RoadRunner.Shared.Classes;

namespace RoadRunnerNew.iOS
{
	public partial class BillCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("BillCell");
		public static readonly UINib Nib;

		static BillCell ()
		{
			Nib = UINib.FromName ("BillCell", NSBundle.MainBundle);
		}

		public BillCell (IntPtr handle) : base (handle)
		{
		}

		public void SetCell(GetCreditCardDetailsNewForPhoneResponseItem card)
		{
			lblBillID.Text = card.CardNumber;
			lblBillDate.Text = string.Format ("Exp: {0}/{1}", card.ExpirationMonth, card.ExpirationYear);

			CardType cardType = CardType.Unknown; 

			if (!string.IsNullOrEmpty (card.CardType)) {
				cardType = (CardType)int.Parse (card.CardType);
			}

			switch (cardType)
			{
			case CardType.Mastercard:
				lblBillID.Text = string.Format ("---- ------ -{0}", card.CardNumber.Substring (12, 4));
				imgBill.Image = UIImage.FromBundle (@"fa-cc-mastercard");
				break;
			case CardType.Visa:				
				lblBillID.Text = string.Format ("---- ------ -{0}", card.CardNumber.Substring (12, 4));
				imgBill.Image = UIImage.FromBundle (@"fa-cc-visa");
				break;
			case CardType.AmericanExpress:
				lblBillID.Text = string.Format ("---- ------ --{0}", card.CardNumber.Substring (12, 3));
				imgBill.Image = UIImage.FromBundle (@"fa-cc-amex");
				break;
			case CardType.Discover:
				lblBillID.Text = string.Format ("---- ------ -{0}", card.CardNumber.Substring (12, 4));
				imgBill.Image = UIImage.FromBundle (@"fa-cc-discover");
				break;
			case CardType.DinersClub:
				lblBillID.Text = card.CardNumber;
				imgBill.Image = UIImage.FromBundle (@"fa-cc-diners-club");
				break;
			default:
				lblBillID.Text = card.CardNumber;
				imgBill.Image = UIImage.FromBundle (@"oi-credit-card");
				break;
			}
		}
	}
}
