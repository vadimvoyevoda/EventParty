using DataAccess.Abstract;
using DataModel;
using EncryptPass;
using Newtonsoft.Json;
using SearchModelLibrary;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class DbStore : IStore
    {      
        public List<EventCustomer> GetAllUsers()
        {
            using (var ctx = new EventContext())
            {
                return ctx.Customers.Include("Rating").Include("Permissions")
                    .Where(c=>c.IsBan == false && c.IsDeleted == false).ToList();
            }
        }
        public List<EventCustomer> GetBannedUsers()
        {
            using (var ctx = new EventContext())
            {
                return ctx.Customers.Include("Rating").Include("Permissions")
                    .Where(c => c.IsBan == true && c.IsDeleted == false).ToList();
            }
        }
        public List<EventCustomer> GetDeletedUsers()
        {
            using (var ctx = new EventContext())
            {
                return ctx.Customers.Include("Rating").Include("Permissions")
                    .Where(c => c.IsDeleted == true).ToList();
            }
        }
        public List<EventRating> GetAllRatings()
        {
            using (var ctx = new EventContext())
            {
                return ctx.Ratings.OrderBy(r=>r.Id).ToList();
            }
        }
        public List<EventPermission> GetAllPermissions()
        {
            using (var ctx = new EventContext())
            {
                return ctx.Permissions.OrderBy(r => r.Id).ToList();
            }
        }
        public List<EventPersonCategory> GetAllPersonCategories()
        {
            using (var ctx = new EventContext())
            {
                return ctx.PersonCategories.OrderBy(r => r.Id).ToList();
            }
        }
        public List<EventType> GetAllEventTypes()
        {
            using (var ctx = new EventContext())
            {
                return ctx.EventTypes.OrderBy(r => r.Id).ToList();
            }
        }
        public List<EventCountry> GetAllCountries()
        {
            using (var ctx = new EventContext())
            {
                return ctx.Countries.Include("Regions").ToList();
            }
        }
        public List<EventCity> GetAllCities()
        {
            using (var ctx = new EventContext())
            {
                return ctx.Cities.Include("Region").Include("Region.Country").ToList();
            }
        }
        public List<EventRegion> GetAllRegions()
        {
            using (var ctx = new EventContext())
            {
                return ctx.Regions.Include("Cities").Include("Country").ToList();
            }
        }
        public List<EventModel> GetAllEvents()
        {
            using (var ctx = new EventContext())
            {
                return ctx.Events.Include("Type").Include("PersonsCategory")
                    .Include("Publisher").Include("City").Include("City.Region")
                    .Include("City.Region.Country").Include("Likes").Include("Dislikes").ToList();
            }
        }

        public int GetEventsCount()
        {
            using (var ctx = new EventContext())
            {
                return ctx.Events.Count();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <param name="skip"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<EventModel> GetFilteredEvents( SearchModel model=null, int count=-1, int skip=-1)
        {
            using (var ctx = new EventContext())
            {
                IEnumerable<EventModel> res = ctx.Events.Include("Type")
                    .Include("PersonsCategory")
                    .Include("Publisher")
                    .Include("City")
                    .Include("City.Region")
                    .Include("City.Region.Country")
                    .Include("Likes")
                    .Include("Dislikes")
                    .OrderByDescending(e => e.Id);

                if (count != -1)
                {
                    res = res.Skip(skip).Take(count);
                }

                if (model != null)
                {
                    PlaceFilter(model, ref res);
                    TimeFilter(model, ref res);
                    AddingFilter(model, ref res);
                    TextFilter(model, ref res);
                }
                return res.ToList();
            }
        }

        public void PlaceFilter(SearchModel searchModel, ref IEnumerable<EventModel> model)
        {
            if (!string.IsNullOrWhiteSpace(searchModel.Country))
            {
                model = model.Where(e => e.City.Region.Country.Country == searchModel.Country);
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Region))
            {
                model = model.Where(e => e.City.Region.RegionName == searchModel.Region);
            }
            if (!string.IsNullOrWhiteSpace(searchModel.City))
            {
                model = model.Where(e => e.City.CityName == searchModel.City);
            }
        }
        public void TimeFilter(SearchModel searchModel, ref IEnumerable<EventModel> model)
        {
            if (!string.IsNullOrEmpty(searchModel.FromTime))
            {
                try
                {
                    DateTime from = Convert.ToDateTime(searchModel.FromTime);
                    model = model.Where(e => e.StartTime >= from);
                }
                catch (Exception exc)
                {
                }
            }
            if (!string.IsNullOrEmpty(searchModel.ToTime))
            {
                try
                {
                    DateTime to = Convert.ToDateTime(searchModel.ToTime);
                    model = model.Where(e => e.StartTime <= to);
                }
                catch (Exception exc)
                {
                }
            }
        }
        public void TextFilter(SearchModel searchModel, ref IEnumerable<EventModel> model)
        {
            if (!string.IsNullOrEmpty(searchModel.Text))
            {
                var text = searchModel.Text.ToLower();
                if (searchModel.OnlyInTitles)
                {
                    model = model.Where(e => e.Title.ToLower().Contains(text));
                }
                else
                {
                    model = model.Where(e => e.Title.ToLower().Contains(text) ||
                                        e.Description.ToLower().Contains(text) ||
                                        e.ShortDescription.ToLower().Contains(text) ||
                                        e.Organizers.ToLower().Contains(text) ||
                                        e.Place.ToLower().Contains(text) ||
                                        e.Publisher.Name.ToLower().Contains(text) ||
                                        e.Publisher.LastName.ToLower().Contains(text) ||
                                        (!string.IsNullOrEmpty(e.Enter) && e.Enter.ToLower().Contains(text)) ||
                                        (!string.IsNullOrEmpty(e.Sponsors) && e.Sponsors.ToLower().Contains(text)));
                }
            }
        }
        public void AddingFilter(SearchModel searchModel, ref IEnumerable<EventModel> model)
        {
            if (searchModel.IsFree)
            {
                model = model.Where(e => e.isFreeEntrance == true);
            }
            if (searchModel.IsCharitable)
            {
                model = model.Where(e => e.IsCharitable == true);
            }

            if (searchModel.Types != null)
            {
                model = model.Where(e => searchModel.Types.Contains(e.Type.Type));
            }
            if (searchModel.Categories != null)
            {
                model = model.Where(e => searchModel.Categories.Contains(e.PersonsCategory.Category));             
            }
        }
    }
}
