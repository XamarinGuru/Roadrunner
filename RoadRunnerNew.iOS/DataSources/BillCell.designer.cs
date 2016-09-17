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
	[Register ("BillCell")]
	partial class BillCell
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView imgBill { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblBillDate { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblBillID { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (imgBill != null) {
				imgBill.Dispose ();
				imgBill = null;
			}
			if (lblBillDate != null) {
				lblBillDate.Dispose ();
				lblBillDate = null;
			}
			if (lblBillID != null) {
				lblBillID.Dispose ();
				lblBillID = null;
			}
		}
	}
}
