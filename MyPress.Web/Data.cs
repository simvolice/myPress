using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using Newtonsoft.Json;

namespace MyPress.Web
{


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
        public List<Bing> Bing { get; set; }


        public Data()
        {
        }

        public Data(DataPersistance dataPersistance)
        {

            

            this.Login = dataPersistance.Login;
            this.Pass = dataPersistance.Pass;
            this.Email = dataPersistance.Email;


            this.Bing = dataPersistance.Bing;



        }


        [OperationContract]
        public DataPersistance GetDataPersistance()
        {

            DataPersistance dataPersistance = new DataPersistance();


            dataPersistance.Bing = this.Bing;
            dataPersistance.Email = this.Email;
            dataPersistance.Pass = this.Pass;
            dataPersistance.Login = this.Login;


           
            return dataPersistance;
        }









    }
}