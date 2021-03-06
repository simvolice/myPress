﻿using System;
using System.Reactive.Linq;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MyPress.Client.Model;
using MyPress.Client.ServiceMyPress;

namespace MyPress.Client.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class CreateRubViewModel1 : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the CreateRubViewModel1 class.
        /// </summary>
        /// 
        /// 
        /// 


        Rubriki _rubriki = new Rubriki();

        


        private IDisposable disposableCheckLogin;

        /// <summary>
        /// The <see cref="NameRub" /> property's name.
        /// </summary>
        public const string NameRubPropertyName = "NameRub";

        private string _nameRub = string.Empty;





        /// <summary>
        /// Sets and gets the NameRub property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string NameRub
        {
            get
            {
                return _nameRub;
            }

            set
            {
                if (_nameRub == value)
                {
                    return;
                }

                RaisePropertyChanging(NameRubPropertyName);
                _nameRub = value;
                RaisePropertyChanged(NameRubPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Query" /> property's name.
        /// </summary>
        public const string QueryPropertyName = "Query";

        private string _query = string.Empty;

        /// <summary>
        /// Sets and gets the Query property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Query
        {
            get
            {
                return _query;
            }

            set
            {
                if (_query == value)
                {
                    return;
                }

                RaisePropertyChanging(QueryPropertyName);
                _query = value;
                RaisePropertyChanged(QueryPropertyName);
            }
        }



        private readonly IDataService _dataService;
        ServiceMyPress.MyPressServiceClient myService = new MyPressServiceClient();
        ServiceMyPress.Data myData = new Data();


        private RelayCommand createRub;
      

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand CreateR
        {
            get
            {
                return createRub ?? (createRub = new RelayCommand(
                    ExecuteCreateRubCommand,
                    CanExecuteCreateRubCommand));
            }
        }

        private void ExecuteCreateRubCommand()
        {

            var func = Observable.FromEventPattern<GetCurrUserCompletedEventArgs>(myService, "GetCurrUserCompleted")
                 .ObserveOnDispatcher();
            myService.GetCurrUserAsync();
            disposableCheckLogin = func.ObserveOnDispatcher().Select(x => x.EventArgs.Result).Subscribe(c => CheckEr(c));


           





        }

        private void CheckEr(string c)
        {

            disposableCheckLogin.Dispose();




            _rubriki.Name = NameRub;
            _rubriki.DateCreate = DateTime.Now;
            _rubriki.Query = Query;
            _rubriki.CountCircle = 2;
            _rubriki.Market = "ru-RU";
           




            myData.Login = c;

            myService.QueryToBingAsync( myData, _rubriki);


        }

        private bool CanExecuteCreateRubCommand()
        {
            return true;
        }


        public CreateRubViewModel1(IDataService dataService)
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

      

       
    }
}