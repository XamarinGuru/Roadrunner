using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using Xamarin;
using RoadRunner.Shared;

namespace RoadRunnerNew.iOS
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main (string[] args)
		{
			//#if DEBUG
			//   Insights.Initialize (Insights.DebugModeKey);
			//#else
			//   Xamarin.Insights.Initialize ("37f8347b1aa3979ce023e220274aa739d6ea73ba");
			//#endif

			Xamarin.Insights.Initialize ("37f8347b1aa3979ce023e220274aa739d6ea73ba");

			Insights.HasPendingCrashReport += (sender, isStartupCrash) => {
				if (isStartupCrash) {
					Insights.PurgePendingCrashReports ().Wait ();
				}
			};
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}
