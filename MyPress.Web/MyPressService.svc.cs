using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.IO;
using System.Linq;
using MongoRepository;



namespace MyPress.Web
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "MyPressService" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы MyPressService.svc или MyPressService.svc.cs в обозревателе решений и начните отладку.
    public class MyPressService : IMyPressService
    {



        public string GetCurrUser()
        {

            MongoRepository<DataCursor> dataCursors = new MongoRepository<DataCursor>();



            var curUsr = dataCursors.FirstOrDefault();





            return curUsr.CurrentUser;




        }


        public void AddCurrUser(DataCr dataCr)
        {
        


         MongoRepository<DataCursor> dataCursors = new MongoRepository<DataCursor>();
          


            var curUsr = dataCursors.FirstOrDefault();


            if (curUsr != null)
            {

                curUsr.CurrentUser = dataCr.CurrentUser;

                dataCursors.Update(curUsr);



            }

            else
            {



                dataCursors.Add(dataCr.GetDataCursor());


            }



                

            
        }

        public void QueryToBing( Data data, Rubriki  rubriki)
        {

            


            MongoRepository<DataPersistance> repository = new MongoRepository<DataPersistance>();
            ResultFromBing resultFromBing = new ResultFromBing();
          



            var curUser = repository.Where(x => x.Login == data.Login).SingleOrDefault();






            rubriki.Bing = resultFromBing.ResultFBing(rubriki.Query, rubriki.Market, rubriki.CountCircle);
            rubriki.CountRubr = resultFromBing.ResultFBing(rubriki.Query, rubriki.Market, rubriki.CountCircle).Count;

            curUser.Rubriki.Add(rubriki);

            repository.Update(curUser);



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


                var Pass = repository.Where(x => x.Email == data.Email).SingleOrDefault();

                StreamReader str = File.OpenText(@"C:\Users\mKazi_000\Downloads\geometric-updated\geometric\html_with_cm_tags\full_width_alt.html");

                




               
                SmtpSection section = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

                System.Net.Mail.MailMessage mail = new MailMessage();

                mail.From = new MailAddress(section.From);
                mail.To.Add(data.Email);
                mail.Subject = Resource.ResAll.RestorePass;
               



                mail.IsBodyHtml = true;




                
                mail.Body = str.ReadToEnd().Replace("[Password]",Pass.Pass);

                


                SmtpClient smtp = new SmtpClient(section.Network.Host, section.Network.Port);

                smtp.Credentials = new NetworkCredential(section.Network.UserName, section.Network.Password);
                smtp.EnableSsl = section.Network.EnableSsl;

              

                smtp.Send(mail);




             

              



          




            
               return ErrorList.Succes;
            }




            
            return ErrorList.EmailNull;





        }

      




    }


}
