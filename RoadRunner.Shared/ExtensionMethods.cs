using System;

namespace RoadRunner.Shared
{
	public static class ExtensionMethods
	{
		public static string FormatAsSQLDate(this DateTime dateTime)
		{
			return dateTime.ToString ("yyyy-MM-dd HH:mm");
		}

		public static string FormatAsSQLDate(this string dateString)
		{
			if (string.IsNullOrEmpty (dateString))
				return string.Empty;
			
			return DateTime.Parse (dateString).ToString ("yyyy-MM-dd HH:mm");
		}
	}
}

