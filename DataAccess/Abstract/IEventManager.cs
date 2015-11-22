using ChangeModelLibrary;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IEventManager
    {
        bool CreateEvent(EventModel model);
        EventModel GetEventById(int id);
        bool ChangeEvent(ChangeModel changedModel);
        bool ChangeMainPhoto(int id, string newImage);
        bool DeleteEvent(int id);
    }
}
