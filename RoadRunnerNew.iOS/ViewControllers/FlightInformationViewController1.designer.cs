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
	[Register ("FlightInformationViewController1")]
	partial class FlightInformationViewController1
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnScheduleARide3 { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField editAirportDropOffLocation { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField edtAirportPickUpLocation { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField edtPickUpAirlines { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField edtPickUpFlight { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField edtPickUpFlightTime { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField edtPickUpFlightType { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField edtRequestedArrivalTimeAndDate { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnScheduleARide3 != null) {
				btnScheduleARide3.Dispose ();
				btnScheduleARide3 = null;
			}
			if (editAirportDropOffLocation != null) {
				editAirportDropOffLocation.Dispose ();
				editAirportDropOffLocation = null;
			}
			if (edtAirportPickUpLocation != null) {
				edtAirportPickUpLocation.Dispose ();
				edtAirportPickUpLocation = null;
			}
			if (edtPickUpAirlines != null) {
				edtPickUpAirlines.Dispose ();
				edtPickUpAirlines = null;
			}
			if (edtPickUpFlight != null) {
				edtPickUpFlight.Dispose ();
				edtPickUpFlight = null;
			}
			if (edtPickUpFlightTime != null) {
				edtPickUpFlightTime.Dispose ();
				edtPickUpFlightTime = null;
			}
			if (edtPickUpFlightType != null) {
				edtPickUpFlightType.Dispose ();
				edtPickUpFlightType = null;
			}
			if (edtRequestedArrivalTimeAndDate != null) {
				edtRequestedArrivalTimeAndDate.Dispose ();
				edtRequestedArrivalTimeAndDate = null;
			}
		}
	}
}
