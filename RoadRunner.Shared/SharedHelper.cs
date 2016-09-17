using System;
using Connectivity.Plugin;

namespace RoadRunner.Shared
{
	public static class SharedHelper
	{
		public static bool IsConnected
		{
			get 
			{
				return CrossConnectivity.Current.IsConnected;
			}
		}

		public static string GetTrackingIdentifier(string category, string description)
		{
			return string.Format("{0} - {1}", category, description);
		}
	}
}

