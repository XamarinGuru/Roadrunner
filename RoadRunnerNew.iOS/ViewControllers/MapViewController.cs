using System;
using System.Linq;
using UIKit;
using Foundation;
using Google.Maps;
using CoreGraphics;
using System.Drawing;
using System.Threading;
using CoreLocation;
using RoadRunner.Shared;
using RoadRunner.Shared.Classes;
using System.Threading.Tasks;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Helpers;
using RoadRunnerNew.iOS.Extension;
using RoadRunnerNew.iOS.View;

namespace RoadRunnerNew.iOS
{
	partial class MapViewController : BaseTitleViewController
	{
		private MapView mapView;
		private Timer timer;
		private static bool IsFirstTime = true;
		//private bool _firstLocationUpdate;
		private Marker _selectedMarker = null;

		private UINavigationController thisController { get; set; }

		private List<Dictionary<String, String>> _pickups = null;

		public MapViewController (IntPtr handle) : base (handle, "ROADRUNNER SHUTTLE")
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var tap = new UITapGestureRecognizer(() => { View.EndEditing(true); });
			View.AddGestureRecognizer(tap);

			txtMapCard.ShouldReturn += TextFieldShouldReturn;
			txtMapPromo.ShouldReturn += TextFieldShouldReturn;

			btnMapAddCreditCard.SetCustomButton ();
			btnMapSelectPromo.SetCustomButton ();
			btnMapSelectPromo.TouchUpInside += VerifyPromoCode;

			btnReadyPickup.SetCustomButton ();
			btnScheduleARide.SetCustomButton ();

			var lResult = LocationHelper.GetLocationResult ();

			var camera = CameraPosition.FromCamera (latitude: lResult.Latitude, 
				longitude: lResult.Longitude, 
				zoom: 14);
			mapView = MapView.FromCamera (RectangleF.Empty, camera);
			mapView.MyLocationEnabled = false;

			timer = new Timer (ttimerCallback, null, TimeSpan.FromSeconds (0), TimeSpan.FromSeconds (60));

			SetBindings ();
			if (IsFirstTime) {
				IsFirstTime = false;
				SetBindingOnce ();
			}
		}

		private void SetBindings(){

			this.SetBinding (
				() => Facade.Instance.CurrentRide.PickUpLocation,
				() => txtSearchLocation.Text,
				BindingMode.TwoWay)
				.UpdateSourceTrigger ("PickUpLocationChanges");
			
			this.SetBinding (
				() => Facade.Instance.CurrentRide.CreditCard,
				() => txtMapCard.Text,
				BindingMode.TwoWay)
				.UpdateTargetTrigger ("ValueChanged");

			txtMapPromo.Text = Facade.Instance.CurrentRide.PromoCode;

			this.SetBinding (
				() => txtMapPromo.Text,
				() => Facade.Instance.CurrentRide.PromoCode,
				BindingMode.TwoWay)
				.UpdateSourceTrigger ("EditingChanged");
			
			btnMapAddCreditCard.TouchUpInside+= (object sender, EventArgs e) => {
				UIStoryboard sb = UIStoryboard.FromName ("MainStoryboard", null);
				AddCreditCardViewController pvc = (AddCreditCardViewController)sb.InstantiateViewController ("AddCreditCardViewController");
				pvc.fromWhere = "map";
				pvc.callback = LoadCreditCards;
				NavigationController.PushViewController (pvc, true);
			};

			btnReadyPickup.TouchUpInside += ReadyForPickup;

			btnPickupLocation.TouchUpInside += (object sender, EventArgs e) => {
				UIStoryboard sb = UIStoryboard.FromName ("MainStoryboard", null);
				PickUpViewController pvc = (PickUpViewController)sb.InstantiateViewController ("PickUpViewController");
				pvc.IsPickUpLocation = true;
				txtSearchLocation.ResignFirstResponder ();
				NavigationController.PushViewController (pvc, true);
			};

			btnScheduleARide.SetCommand ("TouchUpInside", Facade.Instance.CurrentRide.GoToTheRideInformation);
		}

		private void SetBindingOnce()
		{
			this.SetBinding (
				() => Facade.Instance.CurrentRide.CanGoToTheRideInformation)
				.UpdateSourceTrigger ("CanGoToTheRideInformationChanges")
				.WhenSourceChanges (
					async () => {
						if (Facade.Instance.CurrentRide.CanGoToTheRideInformation) {
							UIStoryboard sb = UIStoryboard.FromName ("MainStoryboard", null);
							RideInformationViewController pvc = (RideInformationViewController)sb.InstantiateViewController ("ScheduleARideViewController");
							pvc.isFromMenu = false;
							AppSettings.CurrentNavigation.PushViewController (pvc, true);
						} else {
							if (Facade.Instance.CurrentRide.ValidaionError != null && Facade.Instance.CurrentRide.ValidaionError.Count > 0) {
								string header = Facade.Instance.CurrentRide.ValidaionError.Count > 1
									? "Just a couple things left:"
									: "Please correct the last thing:";

								var delimeter = Environment.NewLine + Environment.NewLine;
								var message = String.Join (delimeter, Facade.Instance.CurrentRide.ValidaionError.Select (r => r.ErrorMessage));
								InvokeOnMainThread (() => new UIAlertView (header, message, null, "Ok", null).Show ());
							}
						}
					});
		}

		public override void ViewWillAppear (bool animated)
		{
			AppSettings.CurrentNavigation = this.NavigationController;
			base.ViewWillAppear (animated);
			//thisController = NavigationController;
			LoadCreditCards ();
		}

		public override void ViewWillLayoutSubviews()
		{
			if (mapView != null && mMapView != null && mMapView.Window != null)
			{
				RepaintMap ();
			}
		}

		public void RepaintMap(){
			foreach (var subview in mMapView.Subviews) {
				subview.RemoveFromSuperview ();
			}			

			var width = mMapView.Frame.Width;
			var height = mMapView.Frame.Height;
			mapView.Frame = new CGRect (0,0, width, height);

			mMapView.AddSubview (mapView);
			mMapView.SendSubviewToBack (mapView);

			mapView.Tapped += (object sender, GMSCoordEventArgs e) => {
				View.EndEditing(true);
			};
		}

		private void ttimerCallback(object state){
			InvokeOnMainThread(async () => { await ResetMapView();});
		}

		private readonly List<Marker> _currentMarkers = new List<Marker>();

		//private async System.Threading.Tasks.Task ResetMapView()
		private async System.Threading.Tasks.Task ResetMapView()
		{
			try
			{
				UserTrackingReporter.TrackUser(Constant.CATEGORY_HOME, "Getting reservation vehicle locations");

				if (mapView == null ) return;

				GetMyValidBookedReservations();

			}
			catch (Exception exc)
			{
				CrashReporter.Report(exc);
			}
		}

		UIView markerInfoWindow(UIView view, Marker marker)
		{
			if (marker.ZIndex == 0)
				return null;
			
			MapMarkerView v = MapMarkerView.Create();

			var resID = marker.ZIndex.ToString ();

			for (int i = 0; i < _pickups.Count; i++) {
				if (_pickups [i]["ResID"] == resID) {
					var pickup = _pickups [i];
					var pos1 = new CLLocation (double.Parse (pickup ["Latitude"]), double.Parse (pickup ["Longitude"]));
					var pos2 = new CLLocation (LocationHelper.GetLocationResult().Latitude, LocationHelper.GetLocationResult().Longitude);
					var distance = string.Format("{0}m", (Math.Round(pos1.DistanceFrom(pos2))).ToString());

					DateTime pDate = DateTime.Parse(pickup ["Date"]);
					string[] arrDate = pDate.GetDateTimeFormats ();
					string strType = pickup ["ArvDep"] == "1" ? "Drop-Off" : "Pick-Up";
					string title = string.Format ("{0} - {1} {2}", pickup ["ResType"], arrDate[108], strType);
					v.SetView (title, distance);
				}
			}
			return v;
		}


		private void ReadyForPickup(object sender, EventArgs e)
		{
			UIButton btnReadyPickup = (UIButton)sender;

			if (btnReadyPickup.BackgroundColor == UIColor.Gray) {
				InvokeOnMainThread (() => new UIAlertView ("gray", "Thanks for the heads up - your driver has been notified you are ready.", null, "Ok", null).Show ());
			} else if (btnReadyPickup.BackgroundColor == UIColor.Green) {
				txtBtnReadyPickup.BecomeFirstResponder ();
			}
		}
			
		private async void GetMyValidBookedReservations()
		{
			ShowLoadingView ("Loading data...");

			_pickups = new List<Dictionary<string, string>> ();

			try
			{
				var lResult = LocationHelper.GetLocationResult();

				if(_selectedMarker!=null){
					_selectedMarker.Map = null;
					mapView.Clear();
				}


				if (_currentMarkers != null)
				{
					foreach (var marker in _currentMarkers)
					{
						marker.Map = null;
					}
					_currentMarkers.Clear();
					mapView.MarkerInfoWindow = null;
				}

				NSDateFormatter dateFormat = new NSDateFormatter();
				dateFormat.DateFormat = "MM/dd/yyyy";
				NSDate now = NSDate.FromTimeIntervalSinceReferenceDate((DateTime.Now - (new DateTime(2001, 1, 1, 0, 0, 0))).TotalSeconds);
				var strNow = dateFormat.ToString(now);

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

				for (int i = 0; i < availableReservations.Count; i ++)
				{
					var reservation = availableReservations[i];

					//Task runSync = Task.Factory.StartNew (async () => {
						var pickupData = await GetPickupDataForReservation(reservation);
						_pickups.Add(pickupData);
					//}).Unwrap ();
					//runSync.Wait ();
				}

				var bounds = new CoordinateBounds();

				var listReservations = new List<KeyValuePair<object, string>>();
				listReservations.Add(new KeyValuePair<object, string>("0", "Ready for all rides"));
				foreach ( Dictionary<String, String> pickup in _pickups)
				{
					var marker = new Marker
					{
						Position = new CLLocationCoordinate2D(double.Parse(pickup["Latitude"]), double.Parse(pickup["Longitude"])),
						Map = mapView,
						Icon = rotateImage(UIImage.FromBundle(getMapVehicleIconByServiceID(pickup["ServiceID"])), float.Parse(pickup["Angle"])),
						ZIndex = int.Parse(pickup["ResID"])
					};
					_currentMarkers.Add(marker);
					bounds = bounds.Including(marker.Position);

					listReservations.Add(new KeyValuePair<object, string>(pickup["ResID"], pickup["DisplayTxt"]));//string.Format("Shared Van (7 Passengers) \n 3:45 Pick-Up to Los Angeles", pickup["ResID"], pickup["ResType"])));
				}

				if (_pickups.Count > 0)
				{
					btnReadyPickup.BackgroundColor = UIColor.Green;
					btnReadyPickup.SetTitle(string.Format("READY FOR PICKUP ({0})", _pickups.Count.ToString()), UIControlState.Normal);
					SetupReadyPickup (txtBtnReadyPickup, listReservations, CallForReadyPickup);
				}else{
					btnReadyPickup.BackgroundColor = UIColor.Gray;
					btnReadyPickup.SetTitle("READY FOR PICKUP", UIControlState.Normal);
				}

				bounds = bounds.Including(new CLLocationCoordinate2D(lResult.Latitude, lResult.Longitude));

				_selectedMarker = new Marker {
					Position = new CLLocationCoordinate2D (lResult.Latitude, lResult.Longitude),
					Map = mapView,
					Icon = UIImage.FromBundle("icon_mylocation.png")
				};

				mapView.SelectedMarker = _selectedMarker;

				if (_pickups.Count > 0)
				{
					mapView.Animate(CameraUpdate.FitBounds(bounds, 100f));
				}

				mapView.TappedMarker = (map, maker) => {
					mapView.MarkerInfoWindow = new GMSInfoFor(markerInfoWindow);
					return false;
				};
			}
			catch (Exception ex)
			{
				CrashReporter.Report(ex);
				HideLoadingView();
				return;
			}
			HideLoadingView();
		}


		// please take a look it, Pavel
		private async Task< Dictionary<string, string> > GetPickupDataForReservation(GetReadyForPickupListItem item)
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
			} catch (Exception ex)
			{
				CrashReporter.Report(ex);
				HideLoadingView();
				return null;
			}
			//HideLoadingView();
		}

		private async void CallForReadyPickup(string reservationId)
		{
			try
			{
				var id = reservationId;
				if (id == null || id == "0")
					return;

				for (int i = 0; i < _pickups.Count; i++) {
					var pickupData = _pickups [i];
					if (pickupData ["ResID"] == id) {
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

						ShowLoadingView ("Sending Request...");
						var result = await AppData.ApiCall(Constant.SETARRIVALCALLEDBYCLIENT, dic);
						var tt = (SetArrivalCalledByClientResponse)AppData.ParseResponse(Constant.SETARRIVALCALLEDBYCLIENT, result);

						var message = string.Format("{0}\n{1}\n{2}", tt.Wait, tt.APInst, tt.EndInst);
						InvokeOnMainThread (() => new UIAlertView (tt.eventStatus, message, null, "Ok", null).Show ());
					}
				}
			} catch (Exception ex)
			{
				CrashReporter.Report(ex);
				HideLoadingView();
				return;
			}
			HideLoadingView();
		}
		private async void LoadCreditCards()
		{
			ShowLoadingView ("Loading data...");

			var dic = new Dictionary<String, String>
			{
				{Constant.GETCREDITCARDDETAILSNEWFORPHONE_CUSTOMERID, AppSettings.UserID},
				{Constant.GETCREDITCARDDETAILSNEWFORPHONE_LOGINTYPE, "-1"},
				{Constant.GETCREDITCARDDETAILSNEWFORPHONE_TOKENID, string.Empty } // do not use the actual token
			};

			String result = String.Empty;

			//System.Threading.Tasks.Task runSync = System.Threading.Tasks.Task.Factory.StartNew(async () => {
				result = await AppData.ApiCall(Constant.GETCREDITCARDDETAILSNEWFORPHONE, dic);

				var tt = (GetCreditCardDetailsNewForPhoneResponse) AppData.ParseResponse(Constant.GETCREDITCARDDETAILSNEWFORPHONE, result);
				var listCreditCards = new List<KeyValuePair<object, string>>();
				var listCreditCardImages = new List<KeyValuePair<object, object>>();

				InvokeOnMainThread(() => { 
					foreach(var card in  tt.CardList){
						
						UIImage cardImage;

						CardType cardType = CardType.Unknown; 

						if (!string.IsNullOrEmpty (card.CardType)) {
							cardType = (CardType)int.Parse (card.CardType);
						}

						switch (cardType)
						{
						case CardType.Mastercard:
							cardImage = UIImage.FromBundle (@"fa-cc-mastercard");
							break;
						case CardType.Visa:				
							cardImage = UIImage.FromBundle (@"fa-cc-visa");
							break;
						case CardType.AmericanExpress:
							cardImage = UIImage.FromBundle (@"fa-cc-amex");
							break;
						case CardType.Discover:
							cardImage = UIImage.FromBundle (@"fa-cc-discover");
							break;
						case CardType.DinersClub:
							cardImage = UIImage.FromBundle (@"fa-cc-diners-club");
							break;
						default:
							cardImage = UIImage.FromBundle (@"oi-credit-card");
							break;
						}

						if(card.Id == AppSettings.SelectedCard)
						{
							txtMapCard.Text = card.CardNumber;
							imgMapCard.Image = cardImage;
						}
						listCreditCards.Add(new KeyValuePair<object, string>(card.Id, card.CardNumber));
						listCreditCardImages.Add(new KeyValuePair<object, object>(card.Id, cardImage));

					}
					SetupCreditCards (txtMapCard, imgMapCard, listCreditCards, listCreditCardImages);
				});

				HideLoadingView();
			//}).Unwrap();
		}

		private async void VerifyPromoCode(object sender, EventArgs e) {
			ShowLoadingView("Veryfying promo code");
			var dic = new Dictionary<String, String> {
				{ Constant.VALIDATEDISCOUNTCOUPON_CODE, txtMapPromo.Text },
				{ Constant.VALIDATEDISCOUNTCOUPON_CUSTOMERID, "-1" },
				{ Constant.VALIDATEDISCOUNTCOUPON_EMAIL, "" },
				{ Constant.VALIDATEDISCOUNTCOUPON_SERVICEID, "-1" },
				{ Constant.VALIDATEDISCOUNTCOUPON_TRAVELDATE, "" },
				{ Constant.VALIDATEDISCOUNTCOUPON_VALIDATIONTYPE, "1" }
			};

			string result;
			ValidateDiscountCouponResponse tt = null;
			try {
				UserTrackingReporter.TrackUser (Constant.CATEGORY_PROMO_CODE, "Validating promo code");

				//Task runSync = Task.Factory.StartNew (async () => {
					result = await AppData.ApiCall (Constant.VALIDATEDISCOUNTCOUPON, dic);
					tt = (ValidateDiscountCouponResponse)AppData.ParseResponse (Constant.VALIDATEDISCOUNTCOUPON, result);
				//}).Unwrap ();
				//runSync.Wait ();

			} catch (Exception ex) {
				CrashReporter.Report (ex);
				HideLoadingView();
				InvokeOnMainThread (() => new UIAlertView ("Failed", "Invalid promo code entered.", null, "Ok", null).Show ());
			}
			if (tt == null || String.IsNullOrEmpty (tt.Result) || tt.Result.ToLower ().Contains ("failed")) {
				UserTrackingReporter.TrackUser (Constant.CATEGORY_PROMO_CODE, "Invalid promo code entered");
				InvokeOnMainThread (() => new UIAlertView ("Failed", "Invalid promo code entered.", null, "Ok", null).Show ());
			} else {
				InvokeOnMainThread (() => new UIAlertView ("Success", "Valid promo code entered.", null, "Ok", null).Show ());
			}
			HideLoadingView();
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewWillAppear (animated);
			if(timer!=null)
			{
				timer.Dispose();
				timer = null;
			}
		}
		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);

			if (disposing) {
				try {
					if (mapView != null) 
					{
						mapView.Dispose ();
						mapView = null;
					}
					if(timer!=null)
					{
						timer.Dispose();
						timer = null;
					}

					GC.Collect();
				} catch {
				}
			}
		}

		public override void TouchesBegan (Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesBegan (touches, evt);

			View.EndEditing(true);
		}
	}
}
