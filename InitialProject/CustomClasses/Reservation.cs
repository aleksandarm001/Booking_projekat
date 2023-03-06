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
        public int AccommodationID { get; set; }
        public int UserID { get; set; }
        public int TourID { get; set; }
        public DateTime StartDate { get; set; }
        public string SStartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string SEndDate { get; set; }
        public float AvgRating { get; set; }


        public Reservation(int accommodationID, int userID, int tourID,DateTime startDate, DateTime endDate)
        {
            AccommodationID = accommodationID;
            UserID = userID;
            TourID = tourID;
            StartDate = startDate;
            EndDate = endDate;
            SStartDate = string.Format("{0:dd.MM.yyyy.}", StartDate);
            SEndDate = string.Format("{0:dd.MM.yyyy.}", EndDate);
        }
        public Reservation(int accommodationID, int userID, DateTime startDate, DateTime endDate)
        {
            AccommodationID = accommodationID;
            UserID = userID;
            TourID = -1;
            StartDate = startDate;
            EndDate = endDate;
            SStartDate = string.Format("{0:dd.MM.yyyy.}", StartDate);
            SEndDate = string.Format("{0:dd.MM.yyyy.}", EndDate);
        }
        public Reservation()
        {
            AccommodationID = -1;
            TourID = -1;
            UserID = -1;
            StartDate = new DateTime();
            EndDate = new DateTime();
            SStartDate = string.Format("{0:dd.MM.yyyy.}", StartDate);
            SEndDate = string.Format("{0:dd.MM.yyyy.}", EndDate);
        }
       
        public override string ToString()
        {
            return AccommodationID.ToString() + ";" + StartDate.ToString() + ";" + EndDate.ToString();
        }
       
        public string[] ToCSV()
        {
            string[] csvValues = {
                UserID.ToString(),
                AccommodationID.ToString(),
                TourID.ToString(),
                StartDate.ToString(),
                EndDate.ToString(),
                AvgRating.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            UserID = Convert.ToInt32(values[0]);
            TourID = Convert.ToInt32(values[1]);
            AccommodationID = Convert.ToInt32(values[2]);
            StartDate = DateTime.Parse(values[3]);
            SStartDate = string.Format("{0:dd.MM.yyyy.}", StartDate);
            EndDate = DateTime.Parse(values[4]); 
            SEndDate = string.Format("{0:dd.MM.yyyy.}", EndDate);
            AvgRating = float.Parse(values[5]);
        }
    }
}
