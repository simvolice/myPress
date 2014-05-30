using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using MongoDB.Bson;
using MongoRepository;
using uk.ac.wlv.sentistrength;

namespace MyPress.Web
{
    public class SearchBing
    {
        private const string AccountKey = "3Whd8eqsVF7lO+fXxRT3iahqRj3lbRYWoXongN8I+NI";

        private const string RootUrl = "https://api.datamarket.azure.com/Bing/Search";




        public IEnumerable<WebResult> MakeRequest(string query, string market)

        {

            int skip = 0;
       

        var bingContainer = new BingSearchContainer(new Uri(RootUrl));


        bingContainer.Credentials = new NetworkCredential(AccountKey, AccountKey);


 

                var webQuery = bingContainer.Web(query, null, null, market, "off", null, null, null);



                webQuery = webQuery.AddQueryOption("$skip", skip);


               


            skip += 50;

            return webQuery.Execute();

 
        }










      
    }
}