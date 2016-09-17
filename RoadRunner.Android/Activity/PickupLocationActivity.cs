
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Locations;

using RoadRunner.Shared;
using RoadRunner.Shared.Classes;

namespace RoadRunner.Android
{
	[Activity (Label = "PickupLocationActivity")]			
	public class PickupLocationActivity : BaseActivity, ILocationListener
	{

		//ArrayList<String> listItems=new ArrayList<String>();

		ListView listView;
		ArrayAdapter<String> adapter;
		List<String> listContents = new List<String>();
		List<PlaceAutocompleteAPI_Prediction> listData;


		private double _longitude;
		private double _latitude;

		LocationManager locMgr;
		string _locationProvider = LocationManager.GpsProvider;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView(Resource.Layout.PickupLocation);

			InitializeLocationManager ();

			var searchText = (SearchView) FindViewById (Resource.Id.searchText);
			searchText.QueryTextChange += UpdateSearchResult;

			adapter = new ArrayAdapter<String> (this, Android.Resource.Layout.item_location, listContents);
			listView = (ListView)FindViewById (Resource.Id.location_list_view);
			listView.Adapter = adapter;

			listView.ItemClick += SelectedLocation;

			var btnBack = (ImageButton)FindViewById (Resource.Id.btn_back);
			btnBack.Click += delegate(object sender , EventArgs e )
			{
				OnBack();
			};
		}

		private void UpdateSearchResult(object sender, SearchView.QueryTextChangeEventArgs e)
		{
			
			//ShowLoadingView ("Searching...");
			var searchTerm = e.NewText.Trim ();

			Task runSync = Task.Factory.StartNew (async (object inputObj) => {
				var sTerm = inputObj != null ? inputObj.ToString () : "";
				if (sTerm.Length > 2) {
					listData = await PerformSearch (sTerm);

					listContents = new List<String>();
					for (var i = 0; i < listData.Count; i ++)
					{
						listContents.Add(listData[i].description);
					}
					RunOnUiThread(() =>
					{
						adapter = new ArrayAdapter<String> (this, Android.Resource.Layout.item_location, listContents);
						listView.Adapter = adapter;
						adapter.NotifyDataSetChanged();
					});
				}
				//HideLoadingView ();
			}, searchTerm).Unwrap (
			);
		}

		private void SelectedLocation (object sender, AdapterView.ItemClickEventArgs e) 
		{
			PlaceAutocompleteAPI_Prediction selectedProduct = listData[e.Position];
			ShowLoadingView ("Getting location details ...");

			Task runSync = Task.Factory.StartNew (async (object inputObj) => {
				var placeId = inputObj != null ? inputObj.ToString () : "";

				if (!String.IsNullOrEmpty (placeId)) {
					var data = await AppData.GetPlaceDetails (placeId);

					string IsPickUpLocation = Intent.GetStringExtra ("IsPickupLocation");

					RunOnUiThread(() =>
					{
						if (IsPickUpLocation == "true") {
							Facade.Instance.CurrentRide.PickUpData = data;
						} else {
							Facade.Instance.CurrentRide.DropOffData = data;
						}
						OnBack();
					});
				}
				HideLoadingView ();
			}, selectedProduct.place_id).Unwrap ();
		}

		private async Task<List<PlaceAutocompleteAPI_Prediction>> PerformSearch(string searchString)
		{
			searchString = searchString.Trim ();

			var data = await AppData.GetPlaceAutocomplete (searchString, _latitude, _longitude);
			var currentPredictions = data.predictions;

			return currentPredictions;
		}

		#region LocationManager

		private void InitializeLocationManager()
		{
			locMgr = (LocationManager) GetSystemService(LocationService);

			Criteria criteriaForLocationService = new Criteria { Accuracy = Accuracy.Fine };
			IList<string> acceptableLocationProviders = locMgr.GetProviders(criteriaForLocationService, true);

			if (acceptableLocationProviders.Any())
			{
				_locationProvider = acceptableLocationProviders.First();
			}
			else
			{
				_locationProvider = string.Empty;
			}
		}

		protected override void OnPause()
		{
			base.OnPause();
			locMgr.RemoveUpdates(this);
		}

		protected override void OnResume ()
		{
			base.OnResume ();
			locMgr.RequestLocationUpdates(_locationProvider, 2000, 0, this);
		}
		public void OnLocationChanged (Location location)
		{
			_longitude = location.Longitude;
			_latitude = location.Latitude;
		}
		public void OnProviderEnabled (string provider) { }
		public void OnProviderDisabled (string provider) { }
		public void OnStatusChanged (string provider, Availability status, Bundle extras) { }

		#endregion
	}
}

