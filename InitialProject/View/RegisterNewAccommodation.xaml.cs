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
using Microsoft.Win32;

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
        /*
        private string _accommodationCity;

        private string _accommodationCountry;

        private string _accommodationType;
        */
        private int _maxGuests;

        private int _minDays;

        private int _cancelationDays;

        private BitmapImage _image;



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
        /*
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
        */
        public int AccommodationMaxGuests
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

        public BitmapImage AccommodationImage
        {
            get => _image;
            set
            {
                if (value != _image)
                {
                    _image = value;
                    OnPropertyChanged(nameof(AccommodationImage));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RegisterNewAccommodation()
        {
            InitializeComponent();
            DataContext = this;

            _accommodationRepository = new AccommodationRepository();
            _locationRepository = new LocationRepository();

            Locations = new ObservableCollection<Location>(_locationRepository.getAll());
            Cities = new ObservableCollection<string>();
            Countries = new ObservableCollection<string>();
            AccomodationCancelationDays = 1;
            ReadCitiesAndCountries();
                
        }

        private void NewAccommodationRegistration(object sender, RoutedEventArgs e)
        {

            Accommodation newAccommodation = new Accommodation();
            newAccommodation.Name = AccommodationName;
            newAccommodation.MaxGuestNumber = AccommodationMaxGuests;
            newAccommodation.DaysBeforeCancelling = AccomodationCancelationDays;
            newAccommodation.MinReservationDays = AccomodationReservationMinDays;
            newAccommodation.Location = new Location(CityComboBox.Text,CountriesComboBox.Text);
            newAccommodation.Image =  AccommodationImage;
            switch (TypeComboBox.Text)
            {
                case "Appartment":
                    newAccommodation.accommodationType = AccommodationType.Appartment;
                    break;

                case "Shack":
                    newAccommodation.accommodationType = AccommodationType.Shack;
                    break;

                case "House":
                    newAccommodation.accommodationType = AccommodationType.House;
                    break;
            }
            
                 _accommodationRepository.Save(newAccommodation);
            Close();
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
                CityComboBox.SelectedIndex = 1;
            }
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UploadImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
            if(openFileDialog.ShowDialog() == true)
            {
                  string imagePath = openFileDialog.FileName;
                  
                try
                {
                    AccommodationImage = new BitmapImage(new Uri(imagePath));
                    AccomodationImg.Source = AccommodationImage;
                }
                catch(Exception ex)
                {
                   MessageBox.Show("Error loading image: " + ex.Message);
                }
                

                
                
            }
        }
    }
}
