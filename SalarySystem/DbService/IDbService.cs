using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DbService
{
    [ServiceContract]
    public interface IDbService
    {
        [OperationContract]
        bool adminAuth(string username, string password);
        [OperationContract]
        bool userAuth(string username, string password);

    }
}
