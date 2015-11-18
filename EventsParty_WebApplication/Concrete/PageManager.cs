using DataAccess.Concrete;
using EventsParty_WebApplication.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace EventsParty_WebApplication.Concrete
{
    public class PageManager : IManager
    {
        public void SetRoles(DbUserManager userManager, string login)
        {
            IIdentity identity = new GenericIdentity(login);
            string[] roles = userManager.GetRoles(login);
            HttpContext.Current.User = new GenericPrincipal(identity, roles);
        }

        public void RefreshPrincipal()
        {
            IPrincipal principal = HttpContext.Current.User;
            if (principal != null && principal.Identity != null && principal.Identity.IsAuthenticated)
            {
                SetRoles(new DbUserManager(), principal.Identity.Name);
            }
        }

        string _defaultImage, _serverPath;
        public string DefaultImage
        {
            get
            {
                if (_defaultImage == null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        // Convert Image to byte[]
                        string path = _serverPath + "/Images/defaultUserPhoto.png";
                        using (FileStream fs = File.Open(path, FileMode.Open))
                        {
                            fs.CopyTo(ms);
                        }
                        byte[] imageBytes = ms.ToArray();

                        // Convert byte[] to Base64 String
                        _defaultImage = Convert.ToBase64String(imageBytes);
                    }
                }
                return _defaultImage;
            }
        }

        public string GetImage(object img, string serverPath)
        {
            string imgUser = "data:image/png;base64,";
            if (img != null)
            {
                imgUser += (string)img;
            }
            else
            {
                _serverPath = serverPath;
                imgUser += DefaultImage;
            }
            return imgUser;
        }
    }
}