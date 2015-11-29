using DataAccess.Abstract;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class TypeManager : IDataManager
    {
        public bool Delete(object name)
        {
            int eventTypeId = Convert.ToInt32(name);
            using (var ctx = new EventContext())
            {
                var delType = (from p in ctx.EventTypes
                               where p.Id == eventTypeId
                               select p).FirstOrDefault();

                if (delType != default(EventType))
                {
                    try
                    {
                        ctx.EventTypes.Remove(delType);
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
                    ctx.EventTypes.Where(p => p.Id == actualId).FirstOrDefault().Type = newName;
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
                foreach (var r in ctx.EventTypes)
                {
                    if (r.Type == name)
                    {
                        return false;
                    }
                }

                ctx.EventTypes.Add(new EventType { Type = name });
                ctx.SaveChanges();
                return true;
            }
        }

        public bool isExist(string name, int id = -1)
        {
            using (var ctx = new EventContext())
            {
                var suchType = (from t in ctx.EventTypes
                                where t.Type == name
                                select t).FirstOrDefault();

                if (suchType != default(EventType))
                {
                    return true;
                }
                return false;
            }
        }

        public bool SetColor(int id, string color)
        {
            using (var ctx = new EventContext())
            {
                var suchType = (from t in ctx.EventTypes
                                where t.Id == id
                                select t).FirstOrDefault();

                if (suchType != default(EventType))
                {
                    suchType.Color = color;
                    ctx.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
