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
        private string accomodationID;
        private string naziv;
        private Location location;
        private Type type;
        private int maxGuestNumber;
        private int minReservationDays;
        private int daysBeforeCancelling = 1;
        private List<Image> images = new List<Image>();

        public Accommodation(string naziv, Location location, Type type, int maxGuestNumber, int minReservationDays, int daysBeforeCancelling, List<Image> images)
        {
            this.naziv = naziv;
            this.location = location;
            this.type = type;
            this.maxGuestNumber = maxGuestNumber;
            this.minReservationDays = minReservationDays;
            this.daysBeforeCancelling = daysBeforeCancelling;
            this.images = images;
        }
        public Accommodation()
        {
            this.naziv = "";
            this.location = new Location();
            this.type = Type.appartment;
            maxGuestNumber = 0;
            minReservationDays = 0;
            daysBeforeCancelling = 1;
            this.images = new List<Image>();
        }

        public string Naziv { get => naziv; set => naziv = value; }
        public Location Location { get => location; set => location = value; }
        public Type Type { get => type; set => type = value; }
        public int MaxGuestNumber { get => maxGuestNumber; set => maxGuestNumber = value; }
        public int MinReservationDays { get => minReservationDays; set => minReservationDays = value; }
        public int DaysBeforeCancelling { get => daysBeforeCancelling; set => daysBeforeCancelling = value; }
        public List<Image> Images { get => images; set => images = value; }


    }
}
