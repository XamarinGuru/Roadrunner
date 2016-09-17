using System;

namespace RoadRunner.Shared
{
	

	public class GetConfirmationByDetailsNewResponse
	{
		public String Reservation { get; set; }

		public String ResWithOutEncode { get; set; }

		public override string ToString ()
		{
			return String.Format ("{0}Reservation = {1}{0}ResWithOutEncode = {2}{0}",
				Environment.NewLine, Reservation, ResWithOutEncode);
		}
	}

}


