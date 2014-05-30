using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using MongoRepository;
using Newtonsoft.Json;

namespace MyPress.Web
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "MyPressService" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы MyPressService.svc или MyPressService.svc.cs в обозревателе решений и начните отладку.
    public class MyPressService : IMyPressService
    {
        public void QueryToBing(string query, Data data, string market, int countQuery)
        {


            MongoRepository<DataPersistance> repository = new MongoRepository<DataPersistance>();
            ResultFromBing resultFromBing = new ResultFromBing();



            data.Bing = resultFromBing.ResultFBing(query, market, countQuery);






            repository.Update(data.GetDataPersistance());



        }














        public void AddUser(Data data)
        {


            MongoRepository<DataPersistance> repository = new MongoRepository<DataPersistance>();
            repository.Update(data.GetDataPersistance());






        }










        public void EnterUser(Data data)
        {

            MongoRepository<DataPersistance> repository = new MongoRepository<DataPersistance>();









        }

        public void RestorePass()
        {
            throw new NotImplementedException();
        }


     
    }


}
