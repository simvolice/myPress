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

            ServiceReference1.Data data = new Data();
            



            var item = new DataItem(data.Login, data.Pass, data.Email);
            callback(item, null);


            
        }
    }
}