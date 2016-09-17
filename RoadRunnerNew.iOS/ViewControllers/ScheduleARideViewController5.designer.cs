// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace RoadRunnerNew.iOS
{
	[Register ("ScheduleARideViewController5")]
	partial class ScheduleARideViewController5
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblArrival { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblChildPolicy { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblContactUS { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView mainView2 { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIScrollView myScrollView { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (lblArrival != null) {
				lblArrival.Dispose ();
				lblArrival = null;
			}
			if (lblChildPolicy != null) {
				lblChildPolicy.Dispose ();
				lblChildPolicy = null;
			}
			if (lblContactUS != null) {
				lblContactUS.Dispose ();
				lblContactUS = null;
			}
			if (mainView2 != null) {
				mainView2.Dispose ();
				mainView2 = null;
			}
			if (myScrollView != null) {
				myScrollView.Dispose ();
				myScrollView = null;
			}
		}
	}
}
