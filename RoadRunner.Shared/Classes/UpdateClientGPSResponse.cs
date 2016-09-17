using System;
namespace RoadRunner.Shared
{
	public class UpdateClientGPSResponse
	{
		public String Result { get; set; }
		public String markers { get; set; }

		public override string ToString()
		{
			return String.Format("{0}Result = {1}{0}" +
								 "markers = {2}{0}",
				Environment.NewLine, Result, markers);
		}
	}
}

