using System;
using UIKit;

namespace RoadRunnerNew.iOS
{
	public partial class MainNavigationController : UINavigationController
	{
		public MainNavigationController (IntPtr handle) : base (handle)
		{
		}

		public MainNavigationController (UIViewController rootViewController) : base (rootViewController){
			
		}
	}
}
