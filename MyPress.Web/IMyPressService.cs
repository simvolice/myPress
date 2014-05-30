using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MyPress.Web
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IMyPressService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IMyPressService
    {
        [OperationContract]
        void QueryToBing(string query, Data data, string market, int countQuery);

        [OperationContract]
        void AddUser(Data data);


        [OperationContract]
        void EnterUser(Data data);


        [OperationContract]
        void RestorePass();


       


    }
}
