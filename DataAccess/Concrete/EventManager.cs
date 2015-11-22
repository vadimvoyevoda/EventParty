using ChangeModelLibrary;
using DataAccess.Abstract;
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
                var current = ctx.Events.Include("Type").Include("PersonsCategory")
                    .Include("City").Include("City.Region").Include("City.Region.Country")
                    .Include("Publisher").Include("Comments").Include("Likes").Include("MayAttend").Include("Members").Include("NoAttend")
                    .FirstOrDefault(e => e.Id == id);

                if (current != default(EventModel))
                {
                    return current;
                }                
            }
            return null;
        }

        public bool ChangeEvent(ChangeModel changedModel)
        {
            using (var ctx = new EventContext())
            {
               var changeEvent =  ctx.Events.Include("City").Include("Publisher").FirstOrDefault(e => e.Id == changedModel.Id);
               if (changeEvent != default(EventModel))
               {
                   changeEvent.Description = changedModel.Description;
                   changeEvent.EndTime = changedModel.EndTime;
                   changeEvent.Enter = changedModel.Enter;
                   changeEvent.IsCharitable = changedModel.IsCharitable;
                   changeEvent.isFreeEntrance = changedModel.IsFreeEntrance;
                   changeEvent.Organizers = changedModel.Organizers;
                   changeEvent.PersonsCategory = ctx.PersonCategories.FirstOrDefault(p=>p.Id == changedModel.SelectedPersonsCategoryId);
                   //changeEvent.Photos = changedModel.Photos;
                   changeEvent.Place = changedModel.Place;
                   changeEvent.ShortDescription = changedModel.ShortDescription;
                   changeEvent.Sponsors = changedModel.Sponsors;
                   changeEvent.StartTime = changedModel.StartTime;
                   changeEvent.Title = changedModel.Title;
                   changeEvent.Type = ctx.EventTypes.FirstOrDefault(t=>t.Id == changedModel.SelectedTypeId);

                   ctx.SaveChanges();
               }
            }
            return true;
        }
        
        public bool ChangeMainPhoto(int id, string newImage)
        {
            using (var ctx = new EventContext())
            {
                var edit = ctx.Events.Include("City").Include("Publisher").Include("Type").Include("PersonsCategory")
                    .FirstOrDefault(e => e.Id == id);

                if (edit != default(EventModel))
                {
                    edit.MainPhoto = newImage;
                    ctx.SaveChanges();
                    return true;
                }
            }
            return false;
        }


        public bool DeleteEvent(int id)
        {
            using (var ctx = new EventContext())
            {
                var del = ctx.Events.FirstOrDefault(e => e.Id == id);
                if (del != default(EventModel))
                {
                    ctx.Events.Remove(del);
                    ctx.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
