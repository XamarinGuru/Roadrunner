using System;
using UIKit;
using System.Collections.Generic;
using RoadRunnerNew.iOS.Extension;
using GalaSoft.MvvmLight.Helpers;
using RoadRunner.Shared;
using System.Threading.Tasks;
using System.Linq;
using RoadRunner.Shared.Classes;

namespace RoadRunnerNew.iOS
{
	partial class RideInformationViewController : BaseTitleViewController
	{
		private  static bool IsFirstTime = true;
		public bool isFromMenu = true;

		private UINavigationController thisController { get; set; }

		private List<FareContainer> m_fares = new List<FareContainer> ();

		private int m_selectedRideType = 1;
		private int m_selectedRideTypeValue = 1;


		public RideInformationViewController (IntPtr handle) : base (handle)
		{
			Title = "RIDE INFORMATION";
		}

		public override void ViewDidAppear (bool animated)
		{
			GetFares();
		}

		public override void ViewWillAppear (bool animated)
		{
			AppSettings.CurrentNavigation = NavigationController;
			//thisController = NavigationController;
			if (!isFromMenu) {
				NavigationItem.Customize (NavigationController);
			}
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();


			if (!isFromMenu) {
				NavigationItem.Customize (NavigationController);
			}

			var tap = new UITapGestureRecognizer(() => { View.EndEditing(true); });
			View.AddGestureRecognizer(tap);

			//switchPandH.On = false;
			switchPandH.ValueChanged += (sender, e) => {
				
				if(switchPandH.On)
				{
					lblHours.TextColor = UIColor.White;
					edtNumberOfHours.BackgroundColor = UIColor.White;
					edtNumberOfHours.Enabled = true;
				}else{
					lblHours.TextColor = UIColor.DarkGray;
					edtNumberOfHours.BackgroundColor = UIColor.DarkGray;
					edtNumberOfHours.Enabled = false;
				}
				GetFares();
			};

			viewSelectUpto.SetCustomView ();

			btnSwitch1.Selected = false;
			btnSwitch1.BackgroundColor = new UIColor ((nfloat)(116 / 255.0), (nfloat)(143 / 255.0), (nfloat)(175 / 255.0), (nfloat)1.0);

			btnSwitch2.Selected = true;
			btnSwitch2.BackgroundColor = new UIColor ((nfloat)(21 / 255.0), (nfloat)(100 / 255.0), (nfloat)(179 / 255.0), (nfloat)1.0); 

			btnSwitch3.Selected = false;
			btnSwitch3.BackgroundColor = new UIColor ((nfloat)(116 / 255.0), (nfloat)(143 / 255.0), (nfloat)(175 / 255.0), (nfloat)1.0);

			btnSwitch1.TouchUpInside += (sender, e) => {
				SetUpto(0);
			};

			btnSwitch2.TouchUpInside += (sender, e) => {
				SetUpto(1);
			};

			btnSwitch3.TouchUpInside += (sender, e) => {
				SetUpto(2);
			};

			btnFare.SetTitle ("Complete all fields above", UIControlState.Normal);

			btnScheduleARide2.SetCustomButton();

			SetBindings ();
			if (IsFirstTime) {
				IsFirstTime = false;
				SetBindingsOnce ();
			}
		}

		public void SetUpto(int type)
		{
			m_selectedRideTypeValue = type;

			if (type == 0 && !btnSwitch1.Selected) {
				btnSwitch1.Selected = true;
				btnSwitch2.Selected = false;
				btnSwitch3.Selected = false;
			} else if (type == 1 && !btnSwitch2.Selected) {
				btnSwitch2.Selected = true;
				btnSwitch1.Selected = false;
				btnSwitch3.Selected = false;
			} else if (type == 2 && !btnSwitch3.Selected) {
				btnSwitch3.Selected = true;
				btnSwitch1.Selected = false;
				btnSwitch2.Selected = false;
			}

			SetButtonsBackground ();
		}

		private void SetBindings(){

			btnShared.TouchUpInside += (object sender, EventArgs e) => {
				SetRideType(sender);
			};
			btnPrivate.TouchUpInside += (object sender, EventArgs e) => {
				SetRideType(sender);
			};
			btnBlack.TouchUpInside += (object sender, EventArgs e) => {
				SetRideType(sender);
			};
			btnSUV.TouchUpInside += (object sender, EventArgs e) => {
				SetRideType(sender);
			};
			btnLimo.TouchUpInside += (object sender, EventArgs e) => {
				SetRideType(sender);
			};
			btnBus.TouchUpInside += (object sender, EventArgs e) => {
				SetRideType(sender);
			};
			btnWheel.TouchUpInside += (object sender, EventArgs e) => {
				SetRideType(sender);
			};

			this.SetBinding (
				() => Facade.Instance.CurrentRide.ReservationType,
				() => switchPandH.On,
				BindingMode.TwoWay)
				.UpdateTargetTrigger ("ValueChanged");

			SetupPassengers (edtNumberOfPassengers);
			edtNumberOfPassengers.EditingDidEnd += (object sender, EventArgs e) => {
				GetFares();
			};

			this.SetBinding (
				() => Facade.Instance.CurrentRide.NumberOfPassangers,
				() => edtNumberOfPassengers.Text,
				BindingMode.TwoWay)
				.UpdateTargetTrigger ("ValueChanged");

			SetupPassengers (edtNumberOfHours);
			edtNumberOfHours.EditingDidEnd += (object sender, EventArgs e) => {
				GetFares();
			};

			this.SetBinding (
				() => Facade.Instance.CurrentRide.NumberOfHours,
				() => edtNumberOfHours.Text,
				BindingMode.TwoWay)
				.UpdateTargetTrigger ("ValueChanged");

			#region PickUp elements

			this.SetBinding (
				() => Facade.Instance.CurrentRide.PickUpLocation,
				() => edtPickUpLocation.Text,
				BindingMode.TwoWay)
				.UpdateSourceTrigger ("PickUpLocationChanges");

			#endregion

			#region Dropoff elements

			this.SetBinding (
				() => Facade.Instance.CurrentRide.DropOffLocation,
				() => editDropOffLocation.Text,
				BindingMode.TwoWay)
				.UpdateSourceTrigger ("DropOffLocationChanges");

			#endregion

			edtPickUpLocation.EditingDidBegin += (object sender, EventArgs e) => {
				UIStoryboard sb = UIStoryboard.FromName ("MainStoryboard", null);
				PickUpViewController pvc = (PickUpViewController)sb.InstantiateViewController ("PickUpViewController");
				pvc.IsPickUpLocation = true;
				edtPickUpLocation.ResignFirstResponder ();
				NavigationController.PushViewController (pvc, true);
			};

			editDropOffLocation.EditingDidBegin += (object sender, EventArgs e) => {
				UIStoryboard sb = UIStoryboard.FromName ("MainStoryboard", null);
				PickUpViewController pvc = (PickUpViewController)sb.InstantiateViewController ("PickUpViewController");
				pvc.IsPickUpLocation = false;
				editDropOffLocation.ResignFirstResponder ();
				NavigationController.PushViewController (pvc, true);
			};

			btnScheduleARide2.SetCommand ("TouchUpInside", Facade.Instance.CurrentRide.GoToTheFlightInformation);
		}

		private void SetBindingsOnce(){

			this.SetBinding (
				() => Facade.Instance.CurrentRide.CanGoToTheFlightInformation)
				.UpdateSourceTrigger ("CanGoToTheFlightInformationChanges")
				.WhenSourceChanges (
					async () => {
						if (Facade.Instance.CurrentRide.CanGoToTheFlightInformation) {

							UIStoryboard sb = UIStoryboard.FromName ("MainStoryboard", null);
							UIViewController pvc = new UIViewController();

							if (Facade.Instance.CurrentRide.IsPickUpLocationAirport) {
								pvc = sb.InstantiateViewController ("ScheduleARideViewController21");
							} else if(Facade.Instance.CurrentRide.IsDropOffLocationAirport) {
								pvc = sb.InstantiateViewController ("ScheduleARideViewController2");
							} else {
								pvc = sb.InstantiateViewController ("ScheduleARideViewController22");
							}

							//thisController.PushViewController (pvc, true);
							//this.NavigationController.PushViewController (pvc, true);
							AppSettings.CurrentNavigation.PushViewController(pvc, true);
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

		private void SetRideType(object sender)
		{
			btnShared.Selected = false;
			btnPrivate.Selected = false;
			btnBlack.Selected = false;
			btnSUV.Selected = false;
			btnLimo.Selected = false;
			btnBus.Selected = false;
			btnWheel.Selected = false;

			UIButton selectedButton = (UIButton)sender;
			selectedButton.Selected = true;
			selectedButton.ContentScaleFactor = 1.5f;

			m_selectedRideType = (int)selectedButton.Tag;

			switch(selectedButton.Tag)
			{
			case 0:
				btnSwitch1.Enabled = false;
				btnSwitch3.Enabled = false;
				btnSwitch1.SetTitle ("", UIControlState.Normal);
				btnSwitch2.SetTitle ("Up to 7", UIControlState.Normal);
				btnSwitch3.SetTitle ("", UIControlState.Normal);
				btnSwitch1.Selected = false;
				btnSwitch2.Selected = true;
				btnSwitch3.Selected = false;
				break;
			case 1:				
				btnSwitch1.Enabled = true;
				btnSwitch3.Enabled = true;
				btnSwitch1.SetTitle ("Up to 7", UIControlState.Normal);
				btnSwitch2.SetTitle ("Up to 9", UIControlState.Normal);
				btnSwitch3.SetTitle ("Up to 14", UIControlState.Normal);
				break;
			case 2:
				btnSwitch1.Enabled = false;
				btnSwitch3.Enabled = false;
				btnSwitch1.SetTitle ("", UIControlState.Normal);
				btnSwitch2.SetTitle ("Up to 3", UIControlState.Normal);
				btnSwitch3.SetTitle ("", UIControlState.Normal);
				btnSwitch1.Selected = false;
				btnSwitch2.Selected = true;
				btnSwitch3.Selected = false;
				break;
			case 3:
				btnSwitch1.Enabled = false;
				btnSwitch3.Enabled = false;
				btnSwitch1.SetTitle ("", UIControlState.Normal);
				btnSwitch2.SetTitle ("Up to 5", UIControlState.Normal);
				btnSwitch3.SetTitle ("", UIControlState.Normal);
				btnSwitch1.Selected = false;
				btnSwitch2.Selected = true;
				btnSwitch3.Selected = false;
				break;
			case 4:
				btnSwitch1.Enabled = true;
				btnSwitch3.Enabled = false;
				btnSwitch1.SetTitle ("Up to 6", UIControlState.Normal);
				btnSwitch2.SetTitle ("Up to 13", UIControlState.Normal);
				btnSwitch3.SetTitle ("", UIControlState.Normal);
				break;
			case 5:
				btnSwitch1.Enabled = true;
				btnSwitch3.Enabled = false;
				btnSwitch1.SetTitle ("Up to 24", UIControlState.Normal);
				btnSwitch2.SetTitle ("Up to 56", UIControlState.Normal);
				btnSwitch3.SetTitle ("", UIControlState.Normal);
				break;
			case 6:
				btnSwitch1.Enabled = false;
				btnSwitch3.Enabled = false;
				btnSwitch1.SetTitle ("", UIControlState.Normal);
				btnSwitch2.SetTitle ("Up to 9", UIControlState.Normal);
				btnSwitch3.SetTitle ("", UIControlState.Normal);
				btnSwitch1.Selected = false;
				btnSwitch2.Selected = true;
				btnSwitch3.Selected = false;
				break;
			default:
				break;
			}
			SetButtonsBackground ();
		}

		private void SetButtonsBackground()
		{
			if (btnSwitch1.Selected) {
				btnSwitch1.BackgroundColor = new UIColor ((nfloat)(21 / 255.0), (nfloat)(100 / 255.0), (nfloat)(179 / 255.0), (nfloat)1.0); 
				m_selectedRideTypeValue = 0;
			} else {
				btnSwitch1.BackgroundColor = new UIColor ((nfloat)(116 / 255.0), (nfloat)(143 / 255.0), (nfloat)(175 / 255.0), (nfloat)1.0); 
			}

			if (btnSwitch2.Selected) {
				btnSwitch2.BackgroundColor = new UIColor ((nfloat)(21 / 255.0), (nfloat)(100 / 255.0), (nfloat)(179 / 255.0), (nfloat)1.0); 
				m_selectedRideTypeValue = 1;
			} else {
				btnSwitch2.BackgroundColor = new UIColor ((nfloat)(116 / 255.0), (nfloat)(143 / 255.0), (nfloat)(175 / 255.0), (nfloat)1.0); 
			}

			if (btnSwitch3.Selected) {
				btnSwitch3.BackgroundColor = new UIColor ((nfloat)(21 / 255.0), (nfloat)(100 / 255.0), (nfloat)(179 / 255.0), (nfloat)1.0); 
				m_selectedRideTypeValue = 2;
			} else {
				btnSwitch3.BackgroundColor = new UIColor ((nfloat)(116 / 255.0), (nfloat)(143 / 255.0), (nfloat)(175 / 255.0), (nfloat)1.0); 
			}

			SetFare ();
		}

		private void SetFare()
		{
			Facade.Instance.CurrentRide.SelectedFare = null;
			btnFare.SetTitle ("Please select a valid vehicle", UIControlState.Normal);

			if (m_fares.Count == 0 || m_selectedRideType == -1)
				return;

			var serviceID = -1;
			switch(m_selectedRideType)
			{
			case 0:
				serviceID = 0;
				break;
			case 1:				
				if (m_selectedRideTypeValue == 0) {
					serviceID = 1;
				} else if (m_selectedRideTypeValue == 1) {
					serviceID = 11;
				} else if (m_selectedRideTypeValue == 2) {
					serviceID = 12;
				}
				break;
			case 2:
				serviceID = 2;
				break;
			case 3:
				serviceID = 4;
				break;
			case 4:
				if (m_selectedRideTypeValue == 0) {
					serviceID = 3;
				} else if (m_selectedRideTypeValue == 1) {
					serviceID = 6;
				}
				break;
			case 5:
				if (m_selectedRideTypeValue == 0) {
					serviceID = 13;
				} else if (m_selectedRideTypeValue == 1) {
					serviceID = 9;
				}
				break;
			case 6:
				serviceID = 5;
				break;
			default:
				break;
			}

			for (int i = 0; i < m_fares.Count; i++) {
				var fare = m_fares [i];
				if (Convert.ToInt32(fare.serviceid) == serviceID) {
					btnFare.SetTitle (string.Format("Fare - ${0}", fare.amount), UIControlState.Normal);
					Facade.Instance.CurrentRide.SelectedFare = fare;
					Facade.Instance.CurrentRide.SelectedFareType = m_selectedRideType;
				}
			}
		}

		private async void GetFares() {

			if (edtPickUpLocation.Text == string.Empty || editDropOffLocation.Text == string.Empty) {
				btnFare.SetTitle ("Complete all fields above", UIControlState.Normal);
				return;
			}

			ShowLoadingView ("Retrieving Available Vehicles...");

			if(switchPandH.On){
				m_fares = GetFareForReservationCharter();
			}else{
				m_fares = GetFareForReservationPoint();
			}

			if (m_fares.Count == 0) {
				UserTrackingReporter.TrackUser (Constant.CATEGORY_SCHEDULE_RIDE, "No fares returned");
				btnFare.SetTitle ("Complete all fields above", UIControlState.Normal);
				ShowMessageBox ("Fares Error", "No fares available for the selected pickup/dropoff. Please confirm they are correct and try again.");
				HideLoadingView ();
				return;
			}

			UserTrackingReporter.TrackUser (Constant.CATEGORY_SCHEDULE_RIDE, "Fares retrieved successfully");

			SetFare ();
			HideLoadingView ();
			return;
		}

		private List<FareContainer> GetFareForReservationPoint() {

			List<FareContainer> fares = new List<FareContainer> ();

			try {
				var result = string.Empty;
				var dic = new Dictionary<String, String>();

				//var dt = DateTime.Parse(Facade.Instance.CurrentRide.RequestedArrivalTimeAndDate);
				var dt = DateTime.Now.ToLocalTime();
				var dtStr= dt.ToString ("MM/dd/yyyy");

				if(Facade.Instance.CurrentRide.IsPickUpLocationAirport){
					dic = new Dictionary<String, String> {
						{ Constant.GETFARES_DEPAIRPORT,"" },
						{ Constant.GETFARES_DEPZIP, "" },
						{ Constant.GETFARES_DEPPESSANGER, "" },
						{ Constant.GETFARES_DEPDATE, "" },
						{ Constant.GETFARES_ARVAIRPORT, Facade.Instance.CurrentRide.PickUpLocation3CharacterAirportCode },
						{ Constant.GETFARES_ARVZIP, Facade.Instance.CurrentRide.DropOffLocationZip },
						{ Constant.GETFARES_ARVPESSANGER, Facade.Instance.CurrentRide.NumberOfPassangers },
						{ Constant.GETFARES_ARVDATE,  dtStr},
						{ Constant.GETFARES_QUERYSTRING,"" },
						{ Constant.GETFARES_DISCPROMOCODE, Facade.Instance.CurrentRide.PromoCode }
					};
				}
				else{
					if(Facade.Instance.CurrentRide.IsDropOffLocationAirport){
						dic = new Dictionary<String, String> {
							{ Constant.GETFARES_DEPAIRPORT,Facade.Instance.CurrentRide.DropOffLocation3CharacterAirportCode },
							{ Constant.GETFARES_DEPZIP, Facade.Instance.CurrentRide.PickUpLocationZip },
							{ Constant.GETFARES_DEPPESSANGER, Facade.Instance.CurrentRide.NumberOfPassangers },
							{ Constant.GETFARES_DEPDATE, dtStr },
							{ Constant.GETFARES_ARVAIRPORT, "" },
							{ Constant.GETFARES_ARVZIP, "" },
							{ Constant.GETFARES_ARVPESSANGER, "" },
							{ Constant.GETFARES_ARVDATE,  ""},
							{ Constant.GETFARES_QUERYSTRING,"" },
							{ Constant.GETFARES_DISCPROMOCODE, Facade.Instance.CurrentRide.PromoCode }
						};
					}
					else{
						dic = new Dictionary<String, String>
						{
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERHOURS, "1" },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERARVCITY, string.Format("whatever,{0}", Facade.Instance.CurrentRide.DropOffLocationZip) },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERDEPCITY, string.Format("whatever,{0}", Facade.Instance.CurrentRide.PickUpLocationZip) },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERPASSENGER, Facade.Instance.CurrentRide.NumberOfPassangers  },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERTODATE, dtStr },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERFROMDATE, dtStr },
							{ Constant.GETFAREFORRESERVATIONCHARTER_SERVICEID, "" },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CLIENTID, "" },
							{ Constant.GETFAREFORRESERVATIONCHARTER_LOG, "" },
							{ Constant.GETFAREFORRESERVATIONCHARTER_ASSIGNID, "" },
							{ Constant.GETFAREFORRESERVATIONCHARTER_DISCPROMOCODE, Facade.Instance.CurrentRide.PromoCode }
						};

						Task runSync1 = Task.Factory.StartNew (async () => {
							result = await AppData.ApiCall (Constant.GETFAREFORRESERVATIONCHARTER, dic); 
						}).Unwrap ();
						runSync1.Wait ();

						var tt1 = AppData.ParseResponse (Constant.GETFAREFORRESERVATIONCHARTER, result);
						fares = (tt1 as GetFareForReservationCharterResponse).Fares;

						return fares;
					}
				}

				Task runSync = Task.Factory.StartNew (async () => {
					result = await AppData.ApiCall (Constant.GETFARES, dic); 
				}).Unwrap ();
				runSync.Wait ();

				var tt = (GetFaresResponse)AppData.ParseResponse (Constant.GETFARES, result);
				fares = tt.Fares;

			} catch (Exception ex) {
				HideLoadingView ();
				ShowMessageBox ("Error", "An error occurred getting fares. \n\nError: " + ex.Message);
				CrashReporter.Report (ex);
				return null;
			}

			return fares;
		}

		private List<FareContainer> GetFareForReservationCharter() {

			List<FareContainer> fares = new List<FareContainer> ();

			try {
					
				var result = string.Empty;

				var dic = new Dictionary<String, String>();

				var dt = DateTime.Now.ToLocalTime();
				var dtStr= dt.ToString ("MM/dd/yyyy");

				if(Facade.Instance.CurrentRide.IsPickUpLocationAirport){
					dic = new Dictionary<String, String>
					{
						{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERHOURS, edtNumberOfHours.Text },
						{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERARVCITY, string.Format("whatever,{0}", Facade.Instance.CurrentRide.DropOffLocationZip) },
						{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERDEPCITY, string.Format("whatever,{0}", Facade.Instance.CurrentRide.PickUpLocationZip) },
						{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERPASSENGER, Facade.Instance.CurrentRide.NumberOfPassangers  },
						{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERTODATE, dtStr },
						{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERFROMDATE, dtStr },
						{ Constant.GETFAREFORRESERVATIONCHARTER_SERVICEID, "" },
						{ Constant.GETFAREFORRESERVATIONCHARTER_CLIENTID, "" },
						{ Constant.GETFAREFORRESERVATIONCHARTER_LOG, "" },
						{ Constant.GETFAREFORRESERVATIONCHARTER_ASSIGNID, "" },
						{ Constant.GETFAREFORRESERVATIONCHARTER_DISCPROMOCODE, Facade.Instance.CurrentRide.PromoCode }
					};
				}
				else{
					if(Facade.Instance.CurrentRide.IsDropOffLocationAirport){
						dic = new Dictionary<String, String>
						{
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERHOURS, edtNumberOfHours.Text },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERARVCITY, string.Format("whatever,{0}", Facade.Instance.CurrentRide.DropOffLocationZip) },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERDEPCITY, string.Format("whatever,{0}", Facade.Instance.CurrentRide.PickUpLocationZip) },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERPASSENGER, Facade.Instance.CurrentRide.NumberOfPassangers  },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERTODATE, dtStr },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERFROMDATE, dtStr },
							{ Constant.GETFAREFORRESERVATIONCHARTER_SERVICEID, "" },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CLIENTID, "" },
							{ Constant.GETFAREFORRESERVATIONCHARTER_LOG, "" },
							{ Constant.GETFAREFORRESERVATIONCHARTER_ASSIGNID, "" },
							{ Constant.GETFAREFORRESERVATIONCHARTER_DISCPROMOCODE, Facade.Instance.CurrentRide.PromoCode }
						};
					} else{
						dic = new Dictionary<String, String>
						{
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERHOURS, edtNumberOfHours.Text },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERARVCITY, string.Format("whatever,{0}", Facade.Instance.CurrentRide.DropOffLocationZip) },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERDEPCITY, string.Format("whatever,{0}", Facade.Instance.CurrentRide.PickUpLocationZip) },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERPASSENGER, Facade.Instance.CurrentRide.NumberOfPassangers  },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERTODATE, dtStr },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CHARTERFROMDATE, dtStr },
							{ Constant.GETFAREFORRESERVATIONCHARTER_SERVICEID, "" },
							{ Constant.GETFAREFORRESERVATIONCHARTER_CLIENTID, "" },
							{ Constant.GETFAREFORRESERVATIONCHARTER_LOG, "" },
							{ Constant.GETFAREFORRESERVATIONCHARTER_ASSIGNID, "" },
							{ Constant.GETFAREFORRESERVATIONCHARTER_DISCPROMOCODE, Facade.Instance.CurrentRide.PromoCode }
						};
					}
				}

				Task runSync = Task.Factory.StartNew (async () => {
					result = await AppData.ApiCall (Constant.GETFAREFORRESERVATIONCHARTER, dic); 
				});
				runSync.Wait ();

				var tt = AppData.ParseResponse (Constant.GETFAREFORRESERVATIONCHARTER, result);
				fares = (tt as GetFareForReservationCharterResponse).Fares;

			} catch (Exception ex) {
				HideLoadingView ();
				ShowMessageBox ("Error", "An error occurred getting fares. \n\nError: " + ex.Message);
				CrashReporter.Report (ex);
				return null;
			}
			return fares;
		}

	}
}
