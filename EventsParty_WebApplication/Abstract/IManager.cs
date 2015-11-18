using DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsParty_WebApplication.Abstract
{
    interface IManager
    {
        void SetRoles(DbUserManager userManager, string login);
        void RefreshPrincipal();
        string GetImage(object img, string serverPath);
    }
}
