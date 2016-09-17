using Foundation;
using Facebook.CoreKit;
using Facebook.LoginKit;

using Foundation;
using UIKit;
namespace RoadRunnerNew.iOS
{
	public static class AppSettings
	{
		public static UINavigationController CurrentNavigation;

		private const string userTokenKey = "UserToken";
		public static string UserToken
		{
			get { return NSUserDefaults.StandardUserDefaults.StringForKey (userTokenKey); }
			set 
			{
				NSUserDefaults.StandardUserDefaults.SetString (value, userTokenKey);
			}
		}

		private const string userTypeKey = "UserType";
		public static string UserType
		{
			get { return NSUserDefaults.StandardUserDefaults.StringForKey (userTypeKey); }
			set 
			{ 
				NSUserDefaults.StandardUserDefaults.SetString (value, userTypeKey);
			}
		}
		private const string loginTypeKey = "LoginType";
		public static int LoginType 
		{
			get { return (int)NSUserDefaults.StandardUserDefaults.IntForKey (loginTypeKey); }
			set
			{
				NSUserDefaults.StandardUserDefaults.SetInt (value, loginTypeKey);
			}
		}
		private const string userIDKey = "UserID";
		public static string UserID 
		{
			get { return NSUserDefaults.StandardUserDefaults.StringForKey (userIDKey); }
			set
			{
				NSUserDefaults.StandardUserDefaults.SetString (value, userIDKey);
			}
		}

		private const string selectedCard = "SelectedCard";
		public static string SelectedCard 
		{
			get { return NSUserDefaults.StandardUserDefaults.StringForKey (selectedCard); }
			set
			{
				NSUserDefaults.StandardUserDefaults.SetString (value, selectedCard);
			}
		}

		private const string userEmailKey = "UserEmail";
		public static string UserEmail 
		{
			get { return NSUserDefaults.StandardUserDefaults.StringForKey (userEmailKey); }
			set { NSUserDefaults.StandardUserDefaults.SetString (value, userEmailKey); }
		}

		private const string userFirstNameKey = "UserFirstName";
		public static string UserFirstName 
		{
			get { return NSUserDefaults.StandardUserDefaults.StringForKey (userFirstNameKey); }
			set { NSUserDefaults.StandardUserDefaults.SetString (value, userFirstNameKey); }
		}

		private const string userlastNameKey = "UserLastName";
		public static string UserLastName 
		{
			get { return NSUserDefaults.StandardUserDefaults.StringForKey (userlastNameKey); }
			set { NSUserDefaults.StandardUserDefaults.SetString (value, userlastNameKey); }
		}

		private const string userPhotoKey = "UserPhoto";
		public static string UserPhoto 
		{
			get { return NSUserDefaults.StandardUserDefaults.StringForKey (userPhotoKey); }
			set { NSUserDefaults.StandardUserDefaults.SetString (value, userPhotoKey); }
		}

		private const string userPhoneKey = "UserPhone";
		public static string UserPhone 
		{
			get { return NSUserDefaults.StandardUserDefaults.StringForKey (userPhoneKey); }
			set { NSUserDefaults.StandardUserDefaults.SetString (value, userPhoneKey); }
		}

		private const string userLoginKey = "UserLogin";
		public static string UserLogin 
		{
			get { return NSUserDefaults.StandardUserDefaults.StringForKey (userLoginKey); }
			set { NSUserDefaults.StandardUserDefaults.SetString (value, userLoginKey); }
		}

		private const string userPasswordKey = "UserPassword";
		public static string UserPassword 
		{
			get { return NSUserDefaults.StandardUserDefaults.StringForKey (userPasswordKey); }
			set { NSUserDefaults.StandardUserDefaults.SetString (value, userPasswordKey); }
		}

		private const string isSMSKey = "IsSMS";
		public static bool IsSMS 
		{
			get { return NSUserDefaults.StandardUserDefaults.BoolForKey (isSMSKey); }
			set { NSUserDefaults.StandardUserDefaults.SetBool (value, isSMSKey); }
		}

		// private const string promoCodeKey = "PromoCode";
		// public static string PromoCode 
		// {
		// 	get { return NSUserDefaults.StandardUserDefaults.StringForKey (promoCodeKey); }
		// 	set
		// 	{
		// 		NSUserDefaults.StandardUserDefaults.SetString (value, promoCodeKey);
		// 	}
		// }
		// 		 
		// 
		// private const string travelDateKey = "TravelDate";
		// public static string TravelDate 
		// {
		// 	get { return NSUserDefaults.StandardUserDefaults.StringForKey (travelDateKey); }
		// 	set { NSUserDefaults.StandardUserDefaults.SetString (value, travelDateKey); }
		// }
		// 
		// private const string serviceIDKey = "ServiceID";
		// public static string ServiceID 
		// {
		// 	get { return NSUserDefaults.StandardUserDefaults.StringForKey (serviceIDKey); }
		// 	set { NSUserDefaults.StandardUserDefaults.SetString (value, serviceIDKey); }
		// }
		// 

		// 
		// private const string currentPickUpZipCodeKey = "CurrentPickUpZipCode";
		// public static string CurrentPickUpZipCode 
		// {
		// 	get { return NSUserDefaults.StandardUserDefaults.StringForKey (currentPickUpZipCodeKey); }
		// 	set { NSUserDefaults.StandardUserDefaults.SetString (value, currentPickUpZipCodeKey); }
		// }
		// 
		// public static bool HasPromoCode 
		// {
		// 	get { return !string.IsNullOrEmpty (PromoCode); }
		// }
		// 
		// public static void ResetRideSettings()
		// {
		// 	CurrentPickUpZipCode = string.Empty;
		// 	ServiceID = string.Empty;
		// 	TravelDate = string.Empty;
		// 	PromoCode = string.Empty;
		// }
	}
}

