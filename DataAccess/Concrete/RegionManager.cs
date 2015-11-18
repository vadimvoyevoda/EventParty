using DataAccess.Abstract;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class RegionManager : IDataManager
    {
        public bool Add(string name, int id = -1)
        {            
            using (var ctx = new EventContext())
            {
                var thisCountry = (from c in ctx.Countries
                                   where c.Id == id
                                   select c).FirstOrDefault();

                if (thisCountry != default(EventCountry))
                {
                    ctx.Regions.Add(new EventRegion { RegionName = name, Country = thisCountry });
                    ctx.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public bool Delete(object name)
        {
            int regionId = Convert.ToInt32(name);
            using (var ctx = new EventContext())
            {
                var delregion = (from c in ctx.Regions
                                 where c.Id == regionId
                                 select c).FirstOrDefault();

                if (delregion != default(EventRegion))
                {
                    ctx.Regions.Remove(delregion);
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
                var editR = (from r in ctx.Regions
                             where r.Id == actualId
                             select r).FirstOrDefault();

                if (editR != default(EventRegion))
                {
                    editR.RegionName = newName;
                    editR.Country = editR.Country;
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
                var suchRegion = (from r in ctx.Regions
                                  where r.RegionName == name && r.Country.Id == id
                                  select r).FirstOrDefault();

                if (suchRegion != default(EventRegion))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
