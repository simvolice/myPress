using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyPress.Client.Model;
using MyPress.Client.Resources;
using MyPress.Client.ServiceMyPress;
using GalaSoft.MvvmLight.Messaging;


namespace MyPress.Client.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class RestorePassViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly IDataService _dataService;
        ServiceMyPress.MyPressServiceClient myServiceClient = new MyPressServiceClient();
        ServiceMyPress.Data myData = new Data();
        private IDisposable disposableRestore;



       


        /// <summary>
        /// The <see cref="Email" /> property's name.
        /// </summary>
        public const string EmailPropertyName = "Email";

        private string _email = string.Empty;

        /// <summary>
        /// Sets and gets the Email property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>


       
        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                if (_email == value)
                {
                    return;
                }

                RaisePropertyChanging(EmailPropertyName);
                _email = value;
                ValidateProperty(value);
                RaisePropertyChanged(EmailPropertyName);






            }
        }



        private RelayCommand checkEmailCommand;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand CheckCommand
        {
            get
            {
                return checkEmailCommand ?? (checkEmailCommand = new RelayCommand(
                    ExecuteMyCommand,
                    CanExecuteMyCommand));
            }
        }

        private void ExecuteMyCommand()
        {

            if (string.IsNullOrWhiteSpace(Email))
            {
            
            ValidateCustomError("Email", Resource1.ValidationErrorRequiredField);
            
            
            }


            if (!HasErrors)
            {


                myData.Email = Email;

                var func = Observable.FromEventPattern<RestorePassCompletedEventArgs>(myServiceClient, "RestorePassCompleted")
                  .ObserveOnDispatcher();
                myServiceClient.RestorePassAsync(myData);
                disposableRestore = func.ObserveOnDispatcher().Select(x => x.EventArgs.Result).Subscribe(c => CheckEr(c));






            }



        }

        private void CheckEr(ErrorList c)
        {
            
            disposableRestore.Dispose();

            if (c.Equals(ErrorList.EmailNull))
            {
            
            ValidateCustomError("Email",Resource1.EmailNull);
            
            
            
            }

            if (c.Equals(ErrorList.Succes))
            {


                Messenger.Default.Send<bool>(true);

            }





        }

        private bool CanExecuteMyCommand()
        {
            return true;
        }










        public RestorePassViewModel(IDataService dataService)
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