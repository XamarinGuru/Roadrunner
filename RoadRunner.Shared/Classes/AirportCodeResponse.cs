using System;
using System.Xml.Serialization;

namespace RoadRunner.Shared.Classes
{
    public class AirportCodeResponse
    {
		public String TITLE { get; set; }
		public String MSG { get; set; }
		public String IATAAirportCode { get; set; }

		public override string ToString()
		{
			return String.Format ("{0}TITLE = {1}{0}MSG = {2}{0}IATAAirportCode = {2}{0}", Environment.NewLine, TITLE, MSG, IATAAirportCode);
		}
    }
}
