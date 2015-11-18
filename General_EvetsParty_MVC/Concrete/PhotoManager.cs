using General_EvetsParty_MVC.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace General_EvetsParty_MVC.Concrete
{
    public class PhotoManager : IPhotoManager
    {
        string _defaultImage, _serverPath;
        public string DefaultImage(bool isEvent)
        {
            if (_defaultImage == null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // Convert Image to byte[]
                    string path = _serverPath;

                    if (isEvent)
                    {
                        path += "/images/defaultEventImg.png";
                    }
                    else
                    {
                        path += "/images/user.png";
                    }
                    
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

        public string GetImage(object img, string serverPath, bool isEvent)
        {
            string imgUser = "data:image/png;base64,";
            if (img != null)
            {
                imgUser += img;
            }
            else
            {
                _serverPath = serverPath;
                imgUser += DefaultImage(isEvent);
            }
            return imgUser;
        }
    }
}