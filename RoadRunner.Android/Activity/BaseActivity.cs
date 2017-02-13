
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

using AndroidHUD;

namespace RoadRunner.Android
{
	[Activity (Label = "BaseActivity")]			
	public class BaseActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			Window.RequestFeature ( WindowFeatures.NoTitle );
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
			AlertDialog.Builder alert = new AlertDialog.Builder (this);

			alert.SetTitle (title);
			alert.SetMessage (message);
			alert.SetPositiveButton ("OK", (senderAlert, args) => {

			} );

			RunOnUiThread (() => {
				alert.Show();
			} );
		}
		public void ShowMessageBox(string title, string message, Action callback)
		{
			AlertDialog.Builder alert = new AlertDialog.Builder (this);

			alert.SetTitle (title);
			alert.SetMessage (message);
			alert.SetPositiveButton ("OK", (senderAlert, args) => {
				callback();
			} );

			RunOnUiThread (() => {
				alert.Show();
			} );
		}
	}
}

