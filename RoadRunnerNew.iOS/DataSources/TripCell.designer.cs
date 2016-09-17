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
	[Register ("TripCell")]
	partial class TripCell
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnCancelTrip { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnEditTrip { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnReservationID { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblCellDropoffLocation { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblCellPickupLocation { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblDropoffDate { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblPickupDate { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblTitleDate { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnCancelTrip != null) {
				btnCancelTrip.Dispose ();
				btnCancelTrip = null;
			}
			if (btnEditTrip != null) {
				btnEditTrip.Dispose ();
				btnEditTrip = null;
			}
			if (btnReservationID != null) {
				btnReservationID.Dispose ();
				btnReservationID = null;
			}
			if (lblCellDropoffLocation != null) {
				lblCellDropoffLocation.Dispose ();
				lblCellDropoffLocation = null;
			}
			if (lblCellPickupLocation != null) {
				lblCellPickupLocation.Dispose ();
				lblCellPickupLocation = null;
			}
			if (lblDropoffDate != null) {
				lblDropoffDate.Dispose ();
				lblDropoffDate = null;
			}
			if (lblPickupDate != null) {
				lblPickupDate.Dispose ();
				lblPickupDate = null;
			}
			if (lblTitleDate != null) {
				lblTitleDate.Dispose ();
				lblTitleDate = null;
			}
		}
	}
}
