using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using GalaSoft.MvvmLight.Command;
using RoadRunner.Shared.Classes;
//using RoadRunnerNew.iOS.Core;

namespace RoadRunner.Shared.ViewModels
{
    public class RegisterVM : AbstractValidator<RegisterVM>, INotifyPropertyChanged
    {
        private bool _canMoveForward;
        private string _emailAdress;
        private string _firstName;
        private RelayCommand _incrementCommand;
        private bool _isAgree;
        private string _lastName;
        private string _mobileNumber;
        private string _password;
        private string _repeatPassword;
		private string _loginType;
		private string _token;
        private List<ValidationFailure> _validaionError;

        public RegisterVM()
        {
            RuleFor(r => r.FirstName).NotEmpty().WithMessage("Please specify a First name");
            RuleFor(r => r.LastName).NotEmpty().WithMessage("Please specify a Last name");

            RuleFor(r => r.MobileNumber).Must(BeAValidPhone).WithMessage("Please specify a valid phone number");
            RuleFor(r => r.EmailAdress).EmailAddress().WithMessage("Please specify a valid email address");
            RuleFor(r => r.Password).NotEmpty().WithMessage("Please specify a password");
            RuleFor(r => r.RepeatPassword).Equal(k => k.Password).WithMessage("Please make sure password matches");

            RuleFor(r => r.IsAgree).Equal(k => true).WithMessage("You should agree to the terms of service");
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string MobileNumber
        {
            get { return _mobileNumber; }
            set
            {
                if (_mobileNumber != value)
                {
                    _mobileNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public string EmailAdress
        {
            get { return _emailAdress; }
            set
            {
                if (_emailAdress != value)
                {
                    _emailAdress = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        public string RepeatPassword
        {
            get { return _repeatPassword; }
            set
            {
                if (_repeatPassword != value)
                {
                    _repeatPassword = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsAgree
        {
            get { return _isAgree; }
            set
            {
                if (_isAgree != value)
                {
                    _isAgree = value;
                    OnPropertyChanged();
                }
            }
        }

		public string LoginType
		{
			get { return _loginType; }
			set
			{
				if (_loginType != value)
				{
					_loginType = value;
					OnPropertyChanged();
				}
			}
		}

		public string Token
		{
			get { return _token; }
			set
			{
				if (_token != value)
				{
					_token = value;
					OnPropertyChanged();
				}
			}
		}

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

        

        public bool CanMoveForward
        {
            get { return _canMoveForward; }
            set
            {
                _canMoveForward = value;
                OnCanMoveForwardChanges();
            }
        }
			
        public RelayCommand IncrementCommand
        {
            get
            {
                return _incrementCommand
                       ?? (_incrementCommand = new RelayCommand(
                           async () =>
                           {
                               var results = Validate(this);
                               if (results.IsValid)
                               {
								var dic = new Dictionary<String, String>
								{
									{Constant.REGESTRATION_FOR_ANDROID_API_EmailAddress, EmailAdress},
									{Constant.REGESTRATION_FOR_ANDROID_API_FNAME, FirstName},
									{Constant.REGESTRATION_FOR_ANDROID_API_LNAME, LastName},
									{Constant.REGESTRATION_FOR_ANDROID_API_HOMEPHONE, ""},
									{Constant.REGESTRATION_FOR_ANDROID_API_CELLPHONE, MobileNumber},
									{Constant.REGESTRATION_FOR_ANDROID_API_NUMBER, ""},
									{Constant.REGESTRATION_FOR_ANDROID_API_STREET, ""},
									{Constant.REGESTRATION_FOR_ANDROID_API_UNIT, ""},
									{Constant.REGESTRATION_FOR_ANDROID_API_COMPLEX, ""},
									{Constant.REGESTRATION_FOR_ANDROID_API_CITY, ""},
									{Constant.REGESTRATION_FOR_ANDROID_API_ZIP, ""},
									{Constant.REGESTRATION_FOR_ANDROID_API_PASSWORD, Password},
									{Constant.REGESTRATION_FOR_ANDROID_API_DIRECTION, ""},
									{Constant.REGESTRATION_FOR_ANDROID_API_CUSTID, "0"},
									{Constant.REGESTRATION_FOR_ANDROID_API_CUSTTYPE, "0"},
									{Constant.REGESTRATION_FOR_ANDROID_API_USERNAME, EmailAdress},
									{Constant.REGESTRATION_FOR_ANDROID_API_TYPE, LoginType},
									{Constant.REGESTRATION_FOR_ANDROID_API_TOKEN, Token},
									{Constant.REGESTRATION_FOR_ANDROID_API_ISSMS, IsAgree.ToString()}
								};

								try {
									var data = await AppData.ApiCall (Constant.REGESTRATION_FOR_ANDROID_API, dic);
									var registrationInfo = (RegistrationResponseForAndroid)AppData.ParseResponse (Constant.REGESTRATION_FOR_ANDROID_API, data);
									if (!registrationInfo.Result.ToLower ().Contains ("failed")) {
										//AppSettings.UserID = registrationInfo.CustomerId;
										CanMoveForward = true;
									}else
									{
										var validationFailure = new ValidationFailure("Registration API", registrationInfo.Msg);
										var validationFailureList = new List<ValidationFailure> {validationFailure};
										ValidaionError = validationFailureList;
									}
								}  catch (Exception ex) {
									var newList = new List<Exception>();
									newList.Add(ex);
									AppExceptions = newList;

									CrashReporter.Report(ex);
								}

//                                   var dic = new Dictionary<string, string>
//                                   {
//                                       {Constant.REGESTRATIONAPI_EmailAddress, EmailAdress},
//                                       {Constant.REGESTRATIONAPI_FNAME, FirstName},
//                                       {Constant.REGESTRATIONAPI_LNAME, LastName},
//                                       {Constant.REGESTRATIONAPI_HOMEPHONE, ""},
//                                       {Constant.REGESTRATIONAPI_CELLPHONE, MobileNumber},
//                                       {Constant.REGESTRATIONAPI_NUMBER, ""},
//                                       {Constant.REGESTRATIONAPI_STREET, ""},
//                                       {Constant.REGESTRATIONAPI_UNIT, ""},
//                                       {Constant.REGESTRATIONAPI_COMPLEX, ""},
//                                       {Constant.REGESTRATIONAPI_CITY, ""},
//                                       {Constant.REGESTRATIONAPI_ZIP, ""},
//                                       {Constant.REGESTRATIONAPI_PASSWORD, Password},
//                                       {Constant.REGESTRATIONAPI_DIRECTION, ""},
//                                       {Constant.REGESTRATIONAPI_CUSTID, "0"},
//                                       {Constant.REGESTRATIONAPI_CUSTTYPE, "0"} //0 means Email registration
//                                   };

//                                   try
//                                   {
//                                       var data = await AppData.ApiCall(Constant.REGESTRATIONAPI, dic);
//                                       var registrationInfo = (RegistrationResponse) AppData.ParseResponse(Constant.REGESTRATIONAPI, data);
//                                       if (!registrationInfo.Result.ToLower().Contains("failed"))
//                                       {
//											//AppSettings.UserID = registrationInfo.CustomerId;
//                                           	CanMoveForward = true;
//                                       }
//                                       else
//                                       {
//                                           var validationFailure = new ValidationFailure("Registration API", registrationInfo.Msg);
//                                           var validationFailureList = new List<ValidationFailure> {validationFailure};
//                                           ValidaionError = validationFailureList;
//                                       }
//                                   }
//                                   catch (Exception ex)
//                                   {
//                                       var newList = new List<Exception>();
//                                       newList.Add(ex);
//                                       AppExceptions = newList;
//
//                                       CrashReporter.Report(ex);
//                                   }
                               }
                               else
                               {
                                   ValidaionError = new List<ValidationFailure>(results.Errors);
                               }
                           }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool BeAValidPhone(string phonestring)
        {
            var pattern = @"1?\s*\W?\s*([2-9][0-8][0-9])\s*\W?\s*([2-9][0-9]{2})\s*\W?\s*([0-9]{4})(\se?x?t?(\d*))?";

            return Regex.IsMatch(phonestring, pattern, RegexOptions.IgnoreCase);
        }

        public event EventHandler ButtonChanges;
        public event EventHandler ValidaionErrorChanges;
        public event EventHandler AppExceptionsChanges;
        public event EventHandler CanMoveForwardChanges;

        //[NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void OnButtonChanges()
        {
            var handler = ButtonChanges;
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

        protected virtual void OnCanMoveForwardChanges()
        {
            var handler = CanMoveForwardChanges;
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
}