using System;

namespace RoadRunner.Shared
{
    public class GetPickupForReservationForAndroidResponse
	{
		public String Result { get; set; }
        public String Message { get; set; }
        public String Lat { get; set; }
        public String Long { get; set; }
		public String angle { get; set; }
		public String speed { get; set; }
        public String Dr { get; set; }
        public String Tr { get; set; }
        public String Eta { get; set; }
        public String Dist { get; set; }
        public String Trtime { get; set; }
        public String Res { get; set; }
 
		public override string ToString()
		{
			return
				String.Format(
					"{0}Result = {1}{0}Message = {10}{0}Lat = {2}{0}Long = {3}{0}Dr = {4}{0}Tr = {5}{0}Eta = {6}{0}Dist = {7}{0}Trtime = {8}{0}Res = {9}{0}angle = {11}{0}speed = {12}{0}",
					Environment.NewLine, Result, Lat, Long, Dr, Tr, Eta, Dist, Trtime, Res, Message, angle, speed);
		}
	}
}
