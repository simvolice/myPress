using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyPress2.Model;
using MyPress2.ServiceReference1;

namespace MyPress2.ViewModel
{
   
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

      
        public const string WelcomeTitlePropertyName = "WelcomeTitle";


        ServiceReference1.Data _data = new Data();
        ServiceReference1.MyPressServiceClient  _myPressServiceClient = new MyPressServiceClient();



        private string _welcomeTitle = string.Empty;


        private RelayCommand _myCommand;

     
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

          

            _myPressServiceClient.QueryToBingAsync("Славянск", _data, "ru-RU",2);


        }

        private bool CanExecuteMyCommand()
        {
            return true;
        }




      
        public string WelcomeTitle
        {
            get
            {
                return _welcomeTitle;
            }

            set
            {
                if (_welcomeTitle == value)
                {
                    return;
                }

                _welcomeTitle = value;
                RaisePropertyChanged(WelcomeTitlePropertyName);
            }
        }

       
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

                    WelcomeTitle = item.Title;
                });
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}