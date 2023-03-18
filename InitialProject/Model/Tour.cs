using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InitialProject.Model
{
    public class Tour : ISerializable
    {
        public int TourId { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public string Description { get; set; }
        public Language Language { get; set; }
        public int MaxGuestNumber { get; set; }
        public DateTime StartingDateTime { get; set; }
        public int Duration { get; set; }

        public bool TourStarted { get; set; }

        //public List<String> Images { get; set; }

        public Tour()
        {
            Name = "";
            Location = new Location();
            Description = "";
            Language = new Language();
            MaxGuestNumber = 1;
            StartingDateTime = DateTime.Now;
            Duration = 1;
            //Images = new List<String>();
        }

        public Tour(string name, Location location, string description, Language language, int maxGuestNumber, DateTime startingDateTimes, int duration)
        {
            Name = name;
            Location = location;
            Description = description;
            Language = language;
            MaxGuestNumber = maxGuestNumber;
            StartingDateTime = startingDateTimes;
            Duration = duration;
            //Images = images;
        }

        public void FromCSV(string[] values)
        {
            TourId = Convert.ToInt32(values[0]);
            Name = values[1];
            Location = Location.fromStringToLocation(values[2]);
            Description = values[3];
            Language = Language.fromStringToLanguage(values[4]);
            MaxGuestNumber = Convert.ToInt32(values[5]);
            StartingDateTime = DateTime.Parse(values[6]); 
            Duration = Convert.ToInt32(values[7]);
            //Images = values[9].Split(";").ToList<string>();

        }


        public string DateTimeToCSV(List<DateTime> startingDateTimes) 
        {
            StringBuilder dateTimes = new StringBuilder();
            foreach (DateTime startingDateTime in startingDateTimes)
            {
                dateTimes.Append(";" + startingDateTime.ToString());
            }
            
            return dateTimes.ToString().Substring(1);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { TourId.ToString(), Name, Location.ToString(), Description, Language.ToString(), MaxGuestNumber.ToString(), StartingDateTime.ToString(), Duration.ToString() };
            return csvValues;
        }
    }



}
