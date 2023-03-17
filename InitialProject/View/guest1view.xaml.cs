﻿using InitialProject.CustomClasses;
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
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.Profile;

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
        public Accommodation SelectedAccommodation { get; set; }
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
        private string _selectedCity;
        public string SelectedCity
        {
            get => _selectedCity;
            set
            {
                if(value != _selectedCity)
                {
                    _selectedCity = value;
                    OnPropertyChanged("SelectedCity");
                }
            }
        }
        private string _selectedCountry;
        public string SelectedCountry
        {
            get => _selectedCountry;
            set
            {
                if (value != _selectedCountry)
                {
                    _selectedCountry = value;
                    FilterCities();
                    OnPropertyChanged("SelectedCountry");
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
        private bool _isAppartmentSelected;
        public bool IsAppartmentSelected
        {
            get => _isAppartmentSelected;
            set
            {
                if (_isAppartmentSelected != value)
                {
                    _isAppartmentSelected = value;
                    OnPropertyChanged(nameof(IsAppartmentSelected));
                }
            }
        }
        private bool _isHouseSelected;
        public bool IsHouseSelected
        {
            get => _isHouseSelected;
            set
            {
                if (_isHouseSelected != value)
                {
                    _isHouseSelected = value;
                    OnPropertyChanged(nameof(IsHouseSelected));
                }
            }
        }
        private bool _isShackSelected;
        public bool IsShackSelected
        {
            get => _isShackSelected;
            set
            {
                if (_isShackSelected != value)
                {
                    _isShackSelected = value;
                    OnPropertyChanged(nameof(IsShackSelected));
                }
            }
        }
        public Guest1View(ObservableCollection<Location> locations, ObservableCollection<Accommodation> accommodations)
        {
            InitializeComponent();
            DataContext = this;
            Accommodations = accommodations;
            Locations = locations;
            Cities = new ObservableCollection<string>();
            Countries = new ObservableCollection<string>();
            ReadCitiesAndCountries();
        }
        private bool AccommodationTypeFilter(Accommodation accommodation)
        {
            if (IsAppartmentSelected && IsHouseSelected && IsShackSelected)
            {
                return accommodation.accommodationType == AccommodationType.Appartment ||
                    accommodation.accommodationType == AccommodationType.House ||
                    accommodation.accommodationType == AccommodationType.Shack;
            }
            else if (IsAppartmentSelected && IsHouseSelected)
            {
                return accommodation.accommodationType == AccommodationType.Appartment ||
                    accommodation.accommodationType == AccommodationType.House;
            }
            else if (IsAppartmentSelected && IsShackSelected)
            {
                return accommodation.accommodationType == AccommodationType.Appartment ||
                    accommodation.accommodationType == AccommodationType.Shack;
            }
            else if (IsHouseSelected && IsShackSelected)
            {
                return accommodation.accommodationType == AccommodationType.House ||
                    accommodation.accommodationType == AccommodationType.Shack;
            }
            else if (IsAppartmentSelected)
            {
                return accommodation.accommodationType == AccommodationType.Appartment;
            }
            else if (IsHouseSelected)
            {
                return accommodation.accommodationType == AccommodationType.House;
            }
            else if (IsShackSelected)
            {
                return accommodation.accommodationType == AccommodationType.Shack;
            }
            else
            {
                return true;
            }
        }
        private void ApplyAdditionalSearch(object sender, RoutedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(Accommodations);
            view.Filter = (obj) =>
            {
                Accommodation accommodation = obj as Accommodation;
                if (accommodation == null) return false;
                return (AccommodationNameFilter(accommodation) &&
                    CountryFilter(accommodation) &&
                    CityFilter(accommodation) &&
                    GuestNumberFilter(accommodation) &&
                    DaysReservationFilter(accommodation) &&
                    AccommodationTypeFilter(accommodation));
            };
        }
        bool AccommodationNameFilter(Accommodation accommodation)
        {
            return string.IsNullOrEmpty(AccommodationName) || accommodation.Name.ToLower().Contains(AccommodationName.ToLower());
        }
        bool CountryFilter(Accommodation accommodation)
        {
            return string.IsNullOrEmpty(SelectedCountry) || accommodation.Location.Country == SelectedCountry;
        }
        bool CityFilter(Accommodation accommodation)
        {
            return string.IsNullOrEmpty(SelectedCity) || accommodation.Location.City == SelectedCity;
        }
        bool GuestNumberFilter(Accommodation accommodation)
        {
            return string.IsNullOrEmpty(StrNumberOfGuests) || accommodation.MaxGuestNumber >= NumberOfGuests;
        }
        bool DaysReservationFilter(Accommodation accommodation)
        {
            return string.IsNullOrEmpty(StrReservationDays) || accommodation.MinReservationDays <= ReservationDays;
        }
        
        private void ReadCitiesAndCountries()
        {
            Cities.Clear();
            Countries.Clear();
            foreach (Location l in Locations)
            {
                Cities.Add(l.City);
                if (!Countries.Contains(l.Country))
                {
                    Countries.Add(l.Country);
                }
            }
            Countries.Insert(0, string.Empty);
            Cities.Insert(0, string.Empty);
        }
        private void FilterCities()
        {
            if (string.IsNullOrEmpty(SelectedCountry))
            {
                ReadCitiesAndCountries();
                SelectedCity = Cities.FirstOrDefault();
            }
            else
            {
                Cities.Clear();
                foreach (Location loc in Locations)
                {
                    if (loc.Country == SelectedCountry)
                    {
                        Cities.Add(loc.City);
                    }
                }
                Cities.Insert(0, string.Empty);
                SelectedCity = Cities[1];
            }
        }
        private void InitializeNumberOfGuests(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmbx = (ComboBox)sender;
            if (!string.IsNullOrEmpty(cmbx.SelectedItem.ToString()))
            {
                NumberOfGuests = Convert.ToInt32(cmbx.SelectedItem);
            }
        }
        private void InitializeReservationsDays(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmbx = (ComboBox)sender;
            if (!string.IsNullOrEmpty(cmbx.SelectedItem.ToString()))
            {
                ReservationDays = Convert.ToInt32(cmbx.SelectedItem);
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void BookClick(object sender, RoutedEventArgs e)
        {
            if(SelectedAccommodation == null)
            {
                MessageBox.Show("Please select accommodation.");
            }
            else
            {
                AccommodationReservationForm accommodationReservationFormWindow = new AccommodationReservationForm(SelectedAccommodation);
                accommodationReservationFormWindow.Owner = this;
                accommodationReservationFormWindow.ShowDialog();
            }
        }
    }
}
