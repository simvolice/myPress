using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace MyPress.Web
{
   
   public enum ErrorList
    {
    
    Succes = 0,
        DublicateName = 1,
        DublicateEmail = 2,
       SuccesPassword = 3,
      
       FailedPass = 4,
       EmailNull = 5
    
    }
}