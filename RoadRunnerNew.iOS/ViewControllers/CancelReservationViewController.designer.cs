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
	[Register ("CancelReservationViewController")]
	partial class CancelReservationViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnCancelReservation { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnDetailReservationID { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblDetailDropoffDate { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblDetailDropoffLocation { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblDetailPickupDate { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblDetailPickupLocation { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblDetailTripTitle { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnCancelReservation != null) {
				btnCancelReservation.Dispose ();
				btnCancelReservation = null;
			}
			if (btnDetailReservationID != null) {
				btnDetailReservationID.Dispose ();
				btnDetailReservationID = null;
			}
			if (lblDetailDropoffDate != null) {
				lblDetailDropoffDate.Dispose ();
				lblDetailDropoffDate = null;
			}
			if (lblDetailDropoffLocation != null) {
				lblDetailDropoffLocation.Dispose ();
				lblDetailDropoffLocation = null;
			}
			if (lblDetailPickupDate != null) {
				lblDetailPickupDate.Dispose ();
				lblDetailPickupDate = null;
			}
			if (lblDetailPickupLocation != null) {
				lblDetailPickupLocation.Dispose ();
				lblDetailPickupLocation = null;
			}
			if (lblDetailTripTitle != null) {
				lblDetailTripTitle.Dispose ();
				lblDetailTripTitle = null;
			}
		}
	}
}
