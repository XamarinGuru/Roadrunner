using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RoadRunner.Shared;
using RoadRunner.Shared.Classes;

namespace RoadRunner.TestConsole
{
	class MainClass
	{
        public static void Main(String[] args)
        {
		    for (;;)
		    {
                Console.Clear();
		        WriteToConsole("Welcome to the test API console");
		        var key = PrintMenu();
		        if (key == 0)
		        {
		            break;
		        }
                WriteToConsole("");
                WriteToConsole("Press any key to continue");
		        Console.ReadKey();
		    }
		}

		private static int PrintMenu()
		{
			Console.Clear();
			WriteToConsole("1 Get city api");
			WriteToConsole("2 Get airline");
			WriteToConsole("3 Validate discount coupon");
			WriteToConsole("4 Login");
			WriteToConsole("5 Get Credit Card Details New For Phone");
			WriteToConsole("6 Insert Credit Card For Phone");
			WriteToConsole("7 ***Doesn't work on the client's side*** Delete Credit Card");
			WriteToConsole("71 Delete Credit Card New");
			WriteToConsole("8 ***Doesn't work on the client's side*** Update Credit Card For Phone");

			WriteToConsole("9 Get Fares");
			WriteToConsole("10 Get Recent Pick Up Address");
			WriteToConsole("11 Get My Booked Reservations");
			WriteToConsole("12 Cancel Reservation For Android");
			WriteToConsole("13 Get Nearest Vehicle For Current Location For Android");
			WriteToConsole("14 Get Special Services");
			WriteToConsole("15 Send Email Confirmation For Android");
			WriteToConsole("17 GetConfirmationByDetailsNew");

			WriteToConsole("18 CheckLoginForAndroid");
			WriteToConsole("19 GetGasSurcharge");
			WriteToConsole("20 GetPickupForReservationForAndroid");
			WriteToConsole("21 GetFareForReservationCharter");

			WriteToConsole("30 NewUserRegistrationForAndroid");

			WriteToConsole("31 SetArrivalCalledByClient");
			WriteToConsole("32 UpdateClientGPS");
			WriteToConsole("33 GetDisclaimer");

			WriteToConsole("34 GetReadyForPickupList");
            WriteToConsole("35 GetConfirmationText");
            WriteToConsole(Environment.NewLine);
			WriteToConsole(Environment.NewLine);

			WriteToConsole("0 Exit");
			WriteToConsole(Environment.NewLine);
			WriteToConsole("Enter a number and press Enter");

			var key = Console.ReadLine();
			key = String.IsNullOrEmpty(key) ? "-1" : key;
			int result;
			if (int.TryParse(key, out result))
			{
				switch (result)
				{
					case 0:
						return 0;
					case 1:
						Console.Clear();
						//GetCityApi_ZIP();
						break;
					case 2:
						Console.Clear();
						//GetAirline();
						break;
					case 3:
						Console.Clear();
						//ValidateDiscountCoupon();
						break;
					case 4:
						Console.Clear();
						//Login();
						break;
					case 5:
						Console.Clear();
						//GetCreditCardDetailsNewForPhoneResponse();
						break;
					case 6:
						Console.Clear();
						//InsertCreditCardDetailsForPhone();
						break;
					case 7:
						Console.Clear();
						//DeleteCreditCard();
						break;
					case 71:
						Console.Clear();
						DeleteCreditCardNew();
						break;
					case 8:
						Console.Clear();
						//UpdateCreditCardForPhone();
						break;
					case 9:
						Console.Clear();
						GetFares();
						break;
					case 10:
						Console.Clear();
						GetRecentPickUpAddress();
						break;
					case 11:
						Console.Clear();
						GetMyBookedReservations();
						break;
					case 12:
						Console.Clear();
						CancelReservationForAndroid();
						break;
					case 13:
						Console.Clear();
						//GetNearestVehicleForCurrentLocationForAndroid();
						break;
					case 14:
						Console.Clear();
						GetSpecialServices();
						break;
					case 15:
						Console.Clear();
						SendEmailConfirmationForAndroid();
						break;
					case 16:
						Console.Clear();
						//BookReservationForAndroid();
						break;
					case 17:
						Console.Clear();
						GetConfirmationByDetailsNew();
						break;
					case 18:
						Console.Clear();
						CheckLoginForAndroid();
						break;
					case 19:
						Console.Clear();
						GetGasSurcharge();
						break;
					case 20:
						Console.Clear();
						GetPickupForReservationForAndroid();
						break;
					case 21:
						Console.Clear();
						GetFareForReservationCharter();
						break;

					case 30:
						Console.Clear();
						NewUserRegistrationForAndroid();
						break;

					case 31:
						Console.Clear();
						SetArrivalCalledByClient();
						break;

					case 32:
						Console.Clear();
						UpdateClientGPS();
						break;

					case 33:
						Console.Clear();
						GetDisclaimer();
						break;

					case 34:
						Console.Clear();
						GetReadyForPickupList();
						break;

                    case 35:
                        Console.Clear();
                        GetConfirmationText();
                        break;

                }
			}

			return result;
		}

        private static GetConfirmationTextResponse GetConfirmationText()
        {
            try
            {
                WriteToConsole("Going to test GetConfirmationText");
                WriteToConsole("");
                WriteToConsole("Please type RESID");
                String RESID = Console.ReadLine();

                var dic = new Dictionary<String, String>
                {
                    {Constant.GETCONFIRMATIONTEXT_RESID, String.IsNullOrEmpty(RESID) ? "1536724" : RESID}
                };

                Task<String> task = AppData.ApiCall(Constant.GETCONFIRMATIONTEXT, dic);
                task.Wait(); //Playing sync in console
                WriteToConsole("Done waiting, now parsing");

                GetConfirmationTextResponse tt = (GetConfirmationTextResponse)AppData.ParseResponse(Constant.GETCONFIRMATIONTEXT, task.Result);

                WriteToConsole(tt.ToString(), ConsoleColor.White);

                WriteToConsole("");
                return tt;

            }
            catch (Exception e)
            {
                WriteToConsole(e.ToString(), ConsoleColor.Red);
            }

            return null;
        }

        private static GetReadyForPickupListResponse GetReadyForPickupList()
		{
			try
			{
				WriteToConsole("Going to test GetReadyForPickupList");
				WriteToConsole("");

				WriteToConsole("Please type Customerid");
				String Customerid = Console.ReadLine();

				WriteToConsole("Please type CurrentDate");
				String CurrentDate = Console.ReadLine();

				var dic = new Dictionary<String, String>
				{
					{Constant.GETREADYFORPICKUPLIST_CUSTOMERID, String.IsNullOrEmpty(Customerid) ? "553890" : Customerid},
					{Constant.GETREADYFORPICKUPLIST_CURRENTDATE, String.IsNullOrEmpty(CurrentDate) ? "2/13/2015" : CurrentDate}
				};

				Task<String> task = AppData.ApiCall(Constant.GETREADYFORPICKUPLIST, dic);
				task.Wait(); //Playing sync in console
				WriteToConsole("Done waiting, now parsing");

				var tt = (GetReadyForPickupListResponse)AppData.ParseResponse(Constant.GETREADYFORPICKUPLIST, task.Result);

				WriteToConsole(tt.ToString(), ConsoleColor.White);

				WriteToConsole("");
				return tt;

			}
			catch (Exception e)
			{
				WriteToConsole(e.ToString(), ConsoleColor.Red);
			}

			return null;
		}

	    private static GetDisclaimerResponse GetDisclaimer()
	    {
	        try
	        {
	            WriteToConsole("Going to test GetDisclaimer");
	            WriteToConsole("");
                
	            var dic = new Dictionary<String, String>();

	            Task<String> task = AppData.ApiCall(Constant.GETDISCLAIMER, dic);
	            task.Wait(); //Playing sync in console
	            WriteToConsole("Done waiting, now parsing");

	            var tt = (GetDisclaimerResponse) AppData.ParseResponse(Constant.GETDISCLAIMER, task.Result);

	            WriteToConsole(tt.ToString(), ConsoleColor.White);

	            WriteToConsole("");
	            return tt;

	        }
	        catch (Exception e)
	        {
	            WriteToConsole(e.ToString(), ConsoleColor.Red);
	        }

	        return null;
	    }

	    private static UpdateClientGPSResponse UpdateClientGPS()
	    {
	        try
	        {
	            WriteToConsole("Going to test SetArrivalCalledByClient");
	            WriteToConsole("");

	            WriteToConsole("Please type SCHDATE");
	            String SCHDATE = Console.ReadLine();

	            WriteToConsole("Please type RESID");
	            String RESID = Console.ReadLine();

	            WriteToConsole("Please type ARVDEP");
	            String ARVDEP = Console.ReadLine();

	            WriteToConsole("Please type CUSTID");
	            String CUSTID = Console.ReadLine();

	            WriteToConsole("Please type REMARK");
	            String REMARK = Console.ReadLine();

	            WriteToConsole("Please type LATITUDE");
	            String LATITUDE = Console.ReadLine();

	            WriteToConsole("Please type LONGITUDE");
	            String LONGITUDE = Console.ReadLine();

	            WriteToConsole("Please type GPSTIME");
	            String GPSTIME = Console.ReadLine();

	            var dic = new Dictionary<String, String>
	            {
	                {Constant.UPDATECLIENTGPS_SCHDATE, String.IsNullOrEmpty(SCHDATE) ? "2/12/2015" : SCHDATE},
	                {Constant.UPDATECLIENTGPS_RESID, String.IsNullOrEmpty(RESID) ? "1536287" : RESID},
	                {Constant.UPDATECLIENTGPS_ARVDEP, String.IsNullOrEmpty(ARVDEP) ? "1" : ARVDEP},
	                {Constant.UPDATECLIENTGPS_CUSTID, String.IsNullOrEmpty(CUSTID) ? "161088" : CUSTID},
	                {Constant.UPDATECLIENTGPS_REMARK, String.IsNullOrEmpty(REMARK) ? "werty" : REMARK},
	                {Constant.UPDATECLIENTGPS_LATITUDE, String.IsNullOrEmpty(LATITUDE) ? "34.123" : LATITUDE},
	                {Constant.UPDATECLIENTGPS_LONGITUDE, String.IsNullOrEmpty(LONGITUDE) ? "-118.567" : LONGITUDE},
	                {Constant.UPDATECLIENTGPS_GPSTIME, String.IsNullOrEmpty(GPSTIME) ? "6/14/16 16:50" : GPSTIME},
	            };

	            Task<String> task = AppData.ApiCall(Constant.UPDATECLIENTGPS, dic);
	            task.Wait(); //Playing sync in console
	            WriteToConsole("Done waiting, now parsing");

	            var tt = (UpdateClientGPSResponse) AppData.ParseResponse(Constant.UPDATECLIENTGPS, task.Result);

	            WriteToConsole(tt.ToString(), ConsoleColor.White);

	            WriteToConsole("");
	            return tt;

	        }
	        catch (Exception e)
	        {
	            WriteToConsole(e.ToString(), ConsoleColor.Red);
	        }

	        return null;
	    }

	    private static SetArrivalCalledByClientResponse SetArrivalCalledByClient()
        {
            try
            {
                WriteToConsole("Going to test SetArrivalCalledByClient");
                WriteToConsole("");

                WriteToConsole("Please type SCHDATE");
                String SCHDATE = Console.ReadLine();

                WriteToConsole("Please type RESID");
                String RESID = Console.ReadLine();

                WriteToConsole("Please type ARVDEP");
                String ARVDEP = Console.ReadLine();

                WriteToConsole("Please type CUSTID");
                String CUSTID = Console.ReadLine();
                
                WriteToConsole("Please type REMARK");
                String REMARK = Console.ReadLine();

                WriteToConsole("Please type LATITUDE");
                String LATITUDE = Console.ReadLine();

                WriteToConsole("Please type LONGITUDE");
                String LONGITUDE = Console.ReadLine();
                var dic = new Dictionary<String, String>
                {
                    {Constant.SETARRIVALCALLEDBYCLIENT_SCHDATE, String.IsNullOrEmpty(SCHDATE) ? "2/12/2015" : SCHDATE},
                    {Constant.SETARRIVALCALLEDBYCLIENT_RESID, String.IsNullOrEmpty(RESID) ? "1536287" : RESID},
                    {Constant.SETARRIVALCALLEDBYCLIENT_ARVDEP, String.IsNullOrEmpty(ARVDEP) ? "1" : ARVDEP},
                    {Constant.SETARRIVALCALLEDBYCLIENT_CUSTID, String.IsNullOrEmpty(CUSTID) ? "161088" : CUSTID},
                    {Constant.SETARRIVALCALLEDBYCLIENT_REMARK, String.IsNullOrEmpty(REMARK) ? "werty" : REMARK},
                    {Constant.SETARRIVALCALLEDBYCLIENT_LATITUDE, String.IsNullOrEmpty(LATITUDE) ? "34.123" : LATITUDE},
                    {Constant.SETARRIVALCALLEDBYCLIENT_LONGITUDE, String.IsNullOrEmpty(LONGITUDE) ? "-118.567" : LONGITUDE},
                };

                Task<String> task = AppData.ApiCall(Constant.SETARRIVALCALLEDBYCLIENT, dic);
                task.Wait(); //Playing sync in console
                WriteToConsole("Done waiting, now parsing");

                var tt = (SetArrivalCalledByClientResponse)AppData.ParseResponse(Constant.SETARRIVALCALLEDBYCLIENT, task.Result);

                WriteToConsole(tt.ToString(), ConsoleColor.White);

                WriteToConsole("");
                return tt;

            }
            catch (Exception e)
            {
                WriteToConsole(e.ToString(), ConsoleColor.Red);
            }

            return null;
        }

        private static RegistrationResponseForAndroid NewUserRegistrationForAndroid()
		{
			try
			{
				WriteToConsole("Going to test NewUserRegistrationForAndroid");
				WriteToConsole("");


				// String 
				// EmailAddress,
				// FName,
				// LName,
				// HomePhone,
				// CellPhone,
				// Number,
				// Street,
				// Unit,
				// Complex,
				// City,
				// Zip,
				// Password,
				// Direction,
				// Custid,
				// CustType,
				// userName,
				// type,
				// token,
				// IsSMS;



				WriteToConsole("Please type EmailAddress");
				String EmailAddress = Console.ReadLine();

				WriteToConsole("Please type FName");
				String FName = Console.ReadLine();

				WriteToConsole("Please type LName");
				String LName = Console.ReadLine();

				WriteToConsole("Please type HomePhone");
				String HomePhone = Console.ReadLine();

				WriteToConsole("Please type CellPhone");
				String CellPhone = Console.ReadLine();

				WriteToConsole("Please type Number");
				String Number = Console.ReadLine();

				WriteToConsole("Please type Street");
				String Street = Console.ReadLine();

				WriteToConsole("Please type Unit");
				String Unit = Console.ReadLine();

				WriteToConsole("Please type Complex");
				String Complex = Console.ReadLine();

				WriteToConsole("Please type City");
				String City = Console.ReadLine();

				WriteToConsole("Please type Zip");
				String Zip = Console.ReadLine();

				WriteToConsole("Please type Password");
				String Password = Console.ReadLine();

				WriteToConsole("Please type Direction");
				String Direction = Console.ReadLine();


				//WriteToConsole("Please type Custid");
				String Custid = "0";// Console.ReadLine();

				//WriteToConsole("Please type CustType");
				String CustType = "0";// Console.ReadLine();

				//WriteToConsole("Please type userName");
				String userName = EmailAddress; // Console.ReadLine();

				WriteToConsole("Please type type");
				String type = Console.ReadLine();

				WriteToConsole("Please type token");
				String token = Console.ReadLine();


				WriteToConsole("Please type IsSMS");
				String IsSMS = Console.ReadLine();


				var dic = new Dictionary<String, String>
				{
					{Constant.REGESTRATION_FOR_ANDROID_API_EmailAddress, String.IsNullOrEmpty(EmailAddress) ? "" : EmailAddress},
					{Constant.REGESTRATION_FOR_ANDROID_API_FNAME, String.IsNullOrEmpty(FName) ? "" : FName},
					{Constant.REGESTRATION_FOR_ANDROID_API_LNAME, String.IsNullOrEmpty(LName) ? "" : LName},
					{Constant.REGESTRATION_FOR_ANDROID_API_HOMEPHONE, String.IsNullOrEmpty(HomePhone) ? "" : HomePhone},
					{Constant.REGESTRATION_FOR_ANDROID_API_CELLPHONE, String.IsNullOrEmpty(CellPhone) ? "" : CellPhone},
					{Constant.REGESTRATION_FOR_ANDROID_API_NUMBER, String.IsNullOrEmpty(Number) ? "" : Number},
					{Constant.REGESTRATION_FOR_ANDROID_API_STREET, String.IsNullOrEmpty(Street) ? "" : Street},
					{Constant.REGESTRATION_FOR_ANDROID_API_UNIT, String.IsNullOrEmpty(Unit) ? "" : Unit},
					{Constant.REGESTRATION_FOR_ANDROID_API_COMPLEX, String.IsNullOrEmpty(Complex) ? "" : Complex},
					{Constant.REGESTRATION_FOR_ANDROID_API_CITY, String.IsNullOrEmpty(City) ? "" : City},
					{Constant.REGESTRATION_FOR_ANDROID_API_ZIP, String.IsNullOrEmpty(Zip) ? "" : Zip},
					{Constant.REGESTRATION_FOR_ANDROID_API_PASSWORD, String.IsNullOrEmpty(Password) ? "" : Password},
					{Constant.REGESTRATION_FOR_ANDROID_API_DIRECTION, String.IsNullOrEmpty(Direction) ? "" : Direction},
					{Constant.REGESTRATION_FOR_ANDROID_API_CUSTID, String.IsNullOrEmpty(Custid) ? "" : Custid},
					{Constant.REGESTRATION_FOR_ANDROID_API_CUSTTYPE, String.IsNullOrEmpty(CustType) ? "" : CustType},
					{Constant.REGESTRATION_FOR_ANDROID_API_USERNAME, String.IsNullOrEmpty(userName) ? "" : userName},
					{Constant.REGESTRATION_FOR_ANDROID_API_TYPE, String.IsNullOrEmpty(type) ? "" : type},
					{Constant.REGESTRATION_FOR_ANDROID_API_TOKEN, String.IsNullOrEmpty(token) ? "" : token},
					{Constant.REGESTRATION_FOR_ANDROID_API_ISSMS, String.IsNullOrEmpty(IsSMS) ? "" : IsSMS}
				};


				Task<String> task = AppData.ApiCall(Constant.REGESTRATION_FOR_ANDROID_API, dic);
				task.Wait(); //Playing sync in console
				WriteToConsole("Done waiting, now parsing");

				var tt = (RegistrationResponseForAndroid) AppData.ParseResponse(Constant.REGESTRATION_FOR_ANDROID_API, task.Result);

				WriteToConsole(tt.ToString(), ConsoleColor.White);

				WriteToConsole("");
				return tt;

			}
			catch (Exception e)
			{
				WriteToConsole(e.ToString(), ConsoleColor.Red);
			}

			return null;
		}


	    private static GetFareForReservationCharterResponse GetFareForReservationCharter()
	    {
	        try
	        {
	            WriteToConsole("Going to test GetFareForReservationCharter");
	            WriteToConsole("");

	            WriteToConsole("Please type CHARTERHOURS");
	            String CHARTERHOURS = Console.ReadLine();

	            WriteToConsole("Please type CHARTERARVCITY");
	            String CHARTERARVCITY = Console.ReadLine();

	            WriteToConsole("Please type CHARTERDEPCITY");
	            String CHARTERDEPCITY = Console.ReadLine();

	            WriteToConsole("Please type CHARTERPASSENGER");
	            String CHARTERPASSENGER = Console.ReadLine();

	            WriteToConsole("Please type CHARTERTODATE");
	            String CHARTERTODATE = Console.ReadLine();

	            WriteToConsole("Please type CHARTERFROMDATE");
	            String CHARTERFROMDATE = Console.ReadLine();

	            WriteToConsole("Please type SERVICEID");
	            String SERVICEID = Console.ReadLine();

	            WriteToConsole("Please type CLIENTID");
	            String CLIENTID = Console.ReadLine();

	            WriteToConsole("Please type LOG");
	            String LOG = Console.ReadLine();

	            WriteToConsole("Please type ASSIGNID");
	            String ASSIGNID = Console.ReadLine();

	            WriteToConsole("Please type DISCPROMOCODE");
	            String DISCPROMOCODE = Console.ReadLine();


	            var dic = new Dictionary<String, String>
	            {
	                {Constant.GETFAREFORRESERVATIONCHARTER_CHARTERHOURS, String.IsNullOrEmpty(CHARTERHOURS) ? "" : CHARTERHOURS},
	                {Constant.GETFAREFORRESERVATIONCHARTER_CHARTERARVCITY, String.IsNullOrEmpty(CHARTERARVCITY) ? "" : CHARTERARVCITY},
	                {Constant.GETFAREFORRESERVATIONCHARTER_CHARTERDEPCITY, String.IsNullOrEmpty(CHARTERDEPCITY) ? "" : CHARTERDEPCITY},
	                {Constant.GETFAREFORRESERVATIONCHARTER_CHARTERPASSENGER, String.IsNullOrEmpty(CHARTERPASSENGER) ? "" : CHARTERPASSENGER},
	                {Constant.GETFAREFORRESERVATIONCHARTER_CHARTERTODATE, String.IsNullOrEmpty(CHARTERTODATE) ? "" : CHARTERTODATE},
	                {Constant.GETFAREFORRESERVATIONCHARTER_CHARTERFROMDATE, String.IsNullOrEmpty(CHARTERFROMDATE) ? "" : CHARTERFROMDATE},
	                {Constant.GETFAREFORRESERVATIONCHARTER_SERVICEID, String.IsNullOrEmpty(SERVICEID) ? "" : SERVICEID},
	                {Constant.GETFAREFORRESERVATIONCHARTER_CLIENTID, String.IsNullOrEmpty(CLIENTID) ? "" : CLIENTID},
	                {Constant.GETFAREFORRESERVATIONCHARTER_LOG, String.IsNullOrEmpty(LOG) ? "" : LOG},
	                {Constant.GETFAREFORRESERVATIONCHARTER_ASSIGNID, String.IsNullOrEmpty(ASSIGNID) ? "" : ASSIGNID},
	                {Constant.GETFAREFORRESERVATIONCHARTER_DISCPROMOCODE, String.IsNullOrEmpty(DISCPROMOCODE) ? "" : DISCPROMOCODE}
	            };


	            Task<String> task = AppData.ApiCall(Constant.GETFAREFORRESERVATIONCHARTER, dic);
	            task.Wait(); //Playing sync in console
	            WriteToConsole("Done waiting, now parsing");

	            var tt = (GetFareForReservationCharterResponse) AppData.ParseResponse(Constant.GETFAREFORRESERVATIONCHARTER, task.Result);

	            WriteToConsole(tt.ToString(), ConsoleColor.White);

	            WriteToConsole("");
	            return tt;

	        }
	        catch (Exception e)
	        {
	            WriteToConsole(e.ToString(), ConsoleColor.Red);
	        }

	        return null;
	    }

	    private static GetPickupForReservationForAndroidResponse GetPickupForReservationForAndroid()
        {
            try
            {
                WriteToConsole("Going to test GetPickupForReservationForAndroid");
                WriteToConsole("");

                WriteToConsole("Please type PHONE");
                String PHONE = Console.ReadLine();

                WriteToConsole("Please type RES");
                String RES = Console.ReadLine();

                WriteToConsole("Please type LNAME");
                String LNAME = Console.ReadLine();

                WriteToConsole("Please type ARVDEP");
                String ARVDEP = Console.ReadLine();

                var dic = new Dictionary<String, String>
	            {
	                {Constant.GETPICKUPFORRESERVATIONFORANDROID_PHONE, String.IsNullOrEmpty(PHONE) ? "" : PHONE},
	                {Constant.GETPICKUPFORRESERVATIONFORANDROID_RES, String.IsNullOrEmpty(RES) ? "" : RES},
                    {Constant.GETPICKUPFORRESERVATIONFORANDROID_LNAME, String.IsNullOrEmpty(LNAME) ? "" : LNAME},
                    {Constant.GETPICKUPFORRESERVATIONFORANDROID_ARVDEP, String.IsNullOrEmpty(ARVDEP) ? "" : ARVDEP},
	            };

                Task<String> task = AppData.ApiCall(Constant.GETPICKUPFORRESERVATIONFORANDROID, dic);
                task.Wait(); //Playing sync in console
                WriteToConsole("Done waiting, now parsing");

                var tt = (GetPickupForReservationForAndroidResponse)AppData.ParseResponse(Constant.GETPICKUPFORRESERVATIONFORANDROID, task.Result);

                WriteToConsole(tt.ToString(), ConsoleColor.White);

                WriteToConsole("");
                return tt;

            }
            catch (Exception e)
            {
                WriteToConsole(e.ToString(), ConsoleColor.Red);
            }

            return null;
        }


        private static GetGasSurchargeResponse GetGasSurcharge()
        {
            try
            {
                WriteToConsole("Going to test GetGasSurcharge");
                WriteToConsole("");

                WriteToConsole("Please type SERVICEID");
                String SERVICEID = Console.ReadLine();
                WriteToConsole("Please type TRAVELDATE");
                String TRAVELDATE = Console.ReadLine();
                WriteToConsole("Please type ARVDEP");
                String ARVDEP = Console.ReadLine();

                var dic = new Dictionary<String, String>
	            {
	                {Constant.GETGASSURCHARGE_SERVICEID, String.IsNullOrEmpty(SERVICEID) ? "" : SERVICEID},
	                {Constant.GETGASSURCHARGE_TRAVELDATE, String.IsNullOrEmpty(TRAVELDATE) ? "" : TRAVELDATE},
	                {Constant.GETGASSURCHARGE_ARVDEP, String.IsNullOrEmpty(ARVDEP) ? "" : ARVDEP}
	            };

                Task<String> task = AppData.ApiCall(Constant.GETGASSURCHARGE, dic);
                task.Wait(); //Playing sync in console
                WriteToConsole("Done waiting, now parsing");

                var tt = (GetGasSurchargeResponse)AppData.ParseResponse(Constant.GETGASSURCHARGE, task.Result);

                WriteToConsole(tt.ToString(), ConsoleColor.White);

                WriteToConsole("");
                return tt;

            }
            catch (Exception e)
            {
                WriteToConsole(e.ToString(), ConsoleColor.Red);
            }

            return null;
        }
	    private static CheckLoginForAndroidResponse CheckLoginForAndroid()
	    {
	        try
	        {
                WriteToConsole("Going to test Get Confirmation By Details New");
                WriteToConsole("");

                WriteToConsole("Please type USERNAME");
                String USERNAME = Console.ReadLine();
                WriteToConsole("Please type PASSWORD");
                String PASSWORD = Console.ReadLine();
                WriteToConsole("Please type TYPE");
                String TYPE = Console.ReadLine();
                WriteToConsole("Please type TOKEN");
                String TOKEN = Console.ReadLine();


	            var dic = new Dictionary<String, String>
	            {
	                {Constant.CHECKLOGINFORANDROID_USERNAME, String.IsNullOrEmpty(USERNAME) ? "" : USERNAME},
	                {Constant.CHECKLOGINFORANDROID_PASSWORD, String.IsNullOrEmpty(PASSWORD) ? "" : PASSWORD},
	                {Constant.CHECKLOGINFORANDROID_TYPE, String.IsNullOrEmpty(TYPE) ? "" : TYPE},
	                {Constant.CHECKLOGINFORANDROID_TOKEN, String.IsNullOrEmpty(TOKEN) ? "" : TOKEN}
	            };

                Task<String> task = AppData.ApiCall(Constant.CHECKLOGINFORANDROID, dic);
                task.Wait(); //Playing sync in console
                WriteToConsole("Done waiting, now parsing");

                var tt = (CheckLoginForAndroidResponse)AppData.ParseResponse(Constant.CHECKLOGINFORANDROID, task.Result);

                WriteToConsole(tt.ToString(), ConsoleColor.White);

                WriteToConsole("");
                return tt;

	        }
	        catch (Exception e)
	        {
                WriteToConsole(e.ToString(), ConsoleColor.Red);
	        }

	        return null;
	    }

	    private static GetConfirmationByDetailsNewResponse GetConfirmationByDetailsNew()
	    {
	        try
	        {
                WriteToConsole("Going to test Get Confirmation By Details New");
	            WriteToConsole("");

	            WriteToConsole("Please type CUSTID");
	            String CUSTID = Console.ReadLine();
	            WriteToConsole("Please type TRAVELLERID");
	            String TRAVELLERID = Console.ReadLine();
	            WriteToConsole("Please type CUSTTYPE");
	            String CUSTTYPE = Console.ReadLine();
	            WriteToConsole("Please type RESTYPE");
	            String RESTYPE = Console.ReadLine();
	            WriteToConsole("Please type ARVDATE");
	            String ARVDATE = Console.ReadLine();
	            WriteToConsole("Please type NOA");
	            String NOA = Console.ReadLine();
	            WriteToConsole("Please type ARVAIRPORT");
	            String ARVAIRPORT = Console.ReadLine();
	            WriteToConsole("Please type ARVPASSENGER");
	            String ARVPASSENGER = Console.ReadLine();
	            WriteToConsole("Please type ARVPICKUPTIME");
	            String ARVPICKUPTIME = Console.ReadLine();
	            WriteToConsole("Please type ARVFEE");
	            String ARVFEE = Console.ReadLine();
	            WriteToConsole("Please type ARVGRATUITY");
	            String ARVGRATUITY = Console.ReadLine();
	            WriteToConsole("Please type DISCOUNT");
	            String DISCOUNT = Console.ReadLine();
	            WriteToConsole("Please type CONF");
	            String CONF = Console.ReadLine();
	            WriteToConsole("Please type SPECIALSERVICEAMT");
	            String SPECIALSERVICEAMT = Console.ReadLine();
	            WriteToConsole("Please type ARVSERVICE");
	            String ARVSERVICE = Console.ReadLine();
	            WriteToConsole("Please type ARVZIP");
	            String ARVZIP = Console.ReadLine();
	            WriteToConsole("Please type SPECIALSERVICEIDARV");
	            String SPECIALSERVICEIDARV = Console.ReadLine();
	            WriteToConsole("Please type REDEMPTIONAMT");
	            String REDEMPTIONAMT = Console.ReadLine();
	            WriteToConsole("Please type CARDTYPE");
	            String CARDTYPE = Console.ReadLine();
	            WriteToConsole("Please type CCNUM");
	            String CCNUM = Console.ReadLine();
	            WriteToConsole("Please type CCNAME");
	            String CCNAME = Console.ReadLine();
	            WriteToConsole("Please type CCEXPDATE");
	            String CCEXPDATE = Console.ReadLine();
	            WriteToConsole("Please type CCCID");
	            String CCCID = Console.ReadLine();
	            WriteToConsole("Please type CCTYPEID");
	            String CCTYPEID = Console.ReadLine();
	            WriteToConsole("Please type CZIP");
	            String CZIP = Console.ReadLine();
	            WriteToConsole("Please type REQTYPE");
	            String REQTYPE = Console.ReadLine();
	            WriteToConsole("Please type INFOID");
	            String INFOID = Console.ReadLine();
	            WriteToConsole("Please type PMTMODE");
	            String PMTMODE = Console.ReadLine();
	            WriteToConsole("Please type PMTDETAILS");
	            String PMTDETAILS = Console.ReadLine();
	            WriteToConsole("Please type ISWINDOWID");
	            String ISWINDOWID = Console.ReadLine();
	            WriteToConsole("Please type ARVCITY");
	            String ARVCITY = Console.ReadLine();
	            WriteToConsole("Please type SPECIALSERVICESJOURNEYTYPE");
	            String SPECIALSERVICESJOURNEYTYPE = Console.ReadLine();
	            WriteToConsole("Please type TRAVELERFNAME");
	            String TRAVELERFNAME = Console.ReadLine();
	            WriteToConsole("Please type TRAVELERLNAME");
	            String TRAVELERLNAME = Console.ReadLine();
	            WriteToConsole("Please type TRAVELERHOMEPHNO");
	            String TRAVELERHOMEPHNO = Console.ReadLine();
	            WriteToConsole("Please type TRAVELERWORKPHNO");
	            String TRAVELERWORKPHNO = Console.ReadLine();
	            WriteToConsole("Please type TRAVELERCELLNO");
	            String TRAVELERCELLNO = Console.ReadLine();
	            WriteToConsole("Please type IP");
	            String IP = Console.ReadLine();
	            WriteToConsole("Please type DEPZIP");
	            String DEPZIP = Console.ReadLine();
	            WriteToConsole("Please type DEPSERVICE");
	            String DEPSERVICE = Console.ReadLine();
	            WriteToConsole("Please type DEPPICKUP");
	            String DEPPICKUP = Console.ReadLine();
	            WriteToConsole("Please type ARVSTREETNUMBER");
	            String ARVSTREETNUMBER = Console.ReadLine();
	            WriteToConsole("Please type DEPSTREETNUMBER");
	            String DEPSTREETNUMBER = Console.ReadLine();
	            WriteToConsole("Please type ARVSTREET");
	            String ARVSTREET = Console.ReadLine();
	            WriteToConsole("Please type DEPSTREET");
	            String DEPSTREET = Console.ReadLine();
	            WriteToConsole("Please type ARVUNIT");
	            String ARVUNIT = Console.ReadLine();
	            WriteToConsole("Please type DEPUNIT");
	            String DEPUNIT = Console.ReadLine();
	            WriteToConsole("Please type ARVDIRECTIONS");
	            String ARVDIRECTIONS = Console.ReadLine();
	            WriteToConsole("Please type DEPDIRECTIONS");
	            String DEPDIRECTIONS = Console.ReadLine();
	            WriteToConsole("Please type COMPLEXARV");
	            String COMPLEXARV = Console.ReadLine();
	            WriteToConsole("Please type COMPLEXDEP");
	            String COMPLEXDEP = Console.ReadLine();
	            WriteToConsole("Please type ARVAP");
	            String ARVAP = Console.ReadLine();
	            WriteToConsole("Please type DEPAP");
	            String DEPAP = Console.ReadLine();
	            WriteToConsole("Please type ARVFLT");
	            String ARVFLT = Console.ReadLine();
	            WriteToConsole("Please type DEPFLT");
	            String DEPFLT = Console.ReadLine();
	            WriteToConsole("Please type DEPPICKUPTIME");
	            String DEPPICKUPTIME = Console.ReadLine();
	            WriteToConsole("Please type ARVFLTTIME");
	            String ARVFLTTIME = Console.ReadLine();
	            WriteToConsole("Please type DEPFLTTIME");
	            String DEPFLTTIME = Console.ReadLine();
	            WriteToConsole("Please type ARVAIRLINE");
	            String ARVAIRLINE = Console.ReadLine();
	            WriteToConsole("Please type DEPAIRLINE");
	            String DEPAIRLINE = Console.ReadLine();
	            WriteToConsole("Please type FLTTYPE");
	            String FLTTYPE = Console.ReadLine();
	            WriteToConsole("Please type ORIGIN");
	            String ORIGIN = Console.ReadLine();
	            WriteToConsole("Please type FLYINGTO");
	            String FLYINGTO = Console.ReadLine();
	            WriteToConsole("Please type DEPPASSENGER");
	            String DEPPASSENGER = Console.ReadLine();
	            WriteToConsole("Please type DEPDATE");
	            String DEPDATE = Console.ReadLine();
	            WriteToConsole("Please type DEPCITY");
	            String DEPCITY = Console.ReadLine();
	            WriteToConsole("Please type DEPFEE");
	            String DEPFEE = Console.ReadLine();
	            WriteToConsole("Please type DEPGRATUITY");
	            String DEPGRATUITY = Console.ReadLine();
	            WriteToConsole("Please type SURCHARGE");
	            String SURCHARGE = Console.ReadLine();
	            WriteToConsole("Please type TRAVELEREMAIL");
	            String TRAVELEREMAIL = Console.ReadLine();
	            WriteToConsole("Please type SPECIALINST");
	            String SPECIALINST = Console.ReadLine();
	            WriteToConsole("Please type RESTYPE1");
	            String RESTYPE1 = Console.ReadLine();
	            WriteToConsole("Please type RESTYPEDETAIL");
	            String RESTYPEDETAIL = Console.ReadLine();
	            WriteToConsole("Please type BILLTO");
	            String BILLTO = Console.ReadLine();
	            WriteToConsole("Please type REQARVTIME");
	            String REQARVTIME = Console.ReadLine();
	            WriteToConsole("Please type ISSENDEMAIL");
	            String ISSENDEMAIL = Console.ReadLine();
	            WriteToConsole("Please type MAILTO");
	            String MAILTO = Console.ReadLine();
	            WriteToConsole("Please type CHARTERDEP");
	            String CHARTERDEP = Console.ReadLine();
	            WriteToConsole("Please type CHARTERARV");
	            String CHARTERARV = Console.ReadLine();
	            WriteToConsole("Please type CLIENTID");
	            String CLIENTID = Console.ReadLine();
	            WriteToConsole("Please type SPECIALSERVICEIDDEP");
	            String SPECIALSERVICEIDDEP = Console.ReadLine();
	            WriteToConsole("Please type DEPSERVICEIDNEW");
	            String DEPSERVICEIDNEW = Console.ReadLine();
	            WriteToConsole("Please type ARVSERVICEIDNEW");
	            String ARVSERVICEIDNEW = Console.ReadLine();
	            WriteToConsole("Please type ARVCANCEL");
	            String ARVCANCEL = Console.ReadLine();
	            WriteToConsole("Please type DEPCANCEL");
	            String DEPCANCEL = Console.ReadLine();
	            WriteToConsole("Please type DISUSERCODE");
	            String DISUSERCODE = Console.ReadLine();
	            WriteToConsole("Please type COUPONEMAIL");
	            String COUPONEMAIL = Console.ReadLine();
	            WriteToConsole("Please type PICKUPLAT");
	            String PICKUPLAT = Console.ReadLine();
	            WriteToConsole("Please type PICKUPLNG");
	            String PICKUPLNG = Console.ReadLine();


	            var dic = new Dictionary<String, String>
	            {
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_CUSTID, String.IsNullOrEmpty(CUSTID) ? "" : CUSTID},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_TRAVELLERID, String.IsNullOrEmpty(TRAVELLERID) ? "" : TRAVELLERID},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_CUSTTYPE, String.IsNullOrEmpty(CUSTTYPE) ? "" : CUSTTYPE},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_RESTYPE, String.IsNullOrEmpty(RESTYPE) ? "" : RESTYPE},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_ARVDATE, String.IsNullOrEmpty(ARVDATE) ? "" : ARVDATE},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_NOA, String.IsNullOrEmpty(NOA) ? "" : NOA},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_ARVAIRPORT, String.IsNullOrEmpty(ARVAIRPORT) ? "" : ARVAIRPORT},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_ARVPASSENGER, String.IsNullOrEmpty(ARVPASSENGER) ? "" : ARVPASSENGER},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_ARVPICKUPTIME, String.IsNullOrEmpty(ARVPICKUPTIME) ? "" : ARVPICKUPTIME},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_ARVFEE, String.IsNullOrEmpty(ARVFEE) ? "" : ARVFEE},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_ARVGRATUITY, String.IsNullOrEmpty(ARVGRATUITY) ? "" : ARVGRATUITY},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_DISCOUNT, String.IsNullOrEmpty(DISCOUNT) ? "" : DISCOUNT},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_CONF, String.IsNullOrEmpty(CONF) ? "" : CONF},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_SPECIALSERVICEAMT, String.IsNullOrEmpty(SPECIALSERVICEAMT) ? "" : SPECIALSERVICEAMT},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_ARVSERVICE, String.IsNullOrEmpty(ARVSERVICE) ? "" : ARVSERVICE},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_ARVZIP, String.IsNullOrEmpty(ARVZIP) ? "" : ARVZIP},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_SPECIALSERVICEIDARV, String.IsNullOrEmpty(SPECIALSERVICEIDARV) ? "" : SPECIALSERVICEIDARV},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_REDEMPTIONAMT, String.IsNullOrEmpty(REDEMPTIONAMT) ? "" : REDEMPTIONAMT},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_CARDTYPE, String.IsNullOrEmpty(CARDTYPE) ? "" : CARDTYPE},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_CCNUM, String.IsNullOrEmpty(CCNUM) ? "" : CCNUM},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_CCNAME, String.IsNullOrEmpty(CCNAME) ? "" : CCNAME},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_CCEXPDATE, String.IsNullOrEmpty(CCEXPDATE) ? "" : CCEXPDATE},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_CCCID, String.IsNullOrEmpty(CCCID) ? "" : CCCID},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_CCTYPEID, String.IsNullOrEmpty(CCTYPEID) ? "" : CCTYPEID},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_CZIP, String.IsNullOrEmpty(CZIP) ? "" : CZIP},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_REQTYPE, String.IsNullOrEmpty(REQTYPE) ? "" : REQTYPE},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_INFOID, String.IsNullOrEmpty(INFOID) ? "" : INFOID},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_PMTMODE, String.IsNullOrEmpty(PMTMODE) ? "" : PMTMODE},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_PMTDETAILS, String.IsNullOrEmpty(PMTDETAILS) ? "" : PMTDETAILS},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_ISWINDOWID, String.IsNullOrEmpty(ISWINDOWID) ? "" : ISWINDOWID},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_ARVCITY, String.IsNullOrEmpty(ARVCITY) ? "" : ARVCITY},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_SPECIALSERVICESJOURNEYTYPE, String.IsNullOrEmpty(SPECIALSERVICESJOURNEYTYPE) ? "" : SPECIALSERVICESJOURNEYTYPE},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_TRAVELERFNAME, String.IsNullOrEmpty(TRAVELERFNAME) ? "" : TRAVELERFNAME},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_TRAVELERLNAME, String.IsNullOrEmpty(TRAVELERLNAME) ? "" : TRAVELERLNAME},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_TRAVELERHOMEPHNO, String.IsNullOrEmpty(TRAVELERHOMEPHNO) ? "" : TRAVELERHOMEPHNO},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_TRAVELERWORKPHNO, String.IsNullOrEmpty(TRAVELERWORKPHNO) ? "" : TRAVELERWORKPHNO},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_TRAVELERCELLNO, String.IsNullOrEmpty(TRAVELERCELLNO) ? "" : TRAVELERCELLNO},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_IP, String.IsNullOrEmpty(IP) ? "" : IP},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_DEPZIP, String.IsNullOrEmpty(DEPZIP) ? "" : DEPZIP},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_DEPSERVICE, String.IsNullOrEmpty(DEPSERVICE) ? "" : DEPSERVICE},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_DEPPICKUP, String.IsNullOrEmpty(DEPPICKUP) ? "" : DEPPICKUP},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_ARVSTREETNUMBER, String.IsNullOrEmpty(ARVSTREETNUMBER) ? "" : ARVSTREETNUMBER},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_DEPSTREETNUMBER, String.IsNullOrEmpty(DEPSTREETNUMBER) ? "" : DEPSTREETNUMBER},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_ARVSTREET, String.IsNullOrEmpty(ARVSTREET) ? "" : ARVSTREET},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_DEPSTREET, String.IsNullOrEmpty(DEPSTREET) ? "" : DEPSTREET},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_ARVUNIT, String.IsNullOrEmpty(ARVUNIT) ? "" : ARVUNIT},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_DEPUNIT, String.IsNullOrEmpty(DEPUNIT) ? "" : DEPUNIT},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_ARVDIRECTIONS, String.IsNullOrEmpty(ARVDIRECTIONS) ? "" : ARVDIRECTIONS},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_DEPDIRECTIONS, String.IsNullOrEmpty(DEPDIRECTIONS) ? "" : DEPDIRECTIONS},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_COMPLEXARV, String.IsNullOrEmpty(COMPLEXARV) ? "" : COMPLEXARV},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_COMPLEXDEP, String.IsNullOrEmpty(COMPLEXDEP) ? "" : COMPLEXDEP},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_ARVAP, String.IsNullOrEmpty(ARVAP) ? "" : ARVAP},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_DEPAP, String.IsNullOrEmpty(DEPAP) ? "" : DEPAP},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_ARVFLT, String.IsNullOrEmpty(ARVFLT) ? "" : ARVFLT},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_DEPFLT, String.IsNullOrEmpty(DEPFLT) ? "" : DEPFLT},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_DEPPICKUPTIME, String.IsNullOrEmpty(DEPPICKUPTIME) ? "" : DEPPICKUPTIME},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_ARVFLTTIME, String.IsNullOrEmpty(ARVFLTTIME) ? "" : ARVFLTTIME},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_DEPFLTTIME, String.IsNullOrEmpty(DEPFLTTIME) ? "" : DEPFLTTIME},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_ARVAIRLINE, String.IsNullOrEmpty(ARVAIRLINE) ? "" : ARVAIRLINE},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_DEPAIRLINE, String.IsNullOrEmpty(DEPAIRLINE) ? "" : DEPAIRLINE},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_FLTTYPE, String.IsNullOrEmpty(FLTTYPE) ? "" : FLTTYPE},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_ORIGIN, String.IsNullOrEmpty(ORIGIN) ? "" : ORIGIN},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_FLYINGTO, String.IsNullOrEmpty(FLYINGTO) ? "" : FLYINGTO},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_DEPPASSENGER, String.IsNullOrEmpty(DEPPASSENGER) ? "" : DEPPASSENGER},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_DEPDATE, String.IsNullOrEmpty(DEPDATE) ? "" : DEPDATE},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_DEPCITY, String.IsNullOrEmpty(DEPCITY) ? "" : DEPCITY},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_DEPFEE, String.IsNullOrEmpty(DEPFEE) ? "" : DEPFEE},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_DEPGRATUITY, String.IsNullOrEmpty(DEPGRATUITY) ? "" : DEPGRATUITY},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_SURCHARGE, String.IsNullOrEmpty(SURCHARGE) ? "" : SURCHARGE},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_TRAVELEREMAIL, String.IsNullOrEmpty(TRAVELEREMAIL) ? "" : TRAVELEREMAIL},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_SPECIALINST, String.IsNullOrEmpty(SPECIALINST) ? "" : SPECIALINST},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_RESTYPE1, String.IsNullOrEmpty(RESTYPE1) ? "" : RESTYPE1},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_RESTYPEDETAIL, String.IsNullOrEmpty(RESTYPEDETAIL) ? "" : RESTYPEDETAIL},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_BILLTO, String.IsNullOrEmpty(BILLTO) ? "" : BILLTO},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_REQARVTIME, String.IsNullOrEmpty(REQARVTIME) ? "" : REQARVTIME},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_ISSENDEMAIL, String.IsNullOrEmpty(ISSENDEMAIL) ? "" : ISSENDEMAIL},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_MAILTO, String.IsNullOrEmpty(MAILTO) ? "" : MAILTO},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_CHARTERDEP, String.IsNullOrEmpty(CHARTERDEP) ? "" : CHARTERDEP},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_CHARTERARV, String.IsNullOrEmpty(CHARTERARV) ? "" : CHARTERARV},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_CLIENTID, String.IsNullOrEmpty(CLIENTID) ? "" : CLIENTID},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_SPECIALSERVICEIDDEP, String.IsNullOrEmpty(SPECIALSERVICEIDDEP) ? "" : SPECIALSERVICEIDDEP},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_DEPSERVICEIDNEW, String.IsNullOrEmpty(DEPSERVICEIDNEW) ? "" : DEPSERVICEIDNEW},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_ARVSERVICEIDNEW, String.IsNullOrEmpty(ARVSERVICEIDNEW) ? "" : ARVSERVICEIDNEW},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_ARVCANCEL, String.IsNullOrEmpty(ARVCANCEL) ? "" : ARVCANCEL},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_DEPCANCEL, String.IsNullOrEmpty(DEPCANCEL) ? "" : DEPCANCEL},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_DISUSERCODE, String.IsNullOrEmpty(DISUSERCODE) ? "" : DISUSERCODE},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_COUPONEMAIL, String.IsNullOrEmpty(COUPONEMAIL) ? "" : COUPONEMAIL},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_PICKUPLAT, String.IsNullOrEmpty(PICKUPLAT) ? "" : PICKUPLAT},
	                {Constant.GETCONFIRMATIONBYDETAILSNEW_PICKUPLNG, String.IsNullOrEmpty(PICKUPLNG) ? "" : PICKUPLNG},
	            };

	            Task<String> task = AppData.ApiCall(Constant.GETCONFIRMATIONBYDETAILSNEW, dic);
	            task.Wait(); //Playing sync in console
	            WriteToConsole("Done waiting, now parsing");

	            var tt = (GetConfirmationByDetailsNewResponse) AppData.ParseResponse(Constant.GETCONFIRMATIONBYDETAILSNEW, task.Result);

	            WriteToConsole(tt.ToString(), ConsoleColor.White);

	            WriteToConsole("");
	            return tt;

	        }
	        catch (Exception e)
	        {
	            WriteToConsole(e.ToString(), ConsoleColor.Red);
	        }
	        return null;
	    }

        //private static BookReservationForAndroidResponse BookReservationForAndroid()
        //{
        //    try
        //    {
        //        WriteToConsole("Going to test Get Nearest Vehicle For Current Location For Android");
        //        WriteToConsole("");

        //        WriteToConsole("Please type RESNO");
        //        String RESNO = Console.ReadLine();

        //        WriteToConsole("Please type CUSTID");
        //        String CUSTID = Console.ReadLine();

        //        WriteToConsole("Please type TRAVALDATE");
        //        String TRAVALDATE = Console.ReadLine();

        //        WriteToConsole("Please type TRAVELPUTIME");
        //        String TRAVELPUTIME = Console.ReadLine();

        //        WriteToConsole("Please type TRAVELREQTIME");
        //        String TRAVELREQTIME = Console.ReadLine();

        //        WriteToConsole("Please type PUADD");
        //        String PUADD = Console.ReadLine();

        //        WriteToConsole("Please type DOADD");
        //        String DOADD = Console.ReadLine();

        //        WriteToConsole("Please type NOPSGR");
        //        String NOPSGR = Console.ReadLine();

        //        WriteToConsole("Please type ADT");
        //        String ADT = Console.ReadLine();

        //        WriteToConsole("Please type SNR");
        //        String SNR = Console.ReadLine();

        //        WriteToConsole("Please type STU");
        //        String STU = Console.ReadLine();

        //        WriteToConsole("Please type DWC");
        //        String DWC = Console.ReadLine();

        //        WriteToConsole("Please type WC");
        //        String WC = Console.ReadLine();

        //        WriteToConsole("Please type PCA");
        //        String PCA = Console.ReadLine();

        //        WriteToConsole("Please type FREECHILD");
        //        String FREECHILD = Console.ReadLine();

        //        WriteToConsole("Please type FEE");
        //        String FEE = Console.ReadLine();

        //        WriteToConsole("Please type DISC");
        //        String DISC = Console.ReadLine();

        //        WriteToConsole("Please type FREECHILD");
        //        String RESTYPE = Console.ReadLine();

        //        WriteToConsole("Please type TRANSDEPARV");
        //        String TRANSDEPARV = Console.ReadLine();


        //        var dic = new Dictionary<String, String>
        //        {
        //            {Constant.BOOKRESERVATIONFORANDROID_RESNO, String.IsNullOrEmpty(RESNO) ? "" : RESNO},
        //            {Constant.BOOKRESERVATIONFORANDROID_CUSTID, String.IsNullOrEmpty(CUSTID) ? "" : CUSTID},
        //            {Constant.BOOKRESERVATIONFORANDROID_TRAVALDATE, String.IsNullOrEmpty(TRAVALDATE) ? "" : TRAVALDATE},
        //            {Constant.BOOKRESERVATIONFORANDROID_TRAVELPUTIME, String.IsNullOrEmpty(TRAVELPUTIME) ? "" : TRAVELPUTIME},
        //            {Constant.BOOKRESERVATIONFORANDROID_TRAVELREQTIME, String.IsNullOrEmpty(TRAVELREQTIME) ? "" : TRAVELREQTIME},
        //            {Constant.BOOKRESERVATIONFORANDROID_PUADD, String.IsNullOrEmpty(PUADD) ? "" : PUADD},
        //            {Constant.BOOKRESERVATIONFORANDROID_DOADD, String.IsNullOrEmpty(DOADD) ? "" : DOADD},
        //            {Constant.BOOKRESERVATIONFORANDROID_NOPSGR, String.IsNullOrEmpty(NOPSGR) ? "" : NOPSGR},
        //            {Constant.BOOKRESERVATIONFORANDROID_ADT, String.IsNullOrEmpty(ADT) ? "" : ADT},
        //            {Constant.BOOKRESERVATIONFORANDROID_SNR, String.IsNullOrEmpty(SNR) ? "" : SNR},
        //            {Constant.BOOKRESERVATIONFORANDROID_STU, String.IsNullOrEmpty(STU) ? "" : STU},
        //            {Constant.BOOKRESERVATIONFORANDROID_DWC, String.IsNullOrEmpty(DWC) ? "" : DWC},
        //            {Constant.BOOKRESERVATIONFORANDROID_WC, String.IsNullOrEmpty(WC) ? "" : WC},
        //            {Constant.BOOKRESERVATIONFORANDROID_PCA, String.IsNullOrEmpty(PCA) ? "" : PCA},
        //            {Constant.BOOKRESERVATIONFORANDROID_FREECHILD, String.IsNullOrEmpty(FREECHILD) ? "" : FREECHILD},
        //            {Constant.BOOKRESERVATIONFORANDROID_FEE, String.IsNullOrEmpty(FEE) ? "" : FEE},
        //            {Constant.BOOKRESERVATIONFORANDROID_DISC, String.IsNullOrEmpty(DISC) ? "" : DISC},
        //            {Constant.BOOKRESERVATIONFORANDROID_RESTYPE, String.IsNullOrEmpty(RESTYPE) ? "" : RESTYPE},
        //            {Constant.BOOKRESERVATIONFORANDROID_TRANSDEPARV, String.IsNullOrEmpty(TRANSDEPARV) ? "" : TRANSDEPARV}
        //        };

        //        Task<String> task = AppData.ApiCall(Constant.BOOKRESERVATIONFORANDROID, dic);
        //        task.Wait(); //Playing sync in console
        //        WriteToConsole("Done waiting, now parsing");

        //        var tt = (BookReservationForAndroidResponse)AppData.ParseResponse(Constant.BOOKRESERVATIONFORANDROID, task.Result);

        //        WriteToConsole(tt.ToString(), ConsoleColor.White);

        //        WriteToConsole("");
        //        return tt;

        //    }
        //    catch (Exception e)
        //    {
        //        WriteToConsole(e.ToString(), ConsoleColor.Red);
        //    }
        //    return null;
        //}

        private static SendEmailConfirmationForAndroidResponse SendEmailConfirmationForAndroid()
        {
            try
            {
                WriteToConsole("Going to test Get Nearest Vehicle For Current Location For Android");
                WriteToConsole("");

                WriteToConsole("Please type RES");
                String RES = Console.ReadLine();

                WriteToConsole("Please type ALTEMAIL");
                String ALTEMAIL = Console.ReadLine();

                var dic = new Dictionary<String, String>
                {
                    {Constant.SENDEMAILCONFIRMATIONFORANDROID_RES, String.IsNullOrEmpty(RES) ? "1473800" : RES},
                    {Constant.SENDEMAILCONFIRMATIONFORANDROID_ALTEMAIL, String.IsNullOrEmpty(ALTEMAIL) ? "test@test.com" : ALTEMAIL}
                };

                Task<String> task = AppData.ApiCall(Constant.SENDEMAILCONFIRMATIONFORANDROID, dic);
                task.Wait(); //Playing sync in console
                WriteToConsole("Done waiting, now parsing");

                var tt = (SendEmailConfirmationForAndroidResponse)AppData.ParseResponse(Constant.SENDEMAILCONFIRMATIONFORANDROID, task.Result);

                WriteToConsole(tt.ToString(), ConsoleColor.White);

                WriteToConsole("");
                return tt;

            }
            catch (Exception e)
            {
                WriteToConsole(e.ToString(), ConsoleColor.Red);
            }
            return null;
        }

        private static GetSpecialServicesResponse GetSpecialServices()
	    {
            try
            {
                WriteToConsole("Going to test Get Nearest Vehicle For Current Location For Android");
                WriteToConsole("");

                WriteToConsole("Please type SERVICEID");
                String SERVICEID = Console.ReadLine();

                WriteToConsole("Please type TRAVELDATE");
                String TRAVELDATE = Console.ReadLine();

                WriteToConsole("Please type ARVDEP");
                String ARVDEP = Console.ReadLine();

                var dic = new Dictionary<String, String>
                {
                    //Default values if needed
                    //{Constant.GETSPECIALSERVICES_SERVICEID, String.IsNullOrEmpty(SERVICEID) ? "1" : SERVICEID},
                    //{Constant.GETSPECIALSERVICES_ARVDEP, String.IsNullOrEmpty(ARVDEP) ? "1" : ARVDEP},
                    //{Constant.GETSPECIALSERVICES_TRAVELDATE, String.IsNullOrEmpty(TRAVELDATE) ? "5/1/2015" : TRAVELDATE},

                    {Constant.GETSPECIALSERVICES_SERVICEID, String.IsNullOrEmpty(SERVICEID) ? "" : SERVICEID},
                    {Constant.GETSPECIALSERVICES_ARVDEP, String.IsNullOrEmpty(ARVDEP) ? "" : ARVDEP},
                    {Constant.GETSPECIALSERVICES_TRAVELDATE, String.IsNullOrEmpty(TRAVELDATE) ? "" : TRAVELDATE}
                };

                Task<String> task = AppData.ApiCall(Constant.GETSPECIALSERVICES, dic);
                task.Wait(); //Playing sync in console
                WriteToConsole("Done waiting, now parsing");

                var tt = (GetSpecialServicesResponse)AppData.ParseResponse(Constant.GETSPECIALSERVICES, task.Result);

                WriteToConsole(tt.ToString(), ConsoleColor.White);

                WriteToConsole("");
                return tt;

            }
            catch (Exception e)
            {
                WriteToConsole(e.ToString(), ConsoleColor.Red);
            }
            return null;
	    }

        //private static GetNearestVehicleForCurrentLocationForAndroidResponse GetNearestVehicleForCurrentLocationForAndroid()
        //{
        //    try
        //    {
        //        WriteToConsole("Going to test Get Nearest Vehicle For Current Location For Android");
        //        WriteToConsole("");

        //        WriteToConsole("Please type LATITUDE");
        //        String LATITUDE = Console.ReadLine();

        //        WriteToConsole("Please type LONGITUDE");
        //        String LONGITUDE = Console.ReadLine();

        //        WriteToConsole("Please type DISTANCE");
        //        String DISTANCE = Console.ReadLine();

        //        WriteToConsole("Please type DISTANCETYPE");
        //        String DISTANCETYPE = Console.ReadLine();

        //        WriteToConsole("Please type SERVICEID");
        //        String SERVICEID = Console.ReadLine();

        //        var dic = new Dictionary<String, String>
        //        {
        //            {Constant.GETNEARESTVEHICLEFORCURRENTLOCATIONFORANDROID_LATITUDE, String.IsNullOrEmpty(LATITUDE) ? "1" : LATITUDE},
        //            {Constant.GETNEARESTVEHICLEFORCURRENTLOCATIONFORANDROID_LONGITUDE, String.IsNullOrEmpty(LONGITUDE) ? "1" : LONGITUDE},
        //            {Constant.GETNEARESTVEHICLEFORCURRENTLOCATIONFORANDROID_DISTANCE, String.IsNullOrEmpty(DISTANCE) ? "500000" : DISTANCE},
        //            {Constant.GETNEARESTVEHICLEFORCURRENTLOCATIONFORANDROID_DISTANCETYPE, String.IsNullOrEmpty(DISTANCETYPE) ? "1" : DISTANCETYPE},
        //            {Constant.GETNEARESTVEHICLEFORCURRENTLOCATIONFORANDROID_SERVICEID, String.IsNullOrEmpty(SERVICEID) ? "1" : SERVICEID}
        //        };

        //        Task<String> task = AppData.ApiCall(Constant.GETNEARESTVEHICLEFORCURRENTLOCATIONFORANDROID, dic);
        //        task.Wait(); //Playing sync in console
        //        WriteToConsole("Done waiting, now parsing");

        //        var tt = (GetNearestVehicleForCurrentLocationForAndroidResponse) AppData.ParseResponse(Constant.GETNEARESTVEHICLEFORCURRENTLOCATIONFORANDROID, task.Result);

        //        WriteToConsole(tt.ToString(), ConsoleColor.White);

        //        WriteToConsole("");
        //        return tt;

        //    }
        //    catch (Exception e)
        //    {
        //        WriteToConsole(e.ToString(), ConsoleColor.Red);
        //    }
        //    return null;
        //}

	    private static CancelReservationForAndroidResponse CancelReservationForAndroid()
        {
            try
            {
                WriteToConsole("Going to test Cancel Reservation For Android");
                WriteToConsole("");

                WriteToConsole("Please type UPDATEMODE");
                String UPDATEMODE = Console.ReadLine();

                WriteToConsole("Please type FLAG");
                String FLAG = Console.ReadLine();

                WriteToConsole("Please type RESID");
                String RESID = Console.ReadLine();

                var dic = new Dictionary<String, String>
                {
                    {Constant.CANCELRESERVATIONFORANDROID_UPDATEMODE, String.IsNullOrEmpty(UPDATEMODE) ? "" : UPDATEMODE},
                    {Constant.CANCELRESERVATIONFORANDROID_FLAG, String.IsNullOrEmpty(FLAG) ? "" : FLAG},
                    {Constant.CANCELRESERVATIONFORANDROID_RESID, String.IsNullOrEmpty(RESID) ? "" : RESID}
                };

                Task<String> task = AppData.ApiCall(Constant.CANCELRESERVATIONFORANDROID, dic);
                task.Wait(); //Playing sync in console
                WriteToConsole("Done waiting, now parsing");

                var tt = (CancelReservationForAndroidResponse)AppData.ParseResponse(Constant.CANCELRESERVATIONFORANDROID, task.Result);

                WriteToConsole(tt.ToString(), ConsoleColor.White);

                WriteToConsole("");
                return tt;

            }
            catch (Exception e)
            {
                WriteToConsole(e.ToString(), ConsoleColor.Red);
            }
            return null;
        }
        
        private static GetMyBookedReservationsResponse GetMyBookedReservations()
        {
            try
            {
                WriteToConsole("Going to test Get My Booked Reservations");
                WriteToConsole("");

                WriteToConsole("Please type CUSTOMERID");
                String CUSTOMERID = Console.ReadLine();

                WriteToConsole("Please type ISFUTURERES");
                String ISFUTURERES = Console.ReadLine();

                WriteToConsole("Please type LASTSYNCON");
                String LASTSYNCON = Console.ReadLine();

                var dic = new Dictionary<String, String>
                {
                    {Constant.GETMYBOOKEDRESERVATIONS_CUSTOMERID, String.IsNullOrEmpty(CUSTOMERID) ? "651001" : CUSTOMERID},
                    {Constant.GETMYBOOKEDRESERVATIONS_ISFUTURERES, String.IsNullOrEmpty(ISFUTURERES) ? "0" : ISFUTURERES},
                    {Constant.GETMYBOOKEDRESERVATIONS_LASTSYNCON, String.IsNullOrEmpty(LASTSYNCON) ? "5/7/2014" : LASTSYNCON}
                };

                Task<String> task = AppData.ApiCall(Constant.GETMYBOOKEDRESERVATIONS, dic);
                task.Wait(); //Playing sync in console
                WriteToConsole("Done waiting, now parsing");

                var tt = (GetMyBookedReservationsResponse) AppData.ParseResponse(Constant.GETMYBOOKEDRESERVATIONS, task.Result);

                WriteToConsole(tt.ToString(), ConsoleColor.White);

                WriteToConsole("");
                return tt;

            }
            catch (Exception e)
            {
                WriteToConsole(e.ToString(), ConsoleColor.Red);
            }
            return null;
        }

        private static GetRecentPickUpAddressResponse GetRecentPickUpAddress()
        {
            try
            {
                WriteToConsole("Going to test Update Credit Card For Phone");
                WriteToConsole("");

                WriteToConsole("Please type CUSTOMERID");
                String CUSTOMERID = Console.ReadLine();

                WriteToConsole("Please type RESNO");
                String RESNO = Console.ReadLine();

                WriteToConsole("Please type ISDARPROFILE");
                String ISDARPROFILE = Console.ReadLine();

                var dic = new Dictionary<String, String>
                {
                    {Constant.GETRECENTPICKUPADDRESS_CUSTOMERID, String.IsNullOrEmpty(CUSTOMERID) ? "667446" : CUSTOMERID},
                    {Constant.GETRECENTPICKUPADDRESS_RESNO, String.IsNullOrEmpty(RESNO) ? "0" : RESNO},
                    {Constant.GETRECENTPICKUPADDRESS_ISDARPROFILE, String.IsNullOrEmpty(ISDARPROFILE) ? "-1" : ISDARPROFILE}
                };

                Task<String> task = AppData.ApiCall(Constant.GETRECENTPICKUPADDRESS, dic);
                task.Wait(); //Playing sync in console
                WriteToConsole("Done waiting, now parsing");

                var tt = (GetRecentPickUpAddressResponse)AppData.ParseResponse(Constant.GETRECENTPICKUPADDRESS, task.Result);

                WriteToConsole(tt.ToString(), ConsoleColor.White);

                WriteToConsole("");
                return tt;

            }
            catch (Exception e)
            {
                WriteToConsole(e.ToString(), ConsoleColor.Red);
            }
            return null;
        }

        private static GetFaresResponse GetFares()
	    {
            try
            {
                WriteToConsole("Going to test Update Credit Card For Phone");
                WriteToConsole("");

                WriteToConsole("Please type DEPAIRPORT");
                String DEPAIRPORT = Console.ReadLine();

                WriteToConsole("Please type DEPZIP");
                String DEPZIP = Console.ReadLine();

                WriteToConsole("Please type DEPPESSANGER");
                String DEPPESSANGER = Console.ReadLine();

                WriteToConsole("Please type DEPDATE");
                String DEPDATE = Console.ReadLine();

                WriteToConsole("Please type ARVAIRPORT");
                String ARVAIRPORT = Console.ReadLine();

                WriteToConsole("Please type ARVZIP");
                String ARVZIP = Console.ReadLine();

                WriteToConsole("Please type ARVPESSANGER");
                String ARVPESSANGER = Console.ReadLine();

                WriteToConsole("Please type ARVDATE");
                String ARVDATE = Console.ReadLine();

                WriteToConsole("Please type QUERYSTRING");
                String QUERYSTRING = Console.ReadLine();

                var dic = new Dictionary<String, String>
                {
                    {Constant.GETFARES_DEPAIRPORT, String.IsNullOrEmpty(DEPAIRPORT) ? "lax" : DEPAIRPORT},
                    {Constant.GETFARES_DEPZIP, String.IsNullOrEmpty(DEPZIP) ? "93010" : DEPZIP},
                    {Constant.GETFARES_DEPPESSANGER, String.IsNullOrEmpty(DEPPESSANGER) ? "1" : DEPPESSANGER},
                    {Constant.GETFARES_DEPDATE, String.IsNullOrEmpty(DEPDATE) ? "5/1/2015" : DEPDATE},
                    {Constant.GETFARES_ARVAIRPORT, String.IsNullOrEmpty(ARVAIRPORT) ? "" : ARVAIRPORT},
                    {Constant.GETFARES_ARVZIP, String.IsNullOrEmpty(ARVZIP) ? "93030" : ARVZIP},
                    {Constant.GETFARES_ARVPESSANGER, String.IsNullOrEmpty(ARVPESSANGER) ? "1" : ARVPESSANGER},
                    {Constant.GETFARES_ARVDATE, String.IsNullOrEmpty(ARVDATE) ? "" : ARVDATE},
                    {Constant.GETFARES_QUERYSTRING, String.IsNullOrEmpty(QUERYSTRING) ? "" : QUERYSTRING}
                };

                Task<String> task = AppData.ApiCall(Constant.GETFARES, dic);
                task.Wait(); //Playing sync in console
                WriteToConsole("Done waiting, now parsing");

                var tt = (GetFaresResponse)AppData.ParseResponse(Constant.GETFARES, task.Result);

                WriteToConsole(tt.ToString(), ConsoleColor.White);

                WriteToConsole("");
                return tt;

            }
            catch (Exception e)
            {
                WriteToConsole(e.ToString(), ConsoleColor.Red);
            }
            return null;
	    }
        
        //private static UpdateCreditCardForPhoneResponse UpdateCreditCardForPhone()
        //{
        //    return null;
        //    try
        //    {
        //        WriteToConsole("Going to test Update Credit Card For Phone");
        //        WriteToConsole("");
        //        WriteToConsole("Let's get the login info first");

        //        var loginInfo = Login();

        //        WriteToConsole("");
        //        WriteToConsole("");

        //        WriteToConsole("Please type credit card INFO ID");
        //        String infoid = Console.ReadLine();

        //        WriteToConsole("Please type CCNAME and press Enter");
        //        String CCNAME = Console.ReadLine();

        //       WriteToConsole("Please type CCTYPEID and press Enter");
        //        String CCTYPEID = Console.ReadLine();

        //        WriteToConsole("Please type CID and press Enter");
        //        String CID = Console.ReadLine();

        //        WriteToConsole("Please type EXPDATE and press Enter");
        //        String EXPDATE = Console.ReadLine();

        //        WriteToConsole("Please type LOGINTYPE and press Enter");
        //        String LOGINTYPE = Console.ReadLine();

        //        WriteToConsole("Please type TOKENID and press Enter");
        //        String TOKENID = Console.ReadLine();

        //        WriteToConsole("Please type ZIP and press Enter");
        //        String ZIP = Console.ReadLine();

        //        WriteToConsole("Start waiting to get card info");

        //        var dic = new Dictionary<String, String>
        //        {
        //            {Constant.UPDATECREDITCARDFORPHONE_CCNAME, CCNAME},
        //            {Constant.UPDATECREDITCARDFORPHONE_CCTYPEID, CCTYPEID},
        //            {Constant.UPDATECREDITCARDFORPHONE_CID, CID},
        //            {Constant.UPDATECREDITCARDFORPHONE_CUSTOMERID, loginInfo.Customerid},
        //            {Constant.UPDATECREDITCARDFORPHONE_EXPDATE, EXPDATE},
        //            {Constant.UPDATECREDITCARDFORPHONE_INFOID, infoid},
        //            {Constant.UPDATECREDITCARDFORPHONE_LOGINTYPE, LOGINTYPE},
        //            {Constant.UPDATECREDITCARDFORPHONE_TOKENID, TOKENID},
        //            {Constant.UPDATECREDITCARDFORPHONE_ZIP, ZIP}
        //        };

        //        Task<String> task = AppData.ApiCall(Constant.UPDATECREDITCARDFORPHONE, dic);
        //        task.Wait(); //Playing sync in console
        //        WriteToConsole("Done waiting, now parsing");

        //        var tt = (UpdateCreditCardForPhoneResponse)AppData.ParseResponse(Constant.UPDATECREDITCARDFORPHONE, task.Result);

        //        WriteToConsole("Message = " + tt.Message, ConsoleColor.White);
        //        WriteToConsole("Result = " + tt.Result, ConsoleColor.White);

        //        WriteToConsole("");
        //        return tt;
        //    }
        //    catch (Exception e)
        //    {
        //        WriteToConsole(e.ToString(), ConsoleColor.Red);
        //    }
        //    return null;
        //}
        
        //private static DeleteCreditCardResponse DeleteCreditCard()
        //{
        //    return null;
        //    try
        //    {
        //        WriteToConsole("Going to test Delete Credit Card");
        //        WriteToConsole("");
        //        WriteToConsole("Please type credit card INFO ID");
        //        String infoid = Console.ReadLine();
        //
        //        WriteToConsole("Start waiting to get card info");
        //
        //        var dic = new Dictionary<String, String>
        //        {
        //            {Constant.DELETECREDITCARD_INFOID, infoid}
        //        };
        //
        //        Task<String> task = AppData.ApiCall(Constant.DELETECREDITCARD, dic);
        //        task.Wait(); //Playing sync in console
        //        WriteToConsole("Done waiting, now parsing");
        //
        //        var tt = (DeleteCreditCardResponse)AppData.ParseResponse(Constant.DELETECREDITCARD, task.Result);
        //
        //        WriteToConsole("Message = " + tt.Message, ConsoleColor.White);
        //        WriteToConsole("Result = " + tt.Result, ConsoleColor.White);
        //
        //        WriteToConsole("");
        //        return tt;
        //    }
        //    catch (Exception e)
        //    {
        //        WriteToConsole(e.ToString(), ConsoleColor.Red);
        //    }
        //    return null;
        //}

        private static DeleteCreditCardNewResponse DeleteCreditCardNew()
        {
            try
            {
                WriteToConsole("Going to test Delete Credit Card New");
                WriteToConsole("");

                WriteToConsole("Please type credit card CUSTOMERID");
                String CUSTOMERID = Console.ReadLine();

                WriteToConsole("Please type credit card INFOID");
                String INFOID = Console.ReadLine();

                WriteToConsole("Please type credit card LOGINTYPE");
                String LOGINTYPE = Console.ReadLine();

                WriteToConsole("Please type credit card TOKENID");
                String TOKENID = Console.ReadLine();

                WriteToConsole("Start waiting ...");

                var dic = new Dictionary<String, String>
                {
                    {Constant.DELETECREDITCARDNEW_CUSTOMERID, CUSTOMERID},
                    {Constant.DELETECREDITCARDNEW_INFOID, INFOID},
                    {Constant.DELETECREDITCARDNEW_LOGINTYPE, LOGINTYPE},
                    {Constant.DELETECREDITCARDNEW_TOKENID, TOKENID}
                };

                Task<String> task = AppData.ApiCall(Constant.DELETECREDITCARDNEW, dic);
                task.Wait(); //Playing sync in console
                WriteToConsole("Done waiting, now parsing");

                var tt = (DeleteCreditCardNewResponse)AppData.ParseResponse(Constant.DELETECREDITCARDNEW, task.Result);

                WriteToConsole(tt.ToString(), ConsoleColor.White);

                WriteToConsole("");
                return tt;
            }
            catch (Exception e)
            {
                WriteToConsole(e.ToString(), ConsoleColor.Red);
            }
            return null;
        }

        //private static InsertCreditCardDetailsForPhoneResponse InsertCreditCardDetailsForPhone()
        //{
        //    try
        //    {
        //        WriteToConsole("Going to test Insert Credit Card For Phone");
        //        WriteToConsole("");
        //        WriteToConsole("Let's get the login info first");

        //        var loginInfo = Login();

        //        WriteToConsole("");
        //        WriteToConsole("");

        //        WriteToConsole("Please type CCNAME and press Enter");
        //        String CCNAME = Console.ReadLine();

        //        WriteToConsole("Please type CCNUM and press Enter");
        //        String CCNUM = Console.ReadLine();

        //        WriteToConsole("Please type CCTYPE and press Enter");
        //        String CCTYPE = Console.ReadLine();

        //        WriteToConsole("Please type CID and press Enter");
        //        String CID = Console.ReadLine();

        //        WriteToConsole("Please type EXPDATE and press Enter");
        //        String EXPDATE = Console.ReadLine();

        //        WriteToConsole("Please type LOGINTYPE and press Enter");
        //        String LOGINTYPE = Console.ReadLine();

        //        WriteToConsole("Please type TOKENID and press Enter");
        //        String TOKENID = Console.ReadLine();

        //        WriteToConsole("Please type ZIP and press Enter");
        //        String ZIP = Console.ReadLine();

        //        WriteToConsole("Start waiting to get card info");

        //        var dic = new Dictionary<String, String>
        //        {
        //            {Constant.INSERTCREDITCARDDETAILSFORPHONE_CCNAME, String.IsNullOrEmpty(CCNAME) ? "Test User" : CCNAME},
        //            {Constant.INSERTCREDITCARDDETAILSFORPHONE_CCNUM, String.IsNullOrEmpty(CCNUM) ? "4111111111111111" : CCNUM},
        //            {Constant.INSERTCREDITCARDDETAILSFORPHONE_CCTYPE, String.IsNullOrEmpty(CCTYPE) ? "2" : CCTYPE},
        //            {Constant.INSERTCREDITCARDDETAILSFORPHONE_CID, String.IsNullOrEmpty(CID) ? "123" : CID},
        //            {Constant.INSERTCREDITCARDDETAILSFORPHONE_CUSTOMERID, loginInfo.Customerid},
        //            {Constant.INSERTCREDITCARDDETAILSFORPHONE_EXPDATE, String.IsNullOrEmpty(EXPDATE) ? "1020" : EXPDATE},
        //            {Constant.INSERTCREDITCARDDETAILSFORPHONE_LOGINTYPE, String.IsNullOrEmpty(LOGINTYPE) ? "-1" : LOGINTYPE},
        //            {Constant.INSERTCREDITCARDDETAILSFORPHONE_TOKENID, String.IsNullOrEmpty(TOKENID) ? "" : TOKENID},
        //            {Constant.INSERTCREDITCARDDETAILSFORPHONE_ZIP, String.IsNullOrEmpty(ZIP) ? "94040" : ZIP}
        //        };

        //        Task<String> task = AppData.ApiCall(Constant.INSERTCREDITCARDDETAILSFORPHONE, dic);
        //        task.Wait(); //Playing sync in console
        //        WriteToConsole("Done waiting, now parsing");

        //        var tt = (InsertCreditCardDetailsForPhoneResponse) AppData.ParseResponse(Constant.INSERTCREDITCARDDETAILSFORPHONE, task.Result);


        //        WriteToConsole("Message = " + tt.Message, ConsoleColor.White);
        //        WriteToConsole("Result = " + tt.Result, ConsoleColor.White);

        //        WriteToConsole("");
        //        return tt;
        //    }
        //    catch (Exception e)
        //    {
        //        WriteToConsole(e.ToString(), ConsoleColor.Red);
        //    }
        //    return null;
        //}

        //private static GetCreditCardDetailsNewForPhoneResponse GetCreditCardDetailsNewForPhoneResponse()
        //{
        //    try
        //    {
        //        WriteToConsole("Going to test Get Credit Card Details New For Phone");
        //        WriteToConsole("");
        //        WriteToConsole("Let's get the login info first");

        //        var loginInfo = Login();

        //        WriteToConsole("Start waiting to get card info");

        //        var dic = new Dictionary<String, String>
        //        {
        //            {Constant.GETCREDITCARDDETAILSNEWFORPHONE_CUSTOMERID, loginInfo.Customerid},
        //            {Constant.GETCREDITCARDDETAILSNEWFORPHONE_LOGINTYPE, "-1"},
        //            {Constant.GETCREDITCARDDETAILSNEWFORPHONE_TOKENID, ""}
        //        };

        //        Task<String> task = AppData.ApiCall(Constant.GETCREDITCARDDETAILSNEWFORPHONE, dic);
        //        task.Wait(); //Playing sync in console
        //        WriteToConsole("Done waiting, now parsing");
        //        var tt = (GetCreditCardDetailsNewForPhoneResponse) AppData.ParseResponse(Constant.GETCREDITCARDDETAILSNEWFORPHONE, task.Result);

        //        WriteToConsole(tt.ToString(), ConsoleColor.White);
        //        WriteToConsole("");
        //        return tt;
        //    }
        //    catch (Exception e)
        //    {
        //        WriteToConsole(e.ToString(), ConsoleColor.Red);
        //    }
        //    return null;
        //}

        //private static LoginResponse Login()
        //{
        //    try
        //    {
        //        WriteToConsole("Going to test Login");
        //        WriteToConsole("Please type username and press Enter (if you just press enter, the default \"test1@test.com\" value will be used)");
        //        String username = Console.ReadLine();
        //        WriteToConsole("Please type password and press Enter (if you just press enter, the default \"test\" value will be used)");
        //        String pwd = Console.ReadLine();


        //        var dic = new Dictionary<String, String>
        //        {
        //            {Constant.LOGINAPI_USERNAME, String.IsNullOrEmpty(username) ? "test1@test.com" : username},
        //            {Constant.LOGINAPI_PASSWORD, String.IsNullOrEmpty(pwd) ? "test1" : pwd}
        //        };

        //        WriteToConsole("Start waiting");
        //        Task<String> task = AppData.ApiCall(Constant.LOGINAPI, dic);
        //        task.Wait(); //Playing sync in console

        //        WriteToConsole("Done waiting, now parsing");
        //        var tt = (LoginResponse) AppData.ParseResponse(Constant.LOGINAPI, task.Result);

        //        WriteToConsole("CustType = " + tt.CustType, ConsoleColor.White);
        //        WriteToConsole("Customerid = " + tt.Customerid, ConsoleColor.White);
        //        WriteToConsole("Email = " + tt.Email, ConsoleColor.White);
        //        WriteToConsole("FirstName = " + tt.FirstName, ConsoleColor.White);
        //        WriteToConsole("LastName = " + tt.LastName, ConsoleColor.White);
        //        WriteToConsole("Phone = " + tt.Phone, ConsoleColor.White);
        //        WriteToConsole("UserName = " + tt.UserName, ConsoleColor.White);
        //        WriteToConsole("message = " + tt.Message, ConsoleColor.White);
        //        WriteToConsole("title = " + tt.Title, ConsoleColor.White);
        //        WriteToConsole("");
        //        return tt;
        //    }
        //    catch (Exception e)
        //    {
        //        WriteToConsole(e.ToString(), ConsoleColor.Red);
        //    }
        //    return null;
        //}

        //private static ValidateDiscountCouponResponse ValidateDiscountCoupon()
        //{
        //    try
        //    {
        //        WriteToConsole("Going to test VALIDATE DISCOUNT COUPON");
        //        WriteToConsole("Please type coupon and press Enter (if you just press enter, the default \"RRTHANKS14\" code will be used)");
        //        String coupon = Console.ReadLine();

        //        var dic = new Dictionary<String, String>
        //        {
        //            {Constant.VALIDATEDISCOUNTCOUPON_CODE, String.IsNullOrEmpty(coupon) ? "RRTHANKS14" : coupon},
        //            {Constant.VALIDATEDISCOUNTCOUPON_CUSTOMERID, "-1"},
        //            {Constant.VALIDATEDISCOUNTCOUPON_EMAIL, ""},
        //            {Constant.VALIDATEDISCOUNTCOUPON_SERVICEID, "-1"},
        //            {Constant.VALIDATEDISCOUNTCOUPON_TRAVELDATE, DateTime.Now.ToString("d")},
        //            {Constant.VALIDATEDISCOUNTCOUPON_VALIDATIONTYPE, "1"}
        //        };

        //        WriteToConsole("Start waiting");
        //        Task<String> task = AppData.ApiCall(Constant.VALIDATEDISCOUNTCOUPON, dic);
        //        task.Wait(); //Playing sync in console

        //        WriteToConsole("Done waiting, now parsing");
        //        var tt = (ValidateDiscountCouponResponse)AppData.ParseResponse(Constant.VALIDATEDISCOUNTCOUPON, task.Result);

        //        WriteToConsole("Result = " + tt.Result, ConsoleColor.White);
        //        WriteToConsole("Message = " + tt.Message, ConsoleColor.White);
        //        WriteToConsole("");
        //        return tt;
        //    }
        //    catch (Exception e)
        //    {
        //        WriteToConsole(e.ToString(), ConsoleColor.Red);
        //    }
        //    return null;
        //}

        //private static GetAirlineResponse GetAirline()
        //{
        //    try
        //    {
        //        WriteToConsole("Going to test GET AIRLINE API");
        //        //WriteToConsole("Please type zip and press Enter");
        //        //String zip = Console.ReadLine();

        //        var dic = new Dictionary<String, String> { { Constant.GETAIRLINE_PREFIX, "" } };
        //        WriteToConsole("Start waiting");
        //        Task<String> task = AppData.ApiCall(Constant.GETAIRLINE, dic);
        //        task.Wait(); //Playing sync in console

        //        WriteToConsole("Done waiting, now parsing");
        //        var tt = (GetAirlineResponse)AppData.ParseResponse(Constant.GETAIRLINE, task.Result);

        //        WriteToConsole("TITLE = " + tt.Title, ConsoleColor.White);
        //        WriteToConsole("MSG = " + tt.Message, ConsoleColor.White);
        //        WriteToConsole("", ConsoleColor.White);
        //        foreach (GetAirlineResponseItem item in tt.List)
        //        {
        //            WriteToConsole("", ConsoleColor.White);
        //            WriteToConsole("Airline = " + item.Airline, ConsoleColor.White);
        //            WriteToConsole("diff = " + item.diff, ConsoleColor.White);
        //            WriteToConsole("id = " + item.id, ConsoleColor.White);
        //        }
        //        return tt;
        //    }
        //    catch (Exception e)
        //    {
        //        WriteToConsole(e.ToString(), ConsoleColor.Red);
        //    }
        //    return null;
        //}

        //private static GetCityResponse GetCityApi_ZIP()
	    //{
        //    try
        //    {
        //        WriteToConsole("Going to test GET CITY API");
        //        WriteToConsole("Please type zip and press Enter");
        //        String zip = Console.ReadLine();
        //
        //        var dic = new Dictionary<String, String> { { Constant.GETCITYAPI_ZIP, zip } };
        //        WriteToConsole("Start waiting");
        //        Task<String> task = AppData.ApiCall(Constant.GETCITYAPI, dic);
        //        task.Wait(); //Playing sync in console
        //
        //        WriteToConsole("Done waiting, now parsing");
        //        var tt = (GetCityResponse)AppData.ParseResponse(Constant.GETCITYAPI, task.Result);
        //
        //        WriteToConsole("City = " + tt.City, ConsoleColor.White);
        //        WriteToConsole("Result = " + tt.Result, ConsoleColor.White);
        //        return tt;
        //    }
        //    catch (Exception e)
        //    {
        //        WriteToConsole(e.ToString(), ConsoleColor.Red);
        //    }
        //    return null;
	    //}

	    private static void WriteToConsole(String message, ConsoleColor desiredColor = ConsoleColor.Green)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = desiredColor;

	        Console.WriteLine("{0}\t-\t{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), message);

            Console.ForegroundColor = originalColor;
	    }
	}
}
