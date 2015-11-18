using DataAccess.Abstract;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class CityManager : IDataManager
    {
        public bool Add(string name, int id = -1)
        {
            using (var ctx = new EventContext())
            {
                var thisRegion = (from c in ctx.Regions
                                  where c.Id == id
                                  select c).FirstOrDefault();

                if (thisRegion != default(EventRegion))
                {
                    ctx.Cities.Add(new EventCity { CityName = name, Region = thisRegion });
                    ctx.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public bool Delete(object name)
        {
            int cityId = Convert.ToInt32(name);
            using (var ctx = new EventContext())
            {
                var delcity = (from c in ctx.Cities
                               where c.Id == cityId
                               select c).FirstOrDefault();

                if (delcity != default(EventCity))
                {
                    ctx.Cities.Remove(delcity);
                    ctx.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool Edit(int actualId, string newName)
        {
            using (var ctx = new EventContext())
            {
                var editC = (from r in ctx.Cities
                             where r.Id == actualId
                             select r).FirstOrDefault();

                if (editC != default(EventCity))
                {
                    editC.CityName = newName;
                    editC.Region = editC.Region;
                    ctx.SaveChanges();
                    return true;
                }

            }
            return false;
        }

        public bool isExist(string name, int id = -1)
        {
            using (var ctx = new EventContext())
            {
                var city = (from c in ctx.Cities
                            where c.CityName == name && c.Region.Id == id
                            select c).FirstOrDefault();

                if (city != default(EventCity))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
