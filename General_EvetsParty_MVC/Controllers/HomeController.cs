using DataAccess.Concrete;
using DataModel;
using General_EvetsParty_MVC.Concrete;
using General_EvetsParty_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace General_EvetsParty_MVC.Controllers
{
    public class HomeController : Controller
    {
        DbStore store = new DbStore();

        public ActionResult Index()
        {
            IEnumerable<EventModel> model = new List<EventModel>();
            model = store.GetAllEvents().OrderByDescending(e => e.Id);
            if (model.Count() > 9)
            {
                model = model.Take(9);
            }

            foreach (var el in model)
            {
                el.MainPhoto = new PhotoManager().GetImage(el.MainPhoto, Server.MapPath("~"), true);
            }
            return View(model);
        }

        public ActionResult LoadCities(string term)
        {            
            term = term.ToLower();
            var res = store.GetAllCities().Where(c => c.CityName.ToLower().Contains(term)).Select(c => new {name = c.CityName, fullname = String.Format("{0}, {1}, {2}", c.CityName, c.Region.RegionName, c.Region.Country.Country)}).Take(6);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}
