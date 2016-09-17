using System;
using UIKit;

namespace RoadRunnerNew.iOS.Extension
{
	public static class UIButtonExtensions
	{

		public static void ApplySolidBlueBackground(this UIButton button)
		{
			button.SetBackgroundImage(CreateResizableImage("button_blue.png"), UIControlState.Normal);
		}

		static UIImage CreateResizableImage(string image)
		{
			UIEdgeInsets insets = new UIEdgeInsets(0, 5, 0, 5);
			UIImage originalImage = UIImage.FromBundle(image);
			return originalImage.CreateResizableImage(insets);
		}
	}
}

