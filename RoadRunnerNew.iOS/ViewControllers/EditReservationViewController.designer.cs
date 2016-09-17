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
	[Register ("EditReservationViewController")]
	partial class EditReservationViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnDetailReservationID1 { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnEditReservation { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblDetailDropoffDate1 { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblDetailDropoffLocation1 { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblDetailPickupDate1 { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblDetailPickupLocation1 { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblDetailTripTitle1 { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnDetailReservationID1 != null) {
				btnDetailReservationID1.Dispose ();
				btnDetailReservationID1 = null;
			}
			if (btnEditReservation != null) {
				btnEditReservation.Dispose ();
				btnEditReservation = null;
			}
			if (lblDetailDropoffDate1 != null) {
				lblDetailDropoffDate1.Dispose ();
				lblDetailDropoffDate1 = null;
			}
			if (lblDetailDropoffLocation1 != null) {
				lblDetailDropoffLocation1.Dispose ();
				lblDetailDropoffLocation1 = null;
			}
			if (lblDetailPickupDate1 != null) {
				lblDetailPickupDate1.Dispose ();
				lblDetailPickupDate1 = null;
			}
			if (lblDetailPickupLocation1 != null) {
				lblDetailPickupLocation1.Dispose ();
				lblDetailPickupLocation1 = null;
			}
			if (lblDetailTripTitle1 != null) {
				lblDetailTripTitle1.Dispose ();
				lblDetailTripTitle1 = null;
			}
		}
	}
}
