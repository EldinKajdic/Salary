using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DbService
{
    public class DbService : IDbService
    {
        UsersDbEntities db = new UsersDbEntities();
        public bool adminAuth(string username, string password)
        {
            var adminQuery = (from admins in db.admin_db
                              where admins.username == username &&
                              admins.password == password
                              select admins);

            if(adminQuery.SingleOrDefault() != null)
            {
                return true;
            }

            return false;
        }

        public bool userAuth(string username, string password)
        {
            var userQuery = (from users in db.userinfo_db
                             where users.email == username &&
                             users.password == password
                             select users);

            if(userQuery.SingleOrDefault() != null)
            {
                return true;
            }

            return false;
        }

    }
}
