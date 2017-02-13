
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

namespace RoadRunner.Android
{
	[Activity(Label = "TermsActivity")]
	public class TermsActivity : NavigationActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Terms);

			GetDisclaimer();

			var btnBack = (ImageButton)FindViewById(Resource.Id.btn_back);
			btnBack.Click += delegate (object sender, EventArgs e)
			{
				StartActivity(new Intent(this, typeof(SignUpActivity)));
				OverridePendingTransition(Resource.Animation.fromRight, Resource.Animation.toLeft);
			};
		}

		private void GetDisclaimer()
		{
			ShowLoadingView("Loading data...");

			var dic = new Dictionary<String, String> { };

			String result = String.Empty;

			System.Threading.Tasks.Task runSync = System.Threading.Tasks.Task.Factory.StartNew(async () =>
			{
				result = await AppData.ApiCall(Constant.GETDISCLAIMER, dic);

				var tt = (GetDisclaimerResponse)AppData.ParseResponse(Constant.GETDISCLAIMER, result);

				RunOnUiThread(() =>
				{
					var contentLinear = (LinearLayout)FindViewById(Resource.Id.contentLinear);
					var txtHeader = (TextView)FindViewById(Resource.Id.txtHeader);
					var txtBody = (TextView)FindViewById(Resource.Id.txtBody);

					for (var i = 0; i < tt.DisclaimerList.Count; i++)
					{
						txtHeader.Visibility = ViewStates.Gone;
						txtBody.Visibility = ViewStates.Gone;
						var header = new TextView(this);
						header.SetText(tt.DisclaimerList[i].Header, TextView.BufferType.Normal);
						header.SetPadding(0, 15, 0, 0);
						header.Gravity = GravityFlags.Center;
						header.Typeface = txtHeader.Typeface;
						contentLinear.AddView(header);

						txtBody.Visibility = ViewStates.Invisible;
						var body = new TextView(this);
						body.SetText(tt.DisclaimerList[i].Body, TextView.BufferType.Normal);
						body.SetPadding(0, 5, 0, 0);
						body.Gravity = GravityFlags.Center;
						body.Typeface = txtBody.Typeface;
						contentLinear.AddView(body);
					}
				});

				HideLoadingView();
			});
		}
	}
}

