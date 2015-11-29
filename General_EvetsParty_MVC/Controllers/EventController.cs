using ChangeModelLibrary;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataModel;
using General_EvetsParty_MVC.Concrete;
using General_EvetsParty_MVC.Models;
using Newtonsoft.Json;
using SearchModelLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace General_EvetsParty_MVC.Controllers
{
    public class EventController : Controller
    {
        DbStore store = new DbStore();
        IEventManager manager = new EventManager();
        static IWideModel currentEvent;
        static string TempEventPhoto= null;

        [HttpGet]
        public ActionResult Index()
        {            
            EventViewModel model = new EventViewModel();
            SetModelCollections(model);            

            return View(model);
        }
        
        public void SetModelCollections(EventViewModel model)
        {
            model.EventTypes = store.GetAllEventTypes().OrderBy(t => t.Type)
                .Select(eType => new SelectListItem { Value = eType.Id.ToString(), Text = eType.Type });

            model.EventPersonCategories = store.GetAllPersonCategories().OrderBy(c => c.Category)
                .Select(eCat => new SelectListItem { Value = eCat.Id.ToString(), Text = eCat.Category });

            model.Countries = store.GetAllCountries().OrderBy(c => c.Country)
                .Select(eC => new SelectListItem { Value = eC.Id.ToString(), Text = eC.Country });

            model.MainPhoto = new PhotoManager().GetImage(null, Server.MapPath("~"), true);
        }

        [HttpPost]
        public ActionResult LoadRegions(string CountryId)
        {
            int IdCountry = Convert.ToInt32(CountryId);
            var country = store.GetAllCountries().FirstOrDefault(c => c.Id == IdCountry);
            var res = country.Regions.Select(eR => new SelectListItem { Value = eR.Id.ToString(), Text = eR.RegionName });
            return Json(res);
        }
        
        [HttpPost]
        public ActionResult Index(EventViewModel model)
        {
            if (ModelState.IsValid)
            {
                currentEvent = model;
                if (!User.Identity.IsAuthenticated)
                {
                    return Redirect("http://localhost:9456/Pages/LoginPage.aspx?publish=true");
                }
                else
                {                    
                    return RedirectToAction("SaveEvent");
                }
            }

            model.SelectedCountryId = 0;

            SetModelCollections(model);
            return View(model);
        }

        public ActionResult SaveEvent()
        {
            string userName = HttpContext.User.Identity.Name;
            var model = new EventModel
            {
                Title = currentEvent.Title,
                StartTime = currentEvent.StartTime,
                EndTime = currentEvent.EndTime,
                Type = new EventType { Id = currentEvent.SelectedTypeId },
                PersonsCategory = new EventPersonCategory { Id = currentEvent.SelectedPersonsCategoryId },
                Place = currentEvent.Place,
                MainPhoto = TempEventPhoto,
                Organizers = currentEvent.Organizers,
                Description = currentEvent.Description,
                ShortDescription = currentEvent.ShortDescription,
                Sponsors = currentEvent.Sponsors,
                IsCharitable = currentEvent.IsCharitable,
                isFreeEntrance = currentEvent.IsFreeEntrance,
                Enter = currentEvent.Enter,
                Publisher = new EventCustomer { Login = userName },
                City = new EventCity { Id = currentEvent.CityId}               
            };

            if (manager.CreateEvent(model))
            {
                return RedirectToAction("Events", "Profile");
            }
            return View("Index", currentEvent);
        }

        public ActionResult AdvancedSearch(SearchModel searchModel)
        {
            IEnumerable<EventModel> model = new List<EventModel>();
            model = store.GetFilteredEvents(searchModel).OrderByDescending(e=>e.Id);            

            foreach (var el in model)
            {
                el.MainPhoto = new PhotoManager().GetImage(el.MainPhoto, Server.MapPath("~"), true);
            }

            if (searchModel.Filter)
            {
                return PartialView("FilteredSearch", model);
            }

            ViewBag.Types = store.GetAllEventTypes().Select(t => t.Type);
            ViewBag.Categories = store.GetAllPersonCategories().Select(c => c.Category);
                        
            return View(model);
        }               
       
        public ActionResult LoadCountries(string term)
         {
             term = term.ToLower();
             var res = store.GetAllCountries().Where(c => c.Country.ToLower().Contains(term)).Select(c => new { name = c.Country }).Take(5);
             return Json(res, JsonRequestBehavior.AllowGet);
         }

        public ActionResult LoadRegionsTerm(string term, string country)
        {
            term = term.ToLower();
            var resC = store.GetAllCountries().FirstOrDefault(c => c.Country == country);
           
            if(resC!= default(EventCountry))
            {
                var res = resC.Regions.Where(r=>r.RegionName.ToLower().Contains(term)).Select(c => new { name = c.RegionName}).Take(5);
                return Json(res, JsonRequestBehavior.AllowGet);
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadCities(string term, string region)
        {
            term = term.ToLower();
            var resR = store.GetAllRegions().FirstOrDefault(c => c.RegionName == region);

            if (resR != default(EventRegion))
            {
                var res = resR.Cities.Where(r => r.CityName.ToLower().Contains(term)).Select(c => new { name = c.CityName, value = c.Id}).Take(5);
                return Json(res, JsonRequestBehavior.AllowGet);
            }

            return Json("", JsonRequestBehavior.AllowGet);           
        }

        public ActionResult LoadTypes()
        {
            var types = store.GetAllEventTypes().Select(t=>t.Type);
            return Json(types, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadCategories()
        {
            var categories = store.GetAllPersonCategories().Select(t => t.Category);
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        public string GetPhoto()
        {
            HttpPostedFile photo = System.Web.HttpContext.Current.Request.Files["HelpSectionImages"];
            if (photo != null && photo.ContentLength > 0)
            {
                var fileName = Path.GetFileName(photo.FileName);
                using (MemoryStream ms = new MemoryStream())
                {
                    photo.InputStream.CopyTo(ms);
                    byte[] imageBytes = ms.ToArray();

                    // Convert byte[] to Base64 String
                    return Convert.ToBase64String(imageBytes);
                }
            }
            return null;
        }

        public bool SetEventPhoto()
        {
            TempEventPhoto = GetPhoto();
            if(TempEventPhoto == null)
            {
                return false;
            } 
            return true;
        }

        public void CreateModel(IModel model, EventModel current)
        {
            model.Id = current.Id;
            model.Description = current.Description;
            model.EndTime = current.EndTime;
            model.Enter = current.Enter;
            model.IsCharitable = current.IsCharitable;
            model.IsFreeEntrance = current.isFreeEntrance;
            model.MainPhoto = new PhotoManager().GetImage(current.MainPhoto, Server.MapPath("~"), true);
            //model.Photos = current.Photos;
            model.Organizers = current.Organizers;
            model.Place = current.Place;
            model.ShortDescription = current.ShortDescription;
            model.Sponsors = current.Sponsors;
            model.StartTime = current.StartTime;
            model.Title = current.Title;            
        }

        public ActionResult EventDetails(int id)
        {
            var model = new EventDetailsViewModel();
            var current = new EventManager().GetEventById(id);
            CreateModel(model, current);
            
            model.City = current.City;
            model.Type = current.Type;
            model.PersonsCategory = current.PersonsCategory;
            model.Publisher = current.Publisher;
            model.Publisher.Photo = new PhotoManager().GetImage(current.Publisher.Photo, Server.MapPath("~"), true);
            model.Members = current.Members;
            model.MayAttend = current.MayAttend;
            model.NoAttend = current.NoAttend;
            model.Likes = current.Likes;
            model.DisLikes = current.Dislikes;
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = new EditViewModel();
            var current = new EventManager().GetEventById(id);
            CreateModel(model, current);

            model.SelectedTypeId = current.Type.Id;
            model.SelectedPersonsCategoryId = current.PersonsCategory.Id;
            model.EventTypes = store.GetAllEventTypes().OrderBy(t => t.Type)
                .Select(eType => new SelectListItem { Value = eType.Id.ToString(), Text = eType.Type });
            model.EventPersonCategories = store.GetAllPersonCategories().OrderBy(c => c.Category)
                .Select(eCat => new SelectListItem { Value = eCat.Id.ToString(), Text = eCat.Category });
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Redirect("http://localhost:9456/Pages/LoginPage.aspx?publish=true");
                }
                else
                {
                    currentEvent = model;
                    return RedirectToAction("ChangeEvent");
                }
            }

            return View(model);
        }

        public ActionResult ChangeEvent()
        {
            string userName = HttpContext.User.Identity.Name;

            ChangeModel model = new ChangeModel();
            model.Title = currentEvent.Title;
            model.StartTime = currentEvent.StartTime;
            model.Sponsors = currentEvent.Sponsors;
            model.ShortDescription = currentEvent.ShortDescription;
            model.SelectedTypeId = currentEvent.SelectedTypeId;
            model.SelectedPersonsCategoryId = currentEvent.SelectedPersonsCategoryId;
            model.Place = currentEvent.Place;
            model.Photos = currentEvent.Photos;
            model.Organizers = currentEvent.Organizers;
            model.IsFreeEntrance = currentEvent.IsFreeEntrance;
            model.IsCharitable = currentEvent.IsCharitable;
            model.Id = currentEvent.Id;
            model.Enter = currentEvent.Enter;
            model.EndTime = currentEvent.EndTime;
            model.Description = currentEvent.Description;
            
            //дописати додавання інших фото            
            if (manager.ChangeEvent(model))
            {
                return RedirectToAction("EventDetails", "Event", new {id = model.Id});
            }
            return View("Edit", currentEvent);
        }

        public bool ChangeMainPhoto(int id)
        {
            string newImage = GetPhoto();
            if (newImage != null)
            {
                return manager.ChangeMainPhoto(id, newImage);
            }
            return false;
        }

        public ActionResult DeleteEvent(int id)
        {
            if(manager.DeleteEvent(id))
            {
                return RedirectToAction("Events", "Profile");
            }
            return RedirectToAction("Edit", new { id = id });
        }

        public string AddLike(int id, bool isLike)
        {
            if(User.Identity.IsAuthenticated)
            {
                var login = User.Identity.Name;
                if ((manager as EventManager).AddLike(id, isLike, login))
                {                    
                    return "Success";
                }
                return "! You already voted";
            }
            return "! You need to login";            
        }
    }
}
