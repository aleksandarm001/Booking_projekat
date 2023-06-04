using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domen.CustomClasses
{
    public class ReservationReportDTO
    {
        public string AccommodationName { get; set; }   
        public Location AccommdoationLocation { get; set; }
        public DateRange ReservationDateRange { get; set; }   
        public int NumberOfGuests { get; set; }
        public int DaysBeforeCancelling { get; set; }

        public ReservationReportDTO(string accommodationName, Location accommdoationLocation, DateRange reservationDateRange, int numberOfGuests, int daysBeforeCancelling)
        {
            AccommodationName = accommodationName;
            AccommdoationLocation = accommdoationLocation;
            ReservationDateRange = reservationDateRange;
            NumberOfGuests = numberOfGuests;
            DaysBeforeCancelling = daysBeforeCancelling;
        } 
    }
}
