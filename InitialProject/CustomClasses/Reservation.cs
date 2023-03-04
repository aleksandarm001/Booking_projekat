using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.CustomClasses
{
    public class Reservation
    {
        private string accommodationID;
        private DateTime startDate;
        private string sStartDate;
        private DateTime endDate;
        private string sEndDate;

        public Reservation(string accommodationID, DateTime startDate, DateTime endDate)
        {
            this.AccommodationID = accommodationID;
            this.StartDate = startDate;
            this.EndDate = endDate;
            sStartDate = string.Format("{0:dd.MM.yyyy.}", startDate);
            sEndDate = string.Format("{0:dd.MM.yyyy.}", endDate);
        }

        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }
        public string AccommodationID { get => accommodationID; set => accommodationID = value; }

        public override string ToString()
        {
            return accommodationID + ";" + startDate.ToString() + ";" + endDate.ToString();
        }
        public Reservation fromStringToReservation(string s)
        {
            string[] reservation = new string[3];
            reservation = s.Split(';');
            return new Reservation(reservation[0],DateTime.Parse(reservation[1]), DateTime.Parse(reservation[2]));
        }
        public string[] ToCSV()
        {
            string[] csvValues = {
                accommodationID,
                startDate.ToString(),
                endDate.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            accommodationID = values[0];
            startDate = DateTime.Parse(values[1]);
            sStartDate = string.Format("{0:dd.MM.yyyy.}", startDate);
            endDate = DateTime.Parse(values[2]); 
            sEndDate = string.Format("{0:dd.MM.yyyy.}", endDate);
        }
    }
}
