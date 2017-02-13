
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace RoadRunner.Android
{
	public class RideInformationFragment : Fragment
	{
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.fragment_rideInformation, container, false);

			Button btnBookARide = (Button)view.FindViewById (Resource.Id.btn_GoToLocation);
			btnBookARide.Click += delegate(object sender , EventArgs e )
			{
				//StartActivity(new Intent(this.Activity,typeof(LocationActivity)));
				//this.Activity.OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
			};

			return view;
		}
	}
}

