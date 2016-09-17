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
	[Register ("BillAddCell")]
	partial class BillAddCell
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnAddBill { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnAddBill != null) {
				btnAddBill.Dispose ();
				btnAddBill = null;
			}
		}
	}
}
