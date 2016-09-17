using System;
using System.Collections.Generic;
using System.Text;

namespace RoadRunner.Shared
{
	public class GetReadyForPickupListResponse
	{
		public String TITLE { get; set; }
		public String MSG { get; set; }

		public List<GetReadyForPickupListItem> PickupList { get; set; }

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.AppendLine(Environment.NewLine);

			sb.AppendLine(String.Format("TITLE = {0}", TITLE));
			sb.AppendLine(String.Format("MSG = {0}", MSG));

			foreach (var item in PickupList)
			{
				sb.AppendLine(item.ToString());
			}

			return sb.ToString();
		}
	}

	public class GetReadyForPickupListItem
	{
		public String ResID { get; set; }
		public String arvDep { get; set; }
		public String Psgr { get; set; }
		public String type { get; set; }
		public String TravelDate { get; set; }
		public String Service { get; set; }
		public String Pickcity { get; set; }
		public String Dropoff { get; set; }
		public String pickUpTime { get; set; }
		public String DisplayTxt { get; set; }

		public override string ToString()
		{
			return String.Format("{0}ResID = {1}{0}" +
								 "arvDep = {2}{0}" +
								 "Psgr = {3}{0}" +
								 "type = {4}{0}" +
								 "TravelDate = {5}{0}" +
								 "Service = {6}{0}" +
								 "Pickcity = {7}{0}" +
								 "Dropoff = {8}{0}" +
								 "pickUpTime = {9}{0}" +
								 "DisplayTxt = {10}{0}" +
								 "", Environment.NewLine, ResID, arvDep, Psgr, type, TravelDate, Service, Pickcity, Dropoff, pickUpTime, DisplayTxt);
		}
	}
}

