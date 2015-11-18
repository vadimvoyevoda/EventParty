using DataAccess.Abstract;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class CountryManager : IDataManager
    {
        public bool Add(string name, int id = -1)
        {
            using (var ctx = new EventContext())
            {
                ctx.Countries.Add(new EventCountry { Country = name });
                ctx.SaveChanges();
                return true;
            }
        }

        public bool Delete(object name)
        {
            int countryId = Convert.ToInt32(name);
            using (var ctx = new EventContext())
            {
                var delCountry = (from c in ctx.Countries
                                  where c.Id == countryId
                                  select c).FirstOrDefault();

                if (delCountry != default(EventCountry))
                {
                    ctx.Countries.Remove(delCountry);
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
                var editC = (from c in ctx.Countries
                             where c.Id == actualId
                             select c).FirstOrDefault();

                if (editC != default(EventCountry))
                {
                    editC.Country = newName;
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
                var country = (from c in ctx.Countries
                               where c.Country == name
                               select c).FirstOrDefault();
                if (country != default(EventCountry))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
