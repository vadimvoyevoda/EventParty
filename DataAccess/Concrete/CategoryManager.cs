using DataAccess.Abstract;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class CategoryManager : IDataManager
    {
        public bool Delete(object name)
        {
            int categoryTypeId = Convert.ToInt32(name);
            using (var ctx = new EventContext())
            {
                var delCategory = (from p in ctx.PersonCategories
                                   where p.Id == categoryTypeId
                                   select p).FirstOrDefault();
                if (delCategory != default(EventPersonCategory))
                {
                    try
                    {
                        ctx.PersonCategories.Remove(delCategory);
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
                    ctx.PersonCategories.Where(p => p.Id == actualId).FirstOrDefault().Category = newName;
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public bool isExist(string name, int id = -1)
        {
            using (var ctx = new EventContext())
            {
                var suchCategory = (from t in ctx.PersonCategories
                                    where t.Category == name
                                    select t).FirstOrDefault();

                if (suchCategory != default(EventPersonCategory))
                {
                    return true;
                }
                return false;
            }
        }

        public bool Add(string name, int id = -1)
        {
            using (var ctx = new EventContext())
            {
                foreach (var p in ctx.PersonCategories)
                {
                    if (p.Category == name)
                    {
                        return false;
                    }
                }

                ctx.PersonCategories.Add(new EventPersonCategory { Category = name });
                ctx.SaveChanges();
                return true;
            }
        }
    }
}
