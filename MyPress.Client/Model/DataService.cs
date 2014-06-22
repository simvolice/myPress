using System;

namespace MyPress.Client.Model
{
    public class DataService : IDataService
    {
        
       
        public void GetData(Action<ServiceMyPress.Data, Exception> callback)
        {


            var item = new ServiceMyPress.Data();
            callback(item, null);


            
        }
    }
}