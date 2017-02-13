namespace RoadRunner.Shared
{
	// ReSharper disable InconsistentNaming
	public static class Constant
	{
		public const string APPNAME = "RoadRunner";
		public const string CONFIRMATION_EMAIL_TO = "dev@rrshuttle.com";
		public const string LOGOUT = "Sign out";

		public const int HOURS_RANGE_FROM_CURRENT = 2;
		public const int MAP_REFRESH_IN_SECONDS = 20;

		public const string DEFAULT_REQUIRED_API_VALUE = " ";

		public static string[] optionMenu =
		{
			"HOME", "SCHEDULE RIDE", "GET A FREE QUOTE", "RESERVATIONS", "SAVED ADDRESSES", "SUPPORT", "BILLING INFO", //"PREFERENCES",
            "RESET PASSWORD", "LOGOUT"
		};

		public const string BASEURL = @"http://sandbox.gorrshuttle.com/rr09.asmx/";
		//public const string BASEURL = @"http://sandbox.rrshuttle.com/rr09.asmx/";


		public const string AIRPORTCODEURL = @"http://airportcode.riobard.com/search?q={0}&fmt=JSON";
		public const string GOOGLE_PLACE_APIKEY = "AIzaSyAiBwRUm_KZDv_sp3eI7F8hxkePqDTvY20";

		public const string LINKEDIN_OAUTH_DOMAIN = "erlendg.eu.auth0.com";
		public const string LINKEDIN_OAUTH_CLIENT_ID = "aLxCACnX2f4eQQApUxFIstcJ5ss1v0TN";

		public const string INSIGHTS_APIKEY = "6f8195442f971f6f50c19f28e2f8ca936ced57a0";

		public const string MISSING_PICKUP_LOCATION_MESSAGE = "Please Enter Pick up Location";
		public const string MISSING_DROPOFF_LOCATION_MESSAGE = "Please Enter Drop Off Location";
		public const string MISSING_FLIGHT_NUMBER_MESSAGE = "Please Enter Flight No";
		public const string MISSING_FLIGHT_TIME_MESSAGE = "Please Enter Flight Time";
		public const string MISSING_FLIGHT_TYPE_MESSAGE = "Please Select Flight Type";
		public const string MISSING_REQUESTED_ARRIVAL_TIME_MESSAGE = "Please Enter Requested Arrival Time";
		public const string MISSING_NUMBER_OF_PASS_MESSAGE = "Please Enter Number Of Passenger";
		public const string MISSING_CARD_NUMBER_MESSAGE = "Please Enter a Card Number";
		public const string MISSING_PICKUP_DATE_MESSAGE = "Please Enter a Pickup Date";
		public const string MISSING_SERVICE_MESSAGE = "Please select a service";
		public const string NO_INTERNET_MESSAGE = "No internet connection. Please check your connections and try again.";

		public const string LINKEDIN_UNCONFIRMED_EMAIL_MESSAGE = "Your LinkedIn account does not have a confirmed email address. Please confirm email and try again.";

		public const string REQUESTED_DATE_INVALID_DOMESTIC_MESSAGE =
			"Requested Arrival Date is invalid. For domestic flights, the requested arrival date must be at least 60 minutes before flight time.";

		public const string REQUESTED_DATE_INVALID_INTERNATIONAL_MESSAGE =
			"Requested Arrival Date is invalid. For international flights, the requested arrival date must be at least 90 minutes before flight time.";

		public const string EXTRA_BAGGAGE_MESSAGE = "All passengers are allowed 2 medium sized luggages and 1 carry-on each";
		public const string INVALID_LOCATION_ZIP_CODE_MESSAGE = "The location you entered could not be identified. Please enter a new location.";

		public const string EXTRA_BAGGAGE_PRODUCT_ID = "25";
		public const string MEET_AND_GREET_PRODUCT_ID = "0";

		public const string LOGINAPI = @"CheckLogin?";
		public const string LOGINAPI_USERNAME = "UserName";
		public const string LOGINAPI_PASSWORD = "Password";

		// public const string LOGINAPI = @"CheckLoginForAndroid?";
		// public const string LOGINAPI_USERNAME = "username";
		// public const string LOGINAPI_PASSWORD = "password";
		// public const string LOGINAPI_TOKEN = "token";
		// public const string LOGINAPI_TYPE = "type";

		public const string FORGOTPASSWORDAPI = @"ForgotPassword?";
		public const string FORGOTPASSWORDAPIUSEREMAIL = @"UserEmailAddress";

		public const string GETAPPAPI = @"GetAppSettings?";

		public const string REGESTRATIONAPI = @"NewUserRegistration?";
		public const string REGESTRATIONAPI_EmailAddress = "EmailAddress";
		public const string REGESTRATIONAPI_FNAME = "FName";
		public const string REGESTRATIONAPI_LNAME = "LName";
		public const string REGESTRATIONAPI_HOMEPHONE = "HomePhone";
		public const string REGESTRATIONAPI_CELLPHONE = "CellPhone";
		public const string REGESTRATIONAPI_NUMBER = "Number";
		public const string REGESTRATIONAPI_STREET = "Street";
		public const string REGESTRATIONAPI_UNIT = "Unit";
		public const string REGESTRATIONAPI_COMPLEX = "Complex";
		public const string REGESTRATIONAPI_CITY = "City";
		public const string REGESTRATIONAPI_ZIP = "Zip";
		public const string REGESTRATIONAPI_PASSWORD = "Password";
		public const string REGESTRATIONAPI_DIRECTION = "Direction";
		public const string REGESTRATIONAPI_CUSTID = "Custid";
		public const string REGESTRATIONAPI_CUSTTYPE = "CustType";


		public const string REGESTRATION_FOR_ANDROID_API = @"NewUserRegistrationForAndroid?";
		public const string REGESTRATION_FOR_ANDROID_API_EmailAddress = "EmailAddress";
		public const string REGESTRATION_FOR_ANDROID_API_FNAME = "FName";
		public const string REGESTRATION_FOR_ANDROID_API_LNAME = "LName";
		public const string REGESTRATION_FOR_ANDROID_API_HOMEPHONE = "HomePhone";
		public const string REGESTRATION_FOR_ANDROID_API_CELLPHONE = "CellPhone";
		public const string REGESTRATION_FOR_ANDROID_API_NUMBER = "Number";
		public const string REGESTRATION_FOR_ANDROID_API_STREET = "Street";
		public const string REGESTRATION_FOR_ANDROID_API_UNIT = "Unit";
		public const string REGESTRATION_FOR_ANDROID_API_COMPLEX = "Complex";
		public const string REGESTRATION_FOR_ANDROID_API_CITY = "City";
		public const string REGESTRATION_FOR_ANDROID_API_ZIP = "Zip";
		public const string REGESTRATION_FOR_ANDROID_API_PASSWORD = "Password";
		public const string REGESTRATION_FOR_ANDROID_API_DIRECTION = "Direction";
		public const string REGESTRATION_FOR_ANDROID_API_CUSTID = "Custid";
		public const string REGESTRATION_FOR_ANDROID_API_CUSTTYPE = "CustType";
		public const string REGESTRATION_FOR_ANDROID_API_USERNAME = "userName";
		public const string REGESTRATION_FOR_ANDROID_API_TYPE = "type";
		public const string REGESTRATION_FOR_ANDROID_API_TOKEN = "token";
		public const string REGESTRATION_FOR_ANDROID_API_ISSMS = "IsSMS";



		public const string UPDATE_CONTACT_FOR_ANDROID_API = @"UpdateContactForAndroid?";
		public const string UPDATE_CONTACT_FOR_ANDROID_API_CUSTOMERID = "CustomerId";
		public const string UPDATE_CONTACT_FOR_ANDROID_API_FIRSTNAME = "FirstName";
		public const string UPDATE_CONTACT_FOR_ANDROID_API_LASTNAME = "LastName";
		public const string UPDATE_CONTACT_FOR_ANDROID_API_CONTACT = "Contact";
		public const string UPDATE_CONTACT_FOR_ANDROID_API_ISSMS = "IsSMS";


		public const string GET_MY_PROFILE_FOR_ANDROID_API = @"GetMyProfileInfoForAndroid?";
		public const string GET_MY_PROFILE_FOR_ANDROID_API_CUSTOMERID = "CustId";



		public const string INSERTCARD_API = @"InsertCreditCardDetailsForPhone?";
		public const string INSERTCARD_CUSTOMER_ID = "Customerid";
		public const string INSERTCARD_CCTYPE = "CCType";
		public const string INSERTCARD_CID = "Cid";
		public const string INSERTCARD_CCNUM = "CCNum";
		public const string INSERTCARD_EXPDATE = "ExpDate";
		public const string INSERTCARD_CCNAME = "CCName";
		public const string INSERTCARD_ZIP = "Zip";
		public const string INSERTCARD_TOKENID = "TokenId";
		public const string INSERTCARD_LOGINTYPE = "Logintype";


		public const string RESETPASSWORDAPI = @"ResetPassword?";
		public const string RESETPASSWORDAPIUserName = "Username";
		public const string RESETPASSWORDAPIOLDPASSWORD = "Password";
		public const string RESETPASSWORDAPINEWPASSWORD = "NewPassword";

		public const string RESETEMAILAPI = @"ResetEmail?";
		public const string RESETEMAILAPIUSERNAME = "Username";
		public const string RESETEMAILAPIPASSWORD = "Password";
		public const string RESETEMAILAPINEWEMAIL = "newemail";
		public const string RESETEMAILAPILOGINTYPE = "logintype";

		public const string GOOGLECLIENTID = "289340959525-igunj9690u0t90jkffap7b4vikdkthv5.apps.googleusercontent.com";

		public const string FacebookAppId = "1668260886738493";
		public const string LinkedInAppId = "4381863";
		public const string DisplayName = "www.rrshuttle.com";

		public const double KEYBOARD_ANIMATION_DURATION = 0.3;
		public const double MINIMUM_SCROLL_FRACTION = 0.2;
		public const double MAXIMUM_SCROLL_FRACTION = 0.6;
		public const double PORTRAIT_KEYBOARD_HEIGHT = 216;
		public const double LANDSCAPE_KEYBOARD_HEIGHT = 162;

		public const string VALIDATEDISCOUNTCOUPON = @"ValidateDiscountCoupon?";
		public const string VALIDATEDISCOUNTCOUPON_CODE = @"Code";
		public const string VALIDATEDISCOUNTCOUPON_CUSTOMERID = @"customerId";
		public const string VALIDATEDISCOUNTCOUPON_SERVICEID = @"ServiceId";
		public const string VALIDATEDISCOUNTCOUPON_EMAIL = @"Email";
		public const string VALIDATEDISCOUNTCOUPON_TRAVELDATE = @"traveldate";
		public const string VALIDATEDISCOUNTCOUPON_VALIDATIONTYPE = @"ValidationType";

		public const string GETAIRLINE = @"GetAirLine?";
		public const string GETAIRLINE_PREFIX = @"PreFix";

		public const string INSERTCREDITCARDDETAILSFORPHONE = "InsertCreditCardDetailsForPhone?";
		public const string INSERTCREDITCARDDETAILSFORPHONE_CUSTOMERID = "Customerid";
		public const string INSERTCREDITCARDDETAILSFORPHONE_CCTYPE = "CCType";
		public const string INSERTCREDITCARDDETAILSFORPHONE_CID = "Cid";
		public const string INSERTCREDITCARDDETAILSFORPHONE_CCNUM = "CCNum";
		public const string INSERTCREDITCARDDETAILSFORPHONE_EXPDATE = "ExpDate";
		public const string INSERTCREDITCARDDETAILSFORPHONE_CCNAME = "CCName";
		public const string INSERTCREDITCARDDETAILSFORPHONE_ZIP = "Zip";
		public const string INSERTCREDITCARDDETAILSFORPHONE_TOKENID = "TokenId";
		public const string INSERTCREDITCARDDETAILSFORPHONE_LOGINTYPE = "Logintype";

		public const string GETFARES = "GetFares?";
		public const string GETFARES_DEPAIRPORT = "DepAirport";
		public const string GETFARES_DEPZIP = "DepZip";
		public const string GETFARES_DEPPESSANGER = "DepPessanger";
		public const string GETFARES_DEPDATE = "DepDate";
		public const string GETFARES_ARVAIRPORT = "ArvAirport";
		public const string GETFARES_ARVZIP = "ArvZip";
		public const string GETFARES_ARVPESSANGER = "ArvPessanger";
		public const string GETFARES_ARVDATE = "ArvDate";
		public const string GETFARES_QUERYSTRING = "QueryString";
		public const string GETFARES_DISCPROMOCODE = "DiscPromoCode";

		public const string GETRECENTPICKUPADDRESS = "GetRecentPickUpAddress?";
		public const string GETRECENTPICKUPADDRESS_CUSTOMERID = "Customerid";
		public const string GETRECENTPICKUPADDRESS_RESNO = "ResNo";
		public const string GETRECENTPICKUPADDRESS_ISDARPROFILE = "isDARProfile";

		public const string GETMYBOOKEDRESERVATIONS = "GetMyBookedReservations?";
		public const string GETMYBOOKEDRESERVATIONS_CUSTOMERID = "Customerid";
		public const string GETMYBOOKEDRESERVATIONS_LASTSYNCON = "LastSyncOn";
		public const string GETMYBOOKEDRESERVATIONS_ISFUTURERES = "IsFutureRes";

		public const string CANCELRESERVATIONFORANDROID = "CancelReservationForAndroid?";
		public const string CANCELRESERVATIONFORANDROID_UPDATEMODE = "UpdateMode";
		public const string CANCELRESERVATIONFORANDROID_RESID = "ResId";
		public const string CANCELRESERVATIONFORANDROID_FLAG = "Flag";

		public const string GETNEARESTVEHICLEFORCURRENTLOCATIONFORANDROID = "GetNearestVehicleForCurrentLocationForAndroid?";
		public const string GETNEARESTVEHICLEFORCURRENTLOCATIONFORANDROID_LATITUDE = "latitude";
		public const string GETNEARESTVEHICLEFORCURRENTLOCATIONFORANDROID_LONGITUDE = "longitude";
		public const string GETNEARESTVEHICLEFORCURRENTLOCATIONFORANDROID_DISTANCE = "distance";
		public const string GETNEARESTVEHICLEFORCURRENTLOCATIONFORANDROID_DISTANCETYPE = "distanceType";
		public const string GETNEARESTVEHICLEFORCURRENTLOCATIONFORANDROID_SERVICEID = "serviceId";

		public const string GETIATAAIRPORTCODE = "GetIATAAirportCode?";
		public const string GETIATAAIRPORTCODE_LATITUDE = "latitude";
		public const string GETIATAAIRPORTCODE_LONGITUDE = "longitude";
		public const string GETIATAAIRPORTCODE_FULLAIRPORTNAME = "fullAirportName";

		public const string DELETECREDITCARD = "DeleteCreditCard?";
		public const string DELETECREDITCARD_INFOID = "Infoid";

		public const string DELETECREDITCARDNEW = "DeleteCreditCardNew?";
		public const string DELETECREDITCARDNEW_INFOID = "Infoid";
		public const string DELETECREDITCARDNEW_CUSTOMERID = "Customerid";
		public const string DELETECREDITCARDNEW_TOKENID = "TokenId";
		public const string DELETECREDITCARDNEW_LOGINTYPE = "Logintype";



		public const string UPDATECREDITCARDFORPHONE = "UpdateCreditCardForPhone?";
		public const string UPDATECREDITCARDFORPHONE_INFOID = "Infoid";
		public const string UPDATECREDITCARDFORPHONE_CUSTOMERID = "Customerid";
		public const string UPDATECREDITCARDFORPHONE_CCTYPEID = "ccTypeid";
		public const string UPDATECREDITCARDFORPHONE_CID = "Cid";
		public const string UPDATECREDITCARDFORPHONE_EXPDATE = "Expdate";
		public const string UPDATECREDITCARDFORPHONE_CCNAME = "CCName";
		public const string UPDATECREDITCARDFORPHONE_ZIP = "Zip";
		public const string UPDATECREDITCARDFORPHONE_TOKENID = "TokenId";
		public const string UPDATECREDITCARDFORPHONE_LOGINTYPE = "Logintype";


		public const string GETCREDITCARDDETAILSNEWFORPHONE = "GetCreditCardDetailsNewForPhone?";
		public const string GETCREDITCARDDETAILSNEWFORPHONE_CUSTOMERID = "Customerid";
		public const string GETCREDITCARDDETAILSNEWFORPHONE_TOKENID = "TokenId";
		public const string GETCREDITCARDDETAILSNEWFORPHONE_LOGINTYPE = "Logintype";


		public const string GETCONFIRMATIONBYDETAILSNEW = "GetConfirmationByDetailsNew?";
		public const string GETCONFIRMATIONBYDETAILSNEW_CUSTID = "CustID";
		public const string GETCONFIRMATIONBYDETAILSNEW_TRAVELLERID = "TravellerID";
		public const string GETCONFIRMATIONBYDETAILSNEW_CUSTTYPE = "CustType";
		public const string GETCONFIRMATIONBYDETAILSNEW_RESTYPE = "ResType";
		public const string GETCONFIRMATIONBYDETAILSNEW_ARVDATE = "ArvDate";
		public const string GETCONFIRMATIONBYDETAILSNEW_NOA = "NOA";
		public const string GETCONFIRMATIONBYDETAILSNEW_ARVAIRPORT = "ArvAirport";
		public const string GETCONFIRMATIONBYDETAILSNEW_ARVPASSENGER = "ArvPassenger";
		public const string GETCONFIRMATIONBYDETAILSNEW_ARVPICKUPTIME = "ArvPickupTime";
		public const string GETCONFIRMATIONBYDETAILSNEW_ARVFEE = "ArvFee";
		public const string GETCONFIRMATIONBYDETAILSNEW_ARVGRATUITY = "ArvGratuity";
		public const string GETCONFIRMATIONBYDETAILSNEW_DISCOUNT = "Discount";
		public const string GETCONFIRMATIONBYDETAILSNEW_CONF = "Conf";
		public const string GETCONFIRMATIONBYDETAILSNEW_SPECIALSERVICEAMT = "SpecialServiceamt";
		public const string GETCONFIRMATIONBYDETAILSNEW_ARVSERVICE = "ArvService";
		public const string GETCONFIRMATIONBYDETAILSNEW_ARVZIP = "ArvZip";
		public const string GETCONFIRMATIONBYDETAILSNEW_SPECIALSERVICEIDARV = "SpecialServiceIDArv";
		public const string GETCONFIRMATIONBYDETAILSNEW_REDEMPTIONAMT = "RedemptionAmt";
		public const string GETCONFIRMATIONBYDETAILSNEW_CARDTYPE = "CardType";
		public const string GETCONFIRMATIONBYDETAILSNEW_CCNUM = "CCNum";
		public const string GETCONFIRMATIONBYDETAILSNEW_CCNAME = "CCname";
		public const string GETCONFIRMATIONBYDETAILSNEW_CCEXPDATE = "CCExpDate";
		public const string GETCONFIRMATIONBYDETAILSNEW_CCCID = "CCCID";
		public const string GETCONFIRMATIONBYDETAILSNEW_CCTYPEID = "CCTypeID";
		public const string GETCONFIRMATIONBYDETAILSNEW_CZIP = "CZip";
		public const string GETCONFIRMATIONBYDETAILSNEW_REQTYPE = "ReqType";
		public const string GETCONFIRMATIONBYDETAILSNEW_INFOID = "InfoID";
		public const string GETCONFIRMATIONBYDETAILSNEW_PMTMODE = "PmtMode";
		public const string GETCONFIRMATIONBYDETAILSNEW_PMTDETAILS = "PMTDetails";
		public const string GETCONFIRMATIONBYDETAILSNEW_ISWINDOWID = "ISWindowID";
		public const string GETCONFIRMATIONBYDETAILSNEW_ARVCITY = "ArvCity";
		public const string GETCONFIRMATIONBYDETAILSNEW_SPECIALSERVICESJOURNEYTYPE = "SpecialServicesJourneyType";
		public const string GETCONFIRMATIONBYDETAILSNEW_TRAVELERFNAME = "TravelerFName";
		public const string GETCONFIRMATIONBYDETAILSNEW_TRAVELERLNAME = "TravelerLName";
		public const string GETCONFIRMATIONBYDETAILSNEW_TRAVELERHOMEPHNO = "TravelerHomePhno";
		public const string GETCONFIRMATIONBYDETAILSNEW_TRAVELERWORKPHNO = "TravelerWorkPhno";
		public const string GETCONFIRMATIONBYDETAILSNEW_TRAVELERCELLNO = "Travelercellno";
		public const string GETCONFIRMATIONBYDETAILSNEW_IP = "IP";
		public const string GETCONFIRMATIONBYDETAILSNEW_DEPZIP = "DepZip";
		public const string GETCONFIRMATIONBYDETAILSNEW_DEPSERVICE = "DepService";
		public const string GETCONFIRMATIONBYDETAILSNEW_DEPPICKUP = "DepPickUp";
		public const string GETCONFIRMATIONBYDETAILSNEW_ARVSTREETNUMBER = "ArvStreetNumber";
		public const string GETCONFIRMATIONBYDETAILSNEW_DEPSTREETNUMBER = "DepStreetNumber";
		public const string GETCONFIRMATIONBYDETAILSNEW_ARVSTREET = "ArvStreet";
		public const string GETCONFIRMATIONBYDETAILSNEW_DEPSTREET = "DepStreet";
		public const string GETCONFIRMATIONBYDETAILSNEW_ARVUNIT = "ArvUnit";
		public const string GETCONFIRMATIONBYDETAILSNEW_DEPUNIT = "DepUnit";
		public const string GETCONFIRMATIONBYDETAILSNEW_ARVDIRECTIONS = "ArvDirections";
		public const string GETCONFIRMATIONBYDETAILSNEW_DEPDIRECTIONS = "DepDirections";
		public const string GETCONFIRMATIONBYDETAILSNEW_COMPLEXARV = "ComplexArv";
		public const string GETCONFIRMATIONBYDETAILSNEW_COMPLEXDEP = "ComplexDep";
		public const string GETCONFIRMATIONBYDETAILSNEW_ARVAP = "ArvAP";
		public const string GETCONFIRMATIONBYDETAILSNEW_DEPAP = "DepAP";
		public const string GETCONFIRMATIONBYDETAILSNEW_ARVFLT = "ArvFlt";
		public const string GETCONFIRMATIONBYDETAILSNEW_DEPFLT = "DepFlt";
		public const string GETCONFIRMATIONBYDETAILSNEW_DEPPICKUPTIME = "DepPickupTime";
		public const string GETCONFIRMATIONBYDETAILSNEW_ARVFLTTIME = "arvFltTime";
		public const string GETCONFIRMATIONBYDETAILSNEW_DEPFLTTIME = "DepFltTime";
		public const string GETCONFIRMATIONBYDETAILSNEW_ARVAIRLINE = "ArvAirline";
		public const string GETCONFIRMATIONBYDETAILSNEW_DEPAIRLINE = "DepAirline";
		public const string GETCONFIRMATIONBYDETAILSNEW_FLTTYPE = "FltType";
		public const string GETCONFIRMATIONBYDETAILSNEW_ORIGIN = "origin";
		public const string GETCONFIRMATIONBYDETAILSNEW_FLYINGTO = "FlyingTo";
		public const string GETCONFIRMATIONBYDETAILSNEW_DEPPASSENGER = "DepPassenger";
		public const string GETCONFIRMATIONBYDETAILSNEW_DEPDATE = "DepDate";
		public const string GETCONFIRMATIONBYDETAILSNEW_DEPCITY = "DepCity";
		public const string GETCONFIRMATIONBYDETAILSNEW_DEPFEE = "DepFee";
		public const string GETCONFIRMATIONBYDETAILSNEW_DEPGRATUITY = "DepGratuity";
		public const string GETCONFIRMATIONBYDETAILSNEW_SURCHARGE = "Surcharge";
		public const string GETCONFIRMATIONBYDETAILSNEW_TRAVELEREMAIL = "TravelerEmail";
		public const string GETCONFIRMATIONBYDETAILSNEW_SPECIALINST = "specialinst";
		public const string GETCONFIRMATIONBYDETAILSNEW_RESTYPE1 = "Restype1";
		public const string GETCONFIRMATIONBYDETAILSNEW_RESTYPEDETAIL = "Restypedetail";
		public const string GETCONFIRMATIONBYDETAILSNEW_BILLTO = "BillTo";
		public const string GETCONFIRMATIONBYDETAILSNEW_REQARVTIME = "ReqArvTime";
		public const string GETCONFIRMATIONBYDETAILSNEW_ISSENDEMAIL = "IsSendEmail";
		public const string GETCONFIRMATIONBYDETAILSNEW_MAILTO = "MailTo";
		public const string GETCONFIRMATIONBYDETAILSNEW_CHARTERDEP = "CharterDep";
		public const string GETCONFIRMATIONBYDETAILSNEW_CHARTERARV = "CharterArv";
		public const string GETCONFIRMATIONBYDETAILSNEW_CLIENTID = "ClientId";
		public const string GETCONFIRMATIONBYDETAILSNEW_SPECIALSERVICEIDDEP = "SpecialServiceIDDep";
		public const string GETCONFIRMATIONBYDETAILSNEW_DEPSERVICEIDNEW = "DepServiceidNew";
		public const string GETCONFIRMATIONBYDETAILSNEW_ARVSERVICEIDNEW = "ArvServiceidNew";
		public const string GETCONFIRMATIONBYDETAILSNEW_ARVCANCEL = "ArvCancel";
		public const string GETCONFIRMATIONBYDETAILSNEW_DEPCANCEL = "DepCancel";
		public const string GETCONFIRMATIONBYDETAILSNEW_DISUSERCODE = "DisUserCode";
		public const string GETCONFIRMATIONBYDETAILSNEW_COUPONEMAIL = "CouponEmail";
		public const string GETCONFIRMATIONBYDETAILSNEW_PICKUPLAT = "PickupLat";
		public const string GETCONFIRMATIONBYDETAILSNEW_PICKUPLNG = "PickupLng";

		public const string GETSPECIALSERVICES = "GetSpecialServices?";
		public const string GETSPECIALSERVICES_SERVICEID = "Serviceid";
		public const string GETSPECIALSERVICES_TRAVELDATE = "TravelDate";
		public const string GETSPECIALSERVICES_ARVDEP = "ArvDep";

		public const string SENDEMAILCONFIRMATIONFORANDROID = "SendEmailConfirmation?";
		public const string SENDEMAILCONFIRMATIONFORANDROID_RES = "res";
		public const string SENDEMAILCONFIRMATIONFORANDROID_ALTEMAIL = "AltEmail";

		public const string CHECKLOGINFORANDROID = "CheckLoginForAndroid?";
		public const string CHECKLOGINFORANDROID_USERNAME = "USERNAME";
		public const string CHECKLOGINFORANDROID_PASSWORD = "PASSWORD";
		public const string CHECKLOGINFORANDROID_TYPE = "TYPE";
		public const string CHECKLOGINFORANDROID_TOKEN = "TOKEN";

		public const string GETGASSURCHARGE = "getGasSurcharge?";
		public const string GETGASSURCHARGE_SERVICEID = "serviceid";
		public const string GETGASSURCHARGE_TRAVELDATE = "schDate";
		public const string GETGASSURCHARGE_ARVDEP = "ArvDep";

		public const string GETPICKUPFORRESERVATIONFORANDROID = "GetPickupForReservationForAndroid?";
		public const string GETPICKUPFORRESERVATIONFORANDROID_PHONE = "phone";
		public const string GETPICKUPFORRESERVATIONFORANDROID_RES = "res";
		public const string GETPICKUPFORRESERVATIONFORANDROID_LNAME = "lname";
		public const string GETPICKUPFORRESERVATIONFORANDROID_ARVDEP = "arvdep";

		public const string GETFAREFORRESERVATIONCHARTER = "GetFareForReservationCharter?";
		public const string GETFAREFORRESERVATIONCHARTER_CHARTERHOURS = "CharterHours";
		public const string GETFAREFORRESERVATIONCHARTER_CHARTERARVCITY = "CharterArvCity";
		public const string GETFAREFORRESERVATIONCHARTER_CHARTERDEPCITY = "CharterDepCity";
		public const string GETFAREFORRESERVATIONCHARTER_CHARTERPASSENGER = "CharterPassenger";
		public const string GETFAREFORRESERVATIONCHARTER_CHARTERTODATE = "CharterToDate";
		public const string GETFAREFORRESERVATIONCHARTER_CHARTERFROMDATE = "CharterFromDate";
		public const string GETFAREFORRESERVATIONCHARTER_SERVICEID = "Serviceid";
		public const string GETFAREFORRESERVATIONCHARTER_CLIENTID = "ClientId";
		public const string GETFAREFORRESERVATIONCHARTER_LOG = "Log";
		public const string GETFAREFORRESERVATIONCHARTER_ASSIGNID = "AssignId";
		public const string GETFAREFORRESERVATIONCHARTER_DISCPROMOCODE = "DiscPromoCode";

		public const string CATEGORY_LOGIN = "Login";
		public const string CATEGORY_ADD_CREDIT_CARD = "Add Credit Card";
		public const string CATEGORY_CONFIRM_RIDE = "Confirm Ride";
		public const string CATEGORY_DISCLAIMER = "Disclaimer";
		public const string CATEGORY_FORGOT_PASSWORD = "Forgot Password";
		public const string CATEGORY_HOME = "Home";
		public const string CATEGORY_PASSWORD_UPDATE = "Password Update";
		public const string CATEGORY_PAYMENT = "Payment";
		public const string CATEGORY_REGISTRATION = "Registration";
		public const string CATEGORY_RESET_EMAIL = "Reset Email";
		public const string CATEGORY_RESET_PASSWORD = "Reset Password";
		public const string CATEGORY_RIDE_DETAIL = "Ride Detail";
		public const string CATEGORY_SCHEDULE_RIDE = "Schedule Ride";
		public const string CATEGORY_SUPPORT = "Support";
		public const string CATEGORY_THANK_YOU = "Thank You";
		public const string CATEGORY_PREVIOUS_LOCATIONS = "Previous Locations";
		public const string CATEGORY_RESERVATION = "Reservation";
		public const string CATEGORY_MENU = "Menu";
		public const string CATEGORY_PROMO_CODE = "Promo Code";
		public const string CATEGORY_UPDATE_PROFILE = "Update Profile";


		public const string SETARRIVALCALLEDBYCLIENT = "SetArrivalCalledByClient?";
		public const string SETARRIVALCALLEDBYCLIENT_SCHDATE = "schDate";
		public const string SETARRIVALCALLEDBYCLIENT_RESID = "resID";
		public const string SETARRIVALCALLEDBYCLIENT_ARVDEP = "ArvDep";
		public const string SETARRIVALCALLEDBYCLIENT_CUSTID = "CustID";
		public const string SETARRIVALCALLEDBYCLIENT_REMARK = "Remark";
		public const string SETARRIVALCALLEDBYCLIENT_LATITUDE = "latitude";
		public const string SETARRIVALCALLEDBYCLIENT_LONGITUDE = "longitude";


		public const string UPDATECLIENTGPS = "UpdateClientGPS?";
		public const string UPDATECLIENTGPS_SCHDATE = "schDate";
		public const string UPDATECLIENTGPS_RESID = "resID";
		public const string UPDATECLIENTGPS_ARVDEP = "ArvDep";
		public const string UPDATECLIENTGPS_CUSTID = "CustID";
		public const string UPDATECLIENTGPS_REMARK = "Remark";
		public const string UPDATECLIENTGPS_LATITUDE = "latitude";
		public const string UPDATECLIENTGPS_LONGITUDE = "longitude";
		public const string UPDATECLIENTGPS_GPSTIME = "gpsTime";


        public const string GETDISCLAIMER = "GetDisclaimerNew";

		public const string GETREADYFORPICKUPLIST = "GetReadyForPickupList?";
		public const string GETREADYFORPICKUPLIST_CUSTOMERID = "Customerid";
		public const string GETREADYFORPICKUPLIST_CURRENTDATE = "CurrentDate";

        public const string GETCONFIRMATIONTEXT = "GetConfirmationText?";
        public const string GETCONFIRMATIONTEXT_RESID = "ResID";
        
    }
}