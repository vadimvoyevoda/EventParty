using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserManager
    {
        bool IsValidUser(string login, string password);
        string[] GetRoles(string login);        
        bool IsAvailableLogin(string login);
        void CreateNewUser(EventCustomer model);
        bool AddToBanList(int userId);
        bool RemoveFromBanList(int userId);
        bool DeleteUser(int userId);
        bool ReturnUser(int userId);
        bool ChangePermission(int userId, int newPermissionId);
        bool ChangePhoto(int userId, string content);

        bool ChangeUserInfo(EventCustomer model);
    }
}
