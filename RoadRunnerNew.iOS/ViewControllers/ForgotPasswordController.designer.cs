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
	[Register ("ForgotPasswordController")]
	partial class ForgotPasswordController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnRestorePassword { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField edtEmailAdress { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnRestorePassword != null) {
				btnRestorePassword.Dispose ();
				btnRestorePassword = null;
			}
			if (edtEmailAdress != null) {
				edtEmailAdress.Dispose ();
				edtEmailAdress = null;
			}
		}
	}
}
