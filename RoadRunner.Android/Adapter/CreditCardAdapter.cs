
using System;

using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;

using RoadRunner.Shared;
using RoadRunner.Shared.Classes;


namespace RoadRunner.Android
{
	class CreditCardAdapter : BaseAdapter {

		Context mContext;
		GetCreditCardDetailsNewForPhoneResponse mCards;

		Activity mSuperActivity;

		public CreditCardAdapter(Context context, GetCreditCardDetailsNewForPhoneResponse cards, Activity superActivity) {
			mContext = context;
			mCards = cards;
			mSuperActivity = superActivity;
		}

		public override int Count {
			get {
				return mCards.CardList.Count + 1;
			}
		}

		public override Java.Lang.Object GetItem (int position)
		{
			return null;
		}

		public GetCreditCardDetailsNewForPhoneResponseItem GetNavItem(int position)
		{
			return mCards.CardList [position];
		}

		public void RemoveCard(int position)
		{
			mCards.CardList.RemoveAt(position);
			NotifyDataSetChanged();
		}
		public void deleteCard(GetCreditCardDetailsNewForPhoneResponseItem card)
		{
		}
		override public long GetItemId(int position) {
			return position;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			if (position == mCards.CardList.Count) {
				convertView = LayoutInflater.From (mContext).Inflate (Resource.Layout.item_addCard, null);
				var addCard = (Button)convertView.FindViewById (Resource.Id.btnAddCard);

				addCard.Click += (object sender, EventArgs e) => {
					mSuperActivity.StartActivity(new Intent(mSuperActivity,typeof(AddPaymentActivity)));
					mSuperActivity.OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
				};

			} else {
				if (convertView == null) 
				{
					convertView = LayoutInflater.From (mContext).Inflate (Resource.Layout.item_card, null);
				}
				var cardNumber = (TextView)convertView.FindViewById (Resource.Id.number);
				var cardDate = (TextView)convertView.FindViewById (Resource.Id.date);
				var cardImage = (ImageView)convertView.FindViewById (Resource.Id.image);

				var card = (GetCreditCardDetailsNewForPhoneResponseItem) GetNavItem(position);

				cardNumber.Text = card.CardNumber;
				cardDate.Text = string.Format ("Exp: {0}/{1}", card.ExpirationMonth, card.ExpirationYear);

				CardType cardType = CardType.Unknown; 

				if (!string.IsNullOrEmpty (card.CardType)) {
					cardType = (CardType)int.Parse (card.CardType);
				}

				switch (cardType)
				{
				case CardType.Mastercard:
					cardNumber.Text = string.Format ("- - - -  - - - - - -  -{0}", card.CardNumber.Substring (12, 4));
					cardImage.SetImageResource (Resource.Drawable.card_master);
					break;
				case CardType.Visa:				
					cardNumber.Text = string.Format ("- - - -  - - - - - -  -{0}", card.CardNumber.Substring (12, 4));
					cardImage.SetImageResource (Resource.Drawable.card_visa);
					break;
				case CardType.AmericanExpress:
					cardNumber.Text = string.Format ("- - - -  - - - - - -  - -{0}", card.CardNumber.Substring (12, 3));
					cardImage.SetImageResource (Resource.Drawable.card_amer);
					break;
				case CardType.Discover:
					cardNumber.Text = string.Format ("- - - -  - - - - - -  -{0}", card.CardNumber.Substring (12, 4));
					cardImage.SetImageResource (Resource.Drawable.card_discover);
					break;
				case CardType.DinersClub:
					cardNumber.Text = card.CardNumber;
					cardImage.SetImageResource (Resource.Drawable.card_diner);
					break;
				default:
					cardNumber.Text = card.CardNumber;
					cardImage.SetImageResource (Resource.Drawable.icon_card);
					break;
				}
			}
			return convertView;
		}
	}
}