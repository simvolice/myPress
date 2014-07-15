using System.Collections.Generic;

namespace MyPress.Web
{
    public class ResultFromBing
    {


        public List<Bing> ResultFBing(string query, string market, int countQuery)
        {





            List<Bing> list = new List<Bing>();

        SearchBing searchBing = new SearchBing();
            SentimentAnal sentimentAnal = new SentimentAnal();




            for (int i = 0; i < countQuery; i++)
            {


                foreach (var result in searchBing.MakeRequest(query,market))
                {

                 string senRes = sentimentAnal.SetSentimAnalise(result.Description);


                    list.AddRange(new []
                    {
                        new Bing() {Description = result.Description, SentAnalys = senRes, Title = result.Title, Url = result.DisplayUrl}
                    







                    });
                
                
                
                }
            
            
            
            
            
            
            
            
            
            
            
            }






            return list;


        }









    }
}