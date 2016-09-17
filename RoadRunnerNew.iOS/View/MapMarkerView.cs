using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using ObjCRuntime;

namespace RoadRunnerNew.iOS.View
{
	partial class MapMarkerView : UIView
	{
		public MapMarkerView (IntPtr handle) : base (handle)
		{
		}

		public static MapMarkerView Create()
		{
			var arr = NSBundle.MainBundle.LoadNib ("MapMarkerView", null, null);
			var v = Runtime.GetNSObject<MapMarkerView> (arr.ValueAt(0));
			return v;
		}

		public override void AwakeFromNib(){
			lblInfo.Text = "hello from the SomeView class";
		}

		public void SetView(string info, string distance)
		{
			lblInfo.Text = info;
			lblDistance.Text = distance;
		}
	}
}
