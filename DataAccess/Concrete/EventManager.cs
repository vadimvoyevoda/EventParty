﻿using DataAccess.Abstract;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EventManager : IEventManager
    {
        public bool CreateEvent(EventModel model)
        {
            using (var ctx = new EventContext())
            {
                model.Publisher = (from c in ctx.Customers
                                   where c.Login == model.Publisher.Login
                                   select c).FirstOrDefault();

                if (!model.Publisher.IsBan && !model.Publisher.IsDeleted)
                {
                    model.Type = (from t in ctx.EventTypes
                                  where t.Id == model.Type.Id
                                  select t).FirstOrDefault();

                    model.PersonsCategory = (from c in ctx.PersonCategories
                                             where c.Id == model.PersonsCategory.Id
                                             select c).FirstOrDefault();

                    model.City = (from c in ctx.Cities
                                  where c.Id == model.City.Id
                                  select c).FirstOrDefault();

                    if (model.City != default(EventCity))
                    {
                        model.IsPublished = true;

                        ctx.Events.Add(model);
                        ctx.SaveChanges();
                        return true;
                    }
                }
            }
            return false;
        }

        public EventModel GetEventById(int id)
        {
            using (var ctx = new EventContext())
            {
                var current = ctx.Events.Include("Type").Include("PersonsCategory").Include("City").Include("Publisher")
                    .Include("Comments").Include("Likes").Include("MayAttend").Include("Members").Include("NoAttend")
                    .FirstOrDefault(e => e.Id == id);

                if (current != default(EventModel))
                {
                    return current;
                }                
            }
            return null;
        }
    }
}