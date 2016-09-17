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

namespace RoadRunnerNew.iOS.View
{
    [Register ("MapMarkerView")]
    partial class MapMarkerView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgPin { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblDistance { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblInfo { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (imgPin != null) {
                imgPin.Dispose ();
                imgPin = null;
            }

            if (lblDistance != null) {
                lblDistance.Dispose ();
                lblDistance = null;
            }

            if (lblInfo != null) {
                lblInfo.Dispose ();
                lblInfo = null;
            }
        }
    }
}