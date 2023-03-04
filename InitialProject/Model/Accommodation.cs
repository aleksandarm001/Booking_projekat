using InitialProject.CustomClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xaml.Schema;

namespace InitialProject.Model
{
    public enum Type {appartment = 0, house, shack}
    public class Accommodation
    {
        private string accommodationID;
        private string name;
        private Location location;
        private Type type;
        private int maxGuestNumber;
        private int minReservationDays;
        private int daysBeforeCancelling = 1;
        private List<String> images = new List<String>(); 
        private List<Reservation> reservations = new List<Reservation>();

        public Accommodation(string accommodationID, string name, Location location, Type type, int maxGuestNumber, 
            int minReservationDays, int daysBeforeCancelling, List<String> images,
            List<Reservation> reservations)
        {
            this.accommodationID = accommodationID;
            this.name = name;
            this.location = location;
            this.type = type;
            this.maxGuestNumber = maxGuestNumber;
            this.minReservationDays = minReservationDays;
            this.daysBeforeCancelling = daysBeforeCancelling;
            this.images = images;
            this.Reservations = reservations;
        }
        public Accommodation()
        {
            this.accommodationID = "";
            this.name = "";
            this.location = new Location();
            this.type = Type.appartment;
            maxGuestNumber = 0;
            minReservationDays = 0;
            daysBeforeCancelling = 1;
            this.images = new List<String>();
            this.Reservations = new List<Reservation>();
        }

        public string Name { get => name; set => name = value; }
        public Location Location { get => location; set => location = value; }
        public Type Type { get => type; set => type = value; }
        public int MaxGuestNumber { get => maxGuestNumber; set => maxGuestNumber = value; }
        public int MinReservationDays { get => minReservationDays; set => minReservationDays = value; }
        public int DaysBeforeCancelling { get => daysBeforeCancelling; set => daysBeforeCancelling = value; }
        public List<String> Images { get => images; set => images = value; }
        public List<Reservation> Reservations { get => reservations; set => reservations = value; }

        public string[] ToCSV()
        {
            string[] csvValues = { accommodationID,
                name,
                location.ToString(),
                type.ToString(),
                maxGuestNumber.ToString(),
                minReservationDays.ToString(),
                daysBeforeCancelling.ToString(),
                String.Join(";", images)};
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            accommodationID = values[0];
            name = values[1];
            location = location.fromStringToLocation(values[2]);
            maxGuestNumber = Convert.ToInt32(values[3]);
            minReservationDays = Convert.ToInt32(values[4]);
            daysBeforeCancelling = Convert.ToInt32(values[5]);
            images = values[6].Split(";").ToList<string>();
        }
    }
}
