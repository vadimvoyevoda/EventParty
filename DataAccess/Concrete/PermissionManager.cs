using DataAccess.Abstract;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class PermissionManager : IDataManager
    {
        public bool Delete(object name)
        {
            int permissionTypeId = Convert.ToInt32(name);
            using (var ctx = new EventContext())
            {
                var delPermission = (from p in ctx.Permissions
                                     where p.Id == permissionTypeId
                                     select p).FirstOrDefault();
                if (delPermission != default(EventPermission))
                {
                    try
                    {
                        ctx.Permissions.Remove(delPermission);
                        ctx.SaveChanges();
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
                return false;
            }
        }

        public bool Edit(int actualId, string newName)
        {                    
            using (var ctx = new EventContext())
            {
                try
                {
                    ctx.Permissions.Where(p => p.Id == actualId).FirstOrDefault().Type = newName;
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public bool Add(string name, int id = -1)
        {
            using (var ctx = new EventContext())
            {
                foreach (var p in ctx.Permissions)
                {
                    if (p.Type == name)
                    {
                        return false;
                    }
                }

                ctx.Permissions.Add(new EventPermission { Type = name });
                ctx.SaveChanges();
                return true;
            }
        }

        public bool isExist(string name, int id = -1)
        {
            using (var ctx = new EventContext())
            {
                var suchPermission = (from t in ctx.Permissions
                                      where t.Type == name
                                      select t).FirstOrDefault();

                if (suchPermission != default(EventPermission))
                {
                    return true;
                }
                return false;
            }
        }

        public EventPermission GetPermission(string type)
        {
            using(var ctx = new EventContext())
            {
                var rating = ctx.Permissions.FirstOrDefault(p=>p.Type == type);
                if(rating != default(EventPermission))
                {
                    return rating;
                }
                return null;
            }
        }
    }
}
