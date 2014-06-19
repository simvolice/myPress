using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MyPress.Client.Model;

namespace MyPress.Client.ViewModel
{
    
    public class MainViewModel : ViewModelBase


    {

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
            Uri uri = new Uri("/LoginViews", UriKind.Relative);
            Messenger.Default.Send<Uri>(uri, "Navigate"); 

        }

        private bool CanExecuteMyCommand()
        {
            return true;
        }


        private readonly IDataService _dataService;


      
        public MainViewModel(IDataService dataService)
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

     
        
        
        
        
        
        
        
        
        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}



    
    
    }
}