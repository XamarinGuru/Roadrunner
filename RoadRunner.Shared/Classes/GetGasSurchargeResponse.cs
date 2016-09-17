using System;

namespace RoadRunner.Shared
{
    public class GetGasSurchargeResponse
	{
		public String Title { get; set; }
		public String Message { get; set; }
        public String Surcharge { get; set; }
        
		public override string ToString()
		{
            return String.Format("{0}Title = {1}{0}Message = {2}{0}Surcharge = {3}{0}", Environment.NewLine, Title, Message, Surcharge);
		}
	}
}
