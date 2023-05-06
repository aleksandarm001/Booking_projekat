using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services.IServices
{
    public interface  IAddAccommodationService
    {
        List<Location> GetAllLocations();
        List<string> GetCities(List<Location> locations);
        List<string> GetCountries(List<Location> locations);
        Accommodation CreateNewAccommodation(int _userId, string accommodationName, string accommodationMaxGuests, string accommodationCancelationDays, string accommodationMinDays, string country, string city, string type);
        AccommodationType GetAccommodationType(string type);
        void SaveAccommodation(Accommodation accommodation);
        void SaveAccommodationImages(List<AccommodationImage> images);

    }
}
