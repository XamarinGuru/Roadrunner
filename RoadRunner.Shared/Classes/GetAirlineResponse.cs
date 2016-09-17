using System;
using System.Collections.Generic;

namespace RoadRunner.Shared
{
	public class GetAirlineResponse
	{
		public String TITLE { get; set; }
		public String MSG { get; set; }
		public List<GetAirlineResponseItem> List { get; set; }
	}
}

