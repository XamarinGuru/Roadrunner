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
	[Register ("ProfileViewController")]
	partial class ProfileViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnChangePassword { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnConfirmRegistration { get; set; }

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
		UISwitch isSMS { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnChangePassword != null) {
				btnChangePassword.Dispose ();
				btnChangePassword = null;
			}
			if (btnConfirmRegistration != null) {
				btnConfirmRegistration.Dispose ();
				btnConfirmRegistration = null;
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
			if (isSMS != null) {
				isSMS.Dispose ();
				isSMS = null;
			}
		}
	}
}
