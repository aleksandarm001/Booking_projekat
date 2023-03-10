using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using InitialProject.Model;
using InitialProject.Repository;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for RegisterNewAccommodation.xaml
    /// </summary>
    public partial class RegisterNewAccommodation : Window
    {

        private readonly AccommodationRepository _accommodationRepository;

        private readonly LocationRepository _locationRepository;

        public static ObservableCollection<string> Countries { get; set; }
        public static ObservableCollection<string> Cities { get; set; }

        public static ObservableCollection<Location> Locations { get; set; }

        private string _accommodationName;

        private string _accommodationCity;

        private string _accommodationCountry;

        private string _accommodationType;

        private int _maxGuests;

        private int _minDays;

        private int _cancelationDays;



        public string AccommodationName
        {
            get => _accommodationName;
            set
            {
                if (value != _accommodationName)
                {
                    _accommodationName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string AccommodationCity
        {
            get => _accommodationCity;
            set
            {
                if (value != _accommodationCity)
                {
                    _accommodationCity = value;
                    OnPropertyChanged();
                }
            }
        }

        public string AccommodationCountry
        {
            get => _accommodationCountry;
            set
            {
                if (value != _accommodationCountry)
                {
                    _accommodationCountry = value;
                    OnPropertyChanged();
                }
            }
        }

        public string AccommodationType
        {
            get => _accommodationType;
            set
            {
                if (value != _accommodationType)
                {
                    _accommodationType = value;
                    OnPropertyChanged();
                }
            }
        }

        public int AccomodationMaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        public int AccomodationReservationMinDays
        {
            get => _minDays;
            set
            {
                if (value != _minDays)
                {

                    _minDays = value;
                    OnPropertyChanged();
                }
            }
        }

        public int AccomodationCancelationDays
        {
            get => _cancelationDays;
            set
            {
                if (value != _cancelationDays)
                {
                    _cancelationDays = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RegisterNewAccommodation(Accommodation accommodation)
        {
            InitializeComponent();
            DataContext = this;

            _accommodationRepository = new AccommodationRepository();
            _locationRepository = new LocationRepository();

            Cities = new ObservableCollection<string>();
            Countries = new ObservableCollection<string>();


            _accommodationName = accommodation.Name;
            _accommodationCity = accommodation.Location.City;
            _accommodationCountry = accommodation.Location.Country;
            _accommodationType = accommodation.accommodationType.ToString();
            _maxGuests = accommodation.MaxGuestNumber;
            _minDays = accommodation.MinReservationDays;
            _cancelationDays = accommodation.DaysBeforeCancelling;

            ReadCitiesAndCountries();

        }

        private void NewAccommodationRegistration(object sender, RoutedEventArgs e)
        {
            /*
                Accommodation newAccommodation = new Accommodation(Name,)
                Accommodation registerdAccommodation = accommodationRepository.Save(newAccomodation);
            */
        }

        private void ReadCitiesAndCountries()
        {
            Cities.Clear();
            Countries.Clear();
            Cities.Add("");
            Countries.Add("");
            foreach (Location l in Locations)
            {
                Cities.Add(l.City);
                if (!Countries.Contains(l.Country))
                {
                    Countries.Add(l.Country);
                }
            }
        }

        private void FilterCities(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmbx = (ComboBox)sender;
            string country = "";
            try
            {
                if (cmbx.SelectedItem != null)
                {
                    country = cmbx.SelectedItem.ToString();
                }
                else
                {
                    cmbx.SelectedItem = 0;
                }
            }
            catch (System.NullReferenceException)
            {
                ReadCitiesAndCountries();
            }
            if (country == "")
            {

                ReadCitiesAndCountries();
            }
            else
            {
                Cities.Clear();
                Cities.Add("");
                foreach (Location loc in Locations)
                {
                    if (loc.Country == country)
                    {
                        Cities.Add(loc.City);
                    }
                }
                CityCmbx.SelectedIndex = 1;
            }
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
