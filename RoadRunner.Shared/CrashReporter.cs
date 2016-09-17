using System;
using System.Collections.Generic;
using Xamarin;

namespace RoadRunner.Shared
{
	public static class CrashReporter
	{
		public static void Report(Exception ex, Dictionary<string, string> values = null)
		{
			Insights.Report (ex, values);
		}
	}
}

