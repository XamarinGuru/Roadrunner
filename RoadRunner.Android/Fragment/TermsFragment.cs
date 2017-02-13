
using System;
using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.OS;
using Android.Widget;

using RoadRunner.Shared;
using RoadRunner.Shared.Classes;

namespace RoadRunner.Android
{
	public class TermsFragment : Fragment
	{
		MainActivity mSuperActivity;
		View mView;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			mView = inflater.Inflate(Resource.Layout.fragment_terms, container, false);

			mSuperActivity = this.Activity as MainActivity;

			GetDisclaimer();

			return mView;
		}

		private void GetDisclaimer()
		{
			mSuperActivity.ShowLoadingView("Loading data...");

			var dic = new Dictionary<String, String> { };

			String result = String.Empty;

			System.Threading.Tasks.Task runSync = System.Threading.Tasks.Task.Factory.StartNew(async () =>
			{
				result = await AppData.ApiCall(Constant.GETDISCLAIMER, dic);

				var tt = (GetDisclaimerResponse)AppData.ParseResponse(Constant.GETDISCLAIMER, result);

				this.Activity.RunOnUiThread(() =>
				{
					var contentLinear = (LinearLayout)mView.FindViewById(Resource.Id.contentLinear);
					var txtHeader = (TextView)mView.FindViewById(Resource.Id.txtHeader);
					var txtBody = (TextView)mView.FindViewById(Resource.Id.txtBody);

					for (var i = 0; i < tt.DisclaimerList.Count; i++)
					{
						txtHeader.Visibility = ViewStates.Gone;
						txtBody.Visibility = ViewStates.Gone;
						var header = new TextView(mSuperActivity);
						header.SetText(tt.DisclaimerList[i].Header, TextView.BufferType.Normal);
						header.SetPadding(0, 15, 0, 0);
						header.Gravity = GravityFlags.Center;
						header.Typeface = txtHeader.Typeface;
						contentLinear.AddView(header);

						txtBody.Visibility = ViewStates.Invisible;
						var body = new TextView(mSuperActivity);
						body.SetText(tt.DisclaimerList[i].Body, TextView.BufferType.Normal);
						body.SetPadding(0, 5, 0, 0);
						body.Gravity = GravityFlags.Center;
						body.Typeface = txtBody.Typeface;
						contentLinear.AddView(body);
					}
				});

				mSuperActivity.HideLoadingView();
			});
		}
	}
}

