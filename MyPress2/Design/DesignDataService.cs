using System;
using MyPress2.Model;
using MyPress2.ServiceReference1;

namespace MyPress2.Design
{
    public class DesignDataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
            ServiceReference1.Data data = new Data();




            var item = new DataItem(string.Empty, string.Empty, string.Empty);
            callback(item, null);
        }
    }
}