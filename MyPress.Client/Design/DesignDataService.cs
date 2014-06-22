using System;
using MyPress.Client.Model;

namespace MyPress.Client.Design
{
    public class DesignDataService : IDataService
    {
        public void GetData(Action<ServiceMyPress.Data, Exception> callback)
        {
          




            var item = new ServiceMyPress.Data();
            callback(item, null);
        }
    }
}