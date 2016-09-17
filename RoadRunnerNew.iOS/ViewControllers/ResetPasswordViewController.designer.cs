// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using RoadRunnerNew.iOS.View;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace RoadRunnerNew.iOS
{
	[Register ("ResetPasswordViewController")]
	partial class ResetPasswordViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnCancelResetPassword { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnSubmit { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField edtConfNewPassword { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField edtNewPassword { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField edtOldPassword { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnCancelResetPassword != null) {
				btnCancelResetPassword.Dispose ();
				btnCancelResetPassword = null;
			}
			if (btnSubmit != null) {
				btnSubmit.Dispose ();
				btnSubmit = null;
			}
			if (edtConfNewPassword != null) {
				edtConfNewPassword.Dispose ();
				edtConfNewPassword = null;
			}
			if (edtNewPassword != null) {
				edtNewPassword.Dispose ();
				edtNewPassword = null;
			}
			if (edtOldPassword != null) {
				edtOldPassword.Dispose ();
				edtOldPassword = null;
			}
		}
	}
}
