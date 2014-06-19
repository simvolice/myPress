using System;
using MyPress.Client.Model;

namespace MyPress.Client.Design
{
    public class DesignDataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
          




            var item = new DataItem(string.Empty, string.Empty, string.Empty, string.Empty);
            callback(item, null);
        }
    }
}