using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using Microsoft.Internal.Pivot.Interactivity;
using MyPress2.Model;
using MyPress2.Resources;
using System.Runtime.CompilerServices;
using MyPress2.View;
using Syncfusion.Windows.Controls.Primitives;


namespace MyPress2.ViewModel
{
    
    public class LoginViewModel : ViewModelBase, INotifyDataErrorInfo
    {




        private RelayCommand enterCommand;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand EnteRelayCommand
        {
            get
            {
                return enterCommand ?? (enterCommand = new RelayCommand(
                    ExecuteEnter,
                    CanExecuteEnter));
            }
        }

        private void ExecuteEnter()
        {


       







        }

        private bool CanExecuteEnter()
        {
            return true;
        }


        private RelayCommand restorePassCommand;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand RestorePasswCommand
        {
            get
            {
                return restorePassCommand ?? (restorePassCommand = new RelayCommand(
                    ShowRestoreWin,
                    CanShowRestoreWin));
            }
        }

        private void ShowRestoreWin()
        {
            RestorePassword restorePassword = new RestorePassword();
            restorePassword.Show();
        }

        private bool CanShowRestoreWin()
        {
            return true;
        }



           private readonly IDataService _dataService;

           private RelayCommand _myCommand;



           /// <summary>
           /// The <see cref="EnableButtonEnter" /> property's name.
           /// </summary>
           public const string EnableButtonEnterPropertyName = "EnableButtonEnter";

           private bool _enableButtonEnter = false;

           /// <summary>
           /// Sets and gets the EnableButtonEnter property.
           /// Changes to that property's value raise the PropertyChanged event. 
           /// </summary>
           public bool EnableButtonEnter
           {
               get
               {
                   return _enableButtonEnter;
               }

               set
               {
                   if (_enableButtonEnter == value)
                   {
                       return;
                   }

                   RaisePropertyChanging(EnableButtonEnterPropertyName);
                   _enableButtonEnter = value;
                   RaisePropertyChanged(EnableButtonEnterPropertyName);
               }
           }


           /// <summary>
           /// Gets the MyCommand.
           /// </summary>
           public RelayCommand MyCommand
           {
               get
               {
                   return _myCommand ?? (_myCommand = new RelayCommand(
                       ExecuteMyCommand,
                       CanExecuteMyCommand));
               }
           }

           private void ExecuteMyCommand()
           {

              



           }

           private bool CanExecuteMyCommand()
           {
               return true;
           }
      





        public const string PassProp = "Password";

        private string _password = string.Empty;


        [Display(Name = "PasswordLabel", ResourceType = typeof(Resource1))]
        [Required(ErrorMessageResourceName = "ValidationErrorRequiredField", ErrorMessageResourceType = typeof(Resource1))]
        public string Password
        {
            get { return _password; }

            set
            {
                if (_password == value)
                {
                    return;


                }

                   
                    _password = value;
                ValidateProperty(value);
                    RaisePropertyChanged(PassProp);


                    if (string.IsNullOrWhiteSpace(_password) || string.IsNullOrWhiteSpace(_user))
                        EnableButtonEnter = false;
                    else

                        EnableButtonEnter = true;



            }
        }




      
        public const string UserProp = "User";

        private string _user = string.Empty;





      [Display(Name = "UserNameLabel", ResourceType = typeof(Resource1))]
      [Required(ErrorMessageResourceName = "ValidationErrorRequiredField", ErrorMessageResourceType = typeof(Resource1))]
      public string User
        {
            get
            {
                return _user;
            }

            set
            {
                if (_user == value)
                {


                   


                    return;

                }
               
                
                 
               
                _user = value;
               ValidateProperty(value);
                RaisePropertyChanged(UserProp);


                if (string.IsNullOrWhiteSpace(_user) || string.IsNullOrWhiteSpace(_password))
                    EnableButtonEnter = false;
                else
             
                EnableButtonEnter = true;



            }  
   



      
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

                    EnableButtonEnter = false;



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