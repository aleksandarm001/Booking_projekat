namespace InitialProject.Presentation.WPF.View.Guest2
{
    using InitialProject.Aplication.Contracts.Repository;
    using InitialProject.Aplication.Factory;
    using InitialProject.Domen.Model;
    using InitialProject.Repository;
    using InitialProject.Services;
    using InitialProject.Services.IServices;
    using Microsoft.VisualStudio.Services.WebApi.Location;
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

    /// <summary>
    /// Interaction logic for SimpleRequest.xaml
    /// </summary>
    public partial class SimpleRequest : Window
    {
        public TourRequest TourRequest { get; set; }
        public static ObservableCollection<string> Countries { get; set; }
        public static ObservableCollection<string> Cities { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        public static ObservableCollection<Language> Languages { get; set; }
        private readonly ILanguageService _languageService;
        private readonly ITourRequestService _tourRequestService;
        private readonly Services.IServices.ILocationService _locationService;
        public SimpleRequest(int userId)
        {
            InitializeComponent();

            TourRequest = new TourRequest();
            TourRequest.StartingDate = DateTime.Today.AddDays(2);
            TourRequest.EndingDate = DateTime.Today.AddDays(2);
            DataContext = this;
            TourRequest.UserId = userId;
            _tourRequestService = Injector.CreateInstance<ITourRequestService>();
            _languageService = Injector.CreateInstance<ILanguageService>();
            _locationService = Injector.CreateInstance<Services.IServices.ILocationService>();
            Cities = new ObservableCollection<string>();
            Countries = new ObservableCollection<string>();
            Locations = new ObservableCollection<Location>(_locationService.GetAll());
            Languages = new ObservableCollection<Language>(_languageService.GetAll());
            ReadCitiesAndCountries();
            DatePickerStart.DisplayDateStart = DateTime.Today.AddDays(2);
            DatePickerEnd.DisplayDateStart = DateTime.Today.AddDays(2);
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            TourRequest.Location = new Location { Country = CountryComboBox.Text, City = CityComboBox.Text };
            _tourRequestService.MakeTourRequest(TourRequest);
            this.Close();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

            CityComboBox.SelectedIndex = 1;
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

        private void DatePickerStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            TourRequest.EndingDate = (DateTime)DatePickerStart.SelectedDate;
            DatePickerEnd.SelectedDate = TourRequest.EndingDate;
            DatePickerEnd.DisplayDateStart = TourRequest.EndingDate;
        }
    }
}
