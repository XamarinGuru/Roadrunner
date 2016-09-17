using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Linq;
using System.Text;


namespace RoadRunner.Shared
{
    public class Ride
    {
        private bool IsZipToZip
        {
            get
            {
                return Facade.Instance.CurrentRide.IsPickUpLocationAirport == false && Facade.Instance.CurrentRide.IsDropOffLocationAirport == false;
            }
        }
        private bool IsArrival
        {
            get
            {
                return Facade.Instance.CurrentRide.IsPickUpLocationAirport;
            }
        }
        private bool IsDeparture
        {
            get
            {
                return Facade.Instance.CurrentRide.IsDropOffLocationAirport;
            }
        }
        
        public async Task<string> GetConfirmation(string custID)
        {
            var dic = new Dictionary<String, String>();

            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_CUSTID, custID);
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_PICKUPLAT, Facade.Instance.CurrentRide.PickUpLocationLatitude.ToString());
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_PICKUPLNG, Facade.Instance.CurrentRide.PickUpLocationLongitude.ToString());


            //Known Fields:
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_TRAVELLERID, "0");
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_CUSTTYPE, "0");
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_NOA, "");
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_REDEMPTIONAMT, "");
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_CONF, "App"); //Value from Nitin' example
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_CARDTYPE, "");
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_CCNUM, "");
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_CCNAME, "");
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_CCEXPDATE, "");
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_CCCID, "");
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_CCTYPEID, "");
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_CZIP, "");
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_PMTMODE, "");
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_PMTDETAILS, "");
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ISWINDOWID, "");
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_SPECIALSERVICESJOURNEYTYPE, "");
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_TRAVELERFNAME, "");
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_TRAVELERLNAME, "");
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_TRAVELERHOMEPHNO, "");
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_TRAVELERWORKPHNO, "");
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_TRAVELERCELLNO, "");
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_SPECIALINST, "");
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_RESTYPE1, "");
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_RESTYPEDETAIL, "");
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_BILLTO, "");
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_CLIENTID, "");
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DISUSERCODE, "");
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_COUPONEMAIL, "");
            

            //Missing values

            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DISCOUNT, ""); //- sum of all discounts
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_SPECIALSERVICEAMT, ""); // sum of all special service selected
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_REQTYPE, "1"); //Should be from GetCreditCardDetailsNewForPhone
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_INFOID, "");//Should be from GetCreditCardDetailsNewForPhone
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_IP, ""); //IP or Device or Host Identifier
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_SURCHARGE, "");
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ISSENDEMAIL, "true"); // true or false - From UI?
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_MAILTO, ""); //email address if IsSendEmail is True
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_TRAVELEREMAIL, ""); //Email from profile

            //Having instructions

            #region ResType for  Zip - Zip
            //On 10.07.15, 11:26, "Nitin P" < nitin@rrshuttle.com > wrote:
            //Hi Pavel,
            //Yes, you can use GetConfirmationByDetailsNew for  Zip - Zip.
            //Reservation would be Departure
            //And ResType will be 3
            //thanks,
            //-nitin
            #endregion
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_RESTYPE, IsZipToZip ? "3" : "1");
            #region FlyinTo & Origin 
            //Nitin:
            //FlyinTo & Origin  are two field for Departure & Arrivals. You can skip these field as you dont have UI and we want to keep tabon the cost.
            #endregion
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ORIGIN, "");
            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_FLYINGTO, "");

            

            dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_REQARVTIME, Facade.Instance.CurrentRide.RequestedArrivalTimeAndDate.FormatAsSQLDate());

            if (IsArrival)
            {
                var FLTTYPE = Facade.Instance.CurrentRide.PickUpFlightTypeIsDomestic == null ? "" : (bool) Facade.Instance.CurrentRide.PickUpFlightTypeIsDomestic ? "0" : "1";
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_FLTTYPE, FLTTYPE); // I  “0” is domestic and 1 is international.

                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVDATE, GetRequiredValue(Facade.Instance.CurrentRide.RequestedArrivalTimeAndDate.FormatAsSQLDate()));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVAIRPORT, GetRequiredValue(Facade.Instance.CurrentRide.PickUpLocation3CharacterAirportCode));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVPASSENGER, GetRequiredValue(Facade.Instance.CurrentRide.NumberOfPassangers));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVPICKUPTIME, GetRequiredValue(Facade.Instance.CurrentRide.RequestedArrivalTimeAndDate.FormatAsSQLDate()));

                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVFEE, GetRequiredValue(""));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVGRATUITY, GetRequiredValue(""));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVSERVICE, GetRequiredValue(""));

                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVSTREETNUMBER, GetRequiredValue(Facade.Instance.CurrentRide.PickUpLocation_StreetNumber));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVSTREET, GetRequiredValue(Facade.Instance.CurrentRide.PickUpLocation_Street));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_COMPLEXARV, GetRequiredValue(""));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVUNIT, GetRequiredValue(""));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVCITY, GetRequiredValue(Facade.Instance.CurrentRide.PickUpLocation_City));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVZIP, GetRequiredValue(Facade.Instance.CurrentRide.PickUpLocationZip));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVAP, GetRequiredValue(Facade.Instance.CurrentRide.PickUpLocation3CharacterAirportCode));

                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_SPECIALSERVICEIDARV, GetRequiredValue(""));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVDIRECTIONS, GetRequiredValue(Facade.Instance.CurrentRide.PickUpLocationName));
               
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVFLT, GetRequiredValue(Facade.Instance.CurrentRide.PickUpFlightNumber));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVFLTTIME, GetRequiredValue(Facade.Instance.CurrentRide.PickUpFlightTime.FormatAsSQLDate()));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVAIRLINE, GetRequiredValue(Facade.Instance.CurrentRide.PickUpAirlinesId));
               
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_CHARTERARV, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVSERVICEIDNEW, GetRequiredValue(""));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVCANCEL, "");

                #region Empty strings for DEPARTURE side

                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPZIP, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPSERVICE, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPPICKUP, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPSTREETNUMBER, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPSTREET, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPUNIT, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPDIRECTIONS, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_COMPLEXDEP, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPAP, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPFLT, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPPICKUPTIME, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPFLTTIME, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPAIRLINE, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPPASSENGER, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPDATE, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPCITY, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPFEE, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPGRATUITY, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_CHARTERDEP, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_SPECIALSERVICEIDDEP, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPSERVICEIDNEW, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPCANCEL, "");

                #endregion
            }
            else
            {
                #region Empty strings for ARRIVAL side

                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVDATE, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVAIRPORT, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVPASSENGER, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVPICKUPTIME, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVFEE, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVGRATUITY, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVSERVICE, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVZIP, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_SPECIALSERVICEIDARV, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVCITY, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVSTREET, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVSTREETNUMBER, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVUNIT, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVDIRECTIONS, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_COMPLEXARV, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVAP, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVFLT, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVFLTTIME, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVAIRLINE, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_CHARTERARV, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVSERVICEIDNEW, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_ARVCANCEL, "");

                #endregion

                //If it is not an Arrival it is a Departure or Zip-To-Zip

                var FLTTYPE = Facade.Instance.CurrentRide.DropOffFlightTypeIsDomestic == null ? "" : (bool) Facade.Instance.CurrentRide.DropOffFlightTypeIsDomestic ? "0" : "1";
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_FLTTYPE, FLTTYPE); // I  “0” is domestic and 1 is international.

                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPSERVICE, GetRequiredValue(""));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPPICKUP, GetRequiredValue(""));

                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPSTREETNUMBER, GetRequiredValue(Facade.Instance.CurrentRide.DropOffLocation_StreetNumber));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPSTREET, GetRequiredValue(Facade.Instance.CurrentRide.DropOffLocation_Street));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPUNIT, GetRequiredValue(""));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_COMPLEXDEP, "");
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPCITY, GetRequiredValue(Facade.Instance.CurrentRide.DropOffLocation_City));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPZIP, Facade.Instance.CurrentRide.DropOffLocationZip);
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPDIRECTIONS, GetRequiredValue(Facade.Instance.CurrentRide.DropOffLocationName));
                
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPAP,GetRequiredValue(Facade.Instance.CurrentRide.DropOffLocation3CharacterAirportCode));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPFLT,GetRequiredValue(Facade.Instance.CurrentRide.DropOffFlightNumber));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPPICKUPTIME,GetRequiredValue(Facade.Instance.CurrentRide.RequestedArrivalTimeAndDate.FormatAsSQLDate()));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPFLTTIME,GetRequiredValue(Facade.Instance.CurrentRide.DropOffFlightTime.FormatAsSQLDate()));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPAIRLINE,GetRequiredValue(Facade.Instance.CurrentRide.DropOffAirlinesId));

                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPPASSENGER,GetRequiredValue(Facade.Instance.CurrentRide.NumberOfPassangers));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPDATE,GetRequiredValue(Facade.Instance.CurrentRide.RequestedArrivalTimeAndDate.FormatAsSQLDate()));
                
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPFEE,GetRequiredValue(""));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPGRATUITY,GetRequiredValue(""));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_CHARTERDEP, GetRequiredValue(""));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_SPECIALSERVICEIDDEP,GetRequiredValue(""));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPSERVICEIDNEW, GetRequiredValue(""));
                dic.Add(Constant.GETCONFIRMATIONBYDETAILSNEW_DEPCANCEL, "");
            }

            for (int index = 0; index < dic.Count; index++)
            {
                var item = dic.ElementAt(index);
                var itemKey = item.Key;
                var itemValue = item.Value;

                if (!string.IsNullOrEmpty(itemValue))
                {
                    itemValue = Uri.EscapeDataString(itemValue);
                }

                dic[itemKey] = itemValue;
            }

            var result = string.Empty;

            try
            {
                var sb = new StringBuilder();
                foreach (KeyValuePair<string, string> pair in dic)
                {
                    sb.Append(String.Format(@"&{0}={1}", pair.Key, pair.Value));
                }
                string fullURL = Constant.BASEURL + Constant.GETCONFIRMATIONBYDETAILSNEW + sb;

                result = await AppData.ApiCall(Constant.GETCONFIRMATIONBYDETAILSNEW, dic);
            }
            catch (Exception e)
            {
                throw;
            }

            var tt = (GetConfirmationByDetailsNewResponse) AppData.ParseResponse(Constant.GETCONFIRMATIONBYDETAILSNEW, result);

            return tt.ResWithOutEncode;
        }

        private string GetRequiredValue(string value)
        {
            return string.IsNullOrEmpty(value) ? " " : value;
        }

        public async Task SetGasSurcharge(string travelDate)
        {
            var dic = new Dictionary<string, string>
            {
                //{ Constant.GETSPECIALSERVICES_SERVICEID, serviceID },
                //{ Constant.GETSPECIALSERVICES_ARVDEP, (IsDeparture || IsZipToZip) ? "0" : "1" },
                {Constant.GETSPECIALSERVICES_TRAVELDATE, travelDate}
            };

            GetGasSurchargeResponse surchargeResponse = null;

            try
            {
                var result = await AppData.ApiCall(Constant.GETGASSURCHARGE, dic);
                surchargeResponse = (GetGasSurchargeResponse) AppData.ParseResponse(Constant.GETGASSURCHARGE, result);
            }
            catch (Exception e)
            {
                throw;
            }

            //AppData.CurrentRide.Surcharge = surchargeResponse.Surcharge;
        }

        public async Task<GetSpecialServicesResponse> GetSpecialService()
        {
            var dic = new Dictionary<String, String>
            {
				{ Constant.GETSPECIALSERVICES_SERVICEID, Facade.Instance.CurrentRide.SelectedFare.serviceid },
				{ Constant.GETSPECIALSERVICES_ARVDEP, (IsArrival) ? "1" : "0" },
				{Constant.GETSPECIALSERVICES_TRAVELDATE, Facade.Instance.CurrentRide.RequestedArrivalTimeAndDate}
            };

            GetSpecialServicesResponse tt = null;
            try
            {
                var result = await AppData.ApiCall(Constant.GETSPECIALSERVICES, dic);
                tt = (GetSpecialServicesResponse) AppData.ParseResponse(Constant.GETSPECIALSERVICES, result);
            }
            catch (Exception e)
            {
                throw;
            }

            return tt;
        }
    }
}

