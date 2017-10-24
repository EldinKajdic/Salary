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
        public bool AdminAuth(string username, string password)
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

        public bool UserAuth(string username, string password)
        {
            var userQuery = (from users in db.userinfo_db
                             where users.email == username &&
                             users.password == password
                             select users);

            if (userQuery.SingleOrDefault() != null)
            {
                return true;
            }

            return false;
        }

        public bool CreateUser(string name, string email, string password)
        {
            DateTime createdAt = new DateTime();
            createdAt = DateTime.Now;

            var userQuery = (from users in db.userinfo_db
                             where users.email == email
                             select users);

            if (userQuery.SingleOrDefault() == null)
            {
                var user = new userinfo_db
                {
                    name = name,
                    email = email,
                    password = password,
                    created_at = createdAt
                };

                db.userinfo_db.Add(user);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteUser(string email)
        {
            var userQuery = (from users in db.userinfo_db
                             where users.email == email
                             select users);

            if (userQuery.SingleOrDefault() != null)
            {
                db.userinfo_db.RemoveRange(userQuery);
                db.SaveChanges();
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
