using System;

namespace InitialProject.Domen.Model
{
    public class Location : ValidationBase, ISerializable
    {
        private int id;
        private string country;
        private string city;

        public int Id
        {
            get { return id; }

            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }
        public string Country       
        {
            get { return country; }

            set
            {
                if (country != value)
                {
                    country = value;
                    OnPropertyChanged(nameof(Country));
                }
            }
        }
        public string City
        {
            get { return city; }

            set
            {
                if (city != value)
                {
                    city = value;
                    OnPropertyChanged(nameof(City));
                }
            }
        }


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
        public string ToString2()
        {
            return City + " " + Country;
        }
        public string ToString3()
        {
            return City + ", " + Country;
        }
        public Location fromStringToLocation(string s)
        {
            if (s.Equals(String.Empty))
            {
                return new Location(string.Empty, string.Empty);
            }
            string[] locations = new string[2];
            locations = s.Split(';');
            return new Location(locations[0], locations[1]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                City,
                Country};

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            City = values[1];
            Country = values[2];
        }

        protected override void ValidateSelf()
        {
            if (string.IsNullOrWhiteSpace(this.Country))
            {
                this.ValidationErrors["Country"] = "Country should not be empty.";
            }

            if (string.IsNullOrWhiteSpace(this.Country))
            {
                this.ValidationErrors["City"] = "City should not be empty.";
            }
        }
    }
}
