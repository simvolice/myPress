using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MyPress.Client.Model;
using MyPress.Client.ServiceMyPress;
using MyPress.Client.View;



namespace MyPress.Client.ViewModel
{
    
    public class MainViewModel : ViewModelBase


    {


        ServiceMyPress.MyPressServiceClient _myPressServiceClient = new MyPressServiceClient();

        ServiceMyPress.Data _data = new Data();

        private IDisposable _disposable;



        /// <summary>
        /// The <see cref="ListRubrici" /> property's name.
        /// </summary>
        public const string ListRubriciPropertyName = "ListRubrici";

        private ObservableCollection<Rubriki> _listRubrici = new ObservableCollection<Rubriki>();

        /// <summary>
        /// Sets and gets the ListRubrici property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<Rubriki> ListRubrici
        {
            get
            {
                return _listRubrici;
            }

            set
            {
                if (_listRubrici == value)
                {
                    return;
                }

                RaisePropertyChanging(ListRubriciPropertyName);
                _listRubrici = value;
                RaisePropertyChanged(ListRubriciPropertyName);
            }
        }




      




        private RelayCommand goBackRelayCommand;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand GoBackCommand
        {
            get
            {
                return goBackRelayCommand ?? (goBackRelayCommand = new RelayCommand(
                    ExecuteMyCommand,
                    CanExecuteMyCommand));
            }
        }

        private void ExecuteMyCommand()
        {
            ChildWindow1 childWindow1 = new ChildWindow1();
            childWindow1.Show();




        }

        private bool CanExecuteMyCommand()
        {
            return true;
        }


        private readonly IDataService _dataService;


      
        public MainViewModel(IDataService dataService)
        {



            var func = Observable.FromEventPattern<GetCurrUserCompletedEventArgs>(_myPressServiceClient, "GetCurrUserCompleted")
              .ObserveOnDispatcher();
            _myPressServiceClient.GetCurrUserAsync();
            func.ObserveOnDispatcher().Select(x => x.EventArgs.Result).Subscribe(c => CheckEr(c));

          
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

        private void CheckEr(string c)
        {


            

            _data.Login = c;





            var func = Observable.FromEventPattern<GetRubrikiCompletedEventArgs>(_myPressServiceClient, "GetRubrikiCompleted")
              .ObserveOnDispatcher();
            _myPressServiceClient.GetRubrikiAsync(_data);
            func.ObserveOnDispatcher().Select(x => x.EventArgs.Result).Subscribe(z => GetRub(z));




        }

        private void GetRub(System.Collections.ObjectModel.ObservableCollection<Rubriki> z)
        {



            foreach (Rubriki s in z)
            {

                ListRubrici.Add(s);

            }


        }

    
        
        
        
        
        
        
        
        
        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}



    
    
    }
}