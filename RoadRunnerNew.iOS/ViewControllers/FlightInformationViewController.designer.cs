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
	[Register ("ScheduleARideViewController2")]
	partial class FlightInformationViewController
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
		UITextField edtDropOffAirlines { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField edtDropOffFlight { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField edtDropOffFlightTime { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField edtDropOffFlightType { get; set; }

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
			if (edtDropOffAirlines != null) {
				edtDropOffAirlines.Dispose ();
				edtDropOffAirlines = null;
			}
			if (edtDropOffFlight != null) {
				edtDropOffFlight.Dispose ();
				edtDropOffFlight = null;
			}
			if (edtDropOffFlightTime != null) {
				edtDropOffFlightTime.Dispose ();
				edtDropOffFlightTime = null;
			}
			if (edtDropOffFlightType != null) {
				edtDropOffFlightType.Dispose ();
				edtDropOffFlightType = null;
			}
			if (edtRequestedArrivalTimeAndDate != null) {
				edtRequestedArrivalTimeAndDate.Dispose ();
				edtRequestedArrivalTimeAndDate = null;
			}
		}
	}
}
