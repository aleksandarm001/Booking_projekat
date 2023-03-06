using InitialProject.CustomClasses;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;


namespace InitialProject.Model
{
    public enum AccommodationType { Appartment = 0, House, Shack }
    public class Accommodation : ISerializable
    {
        public string Name { get; set; }
        public Location Location { get; set; }
        public AccommodationType accommodationType { get; set; }
        public int MaxGuestNumber { get; set; }
        public int MinReservationDays { get; set; }
        public int DaysBeforeCancelling { get; set; }
        public List<String> Images { get; set; }
        public List<Reservation> Reservations { get; set; }
        public int AccommodationID { get; set; }


        public Accommodation(int accommodationID, string name, Location location, AccommodationType type, int maxGuestNumber,
            int minReservationDays, int daysBeforeCancelling, List<String> images,
            List<Reservation> reservations)
        {
            AccommodationID = accommodationID;
            Name = name;
            Location = location;
            accommodationType = type;
            MaxGuestNumber = maxGuestNumber;
            MinReservationDays = minReservationDays;
            DaysBeforeCancelling = daysBeforeCancelling;
            Images = images;
            Reservations = reservations;
        }
        public Accommodation()
        {
            AccommodationID = 0;
            Name = "";
            Location = new Location();
            accommodationType = 0;
            MaxGuestNumber = 0;
            MinReservationDays = 0;
            DaysBeforeCancelling = 0;
            Images = new List<String>();
            Reservations = new List<Reservation>();
        }


        public string[] ToCSV()
        {
            string[] csvValues = { 
                AccommodationID.ToString(),
                Name,
                Location.ToString(),
                accommodationType.ToString(),
                MaxGuestNumber.ToString(),
                MinReservationDays.ToString()};
            //DaysBeforeCancelling.ToString()
            //string.Join(";", Images)
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            AccommodationID = Convert.ToInt32(values[0]);
            Name = values[1];
            Location = Location.fromStringToLocation(values[2]);
            accommodationType = (AccommodationType)Enum.Parse(typeof(AccommodationType), values[3]);
            MaxGuestNumber = Convert.ToInt32(values[4]);
            MinReservationDays = Convert.ToInt32(values[5]);
            //DaysBeforeCancelling = Convert.ToInt32(values[6]);
            //Images = values[7].Split(";").ToList<string>();
        }
    }
}
