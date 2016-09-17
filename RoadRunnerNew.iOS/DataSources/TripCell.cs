using System;

using Foundation;
using UIKit;
using RoadRunner.Shared;
using RoadRunnerNew.iOS.Extension;

namespace RoadRunnerNew.iOS
{
	public partial class TripCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("TripCell");
		public static readonly UINib Nib;

		private GetMyBookedReservationsResponseReservation cellData;
		private UIViewController m_owner;

		static TripCell ()
		{
			Nib = UINib.FromName ("TripCell", NSBundle.MainBundle);
		}

		public TripCell (IntPtr handle) : base (handle)
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

			btnEditTrip.SetCustomButton();
			btnEditTrip.TouchUpInside -= GoToEditReservationViewController;
			btnEditTrip.TouchUpInside += GoToEditReservationViewController;

			btnCancelTrip.SetCustomButton ();
			btnCancelTrip.TouchUpInside -= GoToCancelReservationViewController;
			btnCancelTrip.TouchUpInside += GoToCancelReservationViewController;

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
			arrDate = pDate.GetDateTimeFormats ();

			lblDropoffDate.Text = String.Format("{1} {2} {3}", Environment.NewLine, "Take off at ", arrDate[106], " from");

			var dCity = ItemData.Dropoff.IsMatchRegex
				? String.Format("{1} {2} {3} {4} {5} {6}", Environment.NewLine, ItemData.Dropoff.stNum, ItemData.Dropoff.street, ItemData.Dropoff.complex, ItemData.Dropoff.unit, ItemData.Dropoff.city,
					ItemData.Dropoff.zip)
				: String.Format("{1}", Environment.NewLine, ItemData.Dropoff.raw);

			lblCellDropoffLocation.Text = dCity;




//			if (Facade.Instance.CurrentRide.IsPickUpLocationAirport) {
//				lblPickupDate.Text = string.Format ("Pickup at {0} from", Facade.Instance.CurrentRide.PickUpFlightTime);
//				lblPickUpAddress.Text = Facade.Instance.CurrentRide.PickUpLocationName;
//				lblDropoffDate.Text = Facade.Instance.CurrentRide.RequestedArrivalTimeAndDate;
//				lblDropOffAddress.Text = Facade.Instance.CurrentRide.IsDropOffLocationAirport ? Facade.Instance.CurrentRide.DropOffLocationName : Facade.Instance.CurrentRide.DropOffLocation;
//			} else if (Facade.Instance.CurrentRide.IsDropOffLocationAirport) {
//				lblPickupDate.Text = Facade.Instance.CurrentRide.RequestedArrivalTimeAndDate;
//				lblPickUpAddress.Text = Facade.Instance.CurrentRide.PickUpLocation;
//				lblDropoffDate.Text = string.Format ("Dropoff at {0} from", Facade.Instance.CurrentRide.DropOffFlightTime);
//				lblDropOffAddress.Text = Facade.Instance.CurrentRide.DropOffLocationName;
//			} else {
//				lblPickupDate.Text = Facade.Instance.CurrentRide.RequestedArrivalTimeAndDate;
//				lblPickUpAddress.Text = Facade.Instance.CurrentRide.PickUpLocation;
//				lblDropoffDate.Text = Facade.Instance.CurrentRide.RequestedArrivalTimeAndDate;
//				lblDropOffAddress.Text = Facade.Instance.CurrentRide.DropOffLocation;
//			}
//			iconPickup.Image = Facade.Instance.CurrentRide.IsPickUpLocationAirport ? UIImage.FromBundle (@"icon_default_flight.png") : UIImage.FromBundle (@"icon_home.png");
//			iconDropoff.Image = Facade.Instance.CurrentRide.IsDropOffLocationAirport ? UIImage.FromBundle (@"icon_default_flight.png") : UIImage.FromBundle (@"icon_home.png");

		}

		internal void GoToEditReservationViewController (object sender, EventArgs ea) {
			var sb = UIStoryboard.FromName("MainStoryboard", null);
			var reservationDetails = (EditReservationViewController)sb.InstantiateViewController("EditReservationViewController");
			reservationDetails.item = cellData;
			m_owner.NavigationController.PushViewController(reservationDetails, true);
		}

		internal void GoToCancelReservationViewController (object sender, EventArgs ea) {
			var sb = UIStoryboard.FromName("MainStoryboard", null);
			var reservationDetails = (CancelReservationViewController)sb.InstantiateViewController("CancelReservationViewController");
			reservationDetails.item = cellData;
			m_owner.NavigationController.PushViewController(reservationDetails, true);
		}
	}
}
