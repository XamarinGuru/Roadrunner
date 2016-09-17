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
	[Register ("ScheduleARideViewController6")]
	partial class ThankyouViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnDone { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnFBShare { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnGPShare { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnLIShare { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnReturnTrip { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField edtThankEmail { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView emailView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblDepartureInstructions { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblReservation { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnDone != null) {
				btnDone.Dispose ();
				btnDone = null;
			}
			if (btnFBShare != null) {
				btnFBShare.Dispose ();
				btnFBShare = null;
			}
			if (btnGPShare != null) {
				btnGPShare.Dispose ();
				btnGPShare = null;
			}
			if (btnLIShare != null) {
				btnLIShare.Dispose ();
				btnLIShare = null;
			}
			if (btnReturnTrip != null) {
				btnReturnTrip.Dispose ();
				btnReturnTrip = null;
			}
			if (edtThankEmail != null) {
				edtThankEmail.Dispose ();
				edtThankEmail = null;
			}
			if (emailView != null) {
				emailView.Dispose ();
				emailView = null;
			}
			if (lblDepartureInstructions != null) {
				lblDepartureInstructions.Dispose ();
				lblDepartureInstructions = null;
			}
			if (lblReservation != null) {
				lblReservation.Dispose ();
				lblReservation = null;
			}
		}
	}
}
