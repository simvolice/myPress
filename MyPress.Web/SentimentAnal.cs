using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using uk.ac.wlv.sentistrength;

namespace MyPress.Web
{
    public class SentimentAnal
    {


        public string  SetSentimAnalise(string stringForAnalyse)
        {

            SentiStrength sentiStrength = new SentiStrength();
            string result = string.Empty;

            String[] ssthInitialisation = { "sentidata", @"C:\Users\weiss\Desktop\SentStrength_Data_Sept2011\" };


            sentiStrength.initialise(ssthInitialisation); 
            

            string resultSentimentAnalyse = sentiStrength.computeSentimentScores(stringForAnalyse);







         
            string stringWithoutWhiteSpace = resultSentimentAnalyse.RemoveWhere(d => d.IsWhiteSpace());
            string resString = stringWithoutWhiteSpace.RemoveWhere(c => c.IsPunctuation());




            char[] charString = resString.ToCharArray();

            if (charString[0] > charString[1])
            {
              result =  "Позитив";
            }
            else if (charString[0] < charString[1])
            {
              result =  "Негатив";
            }
            else if (charString[0] == charString[1])
            {
              result =  "Нейтральное";
            }








            return result;


        }













    }
}