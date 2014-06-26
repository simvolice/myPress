using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MyPress.Client.Model;
using MyPress.Client.Resources;
using MyPress.Client.ServiceMyPress;
using System.Reactive.Linq;
using System.Reactive.Concurrency;

namespace MyPress.Client.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class RegViewModel : ViewModelBase, INotifyDataErrorInfo
    {





        private IDisposable disposableReg;
        ServiceMyPress.MyPressServiceClient myPressService = new MyPressServiceClient();
        ServiceMyPress.Data serviceData = new Data();



        private readonly IDataService _dataService;





        /// <summary>
        /// The <see cref="User" /> property's name.
        /// </summary>
        public const string UserPropertyName = "User";

        private string _myUser = string.Empty;

        /// <summary>
        /// Sets and gets the User property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>



        [Required(ErrorMessageResourceName = "ValidationErrorRequiredField", ErrorMessageResourceType = typeof(Resource1))]
        [Display(Order = 0, Name = "UserNameLabel", ResourceType = typeof(Resource1))]
        [RegularExpression("^[a-zA-Z0-9_]*$", ErrorMessageResourceName = "ValidationErrorInvalidUserName", ErrorMessageResourceType = typeof(Resource1))]
        [StringLength(255, MinimumLength = 4, ErrorMessageResourceName = "ValidationErrorBadUserNameLength", ErrorMessageResourceType = typeof(Resource1))]
        public string User
        {
            get
            {
                return _myUser;
            }

            set
            {
                if (_myUser == value)
                {
                    return;
                }

                RaisePropertyChanging(UserPropertyName);
                _myUser = value;
                ValidateProperty(value);
                RaisePropertyChanged(UserPropertyName);




            }
        }


        /// <summary>
        /// The <see cref="Password" /> property's name.
        /// </summary>
        public const string PasswordPropertyName = "Password";

        private string _myPassword = string.Empty;

        /// <summary>
        /// Sets and gets the Password property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        [Required(ErrorMessageResourceName = "ValidationErrorRequiredField", ErrorMessageResourceType = typeof(Resource1))]
        [Display(Order = 2, Name = "PasswordLabel", Description = "PasswordDescription", ResourceType = typeof(Resource1))]
        [RegularExpression("^.*[^a-zA-Z0-9].*$", ErrorMessageResourceName = "ValidationErrorBadPasswordStrength", ErrorMessageResourceType = typeof(Resource1))]
        [StringLength(50, MinimumLength = 7, ErrorMessageResourceName = "ValidationErrorBadPasswordLength", ErrorMessageResourceType = typeof(Resource1))]
        public string Password
        {
            get
            {
                return _myPassword;
            }

            set
            {
                if (_myPassword == value)
                {
                    return;
                }


                _myPassword = value;
                ValidateProperty(value);

                RaisePropertyChanged(PasswordPropertyName);



            }
        }


        /// <summary>
        /// The <see cref="Email" /> property's name.
        /// </summary>
        public const string EmailPropertyName = "Email";

        private string _myEmail = string.Empty;

        /// <summary>
        /// Sets and gets the Email property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>




        [Required(ErrorMessageResourceName = "ValidationErrorRequiredField", ErrorMessageResourceType = typeof(Resource1))]
        [Display(Order = 1, Name = "EmailLabel", ResourceType = typeof(Resource1))]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                           ErrorMessageResourceName = "ValidationErrorInvalidEmail", ErrorMessageResourceType = typeof(Resource1))]
        public string Email
        {
            get
            {
                return _myEmail;
            }

            set
            {
                if (_myEmail == value)
                {
                    return;
                }

                RaisePropertyChanging(EmailPropertyName);
                _myEmail = value;
                ValidateProperty(value);
                RaisePropertyChanged(EmailPropertyName);




            }
        }



        /// <summary>
        /// The <see cref="RepPassword" /> property's name.
        /// </summary>
        public const string RepPasswordPropertyName = "RepPassword";

        private string _myRepPassword = string.Empty;

        /// <summary>
        /// Sets and gets the RepPassword property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        [Required(ErrorMessageResourceName = "ValidationErrorRequiredField", ErrorMessageResourceType = typeof(Resource1))]
        [Display(Order = 3, Name = "PasswordConfirmationLabel", ResourceType = typeof(Resource1))]
        public string RepPassword
        {
            get
            {
                return _myRepPassword;
            }

            set
            {
                if (_myRepPassword == value)
                {

                    return;
                }

                RaisePropertyChanging(RepPasswordPropertyName);
                _myRepPassword = value;
                ValidateProperty(value);
                CheckPasswordConfirmation();
                RaisePropertyChanged(RepPasswordPropertyName);






            }
        }





        private void CheckPasswordConfirmation()
        {

            if (string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(RepPassword))
            {

                return;

            }

            // Если значения различаются, добавляется ошибка проверки, связанная с обоими указанными элементами.
            if (this.Password != this.RepPassword)
            {

                ValidateCustomError("RepPassword", Resource1.ValidationErrorPasswordConfirmationMismatch);

            }
        }



        private RelayCommand _okButtonCommand;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand OkCommand
        {
            get
            {
                return _okButtonCommand ?? (_okButtonCommand = new RelayCommand(
                    ExecuteMyCommand,
                    CanExecuteMyCommand));
            }
        }



       




        private void ExecuteMyCommand()
        {


           

            if (string.IsNullOrWhiteSpace(User))
            {


                ValidateCustomError("User", Resource1.ValidationErrorRequiredField);



            }


            if (!Regex.IsMatch(User, "^[a-zA-Z0-9_]*$"))
            {

                ValidateCustomError("User", Resource1.ValidationErrorInvalidUserName);
            }



            if ((User.Length >= 1 && User.Length < 4) || User.Length > 255)
            {

                ValidateCustomError("User", Resource1.ValidationErrorBadUserNameLength);
            }



            if (string.IsNullOrWhiteSpace(Email))
            {

                ValidateCustomError("Email", Resource1.ValidationErrorRequiredField);


            }


            if (!Regex.IsMatch(Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
            {

                ValidateCustomError("Email", Resource1.ValidationErrorInvalidEmail);


            }


            if (string.IsNullOrWhiteSpace(Password))
            {

                ValidateCustomError("Password", Resource1.ValidationErrorRequiredField);

            }


            if (!Regex.IsMatch(Password, "^.*[^a-zA-Z0-9].*$"))
            {

                ValidateCustomError("Password", Resource1.ValidationErrorBadPasswordStrength);
            }

            if ((Password.Length >= 1 && Password.Length < 7) || Password.Length > 50)
            {

                ValidateCustomError("Password", Resource1.ValidationErrorBadPasswordLength);
            }

            if (string.IsNullOrWhiteSpace(RepPassword))
            {



                ValidateCustomError("RepPassword", Resource1.ValidationErrorRequiredField);

            }

            if (Password != RepPassword)
            {



                ValidateCustomError("RepPassword", Resource1.ValidationErrorPasswordConfirmationMismatch);

            }



            if (!HasErrors)
            {


                serviceData.Login = User;
                serviceData.Email = Email;
                serviceData.Pass = RepPassword;


                var func = Observable.FromEventPattern<CheckUserCompletedEventArgs>(myPressService, "CheckUserCompleted")
                    .ObserveOnDispatcher();
                myPressService.CheckUserAsync(serviceData);
              disposableReg =  func.ObserveOnDispatcher().Select(x => x.EventArgs.Result).Subscribe(c => CheckEr(c));

            }
        }






        private void CheckEr(ErrorList c)
        {

            disposableReg.Dispose();

            if (c.Equals(ErrorList.DublicateName))
            {

                ValidateCustomError("User", Resource1.DublicateName);

            }


            if (c.Equals(ErrorList.DublicateEmail))
            {


                ValidateCustomError("Email", Resource1.DublicateEmail);

            }

            if (c.Equals(ErrorList.Succes))
            {


                serviceData.Login = User;
                serviceData.Email = Email;
                serviceData.Pass = RepPassword;


               myPressService.AddUserAsync(serviceData);




               Messenger.Default.Send<string>("true");



            }

        }

        private bool CanExecuteMyCommand()
        {
            
            return true;
        }

      







        public RegViewModel(IDataService dataService)
        {

            _dataService = dataService;
            _dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {


                        throw new Exception(error.Message);


                    }



                });





        }


        private Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private object threadLock = new object();

        public bool IsValid
        {
            get { return !this.HasErrors; }

        }

        public void OnErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                if (errors.ContainsKey(propertyName) && (errors[propertyName] != null) && errors[propertyName].Count > 0)
                    return errors[propertyName].ToList();
                else
                    return null;
            }
            else
                return errors.SelectMany(err => err.Value.ToList());
        }

        public bool HasErrors
        {
            get { return errors.Any(propErrors => propErrors.Value != null && propErrors.Value.Count > 0); }
        }

        public void ValidateProperty(object value, [CallerMemberName] string propertyName = null)
        {
            lock (threadLock)
            {
                var validationContext = new ValidationContext(this, null, null);
                validationContext.MemberName = propertyName;
                var validationResults = new List<ValidationResult>();
                Validator.TryValidateProperty(value, validationContext, validationResults);

                //clear previous errors from tested property
                if (errors.ContainsKey(propertyName))
                    errors.Remove(propertyName);
                OnErrorsChanged(propertyName);

                HandleValidationResults(validationResults);
            }
        }



        public void ValidateCustomError(string property, string resourceString)
        {

            List<ValidationResult> validationResults = new List<ValidationResult>();

            validationResults.Add(new ValidationResult(resourceString,
                 new string[] { property }));



            //clear previous errors from tested property
            if (errors.ContainsKey(property))
                errors.Remove(property);
            OnErrorsChanged(property);

            HandleValidationResults(validationResults);


        }




        public void Validate()
        {
            lock (threadLock)
            {
                var validationContext = new ValidationContext(this, null, null);
                var validationResults = new List<ValidationResult>();
                Validator.TryValidateObject(this, validationContext, validationResults, true);

                //clear all previous errors
                var propNames = errors.Keys.ToList();
                errors.Clear();
                propNames.ForEach(pn => OnErrorsChanged(pn));

                HandleValidationResults(validationResults);
            }
        }

        private void HandleValidationResults(List<ValidationResult> validationResults)
        {
            //Group validation results by property names
            var resultsByPropNames = from res in validationResults
                                     from mname in res.MemberNames
                                     group res by mname into g
                                     select g;

            //add errors to dictionary and inform  binding engine about errors
            foreach (var prop in resultsByPropNames)
            {
                var messages = prop.Select(r => r.ErrorMessage).ToList();
                errors.Add(prop.Key, messages);
                OnErrorsChanged(prop.Key);
            }
        }














    }
}