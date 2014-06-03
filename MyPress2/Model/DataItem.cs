using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPress2.Model
{
    public class DataItem
    {


       


        public DataItem(string user, string pass, string email)
        {
            User = user;
            Pass = pass;
            Email = email;
        }

       
        
        
        public string User { get; private set; }



        public string Pass { get; private set; }

        public string Email { get; private set; }

    }



}
