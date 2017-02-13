using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Android.Graphics;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
//using Android.Support.V7.App;

namespace RoadRunner.Android
{
	[Activity (Label = "MainActivity")]			
	public class MainActivity : NavigationActivity
	{ 
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
			FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
			fragmentTx.Replace(Resource.Id.mainFragmentContent, fragment).Commit();

			//Load Slide menu
			TextView username = (TextView)FindViewById (Resource.Id.menu_userName);
			username.Text = AppSettings.UserLogin;

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
					StartActivity(new Intent(this,typeof(LoginActivity)));
					return;
			}

			FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
			fragmentTx.Replace(Resource.Id.mainFragmentContent, fragment).Commit();

			mDrawerList.SetItemChecked(position, true);
			mDrawerLayout.CloseDrawer(mDrawerPane);
		}
	}
}
