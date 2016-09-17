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
    [Register ("TermsOFServiceViewController")]
    partial class TermsOFServiceViewController
    {
        [Outlet]
        UIKit.UITextView tosTextView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView mainView2 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIScrollView scrollView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel txtBody { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel txtDescription2 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel txtHeader { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel txtTitle2 { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (mainView2 != null) {
                mainView2.Dispose ();
                mainView2 = null;
            }

            if (scrollView != null) {
                scrollView.Dispose ();
                scrollView = null;
            }

            if (txtBody != null) {
                txtBody.Dispose ();
                txtBody = null;
            }

            if (txtDescription2 != null) {
                txtDescription2.Dispose ();
                txtDescription2 = null;
            }

            if (txtHeader != null) {
                txtHeader.Dispose ();
                txtHeader = null;
            }

            if (txtTitle2 != null) {
                txtTitle2.Dispose ();
                txtTitle2 = null;
            }
        }
    }
}