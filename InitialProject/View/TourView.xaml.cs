namespace InitialProject.View
{
    using InitialProject.Model;
    using InitialProject.Repository;
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for TourView.xaml
    /// </summary>
    public partial class TourView : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public static ObservableCollection<int> Duration { get; set; }
        public static ObservableCollection<int> GuestNumber { get; set; }
        public static ObservableCollection<string> Languages { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        public static ObservableCollection<string> Cities { get; set; }
        public static ObservableCollection<string> Countries { get; set; }

        private readonly LocationRepository _locationRepository;
        private int UserId { get; }
        public Tour SelectedTour { get; set; }

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
            _locationRepository = new LocationRepository();
            Cities = new ObservableCollection<string>();
            Countries = new ObservableCollection<string>();
            Tours = new ObservableCollection<Tour>(_tourRepository.GetAll());
            Locations = new ObservableCollection<Location>(_locationRepository.getAll());
            InitializeLanguages();
            InitializeGuestNumber();
            InitializeDuration();
            ReadCitiesAndCountries();
        }

        private void InitializeLanguages()
        {
            Languages = new ObservableCollection<string>();
            Languages.Add("");
            foreach (Tour t in Tours)
            {
                if(!Languages.Contains(t.Language))
                {
                    Languages.Add(t.Language);
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
            //Duration.Add(Convert.ToInt32(""));
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
                string messageBoxText = "Morate prvo izabrati turu!";
                string caption = "Rezervacija ture";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;

                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            }
            else
            {
                if (SelectedTour.MaxGuestNumber == 0)
                {

                    string messageBoxText = "Izabrana tura je popunjena, da li želite da vidite slične ture na istoj lokaciji?";
                    string caption = "Rezervacija ture";
                    MessageBoxButton button = MessageBoxButton.YesNo;
                    MessageBoxImage icon = MessageBoxImage.Warning;
                    MessageBoxResult result;

                    result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
                    if (result == MessageBoxResult.Yes)
                    {
                        string _country = SelectedTour.Location.Country;
                        string _city = SelectedTour.Location.City;

                        var filteredCollection = _tourRepository.GetAll().Where(a =>
                        (a.Location.Country == _country) && (a.Location.City == _city) && (a.MaxGuestNumber!=0));

                        Tours = new ObservableCollection<Tour>(filteredCollection);

                    }
                    else
                    {

                    }

                }
                else
                {
                    TourReservation tourReservation = new TourReservation(UserId, SelectedTour);
                    tourReservation.Show();
                }
            }
        }


        private void ApplyFilters_Click(object sender, RoutedEventArgs e)
        {

           
            {
                ObservableCollection<Tour> tempTours = new ObservableCollection<Tour>(_tourRepository.GetAll());
                string _country = CountryCmbx.Text;
                string _city = CityCmbx.Text;
                string _language = LanguageCmbx.Text;
                string _guestNumber = GuestNumberCmbx.Text;
                string _durationFrom = Duration_Box_From.Text;
                string _durationTo = Duration_Box_To.Text;

                var filteredCollection = tempTours.Where(a =>
                    (string.IsNullOrEmpty(_country) || a.Location.Country == _country) &&
                    (string.IsNullOrEmpty(_city) || a.Location.City == _city) &&
                    (string.IsNullOrEmpty(_durationFrom) || a.Duration >= Convert.ToInt32(_durationFrom)) &&
                    (string.IsNullOrEmpty(_durationTo) || a.Duration <= Convert.ToInt32(_durationTo)) &&
                    (string.IsNullOrEmpty(_guestNumber) || a.MaxGuestNumber >= Convert.ToInt32(_guestNumber)) &&
                    (string.IsNullOrEmpty(_language) || a.Language == _language));

                Tours = new ObservableCollection<Tour>(filteredCollection);

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

        private void Filter_Countries(object sender, SelectionChangedEventArgs e)
        {

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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
