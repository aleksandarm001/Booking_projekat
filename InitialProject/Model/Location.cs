using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InitialProject.Serializer;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class Location
    {
        public string City { get; set; }
        public string Country { get; set; }

        public Location(string city, string country)
        {
            City = city;
            Country = country;
        }
        public Location()
        {
            City = "";
            Country = "";
        }

        public override string ToString()
        {
            return City + ";" + Country;
        }
        public Location fromStringToLocation(string s)
        {
            string[] locations = new string[2];
            locations = s.Split(';');
            return new Location(locations[0], locations[1]);
        }

    }
}
