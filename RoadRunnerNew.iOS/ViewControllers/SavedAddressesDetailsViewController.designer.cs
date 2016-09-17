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
	[Register ("SavedAddressesDetailsViewController")]
	partial class SavedAddressesDetailsViewController
	{
		[Outlet]
		UIKit.UILabel cityLbl { get; set; }

		[Outlet]
		UIKit.UILabel comment { get; set; }

		[Outlet]
		UIKit.UILabel pickUpAddressLbl { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollView { get; set; }

		[Outlet]
		UIKit.UILabel streetLblb { get; set; }

		[Outlet]
		UIKit.UITextView twMainTextView { get; set; }

		[Outlet]
		UIKit.UILabel zipLbl { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblCity { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblComment { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblPickUpAddress { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblStreet { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblZip { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (lblCity != null) {
				lblCity.Dispose ();
				lblCity = null;
			}
			if (lblComment != null) {
				lblComment.Dispose ();
				lblComment = null;
			}
			if (lblPickUpAddress != null) {
				lblPickUpAddress.Dispose ();
				lblPickUpAddress = null;
			}
			if (lblStreet != null) {
				lblStreet.Dispose ();
				lblStreet = null;
			}
			if (lblZip != null) {
				lblZip.Dispose ();
				lblZip = null;
			}
		}
	}
}
