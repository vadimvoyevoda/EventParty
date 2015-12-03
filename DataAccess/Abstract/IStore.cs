using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using SearchModelLibrary;

namespace DataAccess.Abstract
{
    public interface IStore
    {        
        List<EventCustomer> GetAllUsers();
        List<EventCustomer> GetBannedUsers();
        List<EventCustomer> GetDeletedUsers();
        List<EventRating> GetAllRatings();
        List<EventPermission> GetAllPermissions();
        List<EventPersonCategory> GetAllPersonCategories();
        List<EventType> GetAllEventTypes();
        List<EventCountry> GetAllCountries();
        List<EventCity> GetAllCities();
        List<EventRegion> GetAllRegions();
        List<EventModel> GetAllEvents();
        List<EventModel> GetFilteredEvents(SearchModel model = null, int count = -1, int skip = -1);
        int GetEventsCount();
    }
}
