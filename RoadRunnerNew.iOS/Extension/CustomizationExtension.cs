using System;
using UIKit;
using CoreGraphics;
using RoadRunnerNew.iOS.View;

namespace RoadRunnerNew.iOS.Extension
{
	public static class CustomizationExtension
	{
		public static void Customize(this UINavigationItem navItem, UINavigationController navController)
		{
			var leftButton = new UIButton(new CGRect(0, 0, 15, 25));
			leftButton.SetImage(UIImage.FromFile("icon_back.png"), UIControlState.Normal);
			leftButton.TouchUpInside += (sender, e) => navController.PopViewController(true);
			navItem.LeftBarButtonItem = new UIBarButtonItem(leftButton);

			var rightButton = new UIButton(new CGRect(0, 0, 40, 40));
			rightButton.SetImage(UIImage.FromFile("icon_mark.png"), UIControlState.Normal);
			//rightButton.TouchUpInside += (sender, e) => navController.PopViewController(true);
			navItem.RightBarButtonItem = new UIBarButtonItem(rightButton);
		}

		public static void SetAttributesForTextField(this PaddingTextField field) {
			field.Layer.BackgroundColor =  new UIColor ((nfloat)(244.0 / 255.0), (nfloat)(244.0 / 255.0), (nfloat)(244.0 / 255.0), (nfloat)1.0).CGColor; 
			field.Layer.BorderColor = new UIColor ((nfloat)(215.0 / 255.0), (nfloat)(215.0 / 255.0), (nfloat)(215.0 / 255.0), (nfloat)1.0).CGColor; 
			field.Layer.BorderWidth = (nfloat)0.5;
			//field.EdgeInsets = new UIEdgeInsets(0, 10, 0, 10);
		}

		public static void SetCustomButton(this UIButton button) {
			button.Layer.CornerRadius = 6;
			button.Layer.BorderColor = new UIColor ((nfloat)(0 / 255.0), (nfloat)(0 / 255.0), (nfloat)(0 / 255.0), (nfloat)1.0).CGColor; 
			button.Layer.BorderWidth = 1.0f;
		}

		public static void SetCustomRIDButton(this UIButton button) {
			button.Layer.CornerRadius = 4;
		}

		public static void SetCustomView(this UIView button) {
			button.Layer.CornerRadius = 5;
			button.Layer.BorderColor = new UIColor ((nfloat)(0 / 255.0), (nfloat)(0 / 255.0), (nfloat)(0 / 255.0), (nfloat)1.0).CGColor; 
			button.Layer.BorderWidth = 1.0f;
		}
	}
}

