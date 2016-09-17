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
    [Register ("MapViewController")]
    partial class MapViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView bottomView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnMapAddCreditCard { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnMapSelectPromo { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnPickupLocation { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnReadyPickup { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnScheduleARide { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgMapCard { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView mMapView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISearchDisplayController searchDisplayController { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtBtnReadyPickup { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtMapCard { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtMapPromo { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtSearchLocation { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (bottomView != null) {
                bottomView.Dispose ();
                bottomView = null;
            }

            if (btnMapAddCreditCard != null) {
                btnMapAddCreditCard.Dispose ();
                btnMapAddCreditCard = null;
            }

            if (btnMapSelectPromo != null) {
                btnMapSelectPromo.Dispose ();
                btnMapSelectPromo = null;
            }

            if (btnPickupLocation != null) {
                btnPickupLocation.Dispose ();
                btnPickupLocation = null;
            }

            if (btnReadyPickup != null) {
                btnReadyPickup.Dispose ();
                btnReadyPickup = null;
            }

            if (btnScheduleARide != null) {
                btnScheduleARide.Dispose ();
                btnScheduleARide = null;
            }

            if (imgMapCard != null) {
                imgMapCard.Dispose ();
                imgMapCard = null;
            }

            if (mMapView != null) {
                mMapView.Dispose ();
                mMapView = null;
            }

            if (searchDisplayController != null) {
                searchDisplayController.Dispose ();
                searchDisplayController = null;
            }

            if (txtBtnReadyPickup != null) {
                txtBtnReadyPickup.Dispose ();
                txtBtnReadyPickup = null;
            }

            if (txtMapCard != null) {
                txtMapCard.Dispose ();
                txtMapCard = null;
            }

            if (txtMapPromo != null) {
                txtMapPromo.Dispose ();
                txtMapPromo = null;
            }

            if (txtSearchLocation != null) {
                txtSearchLocation.Dispose ();
                txtSearchLocation = null;
            }
        }
    }
}