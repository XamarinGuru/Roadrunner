using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace RoadRunner.Shared
{
	public static class UserTrackingReporter
	{
		public static void TrackUser(string category, string description, Dictionary<string, string> extraValues = null)
		{
			#if DEBUG
			var sb = new StringBuilder ();

			if (extraValues != null) {				
				foreach (var item in extraValues) {
					sb.AppendFormat("Key={0} value={1} | ", item.Key, item.Value);
				}
			}
			var extras = sb.ToString();
			if(string.IsNullOrEmpty(extras))
			{
				Debug.WriteLine ("category={0} description={1}",category, description);
			}
			else{
				Debug.WriteLine ("category={0} description={1} extraValues={2}",category, description, extras);
			}
			#endif
			Xamarin.Insights.Track (SharedHelper.GetTrackingIdentifier (category, description), extraValues);
		}

	    public static void DebugReport(string message)
	    {
            #if DEBUG
            Debug.WriteLine(message);
            #endif
	    }
	}
}

