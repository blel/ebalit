using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace EbalitWebForms.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EbalitWebService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select EbalitWebService.svc or EbalitWebService.svc.cs at the Solution Explorer and start debugging.
    public class EbalitWebService : IEbalitWebService
    {

        public string TestService(string argument)
        {
            return string.Format("You entered: {0}", argument);
        }
    }
}
