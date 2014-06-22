using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Microsoft.SqlServer.Server;
using MongoRepository;


namespace MyPress.Web
{
   
    public class DataPersistance : Entity
    {



        public DataPersistance()
        {
        
        this.Bing = new List<Bing>();
        
        
        
        }




       public string Login { get; set; }
        
        
        
        public string Pass { get; set; }

        public string RepPass { get; set; }

       public string Email { get; set; }


        public List<Bing> Bing { get; set; }


    }

    [DataContract]
    public class Bing 
    {


        
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string SentAnalys { get; set; }
    }

   



}