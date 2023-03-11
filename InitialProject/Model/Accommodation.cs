using InitialProject.CustomClasses;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace InitialProject.Model
{
    public enum AccommodationType { Appartment = 0, House = 1, Shack = 2}
    public class Accommodation : ISerializable
    {
        public string Name { get; set; }
        public Location Location { get; set; }
        public AccommodationType accommodationType { get; set; }
        public int MaxGuestNumber { get; set; }
        public int MinReservationDays { get; set; }
        public int DaysBeforeCancelling { get; set; }
        public List<string> Images { get; set; }
        public List<Reservation> Reservations { get; set; }
        public int AccommodationID { get; set; }

        public BitmapImage Image { get; set; }
        


        public Accommodation(string name, Location location, AccommodationType type, int maxGuestNumber,
            int minReservationDays, int daysBeforeCancelling, List<String> images,
            List<Reservation> reservations)
        {
            
            Name = name;
            Location = location;
            accommodationType = type;
            MaxGuestNumber = maxGuestNumber;
            MinReservationDays = minReservationDays;
            DaysBeforeCancelling = daysBeforeCancelling;
            Images = images;
            Reservations = reservations;
        }

        
        ~Accommodation()
        {

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
            string testImage = "";

            if(Image != null)
            {
                byte[] bytes;
                using (MemoryStream stream = new MemoryStream())
                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(Image));
                    encoder.Save(stream);
                    bytes = stream.ToArray();
                    testImage = Convert.ToBase64String(bytes);
                }
            }

            string[] csvValues = {
                AccommodationID.ToString(),
                Name,
                Location.ToString(),
                accommodationType.ToString(),
                MaxGuestNumber.ToString(),
                MinReservationDays.ToString(),
                DaysBeforeCancelling.ToString(),
                testImage

            };  
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
            DaysBeforeCancelling = Convert.ToInt32(values[6]);
            //Images = values[7].Split(";").ToList<string>();
        }

        
    }
}
