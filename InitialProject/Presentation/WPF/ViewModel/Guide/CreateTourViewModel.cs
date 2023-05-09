﻿using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.View.Guide;
using InitialProject.Services.IServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace InitialProject.Presentation.WPF.ViewModel.Guide
{
    public class CreateTourViewModel : INotifyPropertyChanged
    {
        public ICommand FillterCommand { get; set; }

        public ICommand EditTourPointsCommand { get; set; }
        public ObservableCollection<string> Countries { get; set; }
        public ObservableCollection<string> Cities { get; set; }
        public ObservableCollection<Language> Languages { get; set; }




        private readonly ILocationService _locationService;

        private readonly ILanguageService _languageService;
        public CreateTourViewModel()
        {
            //FillterCommand = new RelayCommand(test);
            _locationService = Injector.CreateInstance<ILocationService>();
            _languageService = Injector.CreateInstance<ILanguageService>();

            Countries = new ObservableCollection<string>(_locationService.GetAllCountries());
            Cities = new ObservableCollection<string>(_locationService.GetAllCities());
            Languages = new ObservableCollection<Language>(_languageService.GetAll());

            EditTourPointsCommand = new RelayCommand(ivokeTourPoints);
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
            CreatingTourPointView dsa = new CreatingTourPointView();
            dsa.Show();
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
