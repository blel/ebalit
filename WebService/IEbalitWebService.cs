using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace EbalitWebForms.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEbalitWebService" in both code and config file together.
    [ServiceContract]
    public interface IEbalitWebService
    {
        [OperationContract]
        string TestService(string argument);
    }
}
