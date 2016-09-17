using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Text;

using System.Text;

using Android.App;

using Android.Runtime;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Java.Interop;

using Android.Locations;
using Android.Util;

using RoadRunner.Shared;
using RoadRunner.Shared.Classes;

namespace RoadRunner.Android
{
	public class CustomMarkerPopupAdapter : Java.Lang.Object, GoogleMap.IInfoWindowAdapter
	{
		private LayoutInflater _layoutInflater = null;

		public CustomMarkerPopupAdapter(LayoutInflater inflater)
		{
			_layoutInflater = inflater;
		}

		public View GetInfoWindow(Marker marker)
		{
			if (marker.Title == null && marker.Snippet == null)
				return null;
			
			ContextThemeWrapper cw = new ContextThemeWrapper(Application.Context, global::Android.Resource.Color.Transparent);
			LayoutInflater inflater = (LayoutInflater)cw.GetSystemService(Context.LayoutInflaterService);
			View layout = inflater.Inflate(Resource.Layout.item_mapMarker, null);

			var txtDistance = layout.FindViewById<TextView>(Resource.Id.txtDistance);
			txtDistance.Text = marker.Title;

			var txtTitle = layout.FindViewById<TextView>(Resource.Id.txtTitle);
			txtTitle.Text = marker.Snippet;
			return layout;
		}

		public View GetInfoContents(Marker marker)
		{
			//var customPopup = _layoutInflater.Inflate(Resource.Layout.item_mapMarker, null);

			//var txtDistance = customPopup.FindViewById<TextView>(Resource.Id.txtDistance);
			//txtDistance.Text = "distance between two point";

			//var txtTitle = customPopup.FindViewById<TextView>(Resource.Id.txtTitle);
			//txtTitle.Text = "ride type";

			//return customPopup;
			return null;
		}
	}
}