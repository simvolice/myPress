using System;
using System.Windows.Controls;
using MyPress2.ServiceReference1;

namespace MyPress2.Model
{
    public class DataService : IDataService
    {
        
       

       
        
        
        
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to connect to the actual data service

           
            



            var item = new DataItem(string.Empty, string.Empty, string.Empty, string.Empty);
            callback(item, null);


            
        }
    }
}