using MongoRepository;
using Limilabs.Mail;


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



        public ErrorList CheckUserLogin(Data data)
        {


            MongoRepository<DataPersistance> repository = new MongoRepository<DataPersistance>();
         

           
            if (repository.Exists(x => x.Login == data.Login) && repository.Exists(x=> x.Pass == data.Pass))
          
                return ErrorList.SuccesPassword;


           
return ErrorList.FailedPass;


        }


        public void AddUser(Data data)
        {



            MongoRepository<DataPersistance> repository = new MongoRepository<DataPersistance>();

            repository.Add(data.GetDataPersistance());
           

        }



        public ErrorList CheckUser(Data data)
        {
            MongoRepository<DataPersistance> repository = new MongoRepository<DataPersistance>();


            if (repository.Exists(x => x.Login == data.Login))

                return ErrorList.DublicateName;


            if (repository.Exists(x => x.Email == data.Email))
                return ErrorList.DublicateEmail;
        
        return ErrorList.Succes;
        }



        public ErrorList RestorePass(Data data)
        {
            MongoRepository<DataPersistance> repository = new MongoRepository<DataPersistance>();

            if (repository.Exists(x => x.Email == data.Email))
            {
            

               

                //Подключить mail настроить с шаблоном geometric




            
               return ErrorList.Succes;
            }




            
            return ErrorList.EmailNull;





        }





    }


}
