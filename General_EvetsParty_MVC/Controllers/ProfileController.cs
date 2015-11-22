using DataAccess.Abstract;
using DataAccess.Concrete;
using DataModel;
using General_EvetsParty_MVC.Concrete;
using General_EvetsParty_MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace General_EvetsParty_MVC.Controllers
{
    public class ProfileController : Controller
    {
        //
        // GET: /Profile/
        DbStore store = new DbStore();
        IUserManager manager = new DbUserManager();
        static EventCustomer current; 

        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("http://localhost:9456/Pages/LoginPage.aspx");
            }
            else
            {
                current = store.GetAllUsers().FirstOrDefault(c => c.Login == User.Identity.Name);
                current.Photo = new PhotoManager().GetImage(current.Photo, Server.MapPath("~"), false);
                return View(current);
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public string GetImage(object img, bool isEvent)
        {
            return new PhotoManager().GetImage(img, Server.MapPath("~"), isEvent);
        }

        public bool ChangePhoto()
        {
            HttpPostedFile photo = System.Web.HttpContext.Current.Request.Files["HelpSectionImages"];
            if(photo != null && photo.ContentLength > 0)
            {
                var fileName = Path.GetFileName(photo.FileName);
                using (MemoryStream ms = new MemoryStream())
                 {
                     photo.InputStream.CopyTo(ms);
                     byte[] imageBytes = ms.ToArray();

                     // Convert byte[] to Base64 String
                     string newImage = Convert.ToBase64String(imageBytes);
                     return manager.ChangePhoto(current.Id, newImage);
                 }
            }
            return false;
        }

        public ActionResult Events()
        {
            var model = store.GetAllEvents().Where(e => e.Publisher.Login == HttpContext.User.Identity.Name).OrderByDescending(e=>e.Id);
            foreach (var el in model)
            {
                el.MainPhoto = new PhotoManager().GetImage(el.MainPhoto, Server.MapPath("~"), true);
            }
            
            return View(model);
        }

        [HttpGet]
        public ActionResult Settings()
        {
            current = store.GetAllUsers().FirstOrDefault(c => c.Login == User.Identity.Name);

            EventCustomerEditModel cust = new EventCustomerEditModel
            {
                Id = current.Id,
                Name = current.Name,
                LastName = current.LastName,
                Mobile = current.Mobile,
                Birthday = current.Birthday.ToShortDateString(),
                Email = current.Email,
                Country = current.Country,
                Address = current.Address,
                ShowContacts = current.ShowContacts
            };

            return View(cust);
        }

        [HttpPost]
        public ActionResult Settings(EventCustomerEditModel customer)
        {
            if (manager.ChangeUserInfo(new EventCustomer{
                Id = customer.Id,
                Password = customer.Password,
                Name = customer.Name,
                LastName = customer.LastName,
                Birthday = Convert.ToDateTime(customer.Birthday),
                Mobile = customer.Mobile, 
                Email = customer.Email,
                Country = customer.Country,
                Address = customer.Address,
                ShowContacts = customer.ShowContacts
            }))
            {
                return RedirectToAction("Index");
            }

            return View(customer);
            
        }
    }
}
