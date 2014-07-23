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
        string GetCurrUser();


        [OperationContract]
        void AddCurrUser(DataCr dataCr);


        [OperationContract]
        void QueryToBing( Data data, Rubriki rubriki);

        [OperationContract]
        void AddUser(Data data);


        [OperationContract]
        ErrorList RestorePass(Data data);

        [OperationContract]
        ErrorList CheckUser(Data data);

        [OperationContract]
        ErrorList CheckUserLogin(Data data);



    }
}
