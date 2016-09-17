using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FluentValidation;
using FluentValidation.Results;
using GalaSoft.MvvmLight.Command;
using RoadRunner.Shared.Classes;

namespace RoadRunner.Shared.ViewModels
{
    public class ResetPasswordVM : AbstractValidator<ResetPasswordVM>, INotifyPropertyChanged
    {
        private string _CurrentEmail;
        private string _OldPassword;
        private string _NewPassword;
        private string _ConfirmNewPassword;
        private bool _IsEmailLogin;
        private bool _canMoveForward;

        private RelayCommand _SubmitCommand;

        private List<ValidationFailure> _validaionError;

        public ResetPasswordVM()
        {
            RuleFor(r => r.IsEmailLogin).Equal(k => true).WithMessage("You can change password only if you logged in using Roadrunner's email account");

			When (x => x.IsEmailLogin == true, () => {
				RuleFor (r => r.CurrentEmail).NotEmpty ().WithMessage ("Current Email can't be empty");
				RuleFor (r => r.OldPassword).NotEmpty ().WithMessage ("Please specify a Old Password");
				RuleFor (r => r.NewPassword).NotEmpty ().WithMessage ("Please specify a New Password");
				RuleFor (r => r.ConfirmNewPassword).Equal (k => k.NewPassword).WithMessage ("New Password and Confirm New Password don't match");
			});
        }

        public string CurrentEmail
        {
            get { return _CurrentEmail; }
            set
            {
                if (_CurrentEmail != value)
                {
                    _CurrentEmail = value;
                    OnPropertyChanged();
                }
            }
        }

        public string OldPassword
        {
            get { return _OldPassword; }
            set
            {
                if (_OldPassword != value)
                {
                    _OldPassword = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NewPassword
        {
            get { return _NewPassword; }
            set
            {
                if (_NewPassword != value)
                {
                    _NewPassword = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ConfirmNewPassword
        {
            get { return _ConfirmNewPassword; }
            set
            {
                if (_ConfirmNewPassword != value)
                {
                    _ConfirmNewPassword = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsEmailLogin
        {
            get { return _IsEmailLogin; }
            set
            {
                if (_IsEmailLogin != value)
                {
                    _IsEmailLogin = value;
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

        public RelayCommand SubmitCommand
        {
            get
            {
                return _SubmitCommand
                       ?? (_SubmitCommand = new RelayCommand(
                           async () =>
                           {
                               var results = Validate(this);
                               if (results.IsValid)
                               {
                                   var dic = new Dictionary<string, string>()
                                   {
                                       {Constant.RESETPASSWORDAPIUserName, CurrentEmail},
                                       {Constant.RESETPASSWORDAPIOLDPASSWORD, OldPassword},
                                       {Constant.RESETPASSWORDAPINEWPASSWORD, NewPassword}
                                   };
                                   try
                                   {
                                       UserTrackingReporter.TrackUser(Constant.CATEGORY_RESET_PASSWORD, "Resetting password");
                                       var data = await AppData.ApiCall(Constant.RESETPASSWORDAPI, dic);
                                       var response = (ResetPasswordResponse) AppData.ParseResponse(Constant.RESETPASSWORDAPI, data);
                                       if (response.Result.ToLower().Contains("success"))
                                       {
                                           CanMoveForward = true;
                                       }
                                       else
                                       {
                                           var validationFailure = new ValidationFailure("Registration API", response.Msg);
                                           var validationFailureList = new List<ValidationFailure> {validationFailure};
                                           ValidaionError = validationFailureList;
                                       }
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

        public event EventHandler ButtonChanges;
        public event EventHandler ValidaionErrorChanges;
        public event EventHandler AppExceptionsChanges;
        public event EventHandler CanMoveForwardChanges;


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

