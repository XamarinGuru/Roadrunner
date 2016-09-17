using System;

using Foundation;
using UIKit;
using RoadRunner.Shared;
using RoadRunnerNew.iOS.Extension;

namespace RoadRunnerNew.iOS
{
	public partial class CompletedTripCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("CompletedTripCell");
		public static readonly UINib Nib;

		private GetMyBookedReservationsResponseReservation cellData;
		private UIViewController m_owner;

		static CompletedTripCell ()
		{
			Nib = UINib.FromName ("CompletedTripCell", NSBundle.MainBundle);
		}

		public CompletedTripCell (IntPtr handle) : base (handle)
		{

		}

		public void SetCell(GetMyBookedReservationsResponseReservation ItemData, UIViewController owner)
		{
			cellData = ItemData;
			m_owner = owner;

			DateTime pDate = DateTime.Parse(ItemData.createdOn);
			string[] arrDate = pDate.GetDateTimeFormats ();

			lblTitleDate.Text = arrDate[8];

			btnReservationID.SetCustomRIDButton ();
			string strReservationID = String.Format("{1} {2}", Environment.NewLine, "  Reservation #: ", ItemData.ReservationID);
			btnReservationID.SetTitle (strReservationID, UIControlState.Normal);

			if (ItemData.pickUpTime == "")
				pDate = DateTime.Now;
			else
				pDate = DateTime.Parse(ItemData.pickUpTime);
			arrDate = pDate.GetDateTimeFormats ();

			lblPickupDate.Text = String.Format("{1} {2} {3}", Environment.NewLine, "Pick up at ", arrDate[106], " from");

			var pCity = ItemData.Pickcity.IsMatchRegex
				? String.Format("{1} {2} {3} {4} {5} {6}", Environment.NewLine, ItemData.Pickcity.stNum, ItemData.Pickcity.street, ItemData.Pickcity.complex, ItemData.Pickcity.unit,
					ItemData.Pickcity.city, ItemData.Pickcity.zip)
				: String.Format("{1}", Environment.NewLine, ItemData.Pickcity.raw);

			lblCellPickupLocation.Text = pCity;

			if (ItemData.dropOffTime == "")
				pDate = DateTime.Now;
			else
				pDate = DateTime.Parse(ItemData.dropOffTime);
			pDate = DateTime.Parse(ItemData.dropOffTime);
			arrDate = pDate.GetDateTimeFormats ();

			lblDropoffDate.Text = String.Format("{1} {2} {3}", Environment.NewLine, "Take off at ", arrDate[106], " from");

			var dCity = ItemData.Dropoff.IsMatchRegex
				? String.Format("{1} {2} {3} {4} {5} {6}", Environment.NewLine, ItemData.Dropoff.stNum, ItemData.Dropoff.street, ItemData.Dropoff.complex, ItemData.Dropoff.unit, ItemData.Dropoff.city,
					ItemData.Dropoff.zip)
				: String.Format("{1}", Environment.NewLine, ItemData.Dropoff.raw);

			lblCellDropoffLocation.Text = dCity;
		}
	}
}
