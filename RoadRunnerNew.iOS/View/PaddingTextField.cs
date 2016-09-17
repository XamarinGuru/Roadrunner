using System;
using UIKit;
using System.Drawing;
using CoreGraphics;
using Foundation;

namespace RoadRunnerNew.iOS.View
{
	[Register("PaddingTextField")]
	public partial class PaddingTextField : UITextField
	{
		public UIEdgeInsets EdgeInsets { get; set; }

		public PaddingTextField()
		{
			EdgeInsets = UIEdgeInsets.Zero;
		}
		public PaddingTextField(IntPtr intPtr) : base(intPtr)
		{
			EdgeInsets = UIEdgeInsets.Zero;
		}

		public override CGRect TextRect(CGRect forBounds)
		{
			return base.TextRect(InsetRect(forBounds, EdgeInsets));
		}

		public override CGRect EditingRect(CGRect forBounds)
		{
			return base.EditingRect(InsetRect(forBounds, EdgeInsets));
		}

		// Workaround until this method is available in Xamarin.iOS
		public static CGRect InsetRect(CGRect rect, UIEdgeInsets insets)
		{
			return new CGRect(rect.X + insets.Left,
				rect.Y + insets.Top,
				rect.Width - insets.Left - insets.Right,
				rect.Height - insets.Top - insets.Bottom);
		}
	}
}

