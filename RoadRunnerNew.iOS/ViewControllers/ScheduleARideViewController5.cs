using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using CoreGraphics;

namespace RoadRunnerNew.iOS
{
	partial class ScheduleARideViewController5 : BaseTitleViewController
	{
		public ScheduleARideViewController5 (IntPtr handle) : base (handle)
		{
			Title = "Disclaimer";
		}

		protected override void Init (string title)
		{
			//NavigationItem.Prompt = String.Format ("Step {0} out of {1}", 4, 4);

			var buttonMenu = new UIButton(new CGRect(0, 0, 20, 20));
			buttonMenu.SetImage(UIImage.FromBundle("back-ic"), UIControlState.Normal);
			buttonMenu.TouchUpInside += (object sender, EventArgs e) => {
				NavigationController.PopViewController (true);
			};

			NavigationItem.LeftBarButtonItem = new UIBarButtonItem(buttonMenu);
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			//myScrollView.Frame = new CGRect (25, 25, mainView2.Bounds.Width-10f, mainView2.Bounds.Height - 25f);
			//myScrollView.ContentSize = new CGSize (mainView2.Bounds.Width+10f, btnScheduleARide6.Frame.Top + 125f);//lblArrival.Frame.Bottom + 25f);

			//myScrollView.Frame = new CGRect (0, 0, 100f, 100f);
			myScrollView.Frame = new CGRect(0,0,mainView2.Bounds.Width, mainView2.Bounds.Height);
			myScrollView.ContentSize = new CGSize (mainView2.Bounds.Width, lblContactUS.Frame.Bottom + 325f);//lblArrival.Frame.Bottom + 25f);


			//myScrollView.Frame = 

//			btnScheduleARide6.TouchUpInside += (object sender, EventArgs e) => {
//				UIStoryboard sb = UIStoryboard.FromName ("MainStoryboard", null);
//				ThankyouViewController pvc = (ThankyouViewController)sb.InstantiateViewController ("ScheduleARideViewController6");
//				NavigationController.PushViewController (pvc, true);
//			};
		}
	}
}
