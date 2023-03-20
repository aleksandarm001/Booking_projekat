﻿namespace InitialProject.View
{
    using InitialProject.Model;
    using InitialProject.Constants;
    using InitialProject.Repository;
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Forms;

    public partial class TourView : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public static ObservableCollection<int> Duration { get; set; }
        public static ObservableCollection<int> GuestNumber { get; set; }
        public static ObservableCollection<string> Languages { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        public static ObservableCollection<string> Cities { get; set; }
        public static ObservableCollection<string> Countries { get; set; }
        private int UserId { get; }
        public Tour SelectedTour { get; set; }
        public int NumberOfGuests { get; set; }

        public string SelectedLanguage { get; set; }
        public string SelectedCity { get; set; }
        public string SelectedGuestNumber { get; set; }
        public string SelectedDurationFrom { get; set; }
        public string SelectedDurationTo { get; set; }

        private readonly TourRepository _tourRepository;

        private ObservableCollection<Tour> _tours { get; set; }
        public ObservableCollection<Tour> Tours
        {
            get { return _tours; }
            set
            {
                _tours = value;
                OnPropertyChanged(nameof(Tours));
            }
        }

        public TourView(int userId)
        {
            InitializeComponent();
            DataContext = this;
            UserId = userId;
            _tourRepository = new TourRepository();
            Cities = new ObservableCollection<string>();
            Countries = new ObservableCollection<string>();
            Tours = new ObservableCollection<Tour>(_tourRepository.GetAll());
            InitializeLanguages();
            InitializeLocations();
            InitializeGuestNumber();
            InitializeDuration();
            ReadCitiesAndCountries();
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

        private void InitializeLanguages()
        {
            Languages = new ObservableCollection<string>();
            Languages.Add("");
            foreach (Tour t in Tours)
            {

                if (!Languages.Contains(t.Language.Name))
                {
                    Languages.Add(t.Language.Name);
                }

            }
        }

        private void InitializeLocations()
        {
            Locations = new ObservableCollection<Location>();
            foreach (Tour t in Tours)
            {
                if (!Locations.Contains(t.Location))
                {
                    Locations.Add(t.Location);
                }
            }

        }
      

        private void InitializeGuestNumber()
        {
            GuestNumber = new ObservableCollection<int>();
            int max = 1;
            foreach (Tour t in Tours)
            {
                if (max < t.MaxGuestNumber)
                {
                    max = t.MaxGuestNumber;
                }
            }
            for (int i = 1; i <= max; i++)
            {
                GuestNumber.Add(i);
            }
        }

        private void InitializeDuration ()
        {
            Duration = new ObservableCollection<int>();
            int max = 1;
            foreach(Tour t in Tours)
            {
                if (max < t.Duration)
                {
                    max = t.Duration;
                }
            }
            for(int i=1; i<=max; i++)
            {
                Duration.Add(i);
            }
        }
        private void Rezervisi_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour == null)
            {
                MessageBox.Show(TourViewConstants.MustSelectTour, TourViewConstants.Caption, MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Yes);
            }
            else
            {
                if (SelectedTour.MaxGuestNumber == 0)
                {
                    HandleFullTourCapacity(); 
                }
                else
                {
                    TourReservation tourReservation = new TourReservation(UserId, SelectedTour, NumberOfGuests);
                    tourReservation.ShowDialog();
                    SelectedTour.ReduceGuestNumber(tourReservation.NumberOfGuests);
                }
            }
        }


        private void ApplyFilters_Click(object sender, RoutedEventArgs e)
        {
            {
                ObservableCollection<Tour> tempTours = new ObservableCollection<Tour>(_tourRepository.GetAll());
                var filteredCollection = tempTours.Where(tour => CityFilter(tour) && CountryFilter(tour) && DurationFilter(tour) && LanguageFilter(tour) && GuestNumberFilter(tour));
                Tours = new ObservableCollection<Tour>(filteredCollection);
            }
        }

        private void ReadCitiesAndCountries()
        {
            Cities.Clear();
            Countries.Clear();
            foreach (Location l in Locations)
            {
                if (!Cities.Contains(l.City))
                {
                    Cities.Add(l.City);
                }
                if (!Countries.Contains(l.Country))
                {
                    Countries.Add(l.Country);
                }
            }
            Countries.Insert(0, string.Empty);
            Cities.Insert(0, string.Empty);
        }

        private void Filter_Countries(object sender, SelectionChangedEventArgs e)
        {

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
                    if (loc.Country == SelectedCountry && !Cities.Contains(loc.City))
                    {
                        Cities.Add(loc.City);
                    }
                }
                Cities.Insert(0, string.Empty);
                SelectedCity = Cities[1];
            }
        }

        /*private void FilterCities(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmbx = (ComboBox)sender;
            string country = "";
            try
            {
                if (cmbx.SelectedItem != null)
                {
                    country = cmbx.SelectedItem.ToString();
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
                            if (loc.Country == country && !Cities.Contains(loc.City))
                            {
                                Cities.Add(loc.City);
                            }
                        }
                        CityCmbx.SelectedIndex = 1;
                    }
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
        }
*/
        private void HandleFullTourCapacity()
        {
            MessageBoxResult result;
            result = MessageBox.Show(TourViewConstants.MaxGuestNumberIsZero, TourViewConstants.Caption, MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes);
            if (result == MessageBoxResult.Yes)
            {
                var filteredCollection = _tourRepository.GetAll().Where(a =>
                (a.Location.Country == SelectedCountry) && (a.Location.City == SelectedCity) && (a.MaxGuestNumber != 0));
                Tours = new ObservableCollection<Tour>(filteredCollection);

                if (Tours.Count == 0)
                {
                    MessageBox.Show(TourViewConstants.ViewOtherTours, TourViewConstants.Caption, MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.Yes);
                    Tours = new ObservableCollection<Tour>(_tourRepository.GetAll());
                }

            }
        }

        bool CountryFilter(Tour tour)
        {
            return (string.IsNullOrEmpty(SelectedCountry) || tour.Location.Country == SelectedCountry);
        }

        bool CityFilter(Tour tour) 
        {
            return (string.IsNullOrEmpty(SelectedCity) || tour.Location.City == SelectedCity);
        }

        bool GuestNumberFilter(Tour tour) 
        {
            return (string.IsNullOrEmpty(SelectedGuestNumber) || tour.MaxGuestNumber >= Convert.ToInt32(SelectedGuestNumber));
        }

        bool DurationFilter(Tour tour)
        {
            return (string.IsNullOrEmpty(SelectedDurationFrom) || tour.Duration >= Convert.ToInt32(SelectedDurationFrom)) &&
                    (string.IsNullOrEmpty(SelectedDurationTo) || tour.Duration <= Convert.ToInt32(SelectedDurationTo));
        }

        bool LanguageFilter(Tour tour)
        {
            return (string.IsNullOrEmpty(SelectedLanguage) || tour.Language.ToString() == SelectedLanguage);        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}
