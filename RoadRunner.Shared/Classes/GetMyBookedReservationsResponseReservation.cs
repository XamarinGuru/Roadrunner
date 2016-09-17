using System;
using System.Linq;
using RoadRunner.Shared.Classes;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RoadRunner.Shared
{
	//<MyReservation 
	//ReservationID="1496385" 
	//createdOn="2014-12-12T11:58:40.223-08:00" 
	//modifiedOn="2014-12-13T18:36:09-08:00" 
	//noOfPassengers="1" 
	//Service="WheelChair" 
	//Pickcity="<add><complex>b</complex><stNum>123</stNum><street>hihih</street><unit>v</unit><city>Ventura</city><zip>93001</zip></add>" 
	//Dropoff="LAX" 
	//STATUS="Cancelled" 
	//CancelStatus="1" 
	//type="1" 
	//ResType="Regular Airport" 
	//arvDep="0" 
	//arvDepStatus="Departure" 
	//travelDate="12/13/2014" 
	//schdate="2014-12-13T00:00:00-08:00" 
	//dropOffTime="" 
	//pickUpTime="12/13/2014 9:10 PM" 
	//dropTime="" 
	//pickTime="" 
	//fltTime="12/13/2014 10:59 PM" 
	//pickupLat="34.2769" 
	//pickupLong="-119.277" 
	//dropOffLat="33.9444" 
	//dropOffLong="-118.397" 
	//Gratuity="0.0000" 
	//Fee="72.0000" 
	//SURCHARGE="2.0000" 
	//DISCOUNT="0.0000" 
	//fare="74.0000" 
	//FName="Wiley" 
	//LName="Roadrunners" 
	//Phone="" 
	//customerEmail="" 
	//dar="0"/>
	public class GetMyBookedReservationsResponseReservation
	{
		public String ReservationID { get; set; }
		public String createdOn { get; set; }
		public String modifiedOn { get; set; }
		public String noOfPassengers { get; set; }
		public String Service { get; set; }
		public GetMyBookedReservationsResponseReservationLocation Pickcity { get; set; }
		public GetMyBookedReservationsResponseReservationLocation Dropoff { get; set; }
		public String STATUS { get; set; }
		public String CancelStatus { get; set; }
		public String type { get; set; }
		public String ResType { get; set; }
		public String arvDep { get; set; }
		public String arvDepStatus { get; set; }
		public String travelDate { get; set; }
		public String schdate { get; set; }
		public String dropOffTime { get; set; }
		public String pickUpTime { get; set; }
		public String dropTime { get; set; }
		public String pickTime { get; set; }
		public String fltTime { get; set; }
		public String pickupLat { get; set; }
		public String pickupLong { get; set; }
		public String dropOffLat { get; set; }
		public String dropOffLong { get; set; }
		public String Gratuity { get; set; }
		public String Fee { get; set; }
		public String SURCHARGE { get; set; }
		public String DISCOUNT { get; set; }
		public String fare { get; set; }
		public String FName { get; set; }
		public String LName { get; set; }
		public String Phone { get; set; }
		public String customerEmail { get; set; }
		public String dar { get; set; }

		public async Task<CancelReservationForAndroidResponse> Cancel()
		{
			var dic = new Dictionary<String, String> {
				{ Constant.CANCELRESERVATIONFORANDROID_UPDATEMODE, this.type },
				{ Constant.CANCELRESERVATIONFORANDROID_FLAG, "" },
				{ Constant.CANCELRESERVATIONFORANDROID_RESID, this.ReservationID }
			};

			var result = string.Empty;

			try
			{
				result = await AppData.ApiCall (Constant.CANCELRESERVATIONFORANDROID, dic);
			}
			catch
			{
				throw;
			}

			return (CancelReservationForAndroidResponse)AppData.ParseResponse (Constant.CANCELRESERVATIONFORANDROID, result);
		}

		public override string ToString()
		{
			return String.Format("{0}ReservationID = {2}{0}{0}createdOn = {3}{0}modifiedOn = {4}{0}noOfPassengers = {5}{0}Service = {6}{0}" +
				"Pickcity = {7}{0}Dropoff = {8}{0}STATUS = {9}{0}CancelStatus = {10}{0}" +
				"type = {11}{0}ResType = {12}{0}arvDep = {13}{0}arvDepStatus = {14}{0}travelDate = {15}{0}" +
				"schdate = {16}{0}dropOffTime = {17}{0}pickUpTime = {18}{0}dropTime = {19}{0}" +
				"pickTime = {20}{0}fltTime = {21}{0}pickupLat = {22}{0}pickupLong = {23}{0}dropOffLat = {24}{0}" +
				"dropOffLong = {25}{0}Gratuity = {26}{0}Fee = {27}{0}SURCHARGE = {28}{0}" +
				"DISCOUNT = {29}{0}fare = {30}{0}FName = {31}{0}LName = {32}{0}Phone = {33}{0}customerEmail = {34}{0}dar = {35}{0}",
				Environment.NewLine, Pickcity, ReservationID, createdOn, modifiedOn, noOfPassengers, Service, Pickcity, Dropoff, STATUS, CancelStatus,
				type, ResType, arvDep, arvDepStatus, travelDate, schdate, dropOffTime, pickUpTime, dropTime, pickTime, fltTime, pickupLat, pickupLong,
				dropOffLat, dropOffLong, Gratuity, Fee, SURCHARGE, DISCOUNT, fare, FName, LName, Phone, customerEmail, dar);
		}
	}
}

