using System;
using System.Collections.Generic;
using System.IO;
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

        public string CheckForNewFiles()
        {
            DirectoryInfo listOfFiles = new DirectoryInfo(@"D:\Repos\Salary\SalarySystem\Textfiles\SavedFiles\");
            string SavedFilesPath = @"D:\Repos\Salary\SalarySystem\Textfiles\SavedFiles\Old-files\";
            string result = "No files were found.";

            FileInfo[] filer = listOfFiles.GetFiles();

            if(listOfFiles != null)
            {
                foreach (FileInfo item in filer)
                {
                     item.MoveTo(SavedFilesPath + item.Name);
                        result = $"The files were moved to {SavedFilesPath}.";
                }
            }
            else
            {
                result = "No files were found.";
            }
            return result;

        }


    public bool CreateUserFromTxtFile(string path)
        {
            DateTime createdAt = new DateTime();
            createdAt = DateTime.Now;
            string readText;

            try
            {
                readText = File.ReadAllText(path);
            }
            catch (Exception)
            {

                return false;
            }


            List<string> textfileSplitted = new List <string>();
            textfileSplitted = readText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();
            List<string> txtUserList = textfileSplitted.SelectMany(s => s.Split('\t')).ToList();
            List<userinfo_db> userList = new List<userinfo_db>();


            for (int i = 0; i < txtUserList.Count;)
            {
                for (int j = 1; j < txtUserList.Count;)
                {
                    for (int k = 2; k < txtUserList.Count;)
                    {

                        var user = new userinfo_db
                        {
                            name = txtUserList[i],
                            email = txtUserList[j],
                            password = txtUserList[k],
                            created_at = createdAt
                        };

                        var userTxtQuery = (from users in db.userinfo_db
                                            where users.email == user.email
                                            select users);

                        if (userTxtQuery.SingleOrDefault() == null)
                        {
                            userList.Add(user);
                        }

                        i += 3;
                        j += 3;
                        k += 3;

                    }

                }

            }

            if (userList.Count !=  0)
            {
                foreach (var listedUser in userList)
                {
                    db.userinfo_db.Add(listedUser);
                    db.SaveChanges();
                }

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

        public bool UpdatePassword(string email, string password)
        {
            var updateQuery = (from users in db.userinfo_db
                             where users.email == email
                             select users);

            if (updateQuery.SingleOrDefault() != null)
            {
                foreach (var user in updateQuery)
                {
                    user.password = password;
                }

                db.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
