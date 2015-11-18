using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace General_EvetsParty_MVC.Abstract
{
    public interface IPhotoManager
    {
        string GetImage(object img, string serverPath, bool isEvent);
    }
}