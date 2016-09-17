using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FluentValidation;
using FluentValidation.Results;
using GalaSoft.MvvmLight.Command;
using System.Linq;
using FluentValidation.Validators;
using System.Threading.Tasks;
using RoadRunner.Shared.Classes;

namespace RoadRunner.Shared.ViewModels
{
    public class ScheduleARideVM : AbstractValidator<ScheduleARideVM>, INotifyPropertyChanged
    {
        public ScheduleARideVM()
		{
//			When (x => x.CurrentPage >= 1, () => {
//				
//				RuleFor (r => r.PickUpLocation).NotEmpty ().WithMessage ("Please specify a Pick-up location");
//				When (r => !String.IsNullOrEmpty (r.PickUpLocation), () => {
//					RuleFor (r => r.PickUpLocationZip).NotEmpty ().WithMessage ("The location you picked, doesn't have a valid zip code. Please choose another pick-up location.");	
//				});
//
//				RuleFor (r => r.DropOffLocation).NotEmpty ().WithMessage ("Please specify a Drop-off location");
//				When (r => !String.IsNullOrEmpty (r.DropOffLocation), () => {
//					RuleFor (r => r.DropOffLocationZip).NotEmpty ().WithMessage ("The location you picked, doesn't have a valid zip code. Please choose another drop-off location.");	
//				});
//
//				RuleFor (r => r.RequestedArrivalTimeAndDate).NotEmpty ().WithMessage ("Please specify a Requested arrival time and date");
//
//				When (r => r.IsPickUpLocationAirport, () => {
//					RuleFor (r => r.PickUpAirlines).NotEmpty ().WithMessage ("Please specify a Pick-up Airlines");
//					//Extra check for and ID
//					When (r => !String.IsNullOrEmpty (r.PickUpAirlines), () => {
//						RuleFor (r => r.PickUpAirlinesId).NotEmpty ().WithMessage ("Please specify a Pick-up Airlines");	
//					});
//
//					RuleFor (r => r.PickUpFlightNumber).NotEmpty ().WithMessage ("Please specify a Pick-up Flight number");
//					RuleFor (r => r.PickUpFlightTime).NotNull ().NotEmpty ().WithMessage ("Please specify a Pick-up Flight time");
//					RuleFor (r => r.PickUpFlightTypeIsDomestic).NotNull ().WithMessage ("Please specify a Pick-up Flight type");
//				});
//
//				When (r => r.IsPickUpLocationAirport == false && r.IsDropOffLocationAirport, () => {
//
//					RuleFor (r => r.DropOffAirlines).NotEmpty ().WithMessage ("Please specify a Drop-up Airlines");
//					//Extra check for and ID
//					When (r => !String.IsNullOrEmpty (r.DropOffAirlines), () => {
//						RuleFor (r => r.DropOffAirlinesId).NotEmpty ().WithMessage ("Please specify a Drop-up Airlines");	
//					});
//
//					RuleFor (r => r.DropOffFlightNumber).NotEmpty ().WithMessage ("Please specify a Drop-off Flight number");
//					RuleFor (r => r.DropOffFlightTime).NotNull ().NotEmpty ().WithMessage ("Please specify a Drop-off Flight time");
//					RuleFor (r => r.DropOffFlightTypeIsDomestic).NotNull ().WithMessage ("Please specify a Drop-off Flight type");
//				});
//
//				When (r => r.IsPickUpLocationAirport == false && r.IsDropOffLocationAirport==false, () => {
//					RuleFor(r=>r.Dummy).NotEmpty().WithMessage("We are sorry Zip-To-Zip scenario is not implemented yet");
//				});
//			});

//			When (x => x.CurrentPage >= 2, () => {
//
//				RuleFor (r => r.NumberOfPassangers).NotEmpty ().WithMessage ("Please specify a Number of passangers");
//				RuleFor (r => r.CreditCardId).NotEmpty ().WithMessage ("Please specify a Number of passangers");
//
//				When (r => !String.IsNullOrEmpty (r.PromoCode), () => {
//					RuleFor (r => r.PromoCode).SetValidator (new PromoCodeValidator ()).WithMessage ("Please specify a valid Promo Code");
//				});
//			});
//
//			When (x => x.CurrentPage >= 3, () => {
//
//				RuleFor (r => r.SelectedFare).NotNull ().WithMessage ("Please select a Fare");
//
//			});
//
			When (x => x.CurrentPage >= 4, () => {
				//RuleFor (r => r.PickUpLocation).NotEmpty ().WithMessage ("Please specify a Pick-up location");
				When (r => !String.IsNullOrEmpty (r.PickUpLocation), () => {
					RuleFor (r => r.PickUpLocationZip).NotEmpty ().WithMessage ("The location you picked, doesn't have a valid zip code. Please choose another pick-up location.");	
				});

				When (r => !String.IsNullOrEmpty (r.PromoCode), () => {
					RuleFor (r => r.PromoCode).SetValidator (new PromoCodeValidator ()).WithMessage ("Please specify a valid Promo Code");
				});

			});

			When (x => x.CurrentPage >= 5, () => {
				RuleFor (r => r.PickUpLocation).NotEmpty ().WithMessage ("Please specify a Pick-up location");
				When (r => !String.IsNullOrEmpty (r.PickUpLocation), () => {
					RuleFor (r => r.PickUpLocationZip).NotEmpty ().WithMessage ("The location you picked, doesn't have a valid zip code. Please choose another pick-up location.");	
				});

				RuleFor (r => r.DropOffLocation).NotEmpty ().WithMessage ("Please specify a Drop-off location");
				When (r => !String.IsNullOrEmpty (r.DropOffLocation), () => {
					RuleFor (r => r.DropOffLocationZip).NotEmpty ().WithMessage ("The location you picked, doesn't have a valid zip code. Please choose another drop-off location.");	
				});

				RuleFor (r => r.NumberOfPassangers).NotEmpty ().WithMessage ("Please specify a Number of passangers");

				RuleFor (r => r.SelectedFare).NotNull ().WithMessage ("Please select a Fare");

			});

			When (x => x.CurrentPage >= 6, () => {
				RuleFor (r => r.PickUpLocation).NotEmpty ().WithMessage ("Please specify a Pick-up location");
				When (r => !String.IsNullOrEmpty (r.PickUpLocation), () => {
					RuleFor (r => r.PickUpLocationZip).NotEmpty ().WithMessage ("The location you picked, doesn't have a valid zip code. Please choose another pick-up location.");	
				});

				RuleFor (r => r.DropOffLocation).NotEmpty ().WithMessage ("Please specify a Drop-off location");
				When (r => !String.IsNullOrEmpty (r.DropOffLocation), () => {
					RuleFor (r => r.DropOffLocationZip).NotEmpty ().WithMessage ("The location you picked, doesn't have a valid zip code. Please choose another drop-off location.");	
				});

				RuleFor (r => r.RequestedArrivalTimeAndDate).NotEmpty ().WithMessage ("Please specify a Requested arrival time and date");

				When (r => r.IsPickUpLocationAirport, () => {
					RuleFor (r => r.PickUpAirlines).NotEmpty ().WithMessage ("Please specify a Pick-up Airlines");
					//Extra check for and ID
					When (r => !String.IsNullOrEmpty (r.PickUpAirlines), () => {
						RuleFor (r => r.PickUpAirlinesId).NotEmpty ().WithMessage ("Please specify a Pick-up Airlines");	
					});

					RuleFor (r => r.PickUpFlightNumber).NotEmpty ().WithMessage ("Please specify a Pick-up Flight number");
					RuleFor (r => r.PickUpFlightTime).NotNull ().NotEmpty ().WithMessage ("Please specify a Pick-up Flight time");
					RuleFor (r => r.PickUpFlightTypeIsDomestic).NotNull ().WithMessage ("Please specify a Pick-up Flight type");
				});

				When (r => r.IsPickUpLocationAirport == false && r.IsDropOffLocationAirport, () => {

					RuleFor (r => r.DropOffAirlines).NotEmpty ().WithMessage ("Please specify a Drop-up Airlines");
					//Extra check for and ID
					When (r => !String.IsNullOrEmpty (r.DropOffAirlines), () => {
						RuleFor (r => r.DropOffAirlinesId).NotEmpty ().WithMessage ("Please specify a Drop-up Airlines");	
					});

					RuleFor (r => r.DropOffFlightNumber).NotEmpty ().WithMessage ("Please specify a Drop-off Flight number");
					RuleFor (r => r.DropOffFlightTime).NotNull ().NotEmpty ().WithMessage ("Please specify a Drop-off Flight time");
					RuleFor (r => r.DropOffFlightTypeIsDomestic).NotNull ().WithMessage ("Please specify a Drop-off Flight type");
				});

				When (r => r.IsPickUpLocationAirport == false && r.IsDropOffLocationAirport==false, () => {
                    //TODO: Harc told me to remove this check  /Pavel Kovalev 25.05.2016 16:15:13)/
					//RuleFor(r=>r.Dummy).NotEmpty().WithMessage("We are sorry Zip-To-Zip scenario is not implemented yet");
				});
			});

			When (x => x.CurrentPage >= 7, () => {

				RuleFor (r => r.CreditCardId).NotEmpty ().WithMessage ("Please specify a Credit Card");

			});
			
		}

		public void ResetVM(){

			//Page 1

			RequestedArrivalTimeAndDate = "";

			PickUpLocation = "";
            PickUpLocation_StreetNumber = "";
            PickUpLocation_Street = "";
            PickUpLocation_City = "";
            PickUpLocationName = "";
			PickUpLocationId = "";
			PickUpLocationLatitude = 0;
			PickUpLocationLongitude = 0;
			PickUpAirlines = "";
			PickUpAirlinesId = "";
			PickUpFlightNumber = "";
			PickUpFlightTime = "";
			PickUpFlightTypeIsDomestic = null;
			PickUpLocation3CharacterAirportCode = "";
			PickUpLocationZip = "";
			IsPickUpLocationAirport = false;

			DropOffLocation = "";
            DropOffLocation_StreetNumber = "";
            DropOffLocation_Street = "";
            DropOffLocation_City = "";
            DropOffLocationName = "";
			DropOffLocationId = "";
			DropOffLocationLatitude = 0;
			DropOffLocationLongitude = 0;
			DropOffAirlines = "";
			DropOffAirlinesId = "";
			DropOffFlightNumber = "";
			DropOffFlightTime = "";
			DropOffFlightTypeIsDomestic = null;
			DropOffLocation3CharacterAirportCode = "";
			DropOffLocationZip = "";
			IsDropOffLocationAirport = false;

			//Page 2

			NumberOfPassangers = "1";
			NumberOfHours = "1";
			CreditCard = "";
			CreditCardId = "";

			PromoCode = "";
			ExtraBags = "";
			Gratuity = "";
			IsMeetandGreet = true;

			//Page 3

			OnViewModelReset ();
		}

		private String _dummy = String.Empty;
		public String Dummy
		{
			get { return _dummy; }
			set
			{
				_dummy = value;
				OnPropertyChanged();
			}
		}

		private String _requestedArrivalTimeAndDate = String.Empty;
		public String RequestedArrivalTimeAndDate
		{
			get { return _requestedArrivalTimeAndDate; }
			set
			{
				_requestedArrivalTimeAndDate = value;
				OnPropertyChanged();
			}
		}


        #region Pick Up

        #region PickUp Location

        private String _pickUpLocation_StreetNumber;
        public String PickUpLocation_StreetNumber
        {
            get { return _pickUpLocation_StreetNumber; }
            set
            {
                _pickUpLocation_StreetNumber = value;
                OnPropertyChanged();
            }
        }

        private String _pickUpLocation_Street;
        public String PickUpLocation_Street
        {
            get { return _pickUpLocation_Street; }
            set
            {
                _pickUpLocation_Street = value;
                OnPropertyChanged();
            }
        }

        private String _pickUpLocation_City;
        public String PickUpLocation_City
        {
            get { return _pickUpLocation_City; }
            set
            {
                _pickUpLocation_City = value;
                OnPropertyChanged();
            }
        }

        private PlaceDetailsAPI_RootObject _pickUpData;

        public PlaceDetailsAPI_RootObject PickUpData
        {
            get { return _pickUpData; }
            set {
				_pickUpData = value;

				PickUpLocation = PickUpData.result.formatted_address;
				PickUpLocationName = PickUpData.result.name;
				PickUpLocationId = PickUpData.result.place_id;
				PickUpLocationLatitude = PickUpData.result.geometry.location.lat;
				PickUpLocationLongitude = PickUpData.result.geometry.location.lng;

				var zipData = PickUpData.result.address_components.SingleOrDefault (r => r.types.Contains ("postal_code"));
				if (zipData != null) {
					PickUpLocationZip = zipData.short_name.Replace (" ", "").Replace ("-", "").Substring (0, 5);
				} else {
					PickUpLocationZip = "";
				}


				var streetNumber = PickUpData.result.address_components.SingleOrDefault(r => r.types.Contains("street_number"));
                if (streetNumber != null)
                {
                    PickUpLocation_StreetNumber = streetNumber.long_name;
                }

				var street = PickUpData.result.address_components.SingleOrDefault(r => r.types.Contains("route"));
                if (street != null)
                {
                    PickUpLocation_Street = street.long_name;
                }

				var city = PickUpData.result.address_components.SingleOrDefault(r => r.types.Contains("locality"));
                if (city != null)
                {
                    PickUpLocation_City = city.long_name;
                }

                if (PickUpData.result.types.Contains ("airport")) {
					var ThreeCaracterCode = String.Empty;

					Task runSync = Task.Factory.StartNew (async () => {
						ThreeCaracterCode = await AppData.GetAirportCode (PickUpLocationName, PickUpLocationLatitude, PickUpLocationLongitude);
					}).Unwrap ();
					runSync.Wait ();

					PickUpLocation3CharacterAirportCode = ThreeCaracterCode;

					IsPickUpLocationAirport = !String.IsNullOrEmpty (PickUpLocation3CharacterAirportCode);
				} else{
					IsPickUpLocationAirport = false;
				}


				OnPropertyChanged ();
			}
        }

		private String _pickUpLocationName = String.Empty;
		public String PickUpLocationName
		{
			get { return _pickUpLocationName; }
			set
			{
				_pickUpLocationName = value;
				OnPropertyChanged();
			}
		}

		private String _pickUpLocation3CharacterAirportCode;
		public String PickUpLocation3CharacterAirportCode
		{
			get { return _pickUpLocation3CharacterAirportCode; }
			set
			{
				_pickUpLocation3CharacterAirportCode = value;
				OnPropertyChanged();
			}
		}

        private String _pickUpLocationZip;
        public String PickUpLocationZip
        {
            get { return _pickUpLocationZip; }
            set
            {
                _pickUpLocationZip = value;
                OnPropertyChanged();
            }
        }

        private bool _isPickUpLocationAirport;
        public bool IsPickUpLocationAirport
        {
            get { return _isPickUpLocationAirport; }
            set
            {
                _isPickUpLocationAirport = value;
                OnPropertyChanged();
				OnPickUpAirportChanges ();
            }
        }

        private double _pickUpLocationLongitude;
        public double PickUpLocationLongitude
        {
            get { return _pickUpLocationLongitude; }
            set
            {
                _pickUpLocationLongitude = value;
                OnPropertyChanged();
            }
        }

        private double _pickUpLocationLatitude;
        public double PickUpLocationLatitude
        {
            get { return _pickUpLocationLatitude; }
            set
            {
                _pickUpLocationLatitude = value;
                OnPropertyChanged();
            }
        }
        
		private String _pickUpLocationId;
        public String PickUpLocationId
        {
            get { return _pickUpLocationId; }
            set
            {
                _pickUpLocationId = value;
                OnPropertyChanged();
            }
        }

        private String _pickUpLocation = String.Empty;
        public String PickUpLocation
        {
            get { return _pickUpLocation; }
            set
            {
                _pickUpLocation = value;
                OnPropertyChanged();
				OnPickUpLocationChanges();
            }
        }

        #endregion
		private String _pickUpAirlinesId = String.Empty;

		public String PickUpAirlinesId
		{
			get { return _pickUpAirlinesId; }
			set
			{
				_pickUpAirlinesId = value;
				OnPropertyChanged();
			}
		}

        private String _pickUpAirlines;

        public String PickUpAirlines
        {
            get { return _pickUpAirlines; }
            set
            {
                _pickUpAirlines = value;
                OnPropertyChanged();
            }
        }

        private String _pickUpFlightNumber;

        public String PickUpFlightNumber
        {
            get { return _pickUpFlightNumber; }
            set
            {
                _pickUpFlightNumber = value;
                OnPropertyChanged();
            }
        }

		private String _pickUpFlightTime = null;
		public String PickUpFlightTime {
			get { return _pickUpFlightTime; }
			set {
				_pickUpFlightTime = value;
				OnPropertyChanged ();
			}
		}

		private bool? _pickUpFlightTypeIsDomestic;
		public bool? PickUpFlightTypeIsDomestic
        {
			get { return _pickUpFlightTypeIsDomestic; }
            set
            {
				_pickUpFlightTypeIsDomestic = value;
                OnPropertyChanged();
            }
        }

        #endregion


        #region Drop Off



        private PlaceDetailsAPI_RootObject _dropOffData;

        public PlaceDetailsAPI_RootObject DropOffData
        {
            get { return _dropOffData; }
            set
            {
                _dropOffData = value;

                DropOffLocation = DropOffData.result.formatted_address;
				DropOffLocationName = DropOffData.result.name;
                DropOffLocationId = DropOffData.result.place_id;
                DropOffLocationLatitude = DropOffData.result.geometry.location.lat;
                DropOffLocationLongitude = DropOffData.result.geometry.location.lng;
                
                var zip_data = DropOffData.result.address_components.SingleOrDefault(r => r.types.Contains("postal_code"));
				if (zip_data != null) {
					DropOffLocationZip = zip_data.short_name.Replace (" ", "").Replace ("-", "").Substring (0, 5);
				} else {
					DropOffLocationZip = "";
				}

                var streetNumber = DropOffData.result.address_components.SingleOrDefault(r => r.types.Contains("street_number"));
                if (streetNumber != null)
                {
                    DropOffLocation_StreetNumber = streetNumber.long_name;
                }

                var street = DropOffData.result.address_components.SingleOrDefault(r => r.types.Contains("route"));
                if (street != null)
                {
                    DropOffLocation_Street = street.long_name;
                }

                var city = DropOffData.result.address_components.SingleOrDefault(r => r.types.Contains("locality"));
                if (city != null)
                {
                    DropOffLocation_City = city.long_name;
                }

                if (DropOffData.result.types.Contains("airport")) {
					var ThreeCaracterCode = String.Empty;

					Task runSync = Task.Factory.StartNew (async () => {
						ThreeCaracterCode = await AppData.GetAirportCode(DropOffLocationName,DropOffLocationLatitude, DropOffLocationLongitude);
					}).Unwrap ();
					runSync.Wait ();

					DropOffLocation3CharacterAirportCode = ThreeCaracterCode;

					IsDropOffLocationAirport = !String.IsNullOrEmpty (DropOffLocation3CharacterAirportCode);
				}
				else{
					IsDropOffLocationAirport = false;
				}

                OnPropertyChanged();
            }
        }


        #region DropOffLocation

        private String _dropOffLocation_StreetNumber;
        public String DropOffLocation_StreetNumber
        {
            get { return _dropOffLocation_StreetNumber; }
            set
            {
                _dropOffLocation_StreetNumber = value;
                OnPropertyChanged();
            }
        }


        private String _dropOffLocation_Street;
        public String DropOffLocation_Street
        {
            get { return _dropOffLocation_Street; }
            set
            {
                _dropOffLocation_Street = value;
                OnPropertyChanged();
            }
        }

        private String _dropOffLocation_City;
        public String DropOffLocation_City
        {
            get { return _dropOffLocation_City; }
            set
            {
                _dropOffLocation_City = value;
                OnPropertyChanged();
            }
        }



        private String _dropOffUpLocationName = String.Empty;
		public String DropOffLocationName
		{
			get { return _dropOffUpLocationName; }
			set
			{
				_dropOffUpLocationName = value;
				OnPropertyChanged();
			}
		}

		private String _dropOffLocation3CharacterAirportCode;
		public String DropOffLocation3CharacterAirportCode
		{
			get { return _dropOffLocation3CharacterAirportCode; }
			set
			{
				_dropOffLocation3CharacterAirportCode = value;
				OnPropertyChanged();
			}
		}


        private String _dropOffLocationZip;

        public String DropOffLocationZip
        {
            get { return _dropOffLocationZip; }
            set
            {
                _dropOffLocationZip = value;
                OnPropertyChanged();
            }
        }

        private bool _isDropOffLocationAirport;

        public bool IsDropOffLocationAirport
        {
            get { return _isDropOffLocationAirport; }
            set
            {
                _isDropOffLocationAirport = value;
                OnPropertyChanged();
				OnDropOffAirportChanges ();
            }
        }

        private double _dropOffLocationLongitude;

        public double DropOffLocationLongitude
        {
            get { return _dropOffLocationLongitude; }
            set
            {
                _dropOffLocationLongitude = value;
                OnPropertyChanged();
            }
        }

        private double _dropOffLocationLatitude;

        public double DropOffLocationLatitude
        {
            get { return _dropOffLocationLatitude; }
            set
            {
                _dropOffLocationLatitude = value;
                OnPropertyChanged();
            }
        }


        private String _dropOffLocationId;

        public String DropOffLocationId
        {
            get { return _dropOffLocationId; }
            set
            {
                _dropOffLocationId = value;
                OnPropertyChanged();
            }
        }

        private String _dropOffLocation;

        public String DropOffLocation
        {
            get { return _dropOffLocation; }
            set
            {
                _dropOffLocation = value;
                OnPropertyChanged();
				OnDropOffLocationChanges ();
            }
        }

        #endregion

		private String _resID = String.Empty;
		public String ReservationID
		{
			get { return _resID; }
			set
			{
				_resID = value;
				OnPropertyChanged();
			}
		}

		private String _dropOffAirlinesId = String.Empty;
		public String DropOffAirlinesId
		{
			get { return _dropOffAirlinesId; }
			set
			{
				_dropOffAirlinesId = value;
				OnPropertyChanged();
			}
		}

		private String _dropOffAirlines;
        public String DropOffAirlines
        {
            get { return _dropOffAirlines; }
            set
            {
                _dropOffAirlines = value;
                OnPropertyChanged();
            }
        }

        private String _dropOffFlightNumber;

        public String DropOffFlightNumber
        {
            get { return _dropOffFlightNumber; }
            set
            {
                _dropOffFlightNumber = value;
                OnPropertyChanged();
            }
        }

        private String _dropOffFlightTime = null;
        public String DropOffFlightTime
        {
            get { return _dropOffFlightTime; }
            set
            {
                _dropOffFlightTime = value;
                OnPropertyChanged();
            }
        }

		private bool? _dropOffFlightTypeIsDomestic;
		public bool? DropOffFlightTypeIsDomestic
		{
			get { return _dropOffFlightTypeIsDomestic; }
			set
			{
				_dropOffFlightTypeIsDomestic = value;
				OnPropertyChanged();
			}
		}
        
        #endregion

		private bool _reservationType = false;//hourly charter
		public bool ReservationType
		{
			get { return _reservationType; }
			set
			{
				_reservationType = value;
				OnPropertyChanged();
			}
		}

        private String _numberOfPassangers = "1";
		public String NumberOfPassangers
        {
            get { return _numberOfPassangers; }
            set
            {
                _numberOfPassangers = value;
                OnPropertyChanged();
            }
        }

		private String _numberOfHours = "1";
		public String NumberOfHours
		{
			get { return _numberOfHours; }
			set
			{
				_numberOfHours = value;
				OnPropertyChanged();
			}
		}

		private String _extraBags = "0";
		public String ExtraBags
		{
			get { return _extraBags; }
			set
			{
				_extraBags = value;
				OnPropertyChanged();
			}
		}

		private bool _isFirstTime = true;
		public bool IsFirstTime
		{
			get { return _isFirstTime; }
			set
			{
				_isFirstTime = value;
				OnPropertyChanged();
			}
		}

		private bool _isMeetandGreet = true;
		public bool IsMeetandGreet
		{
			get { return _isMeetandGreet; }
			set
			{
				_isMeetandGreet = value;
				OnPropertyChanged();
			}
		}

		private double _meetandGreetFee;
		public double MeetandGreetFee
		{
			get { return _meetandGreetFee; }
			set
			{
				_meetandGreetFee = value;
				OnPropertyChanged();
			}
		}

		private double _extraBagsFee;
		public double ExtraBagsFee
		{
			get { return _extraBagsFee; }
			set
			{
				_extraBagsFee = value;
				OnPropertyChanged();
			}
		}

		private String _gratuity;
		public String Gratuity
		{
			get { return _gratuity; }
			set
			{
				_gratuity = value;
				OnPropertyChanged();
			}
		}

		private String _creditCardId;
		public String CreditCardId
		{
			get { return _creditCardId; }
			set
			{
				_creditCardId = value;
				OnPropertyChanged();
			}
		}

		private String _readyReservationId;
		public String ReadyReservationId
		{
			get { return _readyReservationId; }
			set
			{
				_readyReservationId = value;
				OnPropertyChanged();
			}
		}

        private String _creditCard;
        public String CreditCard
        {
            get { return _creditCard; }
            set
            {
                _creditCard = value;
                OnPropertyChanged();
            }
        }

        private String _promoCode;
        public String PromoCode
        {
            get { return _promoCode; }
            set
            {
                _promoCode = value;
                OnPropertyChanged();
				//OnDummyChanges();
            }
        }

		private FareContainer _selectedFare = null;
		public FareContainer SelectedFare
		{
			get { return _selectedFare; }
			set
			{
				_selectedFare = value;
				OnSelectedFareChanges ();
			}
		}

		private int _selectedFareType;
		public int SelectedFareType
		{
			get { return _selectedFareType; }
			set
			{
				_selectedFareType = value;
				OnSelectedFareChanges ();
			}
		}

		private double _surcharge = 0;
		public double Surcharge
		{
			get { return _surcharge; }
			set
			{
				_surcharge = value;
				OnSelectedFareChanges ();
			}
		}



        private List<ValidationFailure> _validaionError;
        public List<ValidationFailure> ValidaionError
        {
            get { return _validaionError; }
            set
            {
                if (_validaionError != value)
                {
                    _validaionError = value;
                    OnValidaionErrorChanges();
                }
            }
        }

        private List<Exception> _appExceptions;
        public List<Exception> AppExceptions
        {
            get { return _appExceptions; }
            set
            {
                _appExceptions = value;
                OnAppExceptionsChanges();
            }
        }

		private int _currentPage = 1;
		public int CurrentPage
		{
			get { return _currentPage; }
			set
			{
				_currentPage = value;
				OnPropertyChanged();
			}
		}

		private bool _canGoToThePage2 = false;
		public bool CanGoToThePage2
		{
			get { return _canGoToThePage2; }
			set
			{
				_canGoToThePage2 = value;
				OnCanGoToThePage2Changes();
			}
		}

		private bool _canGoToThePage3 = false;
		public bool CanGoToThePage3
		{
			get { return _canGoToThePage3; }
			set
			{
				_canGoToThePage3 = value;
				OnCanGoToThePage3Changes();
			}
		}

		private bool _canGoToThePage4 = false;
		public bool CanGoToThePage4
		{
			get { return _canGoToThePage4; }
			set
			{
				_canGoToThePage4 = value;
				OnCanGoToThePage4Changes();
			}
		}

		private bool _canGoToTheRideInformation = false;
		public bool CanGoToTheRideInformation
		{
			get { return _canGoToTheRideInformation; }
			set
			{
				_canGoToTheRideInformation = value;
				OnCanGoToTheRideInformationChanges();
			}
		}

		private bool _canGoToTheFlightInformation = false;
		public bool CanGoToTheFlightInformation
		{
			get { return _canGoToTheFlightInformation; }
			set
			{
				_canGoToTheFlightInformation = value;
				OnCanGoToTheFlightInformationChanges();
			}
		}

		private bool _canGoToThePaymentInformation = false;
		public bool CanGoToThePaymentInformation
		{
			get { return _canGoToThePaymentInformation; }
			set
			{
				_canGoToThePaymentInformation = value;
				OnCanGoToThePaymentInformationChanges();
			}
		}

		private bool _canGoToTheRideConfirmation = false;
		public bool CanGoToTheRideConfirmation
		{
			get { return _canGoToTheRideConfirmation; }
			set
			{
				_canGoToTheRideConfirmation = value;
				OnCanGoToTheRideConfirmationChanges();
			}
		}

		private RelayCommand _goToThePage2;
		public RelayCommand GoToThePage2
		{
			get {
				return _goToThePage2
				?? (_goToThePage2 = new RelayCommand (
					async () => {
						CurrentPage = 1;
						var results = Validate (this);
						if (results.IsValid) {
							ValidaionError = new List<ValidationFailure> ();
							CanGoToThePage2 = true;
						} else {
							ValidaionError = new List<ValidationFailure> (results.Errors);
							CanGoToThePage2 = false;
						}
					}));
			}
		}

		private RelayCommand _goToThePage3;
		public RelayCommand GoToThePage3
		{
			get {
				return _goToThePage3
					?? (_goToThePage3 = new RelayCommand (
						async () => {
							CurrentPage = 2;
							var results = Validate (this);
							if (results.IsValid) {
								ValidaionError = new List<ValidationFailure> ();
								CanGoToThePage3 = true;
							} else {
								ValidaionError = new List<ValidationFailure> (results.Errors);
								CanGoToThePage3 = false;
							}
						}));
			}
		}

		private RelayCommand _goToThePage4;
		public RelayCommand GoToThePage4
		{
			get {
				return _goToThePage4
					?? (_goToThePage4 = new RelayCommand (
						async () => {
							CurrentPage = 3;
							var results = Validate (this);
							if (results.IsValid) {
								ValidaionError = new List<ValidationFailure> ();
								CanGoToThePage4 = true;
							} else {
								ValidaionError = new List<ValidationFailure> (results.Errors);
								CanGoToThePage4 = false;
							}
						}));
			}
		}

		private RelayCommand _goToTheRideInformation;
		public RelayCommand GoToTheRideInformation
		{
			get {
				return _goToTheRideInformation
					?? (_goToTheRideInformation = new RelayCommand (
						async () => {
							CurrentPage = 4;
							var results = Validate (this);
							if (results.IsValid) {
								ValidaionError = new List<ValidationFailure> ();
								CanGoToTheRideInformation = true;
							} else {
								ValidaionError = new List<ValidationFailure> (results.Errors);
								CanGoToTheRideInformation = false;
							}
						}));
			}
		}

		private RelayCommand _goToTheFlightInformation;
		public RelayCommand GoToTheFlightInformation
		{
			get {
				return _goToTheFlightInformation
					?? (_goToTheFlightInformation = new RelayCommand (
						async () => {
							CurrentPage = 5;
							var results = Validate (this);
							if (results.IsValid) {
								ValidaionError = new List<ValidationFailure> ();
								CanGoToTheFlightInformation = true;
							} else {
								ValidaionError = new List<ValidationFailure> (results.Errors);
								CanGoToTheFlightInformation = false;
							}
						}));
			}
		}

		private RelayCommand _goToThePaymentInformation;
		public RelayCommand GoToThePaymentInformation
		{
			get {
				return _goToThePaymentInformation
					?? (_goToThePaymentInformation = new RelayCommand (
						async () => {
							CurrentPage = 6;
							var results = Validate (this);
							if (results.IsValid) {
								ValidaionError = new List<ValidationFailure> ();
								CanGoToThePaymentInformation = true;
							} else {
								ValidaionError = new List<ValidationFailure> (results.Errors);
								CanGoToThePaymentInformation = false;
							}
						}));
			}
		}

		private RelayCommand _goToTheRideConfirmation;
		public RelayCommand GoToTheRideConfirmation
		{
			get {
				return _goToTheRideConfirmation
					?? (_goToTheRideConfirmation = new RelayCommand (
						async () => {
							CurrentPage = 7;
							var results = Validate (this);
							if (results.IsValid) {
								ValidaionError = new List<ValidationFailure> ();
								CanGoToTheRideConfirmation = true;
							} else {
								ValidaionError = new List<ValidationFailure> (results.Errors);
								CanGoToTheRideConfirmation = false;
							}
						}));
			}
		}


        private RelayCommand _getFares;

        public RelayCommand GetFares
        {
            get
            {
                return _getFares
                       ?? (_getFares = new RelayCommand(
                           async () =>
                           {
                               var results = Validate(this);
                               if (results.IsValid)
                               {
                                   //var dic = new Dictionary<string, string>()
                                   //{
                                   //	{Constant.RESETPASSWORDAPIUserName, CurrentEmail},
                                   //	{Constant.RESETPASSWORDAPIOLDPASSWORD, OldPassword},
                                   //	{Constant.RESETPASSWORDAPINEWPASSWORD, NewPassword}
                                   //};
                                   try
                                   {
                                       //UserTrackingReporter.TrackUser(Constant.CATEGORY_RESET_PASSWORD, "Resetting password");
                                       //var data = await AppData.ApiCall(Constant.RESETPASSWORDAPI, dic);
                                       //var response = (ResetPasswordResponse) AppData.ParseResponse(Constant.RESETPASSWORDAPI, data);
                                       //if (response.Result.ToLower().Contains("success"))
                                       //{
                                       //	CanMoveForward = true;
                                       //}
                                       //else
                                       //{
                                       //	var validationFailure = new ValidationFailure("Registration API", response.Msg);
                                       //	var validationFailureList = new List<ValidationFailure> {validationFailure};
                                       //	ValidaionError = validationFailureList;
                                       //}
                                   }
                                   catch (Exception ex)
                                   {
                                       var newList = new List<Exception>();
                                       newList.Add(ex);
                                       AppExceptions = newList;

                                       CrashReporter.Report(ex);
                                   }
                               }
                               else
                               {
                                   ValidaionError = new List<ValidationFailure>(results.Errors);
                               }
                           }));
            }
        }

        private RelayCommand _scheduleARide;

        public RelayCommand ScheduleARide
        {
            get
            {
                return _scheduleARide
                       ?? (_scheduleARide = new RelayCommand(
                           async () =>
                           {
                               var results = Validate(this);
                               if (results.IsValid)
                               {
                                   //var dic = new Dictionary<string, string>()
                                   //{
                                   //	{Constant.RESETPASSWORDAPIUserName, CurrentEmail},
                                   //	{Constant.RESETPASSWORDAPIOLDPASSWORD, OldPassword},
                                   //	{Constant.RESETPASSWORDAPINEWPASSWORD, NewPassword}
                                   //};
                                   try
                                   {
                                       //UserTrackingReporter.TrackUser(Constant.CATEGORY_RESET_PASSWORD, "Resetting password");
                                       //var data = await AppData.ApiCall(Constant.RESETPASSWORDAPI, dic);
                                       //var response = (ResetPasswordResponse) AppData.ParseResponse(Constant.RESETPASSWORDAPI, data);
                                       //if (response.Result.ToLower().Contains("success"))
                                       //{
                                       //	CanMoveForward = true;
                                       //}
                                       //else
                                       //{
                                       //	var validationFailure = new ValidationFailure("Registration API", response.Msg);
                                       //	var validationFailureList = new List<ValidationFailure> {validationFailure};
                                       //	ValidaionError = validationFailureList;
                                       //}
                                   }
                                   catch (Exception ex)
                                   {
                                       var newList = new List<Exception>();
                                       newList.Add(ex);
                                       AppExceptions = newList;

                                       CrashReporter.Report(ex);
                                   }
                               }
                               else
                               {
                                   ValidaionError = new List<ValidationFailure>(results.Errors);
                               }
                           }));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
		//public event EventHandler PropertyChanged;

		public event EventHandler DummyChanges;

		public event EventHandler ViewModelReset;

        public event EventHandler ValidaionErrorChanges;
        public event EventHandler AppExceptionsChanges;
        
		public event EventHandler CanGoToThePage2Changes;
		public event EventHandler CanGoToThePage3Changes;
		public event EventHandler CanGoToThePage4Changes;
		public event EventHandler CanGoToTheRideInformationChanges;
		public event EventHandler CanGoToTheFlightInformationChanges;
		public event EventHandler CanGoToThePaymentInformationChanges;
		public event EventHandler CanGoToTheRideConfirmationChanges;

		public event EventHandler AirportChanges;

		public event EventHandler PickUpLocationChanges;
		public event EventHandler DropOffLocationChanges;

		public event EventHandler SelectedFareChanges;


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));                
            }
        }

		protected virtual void OnViewModelReset()
		{
			var handler = ViewModelReset;
			if (handler != null)
			{
				handler(this, new EventArgs());
			}
		}


		protected virtual void OnDummyChanges()
		{
			var handler = DummyChanges;
			if (handler != null)
			{
				handler(this, new EventArgs());
			}
		}

		protected virtual void OnCanGoToThePage2Changes()
		{
			var handler = CanGoToThePage2Changes;
			if (handler != null)
			{
				handler(this, new EventArgs());
			}
		}

		protected virtual void OnCanGoToThePage3Changes()
		{
			var handler = CanGoToThePage3Changes;
			if (handler != null)
			{
				handler(this, new EventArgs());
			}
		}

		protected virtual void OnCanGoToThePage4Changes()
		{
			var handler = CanGoToThePage4Changes;
			if (handler != null)
			{
				handler(this, new EventArgs());
			}
		}

		protected virtual void OnCanGoToTheRideInformationChanges()
		{
			var handler = CanGoToTheRideInformationChanges;
			if (handler != null)
			{
				handler(this, new EventArgs());
			}
		}

		protected virtual void OnCanGoToTheFlightInformationChanges()
		{
			var handler = CanGoToTheFlightInformationChanges;
			if (handler != null)
			{
				handler(this, new EventArgs());
			}
		}

		protected virtual void OnCanGoToThePaymentInformationChanges()
		{
			var handler = CanGoToThePaymentInformationChanges;
			if (handler != null)
			{
				handler(this, new EventArgs());
			}
		}

		protected virtual void OnCanGoToTheRideConfirmationChanges()
		{
			var handler = CanGoToTheRideConfirmationChanges;
			if (handler != null)
			{
				handler(this, new EventArgs());
			}
		}

		protected virtual void OnPickUpLocationChanges()
		{
			var handler = PickUpLocationChanges;
			if (handler != null)
			{
				handler(this, new EventArgs());
			}
		}
		protected virtual void OnDropOffLocationChanges()
		{
			var handler = DropOffLocationChanges;
			if (handler != null)
			{
				handler(this, new EventArgs());
			}
		}

		protected virtual void OnPickUpAirportChanges()
		{
			var handler = AirportChanges;
			if (handler != null)
			{
				handler(this, new EventArgs());
			}
		}
		protected virtual void OnDropOffAirportChanges()
		{
			var handler = AirportChanges;
			if (handler != null)
			{
				handler(this, new EventArgs());
			}
		}

		protected virtual void OnSelectedFareChanges()
		{
			var handler = SelectedFareChanges;
			if (handler != null)
			{
				handler(this, new EventArgs());
			}
		}

        protected virtual void OnValidaionErrorChanges()
        {
            var handler = ValidaionErrorChanges;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        protected virtual void OnAppExceptionsChanges()
        {
            var handler = AppExceptionsChanges;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }
    }

	public class PromoCodeValidator : PropertyValidator {

		public PromoCodeValidator() : base("Invalid Promo Code") {

		}

		protected override bool IsValid(PropertyValidatorContext context) {
			String pcode = context.PropertyValue.ToString ();

			var dic = new Dictionary<String, String> {
				{ Constant.VALIDATEDISCOUNTCOUPON_CODE, pcode },
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

				Task runSync = Task.Factory.StartNew (async () => {
					result = await AppData.ApiCall (Constant.VALIDATEDISCOUNTCOUPON, dic);
					tt = (ValidateDiscountCouponResponse)AppData.ParseResponse (Constant.VALIDATEDISCOUNTCOUPON, result);
				}).Unwrap ();
				runSync.Wait ();

			} catch (Exception ex) {
				CrashReporter.Report (ex);
				return false;
			}
			if (tt == null || String.IsNullOrEmpty (tt.Result) || tt.Result.ToLower ().Contains ("failed")) {
				UserTrackingReporter.TrackUser (Constant.CATEGORY_PROMO_CODE, "Invalid promo code entered");
				return false;
			}
			return true;
		}
	}
}