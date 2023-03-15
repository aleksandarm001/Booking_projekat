using InitialProject.Model;
using System;
using System.Collections.Generic;
using InitialProject.Serializer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.CustomClasses
{
    public class Reservation : ISerializable
    {
        public int Id { get; set; }
        public int AccomodationId { get; set; }
        public int UserId { get; set; } //ID usera koji je rezervisao (guest1 ili guest2)
        public int TourId { get; set; }
        public float AvgRating { get; set; }
        public DateRange ReservationDateRange { get; set; }
        public int NumberOfGuests { get; set; }

        public Reservation() 
        {
            AccomodationId = -1;
            UserId = -1;
            TourId = -1;
            AvgRating = 0;
            ReservationDateRange = new DateRange();
            NumberOfGuests = 0;
        }

        public Reservation(int userId, int tourId,DateTime startDate, int numberOfVisitors)
        {
            UserId = userId;
            TourId = tourId;
            ReservationDateRange = new DateRange(startDate, numberOfVisitors);  
            NumberOfGuests = numberOfVisitors;
            AccomodationId = -1;
            AvgRating = 0;
        }

        public Reservation(int accomodationId, int userId, DateRange dateRange, int guestNumber)
        {
            AccomodationId = accomodationId;
            TourId = -1;
            UserId = userId;
            ReservationDateRange = dateRange;
            NumberOfGuests = guestNumber;
        }
        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                UserId.ToString(),
                TourId.ToString(),
                AccomodationId.ToString(),
                AvgRating.ToString(),
                NumberOfGuests.ToString(),
                ReservationDateRange.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);    
            UserId = Convert.ToInt32(values[1]);
            TourId = Convert.ToInt32(values[2]);
            AccomodationId = Convert.ToInt32(values[3]);
            AvgRating = float.Parse(values[4]);
            NumberOfGuests = Convert.ToInt32(values[5]);
            ReservationDateRange = ReservationDateRange.fromStringToDateRange(values[6]);
        }

    }
}
