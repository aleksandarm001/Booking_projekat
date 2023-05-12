using System;
using InitialProject.Domen;

namespace InitialProject.CustomClasses
{
    public class Reservation : ISerializable
    {
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public float AvgRating { get; set; }
        public DateRange ReservationDateRange { get; set; }
        public int NumberOfGuests { get; set; }

        public Reservation() 
        {
            UserId = -1;
            AvgRating = 0;
            ReservationDateRange = new DateRange();
            NumberOfGuests = 0;
        }

        public Reservation(int userId,  DateTime startDate, int numberOfVisitors)
        {
            UserId = userId;
            ReservationDateRange = new DateRange(startDate, numberOfVisitors);  
            NumberOfGuests = numberOfVisitors;
            AvgRating = 0;
        }

        public Reservation(DateRange dateRange, int guestNumber, int userId)
        {
            UserId = userId;
            ReservationDateRange = dateRange;
            NumberOfGuests = guestNumber;
            
        }
        public string[] ToCSV()
        {
            string[] csvValues = {
                ReservationId.ToString(),
                ReservationDateRange.ToString(),
                NumberOfGuests.ToString(),
                UserId.ToString(),
                AvgRating.ToString(),
            };
                
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            ReservationId = Convert.ToInt32(values[0]);
            ReservationDateRange = ReservationDateRange.fromStringToDateRange(values[1]);
            NumberOfGuests = Convert.ToInt32(values[2]);
            UserId = Convert.ToInt32(values[3]);
            AvgRating = float.Parse(values[4]);
        }

    }
}
