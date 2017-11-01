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
        bool AdminAuth(string username, string password);
        [OperationContract]
        bool UserAuth(string username, string password);
        [OperationContract]
        bool CreateUser(string name, string email, string password);
        [OperationContract]
        bool DeleteUser(string email);
        [OperationContract]
        bool UpdatePassword(string email, string password);
        [OperationContract]
        bool CreateUserFromTxtFile();
        [OperationContract]
        string CheckForNewFiles();

    }
}
