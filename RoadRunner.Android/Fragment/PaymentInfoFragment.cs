
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Android.Support.V4.App;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

using RoadRunner.Shared;
using RoadRunner.Shared.Classes;

using Fragment = Android.Support.V4.App.Fragment;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;

namespace RoadRunner.Android
{
	public class PaymentInfoFragment : Fragment
	{
		ListView mDrawerList;
		List<GetCreditCardDetailsNewForPhoneResponseItem> mCardList = new List<GetCreditCardDetailsNewForPhoneResponseItem>();

		MainActivity mSuperActivity;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.fragment_paymentInfo, container, false);

			mDrawerList = (ListView) view.FindViewById(Resource.Id.creditCardList);
			mDrawerList.ItemLongClick += LongClickForDelete;

			var dic = new Dictionary<String, String>
			{
				{Constant.GETCREDITCARDDETAILSNEWFORPHONE_CUSTOMERID, AppSettings.UserID},
				{Constant.GETCREDITCARDDETAILSNEWFORPHONE_LOGINTYPE, "-1"},
				{Constant.GETCREDITCARDDETAILSNEWFORPHONE_TOKENID, string.Empty } // do not use the actual token
			};

			String result = String.Empty;

			mSuperActivity = this.Activity as MainActivity;
			mSuperActivity.ShowLoadingView ("Loading...");
			Task runSync = Task.Factory.StartNew(async () => {
				result = await AppData.ApiCall(Constant.GETCREDITCARDDETAILSNEWFORPHONE, dic);
				var response = (GetCreditCardDetailsNewForPhoneResponse) AppData.ParseResponse(Constant.GETCREDITCARDDETAILSNEWFORPHONE, result);
				mCardList = response.CardList;
				var adapter = new CreditCardAdapter(this.Activity, response, mSuperActivity);
				this.Activity.RunOnUiThread(() =>
				{
					mDrawerList.Adapter = adapter;
					adapter.NotifyDataSetChanged();
					mSuperActivity.HideLoadingView();
				});
			}).Unwrap();

			return view;
		}

		private void LongClickForDelete(object sender, AdapterView.ItemLongClickEventArgs e)
		{
			//var alert = new AlertDialog.Builder (this.Activity);

			//alert.SetTitle ("Are you sure?");
			//alert.SetMessage ("Are you sure to delete this card?");
			//alert.SetPositiveButton ("OK", (senderAlert, args) => {
			//	deleteCard(e.Position);
			//} );

			//this.Activity.RunOnUiThread (() => {
			//	alert.Show();
			//} );
		}

		public async void deleteCard(int index)
		{
			UserTrackingReporter.TrackUser (Constant.CATEGORY_PAYMENT, "Deleting card");

			GetCreditCardDetailsNewForPhoneResponseItem card = mCardList [index];

			var dic = new Dictionary<String, String>
			{
				{Constant.DELETECREDITCARDNEW_CUSTOMERID, AppSettings.UserID},
				{Constant.DELETECREDITCARDNEW_INFOID, card.Id},
				{Constant.DELETECREDITCARDNEW_LOGINTYPE, AppSettings.LoginType.ToString ()},
				{Constant.DELETECREDITCARDNEW_TOKENID, AppSettings.UserToken}
			};
			try
			{
				var result = await AppData.ApiCall (Constant.DELETECREDITCARDNEW, dic);
				var deleteCardResponse = (DeleteCreditCardNewResponse)AppData.ParseResponse (Constant.DELETECREDITCARDNEW, result);

				mSuperActivity.ShowMessageBox(deleteCardResponse.Result, deleteCardResponse.Message);

				mCardList.RemoveAt(index);
				CreditCardAdapter adapter = (CreditCardAdapter)mDrawerList.Adapter;
				adapter.RemoveCard (index);
			}
			catch(Exception ex)
			{
				mSuperActivity.ShowMessageBox ("Exception:", ex.Message);
				CrashReporter.Report (ex);
				return;
			}

		}
	}
}

