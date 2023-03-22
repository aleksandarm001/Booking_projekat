﻿using InitialProject.Model;
using InitialProject.Repository;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
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
using static InitialProject.Model.TourPoint;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourForm.xaml
    /// </summary>
    public partial class TourForm : Window, INotifyPropertyChanged
    {
        private readonly LanguageRepository _languageRepository;
        private readonly TourPointRepository _tourPointRepository;
        private readonly LocationRepository _locationRepository;
        private readonly TourImagesRepository _tourImagesRepository;
        private readonly TourRepository _tourRepository;
        private readonly int tourId;
        public static ObservableCollection<Language> Languages { get; set; }
        public static ObservableCollection<string> Countries { get; set; }
        public static ObservableCollection<string> Cities { get; set; }
        public static ObservableCollection<string> KeyPoints { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        public static ObservableCollection<DateTime> DateAndTime { get; set; }

        public static ObservableCollection<TourImages> Images = new ObservableCollection<TourImages>();
        public TourForm()
        {
            InitializeComponent();
            DataContext = this;
            _languageRepository = new LanguageRepository();
            _tourRepository = new TourRepository();
            _tourPointRepository = new TourPointRepository();
            _locationRepository= new LocationRepository();
            _tourImagesRepository = new TourImagesRepository();
            tourId = _tourRepository.NextId();
            Languages = new ObservableCollection<Language>(_languageRepository.GetAll());
            Locations = new ObservableCollection<Location>(_locationRepository.getAll());
            Cities = new ObservableCollection<string>();
            Countries = new ObservableCollection<string>();
            KeyPoints = new ObservableCollection<string>();
            DateAndTime = new ObservableCollection<DateTime>();
            ReadCitiesAndCountries();
            _tourPointRepository.ClearTemp();

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


        

        

        private void AddKeyPoint_ButtonClick(object sender, RoutedEventArgs e)
        {
            TourPointForm addTourPoint = new TourPointForm(tourId, KeyPoints);
            addTourPoint.Show();
        }

        private void AddDatesAndTimes_ButtonClick(object sender, RoutedEventArgs e)
        {
            DateTimePicker date = new DateTimePicker(DateAndTime);
            date.Show();
        }

        private void AddPictures_ButtonClick(object sender, RoutedEventArgs e)
        {
            TourImages newImage = new TourImages();
            newImage.TourId = tourId;
            newImage.Url= TourImageUrl;
            Images.Add(newImage);
        }
        

        private void Cancel_ButtonClick(object sender, RoutedEventArgs e)
        {
            _tourPointRepository.ClearTemp();
            Close();

        }

        private void Track_ButtonClick(object sender, RoutedEventArgs e)
        {

            TourTracking tt = new TourTracking();
            tt.Show();
            Close();
        }

        private void Save_ButtonClick(object sender, RoutedEventArgs e)
        {
            // Get input values
            string country = CountriesComboBox.Text;
            string city = CityComboBox.Text;
            Language language = new Language();
            Language languageForTour = language.fromStringToLanguage(LanguageComboBox.Text);
            List<DateTime> dates = DateAndTime.ToList();
            int howManyObjectsToBuild = dates.Count();

            // Create location object
            Location location = new Location { Country = country, City = city };

            // Create tour object
            Tour tourToSave = new Tour
            {
                Name = NameTextBox.Text,
                MaxGuestNumber = int.Parse(MaxGuests),
                Duration = int.Parse(TourDuratation),
                Location = location,
                Description = Description,
                Language = languageForTour
            };

            // Save tours
            for (int i = 0; i < howManyObjectsToBuild; i++)
            {
                tourToSave.StartingDateTime = dates[i];
                _tourRepository.Save(tourToSave);
            }

            // Save tour points
            List<TourPoint> pointsToSave = _tourPointRepository.getAllTemp();
            int startTourId = tourId;
            for (int i = 0; i < howManyObjectsToBuild; i++)
            {
                int currentTourId = startTourId + i;
                foreach (var tourPoint in pointsToSave)
                {
                    tourPoint.TourId = currentTourId;
                    _tourPointRepository.Save(tourPoint);
                }
            }

            foreach(var image in Images)
            {
                _tourImagesRepository.Save(image);
            }

            // Close dialog
            Close();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        private string _language;
        public string Language
        {
            get { return _language; }
            set
            {
                _language = value;
                OnPropertyChanged(nameof(_language));
            }
        }

        

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                OnPropertyChanged(nameof(_Description));
            }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(_Name));
            }
        }

        private string _imageUrl;

        public string TourImageUrl
        {
            get => _imageUrl;
            set
            {
                if (value != _imageUrl)
                {
                    _imageUrl = value;
                    OnPropertyChanged(nameof(_imageUrl));
                }
            }
        }



        private string _MaxGuests;
        public string MaxGuests
        {
            get { return _MaxGuests; }
            set
            {
                _MaxGuests = value;
                OnPropertyChanged(nameof(_MaxGuests));
            }
        }

        

        private string _TourDuratation;
        public string TourDuratation
        {
            get { return _TourDuratation; }
            set
            {
                _TourDuratation = value;
                OnPropertyChanged(nameof(_TourDuratation));
            }
        }




    }
}