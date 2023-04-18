using System;
using InitialProject.Domen;

namespace InitialProject.CustomClasses
{
    public class Reservation : ISerializable
    {
        public int ReservationId { get; set; }
        
        public int OwnerId { get; set; }
        public int AccommodationId { get; set; }
        public int TourId { get; set; }
        public int UserId { get; set; }
        public float AvgRating { get; set; }
        public DateRange ReservationDateRange { get; set; }
        public int NumberOfGuests { get; set; }
        public int VoucherId { get; set; }

        public Reservation() 
        {
            UserId = -1;
            OwnerId= -1;
            TourId = -1;
            AvgRating = 0;
            ReservationDateRange = new DateRange();
            NumberOfGuests = 0;
            VoucherId = 0;
        }

        public Reservation(int userId, int tourId, DateTime startDate, int numberOfVisitors, int voucherId)
        {
            UserId = userId;
            TourId = tourId;
            ReservationDateRange = new DateRange(startDate, numberOfVisitors);  
            NumberOfGuests = numberOfVisitors;
            AvgRating = 0;
            VoucherId = voucherId;
        }

        public Reservation(DateRange dateRange, int guestNumber, int userId,int accommodationId, int ownerId)
        {
            TourId = -1;
            UserId = userId;
            ReservationDateRange = dateRange;
            NumberOfGuests = guestNumber;
            AccommodationId = accommodationId;
            OwnerId = ownerId;
        }
        public string[] ToCSV()
        {
            string[] csvValues = {
                ReservationId.ToString(),
                AccommodationId.ToString(),
                TourId.ToString(),
                AvgRating.ToString(),
                NumberOfGuests.ToString(),
                ReservationDateRange.ToString(),
                UserId.ToString(),
                VoucherId.ToString(),
                OwnerId.ToString()
            };
                
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            ReservationId = Convert.ToInt32(values[0]);   
            AccommodationId= Convert.ToInt32(values[1]);
            TourId = Convert.ToInt32(values[2]);
            AvgRating = float.Parse(values[3]);
            NumberOfGuests = Convert.ToInt32(values[4]);
            ReservationDateRange = ReservationDateRange.fromStringToDateRange(values[5]);
            UserId = Convert.ToInt32(values[6]);
            VoucherId = Convert.ToInt32(values[7]);
            OwnerId= Convert.ToInt32(values[8]);
        }

    }
}
