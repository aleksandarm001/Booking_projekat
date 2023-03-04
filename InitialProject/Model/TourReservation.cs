using InitialProject.Serializer;
using System;
using System.Windows.Documents;

namespace InitialProject.Model
{
    public class TourReservation : ISerializable
    {
        public int TourId { get; set; } 
        public int ReservationId { get; set; } 
        public int NumberOfVisitors { get; set; }

        public TourReservation()
        {
        }

        public TourReservation(int tourId, int reservationId, int numberOfVisitors)
        {
            TourId = tourId;
            ReservationId = reservationId;
            NumberOfVisitors = numberOfVisitors;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                TourId.ToString(),
                ReservationId.ToString(),
                NumberOfVisitors.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            TourId = Convert.ToInt32(values[0]);
            ReservationId = Convert.ToInt32(values[1]);
            NumberOfVisitors = Convert.ToInt32(values[2]);
        
        }

      
    }
}
