using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using Microsoft.Internal.Pivot.Interactivity;
using MyPress2.Model;
using MyPress2.Resources;

namespace MyPress2.ViewModel
{
    
    public class LoginViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        
           private readonly IDataService _dataService;


      
        public const string PassProp = "Password";

        private string _password = string.Empty;


        [Display(Name = "PasswordLabel", ResourceType = typeof(Resource1))]
        [Required]
        public string Password
        {
            get { return _password; }

            set
            {
                if (_password != value)
                {





                    ValidateObject();
                    _password = value;
                    RaisePropertyChanged(PassProp);
                    

                }


            }
        }




      
        public const string UserProp = "User";

        private string _user = string.Empty;





        [Display(Name = "UserNameLabel", ResourceType = typeof(Resource1))]
        [Required]
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


        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public void NotifyErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }



        public readonly IDictionary<string, IList<string>> _errors = new Dictionary<string, IList<string>>();

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












        /// <summary>
        /// Для проверки на ввод пользователем
        /// </summary>
        /// <param name="propertyName">Имя проверяемого свойства</param>
        /// <param name="value">Значение из свойства</param>
        public void ValidateProperty(string propertyName, object value)
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

        
        
        
        public LoginViewModel(IDataService dataService)
        {


            _dataService = dataService;
            _dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {


                        throw new Exception(error.Message);


                    }


                    User = item.User;
                    Password = item.Pass;





                });
        
        
        
        
        
        
        }
    }
}