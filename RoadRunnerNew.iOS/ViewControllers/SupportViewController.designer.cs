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
	[Register ("SupportViewController")]
	partial class SupportViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnCallToRR { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnCallToRRTollFree { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnEmailToRR { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnCallToRR != null) {
				btnCallToRR.Dispose ();
				btnCallToRR = null;
			}
			if (btnCallToRRTollFree != null) {
				btnCallToRRTollFree.Dispose ();
				btnCallToRRTollFree = null;
			}
			if (btnEmailToRR != null) {
				btnEmailToRR.Dispose ();
				btnEmailToRR = null;
			}
		}
	}
}
