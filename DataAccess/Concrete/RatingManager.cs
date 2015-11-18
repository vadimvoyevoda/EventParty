using DataAccess.Abstract;
using DataModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class RatingManager : IDataManager
    {
        public bool Delete(object name)
        {
            int ratingTypeId = Convert.ToInt32(name);
            using (var ctx = new EventContext())
            {
                var delRating = (from p in ctx.Ratings
                                 where p.Id == ratingTypeId
                                 select p).FirstOrDefault();
                if (delRating != default(EventRating))
                {
                    try
                    {
                        ctx.Ratings.Remove(delRating);
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
                    ctx.Ratings.Where(p => p.Id == actualId).FirstOrDefault().Name = newName;
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
                foreach (var r in ctx.Ratings)
                {
                    if (r.Name == name)
                    {
                        return false;
                    }
                }

                ctx.Ratings.Add(new EventRating { Name = name });
                ctx.SaveChanges();
                return true;
            }
        }

        public bool isExist(string name, int id = -1)
        {
            using (var ctx = new EventContext())
            {
                var suchRating = (from t in ctx.Ratings
                                  where t.Name == name
                                  select t).FirstOrDefault();

                if (suchRating != default(EventRating))
                {
                    return true;
                }
                return false;
            }
        }
    }
}
