using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InitialProject.Presentation.WPF.ViewModel.Guest2
{
    public class SimpleRequestViewModel
    {
        public TourRequest TourRequest { get; set; }
        public static ObservableCollection<string> Countries { get; set; }
        public static ObservableCollection<string> Cities { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        public static ObservableCollection<Language> Languages { get; set; }
        private readonly ILanguageService _languageService;
        private readonly ITourRequestService _tourRequestService;
        private readonly Services.IServices.ILocationService _locationService;

        public SimpleRequestViewModel(int userId)
        {
            TourRequest = new TourRequest();
            TourRequest.StartingDate = DateTime.Today.AddDays(2);
            TourRequest.EndingDate = DateTime.Today.AddDays(2);
            TourRequest.UserId = userId;
            _tourRequestService = Injector.CreateInstance<ITourRequestService>();
            _languageService = Injector.CreateInstance<ILanguageService>();
            _locationService = Injector.CreateInstance<Services.IServices.ILocationService>();
            Cities = new ObservableCollection<string>();
            Countries = new ObservableCollection<string>();
            Locations = new ObservableCollection<Location>(_locationService.GetAll());
            Languages = new ObservableCollection<Language>(_languageService.GetAll());
            ReadCitiesAndCountries();
            //DatePickerStart.DisplayDateStart = DateTime.Today.AddDays(2);                      Provjeriti kako se binduje preko viewmodela
            //DatePickerEnd.DisplayDateStart = DateTime.Today.AddDays(2);
        }

        private void ReadCitiesAndCountries()
        {
            Cities.Clear();
            Countries.Clear();
            Cities.Add("");
            Countries.Add("");


            foreach (Location location in Locations)
            {
                Cities.Add(location.City);
                if (!Countries.Contains(location.Country))
                {
                    Countries.Add(location.Country);
                }
            }
        }

        private void UpdateCitiesList(string country)
        {
            Cities.Clear();
            Cities.Add("");


            var filteredCities = Locations.Where(loc => loc.Country == country)
                                          .Select(loc => loc.City);

            foreach (string city in filteredCities)
            {
                Cities.Add(city);
            }

            //CityComboBox.SelectedIndex = 1;                                                  Provjeriti kako se binduje preko viewmodela
        }

        private void FilterCities(object sender, SelectionChangedEventArgs e)
        {
            if (sender is not ComboBox cmbx) return;
            string country = cmbx.SelectedItem?.ToString() ?? string.Empty;
            if (string.IsNullOrEmpty(country))
            {
                ReadCitiesAndCountries();
            }
            else
            {
                UpdateCitiesList(country);
            }
        }

        private void SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //TourRequest.EndingDate = (DateTime)DatePickerStart.SelectedDate;               Provjeriti kako se binduje preko viewmodela
            //DatePickerEnd.SelectedDate = TourRequest.EndingDate;
            //DatePickerEnd.DisplayDateStart = TourRequest.EndingDate;
        }
    }
}
