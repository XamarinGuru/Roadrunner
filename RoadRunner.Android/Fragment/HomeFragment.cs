using Android;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using Android.Util;

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using Android.Text;
using Android.Graphics;

using Android.Gms.Maps;
using Android.Gms.Maps.Model;

using Android.Locations;

using System.Threading;

using RoadRunner.Shared;
using RoadRunner.Shared.Classes;
using RoadRunner.Shared.ViewModels;
using GalaSoft.MvvmLight.Helpers;

using Fragment = Android.Support.V4.App.Fragment;
using ActivityCompat = Android.Support.V4.App.ActivityCompat;

namespace RoadRunner.Android
{
	public class HomeFragement : Fragment, IOnMapReadyCallback, ILocationListener
	{
		MainActivity mActivity;

		private Timer timer;

		Location _currentLocation;
		LocationManager _locationManager;

		SupportMapFragment _mapFragment;
		GoogleMap _map = null;

		private List<Dictionary<String, String>> _pickups = null;
		private List<KeyValuePair<object, string>> _readyForPickups = null;

		Spinner spSelectPayment;
		Spinner spReadyForPickup;
		Button btnReadyPickup;
		EditText mPromoCode;

		List<KeyValuePair<object, string>> m_listCreditCards;

		private static bool IsFirstTime = true;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.fragment_home, container, false);

			AppSettings.currentActivity = this.Activity as MainActivity;
			mActivity = this.Activity as MainActivity;

			_locationManager = this.Activity.GetSystemService(Context.LocationService) as LocationManager;


			const string permission = Manifest.Permission.AccessFineLocation;
			if (ActivityCompat.CheckSelfPermission(this.Activity, permission) == (int)Permission.Granted)
			{
				Criteria crit = new Criteria();
				crit.Accuracy = Accuracy.Fine;
				var best = _locationManager.GetBestProvider(crit, true);
				_locationManager.RequestLocationUpdates(best, 0, 1, this);
			}else
			{
				string[] PermissionsLocation =
				{
					  Manifest.Permission.AccessFineLocation
				};
				//Explain to the user why we need to read the contacts
				ActivityCompat.RequestPermissions(this.Activity, PermissionsLocation, 0);
			}

			//Snackbar.Make(layout, "Location access is required to show coffee shops nearby.", Snackbar.LengthIndefinite)
			//.SetAction("OK", v => ActivityCompat.RequestPermissions(this, PermissionsLocation, RequestLocationId))
			//.Show();

			//Criteria crit = new Criteria();
			//crit.Accuracy = Accuracy.Fine;
			//var best = _locationManager.GetBestProvider(crit, true);
			//_locationManager.RequestLocationUpdates(best, 0, 1, this);

			var txtSearchLocation = (EditText)view.FindViewById (Resource.Id.txtSearchLocation);
			txtSearchLocation.Touch += PopupSearchLocation;
			txtSearchLocation.SetBinding (
				() => Facade.Instance.CurrentRide.PickUpLocation,
				() => txtSearchLocation.Text,
				BindingMode.TwoWay)
				.UpdateSourceTrigger ("PickUpLocationChanges");

			var btnAddPayment = (Button)view.FindViewById(Resource.Id.btnAddPayment);
			btnAddPayment.Click += delegate (object sender, EventArgs e)
			{
				StartActivity(new Intent(mActivity, typeof(AddPaymentActivity)));
				mActivity.OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
			};

			spSelectPayment = (Spinner)view.FindViewById(Resource.Id.spSelectPayment);

			spReadyForPickup = (Spinner)view.FindViewById(Resource.Id.spReadyForPickup);

			btnReadyPickup = (Button)view.FindViewById(Resource.Id.btnReadyPickup);
			btnReadyPickup.Click += delegate (object sender, EventArgs e)
			{
				spReadyForPickup.PerformClick();
			};

			mPromoCode = (EditText)view.FindViewById(Resource.Id.txtPromoCode);
			mPromoCode.Text = Facade.Instance.CurrentRide.PromoCode;
			mPromoCode.TextChanged += OnPromoCodeChanged;

			var btnVerifyPromo = (Button)view.FindViewById(Resource.Id.btnVerifyPromo);
			btnVerifyPromo.Click += delegate (object sender, EventArgs e)
			{
				VerifyPromoCode();
			};

			var btnBookARide = (Button)view.FindViewById(Resource.Id.btnBookARide);
			btnBookARide.SetCommand("Click", Facade.Instance.CurrentRide.GoToTheRideInformation);

			if (IsFirstTime)
			{
				IsFirstTime = false;
				SetBindingOnce();
			}

			_mapFragment = (SupportMapFragment)ChildFragmentManager.FindFragmentById(Resource.Id.map);
			_mapFragment.GetMapAsync(this);

			return view;
		}

		public override void OnResume()
		{
			base.OnResume();
			LoadCreditCards();
			mActivity.RunOnUiThread(async () => { await ResetMapView(); });
		}

		public override void OnPause()
		{
			base.OnPause();
			if (timer != null)
			{
				timer.Dispose();
				timer = null;
			}
		}

		public override void OnDestroy()
		{
			base.OnDestroy();
			try
			{
				if (_map != null)
				{
					_map.Dispose();
					_map = null;
				}
				if (timer != null)
				{
					timer.Dispose();
					timer = null;
				}

				GC.Collect();
			}
			catch
			{
			}
		}

		private void ttimerCallback(object state)
		{
			//ResetMapView();
			mActivity.RunOnUiThread(async  () => { await ResetMapView(); });
		}
		private async System.Threading.Tasks.Task ResetMapView()
		//private void ResetMapView()
		{
			try
			{
				UserTrackingReporter.TrackUser(Constant.CATEGORY_HOME, "Getting reservation vehicle locations");

				if (_map == null) return;

				GetMyValidBookedReservations();

			}
			catch (Exception exc)
			{
				CrashReporter.Report(exc);
			}
		}
		private async void GetMyValidBookedReservations()
		{
			//mActivity.ShowLoadingView("Loading data...");

			if (_map == null) return;

			_map.Clear();

			_pickups = new List<Dictionary<string, string>>();

			try
			{
				//DateTime dateFormat = new DateTime();
				//dateFormat.date = "MM/dd/yyyy";
				//NSDate now = NSDate.FromTimeIntervalSinceReferenceDate((DateTime.Now - (new DateTime(2001, 1, 1, 0, 0, 0))).TotalSeconds);
				//var strNow = dateFormat.ToString(now);

				var customerID = AppSettings.UserID;

				// please take a look it, Pavel
				var dic = new Dictionary<String, String>
				{
					{Constant.GETREADYFORPICKUPLIST_CUSTOMERID, "553890"},
					{Constant.GETREADYFORPICKUPLIST_CURRENTDATE, "2/13/2015"}
				};
				//var dic = new Dictionary<String, String>
				//{
				//	{Constant.GETREADYFORPICKUPLIST_CUSTOMERID, customerID},
				//	{Constant.GETREADYFORPICKUPLIST_CURRENTDATE, strNow}
				//};
				var result = await AppData.ApiCall(Constant.GETREADYFORPICKUPLIST, dic);
				var tt = (GetReadyForPickupListResponse)AppData.ParseResponse(Constant.GETREADYFORPICKUPLIST, result);

				var availableReservations = tt.PickupList;

				for (int i = 0; i < availableReservations.Count; i++)
				{
					var reservation = availableReservations[i];

					Task runSync = Task.Factory.StartNew(async () =>
					{
						var pickupData = await GetPickupDataForReservation(reservation);
						_pickups.Add(pickupData);
					}).Unwrap();
					runSync.Wait();
				}

				LatLngBounds.Builder bounds = new LatLngBounds.Builder();

				_readyForPickups = new List<KeyValuePair<object, string>>();
				_readyForPickups.Add(new KeyValuePair<object, string>("0", "Ready for all rides"));
				foreach (Dictionary<String, String> pickup in _pickups)
				{
					var pos1 = new Location("");
					pos1.Latitude = double.Parse(pickup["Latitude"]);
					pos1.Longitude = double.Parse(pickup["Longitude"]);

					var curPos = GetGPSLocation();
					var distanceInMeters = pos1.DistanceTo(curPos);

					var distance = string.Format("{0} km", ((Math.Round(distanceInMeters/100))/10).ToString());

					DateTime pDate = DateTime.Parse(pickup["Date"]);
					string[] arrDate = pDate.GetDateTimeFormats();
					string strType = pickup["ArvDep"] == "1" ? "Drop-Off" : "Pick-Up";
					string title = string.Format("{0} - {1} {2}", pickup["ResType"], arrDate[108], strType);

					var location = new LatLng(double.Parse(pickup["Latitude"]), double.Parse(pickup["Longitude"]));
					MarkerOptions markerOpt2 = new MarkerOptions();
					markerOpt2.SetPosition(location);
					markerOpt2.SetTitle(distance);
					markerOpt2.SetSnippet(title);

					var resourceID = mActivity.getMapVehicleIconByServiceID(pickup["ServiceID"]);
					var markerBitmap = mActivity.rotateImage(resourceID, float.Parse(pickup["Angle"]));
					markerOpt2.SetIcon(BitmapDescriptorFactory.FromBitmap(markerBitmap));
					_map.AddMarker(markerOpt2);

					_readyForPickups.Add(new KeyValuePair<object, string>(pickup["ResID"], pickup["DisplayTxt"]));//string.Format("Shared Van (7 Passengers) \n 3:45 Pick-Up to Los Angeles", pickup["ResID"], pickup["ResType"])));

					bounds.Include(location);
				}

				if (_pickups.Count > 0)
				{
					//btnReadyPickup.SetBackgroundColor(Color.Green);
					btnReadyPickup.SetBackgroundResource(Resource.Drawable.btnReadyForPickup);
					btnReadyPickup.Text = string.Format("READY FOR PICKUP ({0})", _pickups.Count.ToString());
					SetupReadyPickup(CallForReadyPickup);
				}
				else {
					//btnReadyPickup.SetBackgroundColor(Color.Gray);
					btnReadyPickup.SetBackgroundResource(Resource.Drawable.btnReadyForPickup_disable);
					btnReadyPickup.Text = "READY FOR PICKUP";
				}

				var curPos1 = GetGPSLocation();
				var currentLocation = new LatLng(curPos1.Latitude, curPos1.Longitude);
				MarkerOptions markerOpt = new MarkerOptions();
				markerOpt.SetPosition(currentLocation);
				markerOpt.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.icon_mylocation));
				_map.AddMarker(markerOpt);

				bounds.Include(currentLocation);
				CameraUpdate cu = CameraUpdateFactory.NewLatLngBounds(bounds.Build(), 100);

				//_map.MoveCamera(CameraUpdateFactory.NewLatLngZoom(currentLocation , 14f));
				_map.AnimateCamera(cu);
			}
			catch (Exception ex)
			{
				CrashReporter.Report(ex);
				//mActivity.HideLoadingView();
				return;
			}
			//mActivity.HideLoadingView();
		}

		protected void SetupReadyPickup(Action<string> callback)
		{
			mActivity.RunOnUiThread(() =>
			{
				var adapter = new ArrayAdapter(mActivity, Resource.Layout.item_spinner);
				spReadyForPickup.Adapter = adapter;
				foreach (var item in _readyForPickups)
				{
					adapter.Add(item.Value);
				}
				spReadyForPickup.ItemSelected -= new EventHandler<AdapterView.ItemSelectedEventArgs>(OnReadyPickupSelected);
				spReadyForPickup.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(OnReadyPickupSelected);
			});
		}

		// please take a look it, Pavel
		private async Task<Dictionary<string, string>> GetPickupDataForReservation(GetReadyForPickupListItem item)
		{
			try
			{
				var dic = new Dictionary<String, String>
				{
					{Constant.GETPICKUPFORRESERVATIONFORANDROID_PHONE, ""},
					{Constant.GETPICKUPFORRESERVATIONFORANDROID_RES, "1489787"},
					{Constant.GETPICKUPFORRESERVATIONFORANDROID_LNAME, "test"},
					{Constant.GETPICKUPFORRESERVATIONFORANDROID_ARVDEP, "0"},
				};
				//var dic = new Dictionary<String, String>
				//{
				//	{Constant.GETPICKUPFORRESERVATIONFORANDROID_PHONE, ""},
				//	{Constant.GETPICKUPFORRESERVATIONFORANDROID_RES, item.ResID},
				//	{Constant.GETPICKUPFORRESERVATIONFORANDROID_LNAME, AppSettings.UserLastName},
				//	{Constant.GETPICKUPFORRESERVATIONFORANDROID_ARVDEP, item.arvDep},
				//};
				var result = await AppData.ApiCall(Constant.GETPICKUPFORRESERVATIONFORANDROID, dic);
				var tt = (GetPickupForReservationForAndroidResponse)AppData.ParseResponse(Constant.GETPICKUPFORRESERVATIONFORANDROID, result);

				if (tt.Result.ToString() == "Failed") { return null; }

				return new Dictionary<string, string> {
					{ "ResID", item.ResID },
					{ "SchDate", item.TravelDate },
					{ "ServiceID", "4" },//item.Service
					{ "ResType", item.Service },
					{ "Date", item.TravelDate },
					{ "ArvDep", item.arvDep },
					{ "Latitude", tt.Lat },
					{ "Longitude", tt.Long },
					{ "Angle", tt.angle },
					{ "DisplayTxt", item.DisplayTxt }
				};
			}
			catch (Exception ex)
			{
				CrashReporter.Report(ex);
				//mActivity.HideLoadingView();
				return null;
			}
			//mActivity.HideLoadingView();
		}

		private void OnReadyPickupSelected(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Facade.Instance.CurrentRide.ReadyReservationId = _readyForPickups[e.Position].Key.ToString();
			CallForReadyPickup(Facade.Instance.CurrentRide.ReadyReservationId);
		}
		private async void CallForReadyPickup(string reservationId)
		{
			try
			{
				var id = reservationId;
				if (id == null || id == "0")
					return;

				for (int i = 0; i < _pickups.Count; i++)
				{
					var pickupData = _pickups[i];
					if (pickupData["ResID"] == id)
					{
						var dic = new Dictionary<String, String>
						{
							{Constant.UPDATECLIENTGPS_SCHDATE, pickupData["SchDate"]},
							{Constant.UPDATECLIENTGPS_RESID, pickupData ["ResID"]},
							{Constant.UPDATECLIENTGPS_ARVDEP, pickupData["ArvDep"]},
							{Constant.UPDATECLIENTGPS_CUSTID, AppSettings.UserID},
							{Constant.UPDATECLIENTGPS_REMARK, "werty"},
							{Constant.UPDATECLIENTGPS_LATITUDE, pickupData["Latitude"]},
							{Constant.UPDATECLIENTGPS_LONGITUDE, pickupData["Longitude"]},
						};

						mActivity.ShowLoadingView("Sending Request...");
						var result = await AppData.ApiCall(Constant.SETARRIVALCALLEDBYCLIENT, dic);
						var tt = (SetArrivalCalledByClientResponse)AppData.ParseResponse(Constant.SETARRIVALCALLEDBYCLIENT, result);

						var message = string.Format("{0}\n{1}\n{2}", tt.Wait, tt.APInst, tt.EndInst);
						mActivity.ShowMessageBox(tt.eventStatus, message);

						SetupReadyPickup(CallForReadyPickup);
					}
				}
			}
			catch (Exception ex)
			{
				CrashReporter.Report(ex);
				mActivity.HideLoadingView();
				return;
			}
			mActivity.HideLoadingView();
		}

		private void SetBindingOnce()
		{
			this.SetBinding(
				() => Facade.Instance.CurrentRide.CanGoToTheRideInformation)
				.UpdateSourceTrigger("CanGoToTheRideInformationChanges")
				.WhenSourceChanges(
					async () =>
					{
						if (Facade.Instance.CurrentRide.CanGoToTheRideInformation)
						{
							try
							{
						AppSettings.currentActivity.StartActivity(new Intent(AppSettings.currentActivity, typeof(RideInformationActivity)));
								AppSettings.currentActivity.OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
							}
							catch (Exception)
							{

							}
						}
						else {
							if (Facade.Instance.CurrentRide.ValidaionError != null && Facade.Instance.CurrentRide.ValidaionError.Count > 0)
							{
								string header = Facade.Instance.CurrentRide.ValidaionError.Count > 1
									? "Just a couple things left:"
									: "Please correct the last thing:";

								var delimeter = System.Environment.NewLine + System.Environment.NewLine;
								var message = String.Join(delimeter, Facade.Instance.CurrentRide.ValidaionError.Select(r => r.ErrorMessage));
								AppSettings.currentActivity.ShowMessageBox(header, message);
							}
						}
					});
		}

		private void LoadCreditCards()
		{
			//mActivity.ShowLoadingView("Loading data...");

			var dic = new Dictionary<String, String>
			{
				{Constant.GETCREDITCARDDETAILSNEWFORPHONE_CUSTOMERID, AppSettings.UserID},
				{Constant.GETCREDITCARDDETAILSNEWFORPHONE_LOGINTYPE, "-1"},
				{Constant.GETCREDITCARDDETAILSNEWFORPHONE_TOKENID, string.Empty } // do not use the actual token
			};

			String result = String.Empty;

			Task runSync = Task.Factory.StartNew(async () =>
			{
				result = await AppData.ApiCall(Constant.GETCREDITCARDDETAILSNEWFORPHONE, dic);

				var tt = (GetCreditCardDetailsNewForPhoneResponse)AppData.ParseResponse(Constant.GETCREDITCARDDETAILSNEWFORPHONE, result);
				m_listCreditCards = new List<KeyValuePair<object, string>>();
				var listCreditCardImages = new List<KeyValuePair<object, object>>();

				mActivity.RunOnUiThread(() =>
				{
					var adapter = new ArrayAdapter(mActivity, Resource.Layout.item_spinner);
					spSelectPayment.Adapter = adapter;
					foreach (var card in tt.CardList)
					{
						m_listCreditCards.Add(new KeyValuePair<object, string>(card.Id, card.CardNumber));
						adapter.Add(card.CardNumber);
					}
					spSelectPayment.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(OnPaymentChanged);
				});

				//mActivity.HideLoadingView();
			});
		}

		private void OnPaymentChanged(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Facade.Instance.CurrentRide.CreditCardId = m_listCreditCards[e.Position].Key.ToString();
			AppSettings.SelectedCard = m_listCreditCards[e.Position].Key.ToString();
		}

		private void PopupSearchLocation(object sender, View.TouchEventArgs e)
		{
			if (e.Event.Action == MotionEventActions.Down) {
				var nextActivity = new Intent (mActivity, typeof(PickupLocationActivity));
				nextActivity.PutExtra ("IsPickupLocation", "true");
				StartActivity(nextActivity);
				mActivity.OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
			}
		}

		private void OnPromoCodeChanged(object sender, TextChangedEventArgs e)
		{
			Facade.Instance.CurrentRide.PromoCode = e.Text.ToString();
		}

		private void VerifyPromoCode()
		{
			mActivity.ShowLoadingView("Veryfying promo code");
			var dic = new Dictionary<String, String> {
				{ Constant.VALIDATEDISCOUNTCOUPON_CODE, mPromoCode.Text },
				{ Constant.VALIDATEDISCOUNTCOUPON_CUSTOMERID, "-1" },
				{ Constant.VALIDATEDISCOUNTCOUPON_EMAIL, "" },
				{ Constant.VALIDATEDISCOUNTCOUPON_SERVICEID, "-1" },
				{ Constant.VALIDATEDISCOUNTCOUPON_TRAVELDATE, "" },
				{ Constant.VALIDATEDISCOUNTCOUPON_VALIDATIONTYPE, "1" }
			};

			string result;
			ValidateDiscountCouponResponse tt = null;
			try
			{
				UserTrackingReporter.TrackUser(Constant.CATEGORY_PROMO_CODE, "Validating promo code");

				Task runSync = Task.Factory.StartNew(async () =>
				{
					result = await AppData.ApiCall(Constant.VALIDATEDISCOUNTCOUPON, dic);
					tt = (ValidateDiscountCouponResponse)AppData.ParseResponse(Constant.VALIDATEDISCOUNTCOUPON, result);
				}).Unwrap();
				runSync.Wait();

			}
			catch (Exception ex)
			{
				CrashReporter.Report(ex);
				mActivity.HideLoadingView();
				mActivity.RunOnUiThread(() =>
				{
					mActivity.ShowMessageBox("Failed", "Invalid promo code entered.");
				});
			}
			if (tt == null || String.IsNullOrEmpty(tt.Result) || tt.Result.ToLower().Contains("failed"))
			{
				UserTrackingReporter.TrackUser(Constant.CATEGORY_PROMO_CODE, "Invalid promo code entered");
				mActivity.RunOnUiThread(() =>
				{
					mActivity.ShowMessageBox("Failed", "Invalid promo code entered.");
				});
			}
			else {
				mActivity.RunOnUiThread(() =>
				{
					mActivity.ShowMessageBox("Success", "Valid promo code entered.");
				});
			}
			mActivity.HideLoadingView();
		}

		#region google map
		public void OnMapReady(GoogleMap googleMap)
		{
			_map = googleMap;
			googleMap.SetInfoWindowAdapter(new CustomMarkerPopupAdapter(this.Activity.LayoutInflater));
			timer = new Timer(ttimerCallback, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(10));
		}
		#endregion

		#region current location
		public void OnLocationChanged(Location location)
		{
			_currentLocation = location;
		}

		public void OnProviderDisabled(string provider)
		{
			using (var alert = new AlertDialog.Builder(mActivity))
			{
				alert.SetTitle("Please enable GPS");
				alert.SetMessage("Enable GPS in order to get your current location.");

				alert.SetPositiveButton("Enable", (senderAlert, args) =>
				{
					Intent intent = new Intent(global::Android.Provider.Settings.ActionLocationSourceSettings);
					StartActivity(intent);
				});

				alert.SetNegativeButton("Continue", (senderAlert, args) =>
				{
					alert.Dispose();
				});

				Dialog dialog = alert.Create();
				dialog.Show();
			}
		}

		public void OnProviderEnabled(string provider)
		{
			_currentLocation = _locationManager.GetLastKnownLocation(LocationManager.GpsProvider);
		}

		public void OnStatusChanged(string provider, Availability status, Bundle extras)
		{
		}

		private Location GetGPSLocation()
		{
			_locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 2000, 1, this);
			_currentLocation = _locationManager.GetLastKnownLocation(LocationManager.GpsProvider);
			return _currentLocation;
		}
		#endregion
	}
}

