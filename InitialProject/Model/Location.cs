using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class Location
    {
        private string city;
        private string country;

        public string City 
        {
            get => city; 
            set => city = value; 
        }
        public string Country { 
            get => country; 
            set => country = value; 
        }

        public Location(string city, string country)
        {
            this.city = city;
            this.country = country;
        }
        public Location()
        {
            this.city = "";
            this.country = "";
        }

    }
}
