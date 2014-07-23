using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Microsoft.SqlServer.Server;
using MongoDB.Bson.Serialization.Attributes;
using MongoRepository;


namespace MyPress.Web
{



    public class DataCursor : Entity

    {


        public string CurrentUser { get; set; }



    }





    public class DataPersistance : Entity
    {



        public DataPersistance()
        {

            this.Rubriki = new List<Rubriki>();
           
        
        
        }




       public string Login { get; set; }
        
        
        
        public string Pass { get; set; }

     

       public string Email { get; set; }


       public List<Rubriki> Rubriki { get; set; }

     


    }

    [DataContract]
    public class Rubriki 
    {




        public Rubriki() {


            this.Bing = new List<Bing>();
        
        
        
        }



       [DataMember]
       public int CountCircle { get; set; }

        
        [DataMember]
        public string Market { get; set; }

        [BsonIgnore]
        [DataMember]
        public string Query { get; set; }

        [DataMember]
        public string Name { get; set; }
       
        [DataMember]
        public DateTime DateCreate { get; set; }

        
        [DataMember]
        public Int64 CountRubr { get; set; }

        [DataMember]
        public List<Bing> Bing { get; set; }


    }

    [DataContract]
    public class Bing {


        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Title { get; set; }
    
        [DataMember]
        public string SentAnalys { get; set; }

        [DataMember]
        public string Url { get; set; }
    
    }


}