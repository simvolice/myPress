using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using Newtonsoft.Json;

namespace MyPress.Web
{






    [DataContract]
    public class DataCr
    {

        [DataMember]
        public string CurrentUser { get; set; }

        public DataCr( DataCursor dataCursor)
        {

            this.CurrentUser = dataCursor.CurrentUser;


        }



        [OperationContract]
        public DataCursor GetDataCursor()
        {
        DataCursor dataCursor = new DataCursor();


            dataCursor.CurrentUser = CurrentUser;

            return dataCursor;


        }




    }



    [DataContract]
    public class Data
    {







        [DataMember]
       public string Login { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
       public string Pass { get; set; }

      
     

       [DataMember]
        public List<Rubriki> Rubriki { get; set; }

       [DataMember]
       public List<Bing> Bing { get; set; }

        public Data()
        {
        }

        public Data(DataPersistance dataPersistance)
        {


           
  
            this.Login = dataPersistance.Login;
            this.Pass = dataPersistance.Pass;
            this.Email = dataPersistance.Email;


            this.Rubriki = dataPersistance.Rubriki;
            


        }


        [OperationContract]
        public DataPersistance GetDataPersistance()
        {

            DataPersistance dataPersistance = new DataPersistance();


            dataPersistance.Rubriki = this.Rubriki;
            dataPersistance.Email = this.Email;
            dataPersistance.Pass = this.Pass;
            dataPersistance.Login = this.Login;
       

           
            return dataPersistance;
        }









    }
}