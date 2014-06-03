using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using GalaSoft.MvvmLight;

using MyPress2.Model;
using MyPress2.Resources;
using MyPress2.ServiceReference1;
using System.ComponentModel.DataAnnotations;

namespace MyPress2.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase, INotifyDataErrorInfo


    {

       
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        protected void NotifyErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }



        private readonly IDictionary<string, IList<string>> _errors = new Dictionary<string, IList<string>>();

        public IEnumerable GetErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                IList<string> propertyErrors = _errors[propertyName];
                foreach (string propertyError in propertyErrors)
                {
                    yield return propertyError;
                }
            }
            yield break;
        }

        public bool HasErrors
        {
            get { return _errors.Count > 0; }
        }




       

        private readonly IDataService _dataService;

        /// <summary>
        /// The <see cref="User" /> property's name.
        /// </summary>
        public const string UserProp = "User";

        private string _user = string.Empty;

        [Required(ErrorMessageResourceName = "ValidationErrorRequiredField", ErrorMessageResourceType = typeof(ValidationErrorResources))]
        [Display(Order = 0, Name = "login", ResourceType = typeof(RegistrationDataResources))]
        [RegularExpression("^[a-zA-Z0-9_]*$", ErrorMessageResourceName = "ValidationErrorInvalidUserName", ErrorMessageResourceType = typeof(ValidationErrorResources))]
        [StringLength(255, MinimumLength = 4, ErrorMessageResourceName = "ValidationErrorBadUserNameLength", ErrorMessageResourceType = typeof(ValidationErrorResources))]
        public string User
        {
            get
            {
                return _user;
            }

            set
            {
                if (_user != value)
                {





                    ValidateObject();
                _user = value;
                RaisePropertyChanged(UserProp);
            
            
            }
            
            
            }
        }



        public bool ValidateObject()
        {
            ViewModelBase objectToValidate = this;
            _errors.Clear();
            Type objectType = objectToValidate.GetType();
            PropertyInfo[] properties = objectType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.GetCustomAttributes(typeof(ValidationAttribute), true).Any())
                {
                    object value = property.GetValue(objectToValidate, null);
                    ValidateProperty(property.Name, value);
                }
            }

            return !HasErrors;
        }












        protected void ValidateProperty(string propertyName, object value)
        {
            ViewModelBase objectToValidate = this;



           

            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateProperty(
                value,
                new ValidationContext(objectToValidate, null, null)
                {
                    MemberName = propertyName
                },
                results);

            if (isValid)
                RemoveErrorsForProperty(propertyName);
            else
                AddErrorsForProperty(propertyName, results);

            NotifyErrorsChanged(propertyName);
        }

        private void AddErrorsForProperty(string propertyName, IEnumerable<ValidationResult> validationResults)
        {
            RemoveErrorsForProperty(propertyName);
            _errors.Add(propertyName, validationResults.Select(vr => vr.ErrorMessage).ToList());
        }

        private void RemoveErrorsForProperty(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
                _errors.Remove(propertyName);
        }



      







        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }

                   
                    
                    
                    
                    //Присвоить значения свойству ВидМодели из Модели
                    
                    
                    
                    User = item.User;
              
           
                
                
                
                
                
                
                
                
                });
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}