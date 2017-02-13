using System;
using Android.App;
using Android.Content;
using Android.Preferences;

namespace RoadRunner.Android
{
	public static class AppSettings
	{
		private static ISharedPreferences _appSettings = Application.Context.GetSharedPreferences ("App_settings", FileCreationMode.Private);

		private const string userTokenKey = "UserToken";
		public static string UserToken
		{
			get 
			{ 
				return _appSettings.GetString (userTokenKey, null);
			}
			set 
			{
				ISharedPreferencesEditor editor = _appSettings.Edit();
				editor.PutString(userTokenKey, value);
				editor.Apply();
			}
		}

		private const string userTypeKey = "UserType";
		public static string UserType
		{
			get 
			{ 
				return _appSettings.GetString (userTypeKey, null);
			}
			set 
			{
				ISharedPreferencesEditor editor = _appSettings.Edit();
				editor.PutString(userTypeKey, value);
				editor.Apply();
			}
		}
		private const string loginTypeKey = "LoginType";
		public static int LoginType 
		{
			get 
			{ 
				return _appSettings.GetInt (loginTypeKey, 0);
			}
			set 
			{
				ISharedPreferencesEditor editor = _appSettings.Edit();
				editor.PutInt(loginTypeKey, value);
				editor.Apply();
			}
		}
		private const string userIDKey = "UserID";
		public static string UserID 
		{
			get 
			{ 
				return _appSettings.GetString (userIDKey, null);
			}
			set 
			{
				ISharedPreferencesEditor editor = _appSettings.Edit();
				editor.PutString(userIDKey, value);
				editor.Apply();
			}
		}

		private const string selectedCard = "SelectedCard";
		public static string SelectedCard 
		{
			get 
			{ 
				return _appSettings.GetString (selectedCard, null);
			}
			set 
			{
				ISharedPreferencesEditor editor = _appSettings.Edit();
				editor.PutString(selectedCard, value);
				editor.Apply();
			}
		}

		private const string userEmailKey = "UserEmail";
		public static string UserEmail 
		{
			get 
			{ 
				return _appSettings.GetString (userEmailKey, null);
			}
			set 
			{
				ISharedPreferencesEditor editor = _appSettings.Edit();
				editor.PutString(value, userEmailKey);
				editor.Apply();
			}
		}

		private const string userFirstNameKey = "UserFirstName";
		public static string UserFirstName 
		{
			get 
			{ 
				return _appSettings.GetString (userFirstNameKey, null);
			}
			set 
			{
				ISharedPreferencesEditor editor = _appSettings.Edit();
				editor.PutString(userFirstNameKey, value);
				editor.Apply();
			}
		}

		private const string userlastNameKey = "UserLastName";
		public static string UserLastName 
		{
			get 
			{ 
				return _appSettings.GetString (userlastNameKey, null);
			}
			set 
			{
				ISharedPreferencesEditor editor = _appSettings.Edit();
				editor.PutString(userlastNameKey, value);
				editor.Apply();
			}
		}

		private const string userPhotoKey = "UserPhoto";
		public static string UserPhoto 
		{
			get 
			{ 
				return _appSettings.GetString (userPhotoKey, null);
			}
			set 
			{
				ISharedPreferencesEditor editor = _appSettings.Edit();
				editor.PutString(userPhotoKey, value);
				editor.Apply();
			}
		}

		private const string userPhoneKey = "UserPhone";
		public static string UserPhone 
		{
			get 
			{ 
				return _appSettings.GetString (userPhoneKey, null);
			}
			set 
			{
				ISharedPreferencesEditor editor = _appSettings.Edit();
				editor.PutString(userPhoneKey, value);
				editor.Apply();
			}
		}

		private const string userLoginKey = "UserLogin";
		public static string UserLogin 
		{
			get 
			{ 
				return _appSettings.GetString (userLoginKey, null);
			}
			set 
			{
				ISharedPreferencesEditor editor = _appSettings.Edit();
				editor.PutString(userLoginKey, value);
				editor.Apply();
			}
		}

		private const string userPasswordKey = "UserPassword";
		public static string UserPassword 
		{
			get 
			{ 
				return _appSettings.GetString (userPasswordKey, null);
			}
			set 
			{
				ISharedPreferencesEditor editor = _appSettings.Edit();
				editor.PutString(userPasswordKey, value);
				editor.Apply();
			}
		}

		private const string isSMSKey = "IsSMS";
		public static bool IsSMS 
		{
			get 
			{ 
				return _appSettings.GetBoolean (isSMSKey, false);
			}
			set 
			{
				ISharedPreferencesEditor editor = _appSettings.Edit();
				editor.PutBoolean(isSMSKey, value);
				editor.Apply();
			}
		}
	}
}

