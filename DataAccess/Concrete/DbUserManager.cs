using DataAccess.Abstract;
using DataModel;
using EncryptPass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class DbUserManager : IUserManager
    {
        public bool IsValidUser(string login, string password)
        {
            using (var ctx = new EventContext())
            {
                string pass = Encrypt.CalculateMD5Hash(password + ctx.Customers.FirstOrDefault(c => c.Login == login).Salt);
                var user = ctx.Customers.FirstOrDefault(c => c.Password == pass && c.Login == login && c.IsDeleted == false);
                return user != default(EventCustomer);
            }
        }
        public string[] GetRoles(string login)
        {
            using (var ctx = new EventContext())
            {
                List<string> roles = new List<string>();
                var elem = ctx.Customers.FirstOrDefault(c => c.Login == login);
                if(elem != default(EventCustomer))
                {
                    roles.Add(elem.Permissions.Type);
                }                
                    
                return roles.ToArray();
            }
        }
        public bool IsAvailableLogin(string login)
        {
            using (var ctx = new EventContext())
            {
                var res = (from c in ctx.Customers
                           where c.Login == login
                           select c.Id).FirstOrDefault();

                return res == default(int);
            }
        }

        public void CreateNewUser(EventCustomer model)
        {
            using (var ctx = new EventContext())
            {
                string salt = GenerateSalt();
                model.Salt = salt;
                model.Password = Encrypt.CalculateMD5Hash(model.Password + salt);
                model.Rating = ctx.Ratings.FirstOrDefault(r => r.Name == "Low");
                model.Permissions = ctx.Permissions.FirstOrDefault(p => p.Type == "User");

                ctx.Customers.Add(model);
                ctx.SaveChanges();
            }
        }

        private string GenerateSalt()
        {
            Random rand = new Random();
            string salt = string.Empty;
            for (int i = 0; i < 16; i++)
            {
                salt += rand.Next(10);
            }
            return salt;
        }        
        public bool AddToBanList(int userId)
        {
            using (var ctx = new EventContext())
            {
                var editC = (from c in ctx.Customers
                             where c.Id == userId
                             select c).FirstOrDefault();

                if (editC != default(EventCustomer))
                {
                    editC.IsBan = true;
                    editC.Rating = editC.Rating;
                    editC.Permissions = editC.Permissions;
                    ctx.SaveChanges();
                    return true;
                }

            }
            return false;
        }
        public bool RemoveFromBanList(int userId)
        {
            using (var ctx = new EventContext())
            {
                var editC = (from c in ctx.Customers
                             where c.Id == userId
                             select c).FirstOrDefault();
                if (editC != default(EventCustomer))
                {
                    editC.IsBan = false;
                    editC.Rating = editC.Rating;
                    editC.Permissions = editC.Permissions;
                    ctx.SaveChanges();
                    return true;
                }
            }
            return false;

        }
        public bool DeleteUser(int userId)
        {
            using (var ctx = new EventContext())
            {
                var editC = (from c in ctx.Customers
                             where c.Id == userId
                             select c).FirstOrDefault();
                if (editC != default(EventCustomer))
                {
                    editC.IsDeleted = true;
                    editC.Rating = editC.Rating;
                    editC.Permissions = editC.Permissions;
                    ctx.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        public bool ReturnUser(int userId)
        {
            using (var ctx = new EventContext())
            {
                var editC = (from c in ctx.Customers
                             where c.Id == userId
                             select c).FirstOrDefault();
                if (editC != default(EventCustomer))
                {
                    editC.IsDeleted = false;
                    editC.Rating = editC.Rating;
                    editC.Permissions = editC.Permissions;
                    ctx.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        public bool ChangePermission(int userId, int newPermissionId)
        {
            using (var ctx = new EventContext())
            {
                var editC = (from c in ctx.Customers
                             where c.Id == userId
                             select c).FirstOrDefault();

                if (editC != default(EventCustomer))
                {
                    editC.Permissions = (from c in ctx.Permissions
                                         where c.Id == newPermissionId
                                         select c).FirstOrDefault();

                    editC.Rating = editC.Rating;
                    ctx.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        public bool ChangePhoto(int userId, string content)
        {
            using (var ctx = new EventContext())
            {
                var editC = (from c in ctx.Customers
                             where c.Id == userId
                             select c).FirstOrDefault();

                if (editC != default(EventCustomer))
                {
                    editC.Photo = content;
                    editC.Rating = editC.Rating;
                    editC.Permissions = editC.Permissions;
                    ctx.SaveChanges();
                    return true;
                }
            }
            return false;
        }
               
             
        public bool ChangeUserInfo(EventCustomer model)
            //int id, string pass, string name, string lastName, DateTime birth, string mobile,
            //string email, string country, string address, bool showContacts)
        {
            using (var ctx = new EventContext())
            {
                var current = ctx.Customers.FirstOrDefault(c => c.Id == model.Id);
                if (current != default(EventCustomer))
                {
                    if (!string.IsNullOrEmpty(model.Password))
                    {
                        string newPass = Encrypt.GetEncryptedPassword(model.Password, current.Salt);
                        current.Password = newPass;
                    }
                    current.Name = model.Name;
                    current.LastName = model.LastName;
                    current.Birthday = model.Birthday;
                    current.Mobile = model.Mobile;
                    current.Email = model.Email;
                    current.Country = model.Country;
                    current.Address = model.Address;
                    current.ShowContacts = model.ShowContacts;
                    current.Rating = current.Rating;
                    current.Permissions = current.Permissions;

                    ctx.SaveChanges();
                    return true;
                }
            }
            return false;
        }


    }
}
