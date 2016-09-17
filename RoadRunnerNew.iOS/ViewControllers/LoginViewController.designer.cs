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
	[Register ("LoginViewController")]
	partial class LoginViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton _facebookLoginButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton _googleLoginButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton _linkedinLoginButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnCallRoadrunner { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnCreateAccount { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnLogin { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField passwordTextField { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField usernameTextField { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (_facebookLoginButton != null) {
				_facebookLoginButton.Dispose ();
				_facebookLoginButton = null;
			}
			if (_googleLoginButton != null) {
				_googleLoginButton.Dispose ();
				_googleLoginButton = null;
			}
			if (_linkedinLoginButton != null) {
				_linkedinLoginButton.Dispose ();
				_linkedinLoginButton = null;
			}
			if (btnCallRoadrunner != null) {
				btnCallRoadrunner.Dispose ();
				btnCallRoadrunner = null;
			}
			if (btnCreateAccount != null) {
				btnCreateAccount.Dispose ();
				btnCreateAccount = null;
			}
			if (btnLogin != null) {
				btnLogin.Dispose ();
				btnLogin = null;
			}
			if (passwordTextField != null) {
				passwordTextField.Dispose ();
				passwordTextField = null;
			}
			if (usernameTextField != null) {
				usernameTextField.Dispose ();
				usernameTextField = null;
			}
		}
	}
}
