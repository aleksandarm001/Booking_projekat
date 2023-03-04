using InitialProject.CustomClasses;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xaml.Schema;

namespace InitialProject.Model
{
    public enum AccommodationType {appartment = 0, house, shack}
    public class Accommodation : ISerializable
    {
        private int accommodationID;
        private string name;
        private Location location;
        private AccommodationType accommodationType;
        private int maxGuestNumber;
        private int minReservationDays;
        private int daysBeforeCancelling = 1;
        private List<string> images = new List<string>(); 
        private List<Reservation> reservations = new List<Reservation>(); 

        public Accommodation(int accommodationID, string name, Location location, AccommodationType type, int maxGuestNumber, 
            int minReservationDays, int daysBeforeCancelling, List<String> images,
            List<Reservation> reservations)
        {
            this.accommodationID = accommodationID;
            this.name = name;
            this.location = location;
            this.accommodationType = type;
            this.maxGuestNumber = maxGuestNumber;
            this.minReservationDays = minReservationDays;
            this.daysBeforeCancelling = daysBeforeCancelling;
            this.images = images;
            this.Reservations = reservations;
        }
        public Accommodation()
        {
            this.location = new Location();
            this.images = new List<String>();
            this.Reservations = new List<Reservation>();
        }

        public string Name { get => name; set => name = value; }
        public Location Location { get => location; set => location = value; }
        public AccommodationType Type { get => accommodationType; set => accommodationType = value; }
        public int MaxGuestNumber { get => maxGuestNumber; set => maxGuestNumber = value; }
        public int MinReservationDays { get => minReservationDays; set => minReservationDays = value; }
        public int DaysBeforeCancelling { get => daysBeforeCancelling; set => daysBeforeCancelling = value; }
        public List<String> Images { get => images; set => images = value; }
        public List<Reservation> Reservations { get => reservations; set => reservations = value; }
        public int AccommodationID { get => accommodationID; set => accommodationID = value; }

        public string[] ToCSV()
        {
            string[] csvValues = { AccommodationID.ToString(),
                name,
                location.ToString(),
                accommodationType.ToString(),
                maxGuestNumber.ToString(),
                minReservationDays.ToString(),
                daysBeforeCancelling.ToString(),
                String.Join(";", images)};
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            AccommodationID = Convert.ToInt32(values[0]);
            name = values[1];
            location = location.fromStringToLocation(values[2]);
            accommodationType = (AccommodationType)Enum.Parse(typeof(AccommodationType), values[3]);
            maxGuestNumber = Convert.ToInt32(values[4]);
            minReservationDays = Convert.ToInt32(values[5]);
            daysBeforeCancelling = Convert.ToInt32(values[6]);
            images = values[7].Split(";").ToList<string>();
        }
    }
}
