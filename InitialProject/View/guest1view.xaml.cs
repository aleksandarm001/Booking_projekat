using InitialProject.CustomClasses;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using System.Windows;
using System.Collections.ObjectModel;
using InitialProject.Model;
using System.Diagnostics.Metrics;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Specialized;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for guest1view.xaml
    /// </summary>
    public partial class Guest1View : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public static ObservableCollection<Location> Locations { get; set; }
        public static ObservableCollection<string> Cities { get; set; }
        public static ObservableCollection<string> Countries { get; set; }
        public string AccommodationName { get; set; }

        private ObservableCollection<Accommodation> _accommodations;
        public ObservableCollection<Accommodation> Accommodations
        {
            get { return _accommodations; }
            set
            {
                _accommodations = value;
                OnPropertyChanged(nameof(Accommodations));
            }
        }

        public Accommodation SelectedAccommodation { get; set; }
        public DateTime SelectedStartDate { get; set; }
        public DateTime SelectedEndDate { get; set; }
        private readonly AccommodationRepository _repository;
        private readonly LocationRepository _locationRepository;

        private string _city;
        public string City
        {
            get => _city;
            set
            {
                if(value != _city)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _country;
        public string Country
        {
            get => _country;
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged();
                }
            }
        }
        public int NumberOfGuests { get; set; }
        private string _strNumberOfGuests;
        public string StrNumberOfGuests
        {
            get => _strNumberOfGuests;
            set
            {
                if (value != _strNumberOfGuests)
                {
                    try
                    {
                        int _numberOfGuests;
                        int.TryParse(value, out _numberOfGuests);
                        NumberOfGuests = _numberOfGuests;
                    }
                    catch (Exception) { }
                    _strNumberOfGuests = value;
                    OnPropertyChanged();
                }
            }
        }
        public int ReservationDays { get; set; }
        private string _strReservationDays;
        public string StrReservationDays
        {
            get => _strReservationDays;
            set
            {
                if (value != _strReservationDays)
                {
                    try
                    {
                        int _reservationDays;
                        int.TryParse(value, out _reservationDays);
                        ReservationDays = _reservationDays;
                    }
                    catch (Exception) { }
                    _strReservationDays = value;
                    OnPropertyChanged();
                }
            }
        }
        public Guest1View()
        {
            InitializeComponent();
            DataContext = this;
            _repository = new AccommodationRepository();
            _locationRepository = new LocationRepository();
            Accommodations = new ObservableCollection<Accommodation>(_repository.getAll());
            Locations = new ObservableCollection<Location>(_locationRepository.getAll());
            Cities = new ObservableCollection<string>();
            Countries = new ObservableCollection<string>();
            SelectedStartDate = DateTime.Today;
            SelectedEndDate = DateTime.Today;
            ReadCitiesAndCountries();
        }
        private void ReadCitiesAndCountries()
        {
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
        private void ApplyAdditionalSearch(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Accommodation> tempAccommodations = new ObservableCollection<Accommodation>(_repository.getAll());
            string _country = CountryCmbx.Text;
            string _city = CityCmbx.Text;

            if(AppartmentCheckBox.IsChecked == true && HouseCheckBox.IsChecked == true && ShackCheckBox.IsChecked == false)
            {
                var filteredCollectionByTypes = tempAccommodations.Where(a =>
                    (a.accommodationType == AccommodationType.Appartment || a.accommodationType == AccommodationType.House));
                tempAccommodations = new ObservableCollection<Accommodation>(filteredCollectionByTypes);
            }else if(AppartmentCheckBox.IsChecked == true && ShackCheckBox.IsChecked == true && HouseCheckBox.IsChecked == false)
            {
                var filteredCollectionByTypes = tempAccommodations.Where(a =>
                    (a.accommodationType == AccommodationType.Appartment || a.accommodationType == AccommodationType.Shack));
                tempAccommodations = new ObservableCollection<Accommodation>(filteredCollectionByTypes);
            }
            else if (HouseCheckBox.IsChecked == true && ShackCheckBox.IsChecked == true && AppartmentCheckBox.IsChecked == false)
            {
                var filteredCollectionByTypes = tempAccommodations.Where(a =>
                    (a.accommodationType == AccommodationType.House || a.accommodationType == AccommodationType.Shack));
                tempAccommodations = new ObservableCollection<Accommodation>(filteredCollectionByTypes);
            }
            else if (HouseCheckBox.IsChecked == true && ShackCheckBox.IsChecked == true && AppartmentCheckBox.IsChecked == true)
            {
                /* DO NOTHING */
            }
            else
            {
               var filteredCollectionByTypes = tempAccommodations.Where(a =>
               (AppartmentCheckBox.IsChecked == false || a.accommodationType == AccommodationType.Appartment) &&
               (HouseCheckBox.IsChecked == false || a.accommodationType == AccommodationType.House) &&
               (ShackCheckBox.IsChecked == false || a.accommodationType == AccommodationType.Shack));
               tempAccommodations = new ObservableCollection<Accommodation>(filteredCollectionByTypes);
            }

            var filteredCollection = tempAccommodations.Where(a =>
                (string.IsNullOrEmpty(AccommodationName) || a.Name.ToLower().Contains(AccommodationName.ToLower())) &&
                (string.IsNullOrEmpty(_country) || a.Location.Country == _country) &&
                (string.IsNullOrEmpty(_city) || a.Location.City == _city) &&
                (string.IsNullOrEmpty(GuestNumber.Text) || a.MaxGuestNumber >= Convert.ToInt32(GuestNumber.Text)) &&
                (string.IsNullOrEmpty(DaysReservation.Text) || a.MinReservationDays <= Convert.ToInt32(DaysReservation.Text)));

            Accommodations = new ObservableCollection<Accommodation>(filteredCollection);
           
        }

        private void Filter_Cities(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmbx = (ComboBox)sender;
            string country = "";
            try
            {
                if(cmbx.SelectedItem != null)
                {
                    country = cmbx.SelectedItem.ToString();
                }
                else
                {
                    cmbx.SelectedItem = 0;
                }
            }catch(System.NullReferenceException)
            {
                ReadCitiesAndCountries();
            }
            if (country == "")
            {
                Cities.Clear();
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
        private void Filter_Countries(object sender, SelectionChangedEventArgs e)
        {
            
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}
