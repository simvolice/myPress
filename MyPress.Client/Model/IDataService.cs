using System;

namespace MyPress.Client.Model
{
    public interface IDataService
    {
        void GetData(Action<ServiceMyPress.Data, Exception> callback);
    }
}
