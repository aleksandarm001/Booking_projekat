using InitialProject.Serializer;
using System;

namespace InitialProject.CustomClasses
{
    public class AccomodationReservation : ISerializable
    {
        private string accommodationID;
        private DateTime startDate;
        private string sStartDate;
        private DateTime endDate;
        private string sEndDate;

        public AccomodationReservation(string accommodationID, DateTime startDate, DateTime endDate)
        {
            this.accommodationID = accommodationID;
            this.startDate = startDate;
            this.endDate = endDate;
            sStartDate = string.Format("{0:dd.MM.yyyy.}", startDate);
            sEndDate = string.Format("{0:dd.MM.yyyy.}", endDate);
        }

        public AccomodationReservation()
        {
            startDate = new DateTime();
            endDate = new DateTime();
        }

        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }
        public string AccommodationID { get => accommodationID; set => accommodationID = value; }

        public override string ToString()
        {
            return accommodationID + ";" + startDate.ToString() + ";" + endDate.ToString();
        }
        public AccomodationReservation fromStringToReservation(string s)
        {
            string[] reservation = new string[3];
            reservation = s.Split(';');
            return new AccomodationReservation(reservation[0], DateTime.Parse(reservation[1]), DateTime.Parse(reservation[2]));
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
