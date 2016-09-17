
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Android.Support.V4.App;
using Android.App;
using Android.Graphics;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using AndroidHUD;

using FragmentActivity = Android.Support.V4.App.FragmentActivity;

namespace RoadRunner.Android
{
	public class BaseActivity : FragmentActivity
	{
		AlertDialog.Builder alert;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			Window.RequestFeature ( WindowFeatures.NoTitle );
		}

		protected override void OnResume()
		{
			base.OnResume();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
		}

		public void ShowLoadingView(string title)
		{
			AndHUD.Shared.Show(this, title, -1, MaskType.Black);
		}

		public void HideLoadingView()
		{
			AndHUD.Shared.Dismiss (this);
		}

		public void ShowMessageBox(string title, string message)
		{
			alert = new AlertDialog.Builder(this);
			alert.SetTitle (title);
			alert.SetMessage (message);
			alert.SetPositiveButton ("OK", (senderAlert, args) => {
			} );
			RunOnUiThread (() => {
				alert.Show();
			} );
		}

		protected void OnBack()
		{
			base.OnBackPressed();
			OverridePendingTransition(Resource.Animation.fromRight, Resource.Animation.toLeft);
		}

		public Double DistanceInMetres(double lat1, double lon1, double lat2, double lon2)
		{

			if (lat1 == lat2 && lon1 == lon2)
				return 0.0;

			var theta = lon1 - lon2;

			var distance = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) +
						   Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) *
						   Math.Cos(deg2rad(theta));

			distance = Math.Acos(distance);
			if (double.IsNaN(distance))
				return 0.0;

			distance = rad2deg(distance);
			distance = distance * 60.0 * 1.1515 * 1609.344;

			return (distance);
		}
		private static double deg2rad(double deg)
		{
			return (deg * Math.PI / 180.0);
		}

		private static double rad2deg(double rad)
		{
			return (rad / Math.PI * 180.0);
		}

		public int getMapVehicleIconByServiceID(string serviceID)
		{
			int returnName = Resource.Drawable.icon_map_suv;
			switch (serviceID)
			{
				case "0":
					//returnName = 0;
					break;
				case "1":
				case "11":
				case "12":
					//returnName = "Shuttle-Icon.png";
					returnName = Resource.Drawable.icon_map_shuttle;
					break;
				case "2":
					//returnName = "Towncar-Icon.png";
					returnName = Resource.Drawable.icon_map_towncar;
					break;
				case "4":
					//returnName = "SUV-Icon.png";
					returnName = Resource.Drawable.icon_map_suv;
					break;
				case "3":
				case "6":
					//returnName = "Limo-Icon.png";
					returnName = Resource.Drawable.icon_map_limo;
					break;
				case "9":
				case "13":
					//returnName = "Bus-Icon.png";
					returnName = Resource.Drawable.icon_map_bus;
					break;
				case "5":
					//serviceID = 5;
					break;
				default:
					break;
			}

			return returnName;
		}

		public Bitmap rotateImage(int sourceImage, float rotate)
		{
			float rads = (float)Math.PI * rotate / 180;
			//Bitmap returnImage = null;

			Bitmap bitmap = BitmapFactory.DecodeResource(Resources, sourceImage);

			Matrix matrix = new Matrix();
			matrix.PostRotate(rotate);

			Bitmap rotatedBitmap = Bitmap.CreateBitmap(bitmap, 0, 0, bitmap.Width, bitmap.Height, matrix, false); 

			return rotatedBitmap;
		}
	}
}

