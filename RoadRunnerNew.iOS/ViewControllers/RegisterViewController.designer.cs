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
	[Register ("RegisterViewController")]
	partial class RegisterViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnConfirmRegistration { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISwitch chkIAgree { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField edtEmailAddress { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField edtFirstName { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField edtLastName { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField edtMobileNumber { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField edtPassword { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField edtRepeatPassword { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnConfirmRegistration != null) {
				btnConfirmRegistration.Dispose ();
				btnConfirmRegistration = null;
			}
			if (chkIAgree != null) {
				chkIAgree.Dispose ();
				chkIAgree = null;
			}
			if (edtEmailAddress != null) {
				edtEmailAddress.Dispose ();
				edtEmailAddress = null;
			}
			if (edtFirstName != null) {
				edtFirstName.Dispose ();
				edtFirstName = null;
			}
			if (edtLastName != null) {
				edtLastName.Dispose ();
				edtLastName = null;
			}
			if (edtMobileNumber != null) {
				edtMobileNumber.Dispose ();
				edtMobileNumber = null;
			}
			if (edtPassword != null) {
				edtPassword.Dispose ();
				edtPassword = null;
			}
			if (edtRepeatPassword != null) {
				edtRepeatPassword.Dispose ();
				edtRepeatPassword = null;
			}
		}
	}
}
