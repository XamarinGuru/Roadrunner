using Newtonsoft.Json;
using RoadRunner.Shared.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Xml;
using System.Text;
using System.Diagnostics;
using System.Xml.Linq;

namespace RoadRunner.Shared
{
    public static class AppData
    {
        private static Regex ValidEmailRegex = CreateValidEmailRegex();
        private static Regex CreateValidEmailRegex()
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                                       + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                                       + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
        }

        public static bool IsValidEmail(string email)
        {
            bool isValid = ValidEmailRegex.IsMatch(email);
            return isValid;
        }

        public static async Task<string> ApiCall(string url, string[] parameters, string[] values)
        {			
            var httpClient = new HttpClient(); // Xamarin supports HttpClient!

            string urlToAppend = "";

            int i = 0;
            foreach (string param in parameters)
            {
                urlToAppend += param + "=" + values[i];
                i++;
                if (i != parameters.Length)
                {
                    urlToAppend += "&";
                }
            }

            string fullURL = Constant.BASEURL + url + urlToAppend;
            Task<string> contentsTask = httpClient.GetStringAsync(fullURL); // async method!
            string contents = await contentsTask;

            return contents;
        }

        public static async Task<string> ApiCall(string url, Dictionary<String, String> dic)
		{
            var sb = new StringBuilder();
            foreach (KeyValuePair<string, string> pair in dic)
            {
                sb.Append(String.Format(@"&{0}={1}", pair.Key, pair.Value));
            }
            string fullURL = Constant.BASEURL + url + sb;
            using (var client = new HttpClient())
            {
                string str = await client.GetStringAsync(fullURL);
                return str;
            }
        }

		public static async Task<string> GetAirportCode(String fullAirportDescription, double latitude, double longitude)
        {
            try
            {
				var dic = new Dictionary<String, String> 
				{ 
					{ Constant.GETIATAAIRPORTCODE_LATITUDE, latitude.ToString () },
					{ Constant.GETIATAAIRPORTCODE_LONGITUDE, longitude.ToString () },
					{ Constant.GETIATAAIRPORTCODE_FULLAIRPORTNAME,  fullAirportDescription }
				};

				var result = await AppData.ApiCall (Constant.GETIATAAIRPORTCODE, dic);
				var tt = (AirportCodeResponse)AppData.ParseResponse (Constant.GETIATAAIRPORTCODE, result);

				return tt.IATAAirportCode;
            }
            catch (Exception e)
            {
				//TODO: better logging
				Debug.WriteLine ("ApiAirportCodeCall failed: " + e.Message);
            }

			return string.Empty;
        }

        public static object ParseResponse(string type, string xmlRawInput)
        {
            try
            {
                object response;
                using (var xmlReader = XmlReader.Create(new StringReader(xmlRawInput)))
                {
                    switch (type)
                    {
                        case Constant.GETCONFIRMATIONTEXT:

                            #region GETCONFIRMATIONTEXT

                            response = new GetConfirmationTextResponse();
                            ((GetConfirmationTextResponse)response).ConfList = new List<GetConfirmationTextResponseItem>();

                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    switch (xmlReader.Name.ToLower())
                                    {
                                        case "conf":
                                            if (xmlReader.HasAttributes)
                                            {
                                                try
                                                {
                                                    var item = new GetConfirmationTextResponseItem();
                                                    item.Header = xmlReader.GetAttribute("Header");
                                                    item.Body = xmlReader.GetAttribute("Body");
                                                    ((GetConfirmationTextResponse)response).ConfList.Add(item);
                                                }
                                                catch (Exception e)
                                                {

                                                }

                                            }
                                            break;
                                    }
                                }
                            }
                            return response;

                        #endregion

                        case Constant.GETREADYFORPICKUPLIST:

							#region GETREADYFORPICKUPLIST

							response = new GetReadyForPickupListResponse();
							((GetReadyForPickupListResponse)response).PickupList = new List<GetReadyForPickupListItem>();
							while (xmlReader.Read())
							{
								switch (xmlReader.NodeType)
								{
									case XmlNodeType.Element:
										switch (xmlReader.Name.ToLower())
										{
											case "msg":
												((GetReadyForPickupListResponse)response).MSG = xmlReader.ReadElementContentAsString();
												break;
											case "title":
												((GetReadyForPickupListResponse)response).TITLE = xmlReader.ReadElementContentAsString();
												break;
											case "pickuplist":
												if (xmlReader.HasAttributes)
												{
													var item = new GetReadyForPickupListItem();
													try
													{
														item.ResID = xmlReader.GetAttribute("ResID");
														item.arvDep = xmlReader.GetAttribute("arvDep");
														item.Psgr = xmlReader.GetAttribute("Psgr");
														item.type = xmlReader.GetAttribute("type");
														item.TravelDate = xmlReader.GetAttribute("TravelDate");
														item.Service = xmlReader.GetAttribute("Service");
														item.Pickcity = xmlReader.GetAttribute("Pickcity");
														item.Dropoff = xmlReader.GetAttribute("Dropoff");
														item.pickUpTime = xmlReader.GetAttribute("pickUpTime");
														item.DisplayTxt= xmlReader.GetAttribute("DisplayTxt");

														((GetReadyForPickupListResponse)response).PickupList.Add(item);
													}
													catch (Exception e)
													{

													}
												}
												break;
										}
										break;
								}
							}
							return response;
							

						#endregion
						case Constant.GETDISCLAIMER:

                            #region GETDISCLAIMER

                            response = new GetDisclaimerResponse();
                            ((GetDisclaimerResponse) response).DisclaimerList = new List<GetDisclaimerResponseItem>();

                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    switch (xmlReader.Name.ToLower())
                                    {
                                        case "disclaimer":
                                            if (xmlReader.HasAttributes)
                                            {
                                                try
                                                {
                                                    var item = new GetDisclaimerResponseItem();
                                                    item.Header = xmlReader.GetAttribute("Header");
                                                    item.Body = xmlReader.GetAttribute("Body");
                                                    ((GetDisclaimerResponse) response).DisclaimerList.Add(item);
                                                }
                                                catch (Exception e)
                                                {

                                                }

                                            }
                                            break;
                                    }
                                }
                            }
                            return response;

                            #endregion

                        case Constant.UPDATECLIENTGPS:

                            #region UPDATECLIENTGPS

                            response = new UpdateClientGPSResponse();
                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    switch (xmlReader.Name.ToLower())
                                    {
                                        case "markers":
                                            ((UpdateClientGPSResponse) response).markers = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "result":
                                            ((UpdateClientGPSResponse) response).Result = xmlReader.ReadElementContentAsString();
                                            break;
                                    }
                                }
                            }
                            return response;

                            #endregion

                        case Constant.SETARRIVALCALLEDBYCLIENT:

                            #region SETARRIVALCALLEDBYCLIENT

                            response = new SetArrivalCalledByClientResponse();
                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    switch (xmlReader.Name.ToLower())
                                    {
                                        case "arrivalcalled":
                                            if (xmlReader.HasAttributes)
                                            {
                                                ((SetArrivalCalledByClientResponse) response).eventStatus = xmlReader.GetAttribute("eventStatus");
                                                ((SetArrivalCalledByClientResponse) response).Wait = xmlReader.GetAttribute("Wait");
                                                ((SetArrivalCalledByClientResponse) response).APInst = xmlReader.GetAttribute("APInst");
                                                ((SetArrivalCalledByClientResponse) response).EndInst = xmlReader.GetAttribute("EndInst");
                                            }
                                            break;
                                        case "result":
                                            ((SetArrivalCalledByClientResponse) response).Result = xmlReader.ReadElementContentAsString();
                                            break;
                                    }
                                }
                            }
                            return response;

                            #endregion


                        case Constant.GETIATAAIRPORTCODE:

                            #region GETIATAAIRPORTCODE

                            response = new AirportCodeResponse();
                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    switch (xmlReader.Name.ToLower())
                                    {
                                        case "title":
                                            ((AirportCodeResponse) response).TITLE = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "msg":
                                            ((AirportCodeResponse) response).MSG = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "iataairportcode":
                                            ((AirportCodeResponse) response).IATAAirportCode = xmlReader.ReadElementContentAsString();
                                            break;
                                    }
                                }
                            }
                            return response;

                            #endregion

                        case Constant.VALIDATEDISCOUNTCOUPON:

                            #region VALIDATEDISCOUNTCOUPON

                            response = new ValidateDiscountCouponResponse();
                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    switch (xmlReader.Name)
                                    {
                                        case "Result":
                                            ((ValidateDiscountCouponResponse) response).Result = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "Msg":
                                            ((ValidateDiscountCouponResponse) response).Msg = xmlReader.ReadElementContentAsString();
                                            break;
                                    }
                                }
                            }
                            return response;

                            #endregion

                        case Constant.UPDATE_CONTACT_FOR_ANDROID_API:

                            #region UPDATE_CONTACT_FOR_ANDROID_API

                            response = new UpdateContactResponseForAndroid();
                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    switch (xmlReader.Name)
                                    {
                                        case "Result":
                                            ((UpdateContactResponseForAndroid) response).Result = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "Msg":
                                            ((UpdateContactResponseForAndroid) response).Msg = xmlReader.ReadElementContentAsString();
                                            break;
                                    }
                                }
                            }
                            return response;

                            #endregion

                        case Constant.LOGINAPI:

                            #region LoginResponse

                            response = new LoginResponse();
                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    switch (xmlReader.Name)
                                    {
                                        case "TITLE":
                                            ((LoginResponse) response).title = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "MSG":
                                            ((LoginResponse) response).message = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "UserName":
                                            if (xmlReader.HasAttributes)
                                            {
                                                ((LoginResponse) response).Email = xmlReader.GetAttribute("Email");
                                                ((LoginResponse) response).FirstName = xmlReader.GetAttribute("FirstName");
                                                ((LoginResponse) response).LastName = xmlReader.GetAttribute("LastName");
                                                ((LoginResponse) response).Phone = xmlReader.GetAttribute("Phone");
                                                ((LoginResponse) response).Customerid = xmlReader.GetAttribute("Customerid");
                                                ((LoginResponse) response).CustType = xmlReader.GetAttribute("CustType");
                                                ((LoginResponse) response).UserName = xmlReader.GetAttribute("UserName");
                                            }
                                            break;
                                    }
                                }
                            }
                            return response;

                            #endregion

                        case Constant.GET_MY_PROFILE_FOR_ANDROID_API:

                            #region GetMyProfileForAndroidResponse

                            response = new GetMyProfileForAndroidResponse();
                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    switch (xmlReader.Name)
                                    {
                                        case "Result":
                                            ((GetMyProfileForAndroidResponse) response).Result = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "CustId":
                                            ((GetMyProfileForAndroidResponse) response).Customerid = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "FName":
                                            ((GetMyProfileForAndroidResponse) response).FirstName = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "LName":
                                            ((GetMyProfileForAndroidResponse) response).LastName = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "HPH":
                                            ((GetMyProfileForAndroidResponse) response).Phone = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "Email":
                                            ((GetMyProfileForAndroidResponse) response).Email = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "PWD":
                                            ((GetMyProfileForAndroidResponse) response).Password = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "IsSMS":
                                            ((GetMyProfileForAndroidResponse) response).IsSMS = xmlReader.ReadElementContentAsString();
                                            break;
                                    }
                                }
                            }
                            return response;

                            #endregion


                        case Constant.GETAIRLINE:

                            #region GETAIRLINE

                            response = new GetAirlineResponse
                            {
                                List = new List<GetAirlineResponseItem>()
                            };

                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    switch (xmlReader.Name)
                                    {
                                        case "AirLine":
                                            if (xmlReader.HasAttributes)
                                            {
                                                var airlineItem = new GetAirlineResponseItem
                                                {
                                                    Airline = xmlReader.GetAttribute("Airline"),
                                                    diff = xmlReader.GetAttribute("diff"),
                                                    id = xmlReader.GetAttribute("id")
                                                };
                                                ((GetAirlineResponse) response).List.Add(airlineItem);
                                            }
                                            break;
                                        case "TITLE":
                                            ((GetAirlineResponse) response).TITLE = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "MSG":
                                            ((GetAirlineResponse) response).MSG = xmlReader.ReadElementContentAsString();
                                            break;
                                    }
                                }
                            }
                            return response;

                            #endregion

                        case Constant.GETFARES:

                            #region GETFARES

                            response = new GetFaresResponse
                            {
                                Fares = new List<FareContainer>()
                            };

                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    switch (xmlReader.Name.ToLower())
                                    {
                                        case "fares":
                                            if (xmlReader.HasAttributes)
                                            {
                                                var airlineItem = new GetFaresResponseItem
                                                {
                                                    id = xmlReader.GetAttribute("id"),
                                                    NAME = xmlReader.GetAttribute("NAME"),
                                                    capacity = xmlReader.GetAttribute("capacity"),
                                                    amount = xmlReader.GetAttribute("amount"),
                                                    discountAmt = xmlReader.GetAttribute("discountAmt"),
                                                    totalAmt = xmlReader.GetAttribute("totalAmt"),
                                                    serviceImage = xmlReader.GetAttribute("serviceImage"),
                                                    IMAGE = xmlReader.GetAttribute("IMAGE"),
                                                    serviceid = xmlReader.GetAttribute("serviceid"),
                                                    isLA = xmlReader.GetAttribute("isLA"),
                                                    url = xmlReader.GetAttribute("url"),
                                                    discAmt = xmlReader.GetAttribute("discAmt"),
                                                    gratuity = xmlReader.GetAttribute("gratuity")
                                                };
                                                ((GetFaresResponse) response).Fares.Add(airlineItem);
                                            }
                                            break;
                                        case "title":
                                            ((GetFaresResponse) response).TITLE = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "msg":
                                            ((GetFaresResponse) response).MSG = xmlReader.ReadElementContentAsString();
                                            break;
                                    }
                                }
                            }
                            return response;

                            #endregion


                        case Constant.GETCREDITCARDDETAILSNEWFORPHONE:

                            #region GetCreditCardDetailsNewForPhoneResponse

                            response = new GetCreditCardDetailsNewForPhoneResponse();
                            ((GetCreditCardDetailsNewForPhoneResponse) response).CardList = new List<GetCreditCardDetailsNewForPhoneResponseItem>();
                            while (xmlReader.Read())
                            {
                                switch (xmlReader.NodeType)
                                {
                                    case XmlNodeType.CDATA:
                                        using (var subxmlReader = XmlReader.Create(new StringReader(String.Format(@"<root>{0}</root>", xmlReader.Value))))
                                        {
                                            GetCreditCardDetailsNewForPhoneResponseItem item = null;
                                            while (subxmlReader.Read())
                                            {
                                                if (subxmlReader.NodeType == XmlNodeType.Element && subxmlReader.Name.Equals("value", StringComparison.OrdinalIgnoreCase))
                                                {
                                                    var array = subxmlReader.ReadElementContentAsString().Split('|').ToList();
                                                    item = new GetCreditCardDetailsNewForPhoneResponseItem();
                                                    item.CardHolderName = array[0];
                                                    item.CardNumber = array[1];
                                                    item.CardType = array[2];
                                                    item.ExpirationMonth = (array[3].Length == 4) ? array[3].Substring(0, 2) : "0" + array[3].Substring(0, 1);
                                                    item.ExpirationYear = (array[3].Length == 4) ? array[3].Substring(2, 2) : array[3].Substring(1, 2);
                                                    item.CVV = array[4];
                                                    item.ZIP = array[5];
                                                    item.Id = array[6];
                                                    ((GetCreditCardDetailsNewForPhoneResponse) response).CardList.Add(item);
                                                }
                                            }
                                        }
                                        break;
                                    case XmlNodeType.Element:
                                        switch (xmlReader.Name)
                                        {
                                            case "Msg":
                                                ((GetCreditCardDetailsNewForPhoneResponse) response).Msg = xmlReader.ReadElementContentAsString();
                                                break;
                                            case "Result":
                                                ((GetCreditCardDetailsNewForPhoneResponse) response).Result = xmlReader.ReadElementContentAsString();
                                                break;
                                        }
                                        break;
                                }
                            }
                            return response;

                            #endregion

                        case Constant.INSERTCREDITCARDDETAILSFORPHONE:

                            #region InsertCreditCardDetailsForPhoneResponse

                            response = new InsertCreditCardDetailsForPhoneResponse();
                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    switch (xmlReader.Name)
                                    {
                                        case "Msg":
                                            ((InsertCreditCardDetailsForPhoneResponse) response).Msg = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "Result":
                                            ((InsertCreditCardDetailsForPhoneResponse) response).Result = xmlReader.ReadElementContentAsString();
                                            break;
                                    }
                                }
                            }
                            return response;

                            #endregion

                        case Constant.DELETECREDITCARD:

                            #region DeleteCreditCardResponse

                            response = new DeleteCreditCardNewResponse();
                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    switch (xmlReader.Name)
                                    {
                                        case "Msg":
                                            ((DeleteCreditCardNewResponse) response).Message = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "Result":
                                            ((DeleteCreditCardNewResponse) response).Result = xmlReader.ReadElementContentAsString();
                                            break;
                                    }
                                }
                            }
                            return response;

                            #endregion


                        case Constant.DELETECREDITCARDNEW:

                            #region DeleteCreditCardNewResponse

                            response = new DeleteCreditCardNewResponse();
                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    switch (xmlReader.Name.ToLower())
                                    {
                                        case "msg":
                                            ((DeleteCreditCardNewResponse) response).Message = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "result":
                                            ((DeleteCreditCardNewResponse) response).Result = xmlReader.ReadElementContentAsString();
                                            break;
                                    }
                                }
                            }
                            return response;

                            #endregion

                        case Constant.UPDATECREDITCARDFORPHONE:

                            #region UpdateCreditCardForPhoneResponse

                            response = new UpdateCreditCardForPhoneResponse();
                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    switch (xmlReader.Name)
                                    {
                                        case "Msg":
                                            ((UpdateCreditCardForPhoneResponse) response).Msg = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "Result":
                                            ((UpdateCreditCardForPhoneResponse) response).Result = xmlReader.ReadElementContentAsString();
                                            break;
                                    }
                                }
                            }
                            return response;

                            #endregion


                        case Constant.GETCONFIRMATIONBYDETAILSNEW:

                            #region GETCONFIRMATIONBYDETAILSNEW

                            response = new GetConfirmationByDetailsNewResponse();

                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    switch (xmlReader.Name.ToLower())
                                    {
                                        case "reservation":
                                            ((GetConfirmationByDetailsNewResponse) response).Reservation = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "reswithoutencode":
                                            ((GetConfirmationByDetailsNewResponse) response).ResWithOutEncode = xmlReader.ReadElementContentAsString();
                                            break;
                                    }
                                }
                            }

                            return response;

                            #endregion


                        case Constant.GETSPECIALSERVICES:

                            #region GETSPECIALSERVICES

                            response = new GetSpecialServicesResponse
                            {
                                SpecialServices = new List<GetSpecialServicesResponseItem>()
                            };
                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    switch (xmlReader.Name.ToLower())
                                    {
                                        case "title":
                                            ((GetSpecialServicesResponse) response).Title = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "msg":
                                            ((GetSpecialServicesResponse) response).Message = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "specialservices":
                                            var item = new GetSpecialServicesResponseItem
                                            {
                                                ProductID = xmlReader.GetAttribute("ProductID"),
                                                Product = xmlReader.GetAttribute("Product"),
                                                Price = xmlReader.GetAttribute("Price"),
                                                Quantity = xmlReader.GetAttribute("Quantity"),
                                                Type = xmlReader.GetAttribute("Type")
                                            };
                                            ((GetSpecialServicesResponse) response).SpecialServices.Add(item);
                                            break;
                                    }
                                }
                            }
                            return response;

                            #endregion

                        case Constant.GETMYBOOKEDRESERVATIONS:

                            #region GETMYBOOKEDRESERVATIONS

                            response = new GetMyBookedReservationsResponse
                            {
                                MyReservations = new List<GetMyBookedReservationsResponseReservation>()
                            };
                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    switch (xmlReader.Name.ToLower())
                                    {
                                        case "title":
                                            ((GetMyBookedReservationsResponse) response).Title = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "msg":
                                            ((GetMyBookedReservationsResponse) response).Message = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "myreservation":
                                            //var pattern = "(<complex>(?<complex>.+)</complex>)(<stNum>(?<stNum>.+)</stNum>)(<street>(?<street>.+)</street>)(<unit>(?<unit>.+)</unit>)(<city>(?<city>.+)</city>)(<zip>(?<zip>.+)</zip>)";
                                            var pattern =
                                                "(<complex>(?<complex>.*)</complex>)(<stNum>(?<stNum>.*)</stNum>)(<street>(?<street>.*)</street>)(<unit>(?<unit>.*)</unit>)(<city>(?<city>.*)</city>)(<zip>(?<zip>.*)</zip>)";

                                            String Pickcity = xmlReader.GetAttribute("Pickcity");
                                            var r = new Regex(pattern, RegexOptions.IgnoreCase);
                                            var mm = r.Match(Pickcity);

                                            var _pickcity = new GetMyBookedReservationsResponseReservationLocation
                                            {
                                                IsMatchRegex = mm.Success,

                                                complex = mm.Groups["complex"].Success ? mm.Groups["complex"].ToString() : String.Empty,
                                                stNum = mm.Groups["stNum"].Success ? mm.Groups["stNum"].ToString() : String.Empty,
                                                street = mm.Groups["street"].Success ? mm.Groups["street"].ToString() : String.Empty,
                                                unit = mm.Groups["unit"].Success ? mm.Groups["unit"].ToString() : String.Empty,
                                                city = mm.Groups["city"].Success ? mm.Groups["city"].ToString() : String.Empty,
                                                zip = mm.Groups["zip"].Success ? mm.Groups["zip"].ToString() : String.Empty,
                                                raw = Pickcity
                                            };

                                            String Dropoff = xmlReader.GetAttribute("Dropoff");
                                            mm = r.Match(Dropoff);

                                            var _dropoff = new GetMyBookedReservationsResponseReservationLocation
                                            {
                                                IsMatchRegex = mm.Success,

                                                complex = mm.Groups["complex"].ToString(),
                                                stNum = mm.Groups["stNum"].ToString(),
                                                street = mm.Groups["street"].ToString(),
                                                unit = mm.Groups["unit"].ToString(),
                                                city = mm.Groups["city"].ToString(),
                                                zip = mm.Groups["zip"].ToString(),
                                                raw = Dropoff
                                            };

                                            var id = xmlReader.GetAttribute("ReservationID").Trim();

                                            //if (id.Equals("1473800"))
                                            //{
                                            //    int j = 0;
                                            //}

                                            var item = new GetMyBookedReservationsResponseReservation
                                            {
                                                ReservationID = id,
                                                createdOn = xmlReader.GetAttribute("createdOn"),
                                                modifiedOn = xmlReader.GetAttribute("modifiedOn"),
                                                noOfPassengers = xmlReader.GetAttribute("noOfPassengers"),
                                                Service = xmlReader.GetAttribute("Service"),

                                                Pickcity = _pickcity,
                                                Dropoff = _dropoff,

                                                STATUS = xmlReader.GetAttribute("STATUS"),
                                                CancelStatus = xmlReader.GetAttribute("CancelStatus"),
                                                type = xmlReader.GetAttribute("type"),
                                                ResType = xmlReader.GetAttribute("ResType"),
                                                arvDep = xmlReader.GetAttribute("arvDep"),
                                                arvDepStatus = xmlReader.GetAttribute("arvDepStatus"),
                                                travelDate = xmlReader.GetAttribute("travelDate"),
                                                schdate = xmlReader.GetAttribute("schdate"),
                                                dropOffTime = xmlReader.GetAttribute("dropOffTime"),
                                                pickUpTime = xmlReader.GetAttribute("pickUpTime"),
                                                dropTime = xmlReader.GetAttribute("dropTime"),
                                                pickTime = xmlReader.GetAttribute("pickTime"),
                                                fltTime = xmlReader.GetAttribute("fltTime"),
                                                pickupLat = xmlReader.GetAttribute("pickupLat"),
                                                pickupLong = xmlReader.GetAttribute("pickupLong"),
                                                dropOffLat = xmlReader.GetAttribute("dropOffLat"),
                                                dropOffLong = xmlReader.GetAttribute("dropOffLong"),
                                                Gratuity = xmlReader.GetAttribute("Gratuity"),
                                                Fee = xmlReader.GetAttribute("Fee"),
                                                SURCHARGE = xmlReader.GetAttribute("SURCHARGE"),
                                                DISCOUNT = xmlReader.GetAttribute("DISCOUNT"),
                                                fare = xmlReader.GetAttribute("fare"),
                                                FName = xmlReader.GetAttribute("FName"),
                                                LName = xmlReader.GetAttribute("LName"),
                                                Phone = xmlReader.GetAttribute("Phone"),
                                                customerEmail = xmlReader.GetAttribute("customerEmail"),
                                                dar = xmlReader.GetAttribute("dar")
                                            };

                                            ((GetMyBookedReservationsResponse) response).MyReservations.Add(item);
                                            break;
                                    }
                                }
                            }
                            return response;

                            #endregion

                        case Constant.CANCELRESERVATIONFORANDROID:

                            #region CANCELRESERVATIONFORANDROID

                            response = new CancelReservationForAndroidResponse();
                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    switch (xmlReader.Name.ToLower())
                                    {
                                        case "result":
                                            ((CancelReservationForAndroidResponse) response).Result = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "msg":
                                            ((CancelReservationForAndroidResponse) response).Message = xmlReader.ReadElementContentAsString();
                                            break;
                                    }
                                }
                            }
                            return response;

                            #endregion

                        case Constant.CHECKLOGINFORANDROID:

                            #region CHECKLOGINFORANDROID

                            response = new CheckLoginForAndroidResponse();
                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    switch (xmlReader.Name.ToLower())
                                    {
                                        case "result":
                                            ((CheckLoginForAndroidResponse) response).Result = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "msg":
                                            ((CheckLoginForAndroidResponse) response).Message = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "type":
                                            ((CheckLoginForAndroidResponse) response).CustomerType = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "customerid":
                                            ((CheckLoginForAndroidResponse) response).Customerid = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "fname":
                                            ((CheckLoginForAndroidResponse) response).FName = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "lname":
                                            ((CheckLoginForAndroidResponse) response).LName = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "ph":
                                            ((CheckLoginForAndroidResponse) response).PH = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "email":
                                            ((CheckLoginForAndroidResponse) response).Email = xmlReader.ReadElementContentAsString();
                                            break;
                                    }
                                }
                            }
                            return response;

                            #endregion

                        case Constant.GETGASSURCHARGE:

                            #region GETGASSURCHARGE

                            response = new GetGasSurchargeResponse();
                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    switch (xmlReader.Name.ToLower())
                                    {
                                        case "title":
                                            ((GetGasSurchargeResponse) response).Title = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "msg":
                                            ((GetGasSurchargeResponse) response).Message = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "gassurcharge":
                                            ((GetGasSurchargeResponse) response).Surcharge = xmlReader.ReadElementContentAsString();
                                            break;
                                    }
                                }
                            }
                            return response;

                            #endregion

                        case Constant.GETPICKUPFORRESERVATIONFORANDROID:

                            #region GETPICKUPFORRESERVATIONFORANDROID

                            response = new GetPickupForReservationForAndroidResponse();
							while (xmlReader.Read())
							{
								if (xmlReader.NodeType == XmlNodeType.Element)
								{
									switch (xmlReader.Name.ToLower())
									{
										case "result":
											((GetPickupForReservationForAndroidResponse)response).Result = xmlReader.ReadElementContentAsString();
											break;
										case "message":
											((GetPickupForReservationForAndroidResponse)response).Message = xmlReader.ReadElementContentAsString();
											break;
										case "lat":
											((GetPickupForReservationForAndroidResponse)response).Lat = xmlReader.ReadElementContentAsString();
											break;
										case "long":
											((GetPickupForReservationForAndroidResponse)response).Long = xmlReader.ReadElementContentAsString();
											break;
										case "angle":
											((GetPickupForReservationForAndroidResponse)response).angle = xmlReader.ReadElementContentAsString();
											break;
										case "speed":
											((GetPickupForReservationForAndroidResponse)response).speed = xmlReader.ReadElementContentAsString();
											break;
										case "dr":
											((GetPickupForReservationForAndroidResponse)response).Dr = xmlReader.ReadElementContentAsString();
											break;
										case "tr":
											((GetPickupForReservationForAndroidResponse)response).Tr = xmlReader.ReadElementContentAsString();
											break;
										case "eta":
											((GetPickupForReservationForAndroidResponse)response).Eta = xmlReader.ReadElementContentAsString();
											break;
										case "dist":
											((GetPickupForReservationForAndroidResponse)response).Dist = xmlReader.ReadElementContentAsString();
											break;
										case "trtime":
											((GetPickupForReservationForAndroidResponse)response).Trtime = xmlReader.ReadElementContentAsString();
											break;
										case "res":
											((GetPickupForReservationForAndroidResponse)response).Res = xmlReader.ReadElementContentAsString();
											break;
									}
								}
							}
                            return response;

                            #endregion

                        case Constant.GETFAREFORRESERVATIONCHARTER:

                            #region GETFAREFORRESERVATIONCHARTER

                            response = new GetFareForReservationCharterResponse
                            {
                                Fares = new List<FareContainer>()
                            };

                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    switch (xmlReader.Name.ToLower())
                                    {
                                        case "fares":
                                            if (xmlReader.HasAttributes)
                                            {
                                                var airlineItem = new GetFareForReservationCharterResponseItem
                                                {
                                                    id = xmlReader.GetAttribute("id"),
                                                    NAME = xmlReader.GetAttribute("name"),
                                                    capacity = xmlReader.GetAttribute("capacity"),
                                                    amount = xmlReader.GetAttribute("Amount"),
                                                    discountAmt = xmlReader.GetAttribute("DiscountAmt"),
                                                    totalAmt = xmlReader.GetAttribute("TotalAmt"),
                                                    HourlyRate = xmlReader.GetAttribute("HourlyRate"),
                                                    serviceImage = xmlReader.GetAttribute("ServiceImage"),
                                                    IMAGE = xmlReader.GetAttribute("Image"),
                                                    serviceid = xmlReader.GetAttribute("serviceid")
                                                };
                                                double amount;
                                                if (double.TryParse(airlineItem.amount, out amount))
                                                {
                                                    if (amount > 0)
                                                    {
                                                        ((GetFareForReservationCharterResponse) response).Fares.Add(airlineItem);
                                                    }
                                                }
                                            }
                                            break;
                                        case "result":
                                            ((GetFareForReservationCharterResponse) response).Result = xmlReader.ReadElementContentAsString();
                                            break;
                                    }
                                }
                            }
                            return response;

                            #endregion

                        case Constant.GETRECENTPICKUPADDRESS:

                            #region GETRECENTPICKUPADDRESS

                            response = new GetRecentPickUpAddressResponse
                            {
                                Items = new List<GetRecentPickUpAddressResponseItem>()
                            };

                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    switch (xmlReader.Name.ToLower())
                                    {
                                        case "item":
                                            if (xmlReader.HasAttributes)
                                            {
                                                var pickUpAddress = xmlReader.GetAttribute("PickUpAddress").Trim();
                                                var fillAddress = xmlReader.GetAttribute("FillAddress");
                                                var array = fillAddress.Split(new[] {"$", "-100"}, StringSplitOptions.RemoveEmptyEntries).ToList();

                                                // FillAddress="$$240 s glenn dr$Camarillo$93010$$Near rlwy stn$0$0"
                                                if (array.Count > 0)
                                                {
                                                    var item = new GetRecentPickUpAddressResponseItem
                                                    {
                                                        PickUpAddress = pickUpAddress,
                                                        RawInput = fillAddress,

                                                        Street = array.Count > 0 ? array[0].Trim() : String.Empty,
                                                        City = array.Count > 1 ? array[1].Trim() : String.Empty,
                                                        Zip = array.Count > 2 ? array[2].Trim() : String.Empty,
                                                        Comment = array.Count > 3 ? array[3].Trim() : String.Empty
                                                    };
                                                    ((GetRecentPickUpAddressResponse) response).Items.Add(item);
                                                }
                                            }
                                            break;
                                    }
                                }
                            }
                            return response;

                            #endregion

                        case Constant.FORGOTPASSWORDAPI:

                            #region FORGOTPASSWORDAPI

                            response = new ForgotPasswordResponse();
                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    switch (xmlReader.Name.ToLower())
                                    {
                                        case "status":
                                            ((ForgotPasswordResponse) response).status = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "result":
                                            ((ForgotPasswordResponse) response).Result = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "msg":
                                            ((ForgotPasswordResponse) response).MSG = xmlReader.ReadElementContentAsString();
                                            break;
                                    }
                                }
                            }
                            return response;

                            #endregion

                        case Constant.REGESTRATIONAPI:

                            #region REGESTRATIONAPI

                            response = new RegistrationResponse();
                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    switch (xmlReader.Name.ToLower())
                                    {
                                        case "alreadyexist":
                                            ((RegistrationResponse) response).AlreadyExist = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "customerid":
                                            ((RegistrationResponse) response).CustomerId = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "msg":
                                            ((RegistrationResponse) response).Msg = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "result":
                                            ((RegistrationResponse) response).Result = xmlReader.ReadElementContentAsString();
                                            break;
                                    }
                                }
                            }
                            return response;

                            #endregion

                        case Constant.REGESTRATION_FOR_ANDROID_API:

                            #region REGESTRATION_FOR_ANDROID_API

                            response = new RegistrationResponseForAndroid();
                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    switch (xmlReader.Name.ToLower())
                                    {
                                        case "alreadyexist":
                                            ((RegistrationResponseForAndroid) response).AlreadyExist = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "customerid":
                                            ((RegistrationResponseForAndroid) response).CustomerId = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "msg":
                                            ((RegistrationResponseForAndroid) response).Msg = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "result":
                                            ((RegistrationResponseForAndroid) response).Result = xmlReader.ReadElementContentAsString();
                                            break;
                                    }
                                }
                            }
                            return response;

                            #endregion:

                            #region REGESTRATIONAPI

                            response = new RegistrationResponse();
                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    switch (xmlReader.Name.ToLower())
                                    {
                                        case "alreadyexist":
                                            ((RegistrationResponse) response).AlreadyExist = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "customerid":
                                            ((RegistrationResponse) response).CustomerId = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "msg":
                                            ((RegistrationResponse) response).Msg = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "result":
                                            ((RegistrationResponse) response).Result = xmlReader.ReadElementContentAsString();
                                            break;
                                    }
                                }
                            }
                            return response;

                            #endregion

                        case Constant.RESETPASSWORDAPI:

                            #region REGESTRATIONAPI

                            response = new ResetPasswordResponse();
                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType == XmlNodeType.Element)
                                {
                                    switch (xmlReader.Name.ToLower())
                                    {
                                        case "msg":
                                            ((ResetPasswordResponse) response).Msg = xmlReader.ReadElementContentAsString();
                                            break;
                                        case "result":
                                            ((ResetPasswordResponse) response).Result = xmlReader.ReadElementContentAsString();
                                            break;
                                    }
                                }
                            }
                            return response;

                            #endregion
                    }
                }
            }
            catch (Exception e)
            {
                //TODO: better logging
                Debug.WriteLine("ParseResponse failed: " + e.Message);
                throw;
            }
            return null;
        }

        public static async Task<PlaceDetailsAPI_RootObject> GetPlaceDetails(String placeid)
        {			
            PlaceDetailsAPI_RootObject ro = null;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string url = "https://maps.googleapis.com/maps/api/place/details/json?placeid=" + placeid + "&key=" + Constant.GOOGLE_PLACE_APIKEY;
                    var json = await httpClient.GetStringAsync(url);
                    ro = JsonConvert.DeserializeObject<PlaceDetailsAPI_RootObject>(json);
                }
            }
            catch (Exception e)
            {
                //TODO: better logging
				Debug.WriteLine ("GetPlaceDetails failed: " + e.Message);
            }
            return ro;
        }

        public static async Task<PlaceAutocompleteAPI_RootObject> GetPlaceAutocomplete(String input, Double latitude, Double longitude, int radius = 500)
        {
            PlaceAutocompleteAPI_RootObject ro = null;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    //&types=geocode
                    string url = String.Format("https://maps.googleapis.com/maps/api/place/autocomplete/json?input={0}&location={1},{2}&radius={3}&components=country:us&key={4}",
                        input, latitude, longitude, radius, Constant.GOOGLE_PLACE_APIKEY);
                    var json = await httpClient.GetStringAsync(url);
                    ro = JsonConvert.DeserializeObject<PlaceAutocompleteAPI_RootObject>(json);
                }
            }
            catch (Exception e)
            {
				//TODO: better logging
				Debug.WriteLine ("GetPlaceAutocomplete failed: " + e.Message);
            }
            return ro;
        }

    }
}
