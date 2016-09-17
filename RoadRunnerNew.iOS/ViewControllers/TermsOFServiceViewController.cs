using System;
using UIKit;
using CoreGraphics;
using RoadRunnerNew.iOS.Extension;
using RoadRunner.Shared;
using RoadRunner.Shared.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoadRunnerNew.iOS
{
	partial class TermsOFServiceViewController : BaseTitleViewController
	{
		public bool isFromMenu = true;
		public TermsOFServiceViewController (IntPtr handle) : base (handle, "TERMS OF SERVICE")
		{
			
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
			GetDisclaimer ();
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}
		public override void ViewWillAppear (bool animated)
		{
			if (!isFromMenu) {
				NavigationItem.Customize (NavigationController);
			}
		}

		private void GetDisclaimer()
		{
			ShowLoadingView ("Loading data...");

			var dic = new Dictionary<String, String>{};

			String result = String.Empty;

			System.Threading.Tasks.Task runSync = System.Threading.Tasks.Task.Factory.StartNew(async () => {
				result = await AppData.ApiCall(Constant.GETDISCLAIMER, dic);

				var tt = (GetDisclaimerResponse) AppData.ParseResponse(Constant.GETDISCLAIMER, result);

				InvokeOnMainThread(() => {
					nfloat yValue = 15;
					for (var i = 0; i < tt.DisclaimerList.Count; i++)
					{
						txtHeader.Text = tt.DisclaimerList[i].Header;
						txtHeader.SizeToFit();
						txtHeader.Hidden = true;
						UILabel header = new UILabel(new CGRect(txtHeader.Frame.X, yValue, scrollView.Frame.Size.Width, txtHeader.Frame.Size.Height));
						header.Text = tt.DisclaimerList[i].Header;
						header.TextAlignment = UITextAlignment.Center;
						header.TextColor = txtHeader.TextColor;
						header.Font = txtHeader.Font;
						scrollView.AddSubview(header);

						yValue += 5 + txtHeader.Frame.Size.Height;

						txtBody.Text = tt.DisclaimerList[i].Body;
						txtBody.SizeToFit();
						txtBody.Hidden = true;
						UILabel body = new UILabel(new CGRect(txtBody.Frame.X, yValue, scrollView.Frame.Size.Width, txtBody.Frame.Size.Height));
						body.Text = tt.DisclaimerList[i].Body;
						body.TextAlignment = UITextAlignment.Center;
						body.TextColor = txtBody.TextColor;
						body.Font = txtBody.Font;
						body.Lines = 100;
						scrollView.AddSubview(body);

						yValue += 15 + txtBody.Frame.Size.Height;
					}
				});

				HideLoadingView();
			}).Unwrap();
		}
	}
}
