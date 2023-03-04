using InitialProject.Serializer;
using System;
using System.Collections.Generic;


namespace InitialProject.Model
{
    public class Tour : ISerializable
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int MaxGuestNumber { get; set; }
        public string KeyPoints { get; set; }
        public DateTime StartingDateTimes { get; set; }
        public int Duration { get; set; }
        public List<String> Images { get; set; }
        public int TourId { get; set; }

        public Tour()
        {
            Name = "";
            Country = "";
            City = "";
            Description = "";
            Language = "";
            MaxGuestNumber = 1;
            KeyPoints = "";
            StartingDateTimes = DateTime.Now;
            Duration = 1;
            Images = new List<String>();
        }

        public Tour(string name, string country, string city, string description, string language, int maxGuestNumber, string keyPoints, DateTime startingDateTimes, int duration, List<String> images)
        {
            Name = name;
            Country = country;
            City = city;
            Description = description;
            Language = language;
            MaxGuestNumber = maxGuestNumber;
            KeyPoints = keyPoints;
            StartingDateTimes = startingDateTimes;
            Duration = duration;
            Images = images;
        }



        public void FromCSV(string[] values)
        {
            TourId = Convert.ToInt32(values[0]);
            Name = values[1];
            Country = values[2];
            City = values[3];
            Description = values[4];
            Language = values[5];
            MaxGuestNumber = Convert.ToInt32(values[6]);
            KeyPoints = values[7];
            StartingDateTimes = Convert.ToDateTime(values[8]);
            Duration = Convert.ToInt32(values[9]);
            //Images = values[10]; 

        }

        public string[] ToCSV()
        {
            string[] csvValues = { TourId.ToString(), Name, Country, City, Description, Language, MaxGuestNumber.ToString(), KeyPoints, StartingDateTimes.ToString(), Duration.ToString(), Images.ToString() };
            return csvValues;
        }
    }



}
