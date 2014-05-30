using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;


namespace TestsDlls
{
    internal class Program
    {
        private static void Main(string[] args)
        {



            const string consumerKey = "z1X2qDLJ3AidM9cTa6f5lTxiS"; // The application's consumer key
            const string consumerSecret = "I5j9IlvsDKzfCl8n18nhio0nZNhhDwX982jsyGBUErIANiVW0h";
                // The application's consumer secret
            const string accessToken = "2262563806-xNCQuuziSdWbx3pr6KUhlGfLPkCi45dsunCPRpi";
                // The access token granted after OAuth authorization
            const string accessTokenSecret = "N0cpdM67ELr5TiWuMYTIyLh8kmZwJCDy1Ntl1G61QpPSU";
                // The access token secret granted after OAuth authorization











            TweetSharp.TwitterService twitterService = new TwitterService(consumerKey, consumerSecret, accessToken,
                accessTokenSecret);




            var resSearch = twitterService.Search(new SearchOptions() {Q = "Донецк", Count = 500
                , IncludeEntities = true});






            foreach (var wer in resSearch.Statuses)
            {


               
               File.WriteAllText(@"C:\Users\Администратор\Desktop\ret.txt", wer.RawSource);


            }








            Console.ReadKey();




        }

    }

}