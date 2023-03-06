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
        public DateTime StartDate { get; set; }
        public string SStartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string SEndDate { get; set; }

        public Reservation(int accommodationID, DateTime startDate, DateTime endDate)
        {
            AccommodationID = accommodationID;
            StartDate = startDate;
            EndDate = endDate;
            SStartDate = string.Format("{0:dd.MM.yyyy.}", StartDate);
            SEndDate = string.Format("{0:dd.MM.yyyy.}", EndDate);
        }

        public Reservation()
        {
            AccommodationID = 0;
            StartDate = new DateTime();
            EndDate = new DateTime();
            SStartDate = string.Format("{0:dd.MM.yyyy.}", StartDate);
            SEndDate = string.Format("{0:dd.MM.yyyy.}", EndDate);
        }
       
        public override string ToString()
        {
            return AccommodationID.ToString() + ";" + StartDate.ToString() + ";" + EndDate.ToString();
        }
        public Reservation fromStringToReservation(string s)
        {
            string[] reservation = new string[3];
            reservation = s.Split(';');
            return new Reservation(Convert.ToInt32(reservation[0]),DateTime.Parse(reservation[1]), DateTime.Parse(reservation[2]));
        }
        public string[] ToCSV()
        {
            string[] csvValues = {
                AccommodationID.ToString(),
                StartDate.ToString(),
                EndDate.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            AccommodationID = Convert.ToInt32(values[0]);
            StartDate = DateTime.Parse(values[1]);
            SStartDate = string.Format("{0:dd.MM.yyyy.}", StartDate);
            EndDate = DateTime.Parse(values[2]); 
            SEndDate = string.Format("{0:dd.MM.yyyy.}", EndDate);
        }
    }
}
