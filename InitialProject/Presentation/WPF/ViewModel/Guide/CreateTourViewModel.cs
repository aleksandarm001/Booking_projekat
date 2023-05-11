using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.View.Guide;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace InitialProject.Presentation.WPF.ViewModel.Guide
{
    public class CreateTourViewModel : INotifyPropertyChanged
    {
        public ICommand FillterCommand { get; set; }
        public ICommand EditDatesCommand { get; set; }
        public ICommand EditTourPointsCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ObservableCollection<string> Countries { get; set; }
        public ObservableCollection<string> Cities { get; set; }
        public ObservableCollection<Language> Languages { get; set; }

        private readonly ILocationService _locationService;

        private readonly ILanguageService _languageService;

        private readonly ITourService _tourService;

        private readonly ITourPointService _tourPointService;

        private readonly int nextTourId;

        public List<TourPoint> tourPoints;

        public List<DateTime> tourStartingDates;
        public CreateTourViewModel()
        {
            //FillterCommand = new RelayCommand(test);
            _locationService = Injector.CreateInstance<ILocationService>();
            _languageService = Injector.CreateInstance<ILanguageService>();
            _tourService = Injector.CreateInstance<ITourService>();
            _tourPointService = Injector.CreateInstance<ITourPointService>();

            Countries = new ObservableCollection<string>(_locationService.GetAllCountries());
            Cities = new ObservableCollection<string>(_locationService.GetAllCities());
            Languages = new ObservableCollection<Language>(_languageService.GetAll());

            EditTourPointsCommand = new RelayCommand(ivokeTourPoints);
            EditDatesCommand = new RelayCommand(inovkeDates);
            AddCommand = new RelayCommand(CreateTour);

            nextTourId = _tourService.FindNextId();

            tourPoints = new List<TourPoint>();
            tourStartingDates = new List<DateTime>();

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void FilterCities()
        {
            List<string> cities = _locationService.GetCitiesByCountry(Country);
            Cities.Clear();
            foreach (var c in cities)
            {
                Cities.Add(c);
            }
        }

        public void ivokeTourPoints(object obj)
        {
            CreatingTourPointView dsa = new CreatingTourPointView(nextTourId,tourPoints);
            dsa.Show();
        }

        public void inovkeDates(object obj)
        {
            EditingTimeOnTourView dsa = new EditingTimeOnTourView(tourStartingDates);
            dsa.Show();
        }

        public void CreateTour(object obj)
        {
            foreach(var date in tourStartingDates)
            {
                Language language = new();
                Tour tour = new Tour()
                {
                    Name = Name,
                    Location = new Location { City = City, Country = Country },
                    Description = Description,
                    Language = language.fromStringToLanguage(Language),
                    MaxGuestNumber = MaxGuests,
                    StartingDateTime = date,
                    Duration = TourDuratation,
                    TourStarted = false
                };
                _tourService.Save(tour);
                _tourPointService.SaveTourPoints(tourPoints);
                foreach(var tourPoint in tourPoints)
                {
                    tourPoint.TourId++;
                }
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

        private string _Country;
        public string Country
        {
            get { return _Country; }
            set
            {
                _Country = value;
                OnPropertyChanged(nameof(_Country));
                FilterCities();
            }
        }

        private string _City;
        public string City
        {
            get { return _City; }
            set
            {
                _City = value;
                OnPropertyChanged(nameof(_City));
            }
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


        private int _MaxGuests;
        public int MaxGuests
        {
            get { return _MaxGuests; }
            set
            {
                _MaxGuests = value;
                OnPropertyChanged(nameof(_MaxGuests));
            }
        }

        private int _TourDuratation;

        public int TourDuratation
        {
            get { return _TourDuratation; }
            set
            {
                _TourDuratation = value;
                OnPropertyChanged(nameof(_TourDuratation));
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



        

        
    }
}
