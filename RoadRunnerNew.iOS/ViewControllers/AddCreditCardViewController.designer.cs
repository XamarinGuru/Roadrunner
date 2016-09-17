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
	[Register ("AddCreditCardViewController")]
	partial class AddCreditCardViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnScaneCard { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnSubmit { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		RoadRunnerNew.iOS.View.PaddingTextField edtCardHolderName { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField edtCCV { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		RoadRunnerNew.iOS.View.PaddingTextField edtCreditCardData { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		RoadRunnerNew.iOS.View.PaddingTextField edtCreditDate { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField edtZipCode { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView imgAddCard { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnScaneCard != null) {
				btnScaneCard.Dispose ();
				btnScaneCard = null;
			}
			if (btnSubmit != null) {
				btnSubmit.Dispose ();
				btnSubmit = null;
			}
			if (edtCardHolderName != null) {
				edtCardHolderName.Dispose ();
				edtCardHolderName = null;
			}
			if (edtCCV != null) {
				edtCCV.Dispose ();
				edtCCV = null;
			}
			if (edtCreditCardData != null) {
				edtCreditCardData.Dispose ();
				edtCreditCardData = null;
			}
			if (edtCreditDate != null) {
				edtCreditDate.Dispose ();
				edtCreditDate = null;
			}
			if (edtZipCode != null) {
				edtZipCode.Dispose ();
				edtZipCode = null;
			}
			if (imgAddCard != null) {
				imgAddCard.Dispose ();
				imgAddCard = null;
			}
		}
	}
}
