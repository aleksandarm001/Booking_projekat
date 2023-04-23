using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domen.CustomClasses
{
    public class AccommodationReservationDTO
    {
        public int AccommodationId { get; set; }
        public string AccommodationName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public AccommodationType TypeOfAccommodation { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public AccommodationReservationDTO(string accommodationName, string country, string city, AccommodationType accommodationType, DateTime checkInDate, DateTime checkOutDate)
        {
            AccommodationName = accommodationName;
            Country = country;
            City = city;
            TypeOfAccommodation = accommodationType;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
        }
    }

}
