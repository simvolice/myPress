using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MyPress2.Model;
using MyPress2.Resources;
using MyPress2.ServiceReference1;
using System.ComponentModel.DataAnnotations;





namespace MyPress2.ViewModel
{
    
    public class MainViewModel : ViewModelBase


    {




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