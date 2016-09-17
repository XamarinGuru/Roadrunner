using CoreGraphics;
using Foundation;
using UIKit;

namespace RoadRunnerNew.iOS
{
	public static class TextHelper
	{
		public static UIFont GetRalewayRegularFont(float size)
		{
			return UIFont.FromName ("Raleway-Regular", size);
		}

		public static UIFont GetRalewayBoldFont(float size)
		{
			return UIFont.FromName ("Raleway-Bold", size);
		}

		public static UIFont GetRalewaySemiboldFont(float size)
		{
			return UIFont.FromName ("Raleway-SemiBold", size);
		}

		/// <summary>
		/// Can't get attributed text to work using technique in http://developer.xamarin.com/recipes/ios/standard_controls/text_field/style_text/.
		/// So this seems to work instead
		/// </summary>
		public static void SetAttributedText(this UILabel label, string text, UIColor textColor, UIFont font){
			SetAttributedText (label, text, textColor, null, font);
		}

		/// <summary>
		/// Can't get attributed text to work using technique in http://developer.xamarin.com/recipes/ios/standard_controls/text_field/style_text/.
		/// So this seems to work instead
		/// </summary>
		public static void SetAttributedText(this UILabel label, string text, UIColor textColor, UIColor backgroundColor, UIFont font){
			label.Text = text;

			if (textColor != null) {
				label.TextColor = textColor;
			}

			if (backgroundColor != null) {
				label.BackgroundColor = backgroundColor;
			}

			if (font != null) {
				label.Font = font;
			}
		}

		public static CGSize SizeForText(float width, string text, UIFont font)
		{
			CGSize size;
			using (NSString str = new NSString(text))
			{
				size = str.StringSize (font, width, UILineBreakMode.WordWrap);
			}
			return size;
		}
	}
}

