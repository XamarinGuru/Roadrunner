using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Net;
using Android.Graphics;
using Java.IO;
using Android.Graphics.Drawables;
using Android.Util;
using System.Net;
using System.IO;

using Auth0.SDK;

using RoadRunner.Shared;
using System.Globalization;

using Android.Views.Animations;
using Android.Support.V4.App;
using Android.Support.V4.Widget;
//using Android.Support.V7.App;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;
namespace RoadRunner.Android
{
	[Activity(Label = "MainActivity")]
	public class MainActivity : BaseActivity
	{ 
		protected Auth0.SDK.Auth0Client client = new Auth0.SDK.Auth0Client(
			"erlend.eu.auth0.com",
			"LnLnPaFL5cKqKJHEJaQeMwA7ouSsC52t");
		
		ListView mDrawerList;
		RelativeLayout mDrawerPane;
		private DrawerLayout mDrawerLayout;
				List<Menu_Item> mNavItems = new List<Menu_Item>();

		protected override void OnCreate(Bundle savedInstanceState) 
		{
			base.OnCreate(savedInstanceState);
			Window.RequestFeature ( WindowFeatures.NoTitle );
			SetContentView(Resource.Layout.Main);

			initialSettings ();
		}

		private void initialSettings()
		{
			//Load Home fragment
			Fragment fragment = new HomeFragement();
			FragmentTransaction fragmentTx = SupportFragmentManager.BeginTransaction();
			fragmentTx.Replace(Resource.Id.mainFragmentContent, fragment).Commit();

			//Load Slide menu
			var avatar = (ImageView)FindViewById(Resource.Id.avatar);
			if (AppSettings.UserPhoto != null && AppSettings.UserPhoto != "")
			{
				var imageBitmap = GetImageBitmapFromUrl(AppSettings.UserPhoto);
				avatar.SetImageBitmap(imageBitmap);
			}


			TextView username = (TextView)FindViewById (Resource.Id.menu_userName);
			username.Text = String.Format("{0} {1}", AppSettings.UserFirstName, AppSettings.UserLastName);

			mDrawerLayout = (DrawerLayout) FindViewById(Resource.Id.drawerLayout);
			mDrawerPane = (RelativeLayout) FindViewById(Resource.Id.drawerPane);
			mDrawerList = (ListView) FindViewById(Resource.Id.menuList);

			mNavItems.Add(new Menu_Item("Home", Resource.Drawable.micon_clock));
			mNavItems.Add(new Menu_Item("Book a Ride", Resource.Drawable.micon_user));
			mNavItems.Add(new Menu_Item("Payment", Resource.Drawable.micon_terms));
			mNavItems.Add(new Menu_Item("My Trips", Resource.Drawable.micon_clock));
			mNavItems.Add(new Menu_Item("Support", Resource.Drawable.micon_user));
			mNavItems.Add(new Menu_Item("Terms", Resource.Drawable.micon_terms));
			mNavItems.Add(new Menu_Item("Change Login", Resource.Drawable.micon_terms));

			MenuListAdapter adapter = new MenuListAdapter(this, mNavItems);
			mDrawerList.Adapter = adapter;

			// Drawer Item click listeners
			mDrawerList.ItemClick +=(sender, args) => ListItemClicked(args.Position);

			ImageButton menuIconImageView = (ImageButton)FindViewById (Resource.Id.menuIconImgView);
			menuIconImageView.Click += delegate(object sender , EventArgs e )
			{
				mDrawerLayout.OpenDrawer(mDrawerPane);
			};
		}
		private Bitmap GetImageBitmapFromUrl(string url)
		{
			Bitmap imageBitmap = null;

			using (var webClient = new WebClient())
			{
				var imageBytes = webClient.DownloadData(url);
				if (imageBytes != null && imageBytes.Length > 0)
				{
					imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
				}
			}

			return imageBitmap;
		}

		private void ListItemClicked(int position) {
			Fragment fragment = null;

			TextView actionBarTitle = (TextView)FindViewById (Resource.Id.txtActionBarText);
			switch (position)
			{
				case 0:
					actionBarTitle.Text = "ROADRUNNER SHUTTLE";
					fragment = new HomeFragement();
					break;
				case 1:
					actionBarTitle.Text = "RIDE INFORMATION";
					fragment = new RideInformationFragment();
					break;
				case 2:
					actionBarTitle.Text = "PAYMENT INFO";
					fragment = new PaymentInfoFragment();
					break;
				case 3:
					actionBarTitle.Text = "MY TRIPS";
					fragment = new MyTripsFragment();
					break;
				case 4:
					actionBarTitle.Text = "SUPPORT";
					fragment = new SupportFragment();
					break;
				case 5:
					actionBarTitle.Text = "TERMS OF SERVICE";
					fragment = new TermsFragment();
					break;
				case 6:
					LogoutProcess();
					StartActivity(new Intent(this,typeof(LoginActivity)));
					return;
			}

			FragmentTransaction fragmentTx = SupportFragmentManager.BeginTransaction();
			fragmentTx.Replace(Resource.Id.mainFragmentContent, fragment).Commit();

			mDrawerList.SetItemChecked(position, true);
			mDrawerLayout.CloseDrawer(mDrawerPane);
		}

		private async void LogoutProcess()
		{
			UserTrackingReporter.TrackUser(Constant.LOGOUT, "Logout process started");

			LoginType currentLt;
			if (Enum.TryParse(AppSettings.LoginType.ToString(CultureInfo.InvariantCulture), out currentLt))
			{
				switch (currentLt)
				{
					case LoginType.Email:
						AppSettings.UserLogin = "";
						AppSettings.UserPassword = "";
						UserTrackingReporter.TrackUser(Constant.LOGOUT, "Email logout");
						break;
					case LoginType.Facebook:
						AppSettings.UserLogin = "";
						AppSettings.UserPassword = "";
						await this.client.LoginAsync(this, currentLt.ToString());
						UserTrackingReporter.TrackUser(Constant.LOGOUT, "Facebook logout");
						break;
					case LoginType.Google:
						AppSettings.UserLogin = "";
						AppSettings.UserPassword = "";
						await this.client.LoginAsync(this, currentLt.ToString());
						UserTrackingReporter.TrackUser(Constant.LOGOUT, "Google+ logout");
						break;
					case LoginType.LinkedIn:
						AppSettings.UserLogin = "";
						AppSettings.UserPassword = "";
						await this.client.LoginAsync(this, currentLt.ToString());
						UserTrackingReporter.TrackUser(Constant.LOGOUT, "Linkedin logout");
						break;
				}
			}

			UserTrackingReporter.TrackUser(Constant.LOGOUT, "Logout completed, navigating to the login screen");
		}
	}
}
