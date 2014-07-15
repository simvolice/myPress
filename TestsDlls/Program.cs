
using MongoRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;



namespace TestsDlls
{

    public class Data
    {

        
        public string Login { get; set; }

     
        public string Email { get; set; }

       
        public string Pass { get; set; }




       
        public List<Rubriki> Rubriki { get; set; }

        
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

   
    public class Rubriki
    {




        public Rubriki()
        {


            this.Bing = new List<Bing>();



        }

     
        public string Name { get; set; }
       
        public DateTime DateCreate { get; set; }
     
        public Int64 Coll { get; set; }

     
        public List<Bing> Bing { get; set; }


    }

  
    public class Bing
    {


       
        public string Description { get; set; }
       
        public string Title { get; set; }

      
        public string SentAnalys { get; set; }

        
        public string Url { get; set; }

    }








    public class Program

    {



        public static void Start() {


            Data data = new Data();
            
         
            MongoRepository<DataPersistance> repo = new MongoRepository<DataPersistance>();

          


        
            data.Login = "hello";


         


            repo.Add(data.GetDataPersistance());
        
        }
        
        private static void Main(string[] args)
        {

          
            Start();


            Console.WriteLine("Helo");

            Console.ReadKey();


           



        }

    }

}