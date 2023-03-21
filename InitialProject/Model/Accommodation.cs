﻿using InitialProject.CustomClasses;
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
    public enum AccommodationType { Apartment = 0, House = 1, Shack = 2}
    public class Accommodation : ISerializable
    {
        public int UserId { get; set; } //ID Vlasnika smestaja
        public string Name { get; set; }
        public Location Location { get; set; }
        public AccommodationType AccommodationType { get; set; }
        public int MaxGuestNumber { get; set; }
        public int MinReservationDays { get; set; }
        public int DaysBeforeCancelling { get; set; }
        public List<AccommodationImage> Images { get; set; }
        public List<Reservation> Reservations { get; set; }
        public int AccommodationID { get; set; }
                


        public Accommodation(int userId,string name, Location location, AccommodationType type, int maxGuestNumber,
            int minReservationDays, int daysBeforeCancelling, List<AccommodationImage> images,
            List<Reservation> reservations)
        {
            UserId= userId;
            Name = name;
            Location = location;
            AccommodationType = type;
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
            UserId = 0; //defaultna vrednost pre kreiranja sigin forme za vise korisnika
            AccommodationID = 0;
            Name = "";
            Location = new Location();
            AccommodationType = 0;
            MaxGuestNumber = 0;
            MinReservationDays = 0;
            DaysBeforeCancelling = 0;
            Images = new List<AccommodationImage>();
            Reservations = new List<Reservation>();
        }


        public string[] ToCSV()
        {

            string[] csvValues = {
                AccommodationID.ToString(),
                Name,
                Location.ToString(),
                AccommodationType.ToString(),
                MaxGuestNumber.ToString(),
                MinReservationDays.ToString(),
                DaysBeforeCancelling.ToString(),

            };  
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            AccommodationID = Convert.ToInt32(values[0]);
            Name = values[1];
            Location = Location.fromStringToLocation(values[2]);
            AccommodationType = (AccommodationType)Enum.Parse(typeof(AccommodationType), values[3]);
            MaxGuestNumber = Convert.ToInt32(values[4]);
            MinReservationDays = Convert.ToInt32(values[5]);
            DaysBeforeCancelling = Convert.ToInt32(values[6]);
            //Images = values[7].Split(";").ToList<string>();
        }

        
    }
}
