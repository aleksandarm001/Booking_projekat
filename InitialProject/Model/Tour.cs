using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Model
{
    public class Tour : ISerializable
    {
        public int TourId { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int MaxGuestNumber { get; set; }
        public List<int> KeyPointsId { get; set; }
        public List<Location> KeyPoints { get; set; }
        public DateTime StartingDateTimes { get; set; }
        public int Duration { get; set; }
        public List<String> Images { get; set; }

        public Tour()
        {
            Name = "";
            Location = new Location();
            Description = "";
            Language = "";
            MaxGuestNumber = 1;
            KeyPoints = new List<Location>();
            StartingDateTimes = DateTime.Now;
            Duration = 1;
            Images = new List<String>();
        }

        public Tour(string name, Location location, string description, string language, int maxGuestNumber, List<Location> keyPoints, DateTime startingDateTimes, int duration, List<String> images)
        {
            Name = name;
            Location = location;
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
            LocationId = Convert.ToInt32(values[2]);
            Description = values[4];
            Language = values[5];
            MaxGuestNumber = Convert.ToInt32(values[6]);

            KeyPointsId = values[7].Split(";").Select(int.Parse).ToList();

            StartingDateTimes = Convert.ToDateTime(values[8]);
            Duration = Convert.ToInt32(values[9]);
            Images = values[10].Split(";").ToList<string>();

        }

        public string[] ToCSV()
        {
            string[] csvValues = { TourId.ToString(), Name, Location.ToString(), Description, Language, MaxGuestNumber.ToString(), KeyPoints.ToString(), StartingDateTimes.ToString(), Duration.ToString(), Images.ToString() };
            return csvValues;
        }
    }



}
