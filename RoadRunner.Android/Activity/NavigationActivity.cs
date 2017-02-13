
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

namespace RoadRunner.Android
{
	[Activity (Label = "NavigationActivity")]			
	public class NavigationActivity : BaseActivity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
		}

		protected void OnBack()
		{
			base.OnBackPressed ();
			OverridePendingTransition (Resource.Animation.fromRight, Resource.Animation.toLeft);
		}
	}
}

