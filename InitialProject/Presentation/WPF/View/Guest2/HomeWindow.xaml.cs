namespace InitialProject.Presentation.WPF.View.Guest2
{
    using InitialProject.Aplication.Factory;
    using InitialProject.CustomClasses;
    using InitialProject.Domen.Model;
    using InitialProject.Presentation.WPF.Constants;
    using InitialProject.Presentation.WPF.ViewModel.Guest2;
    using InitialProject.Services.IServices;
    using InitialProject.View.Guest2;
    using Microsoft.TeamFoundation.Common;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using Xceed.Wpf.Samples.SampleData;


    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window, INotifyPropertyChanged
    {
        private HomeWindowViewModel _homeWindowViewModel;
        public event PropertyChangedEventHandler PropertyChanged;
        public static ObservableCollection<string> Duration { get; set; }
        public static ObservableCollection<int> GuestNumber { get; set; }
        public static ObservableCollection<string> Languages { get; set; }
        public static ObservableCollection<string> Localization { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        public static ObservableCollection<string> Cities { get; set; }
        public static ObservableCollection<string> Countries { get; set; }

        public static ObservableCollection<Voucher> Vouchers { get; set; }

        public static ObservableCollection<TourAttendance> TourAttendances { get; set; }
        private int UserId { get; }

        public Tour Tour { get; set; }
        public Tour SelectedTour { get; set; }
        public Tour FinishedTour { get; set; }
        public Tour ReservedTour { get; set; }
        public int NumberOfGuests { get; set; }
        public string SelectedLanguage { get; set; }
        public string SelectedCity { get; set; }
        public string SelectedGuestNumber { get; set; }
        public string SelectedDurationFrom { get; set; }
        public string SelectedDurationTo { get; set; }


        private FinishedTourViewModel _finishedTourViewModel;
        private ReservedTourViewModel _reservedTourViewModel;
        private readonly ITourService _tourService;

        private readonly ITourAttendanceService _tourAttendanceService;

        private readonly ITourReservationService _tourReservationService;

        private readonly ITourRequestService _tourRequestService;

        private readonly IVoucherService _voucherService;

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
        private ObservableCollection<Tour> _reservedTours { get; set; }
        public ObservableCollection<Tour> ReservedTours
        {
            get { return _reservedTours; }
            set
            {
                _reservedTours = value;
                OnPropertyChanged(nameof(ReservedTours));
            }
        }
        private ObservableCollection<Tour> _finishedTours { get; set; }
        public ObservableCollection<Tour> FinishedTours
        {
            get { return _finishedTours; }
            set
            {
                _finishedTours = value;
                OnPropertyChanged(nameof(FinishedTours));
            }
        }

        private ObservableCollection<TourRequest> _requestedTours { get; set; }
        public ObservableCollection<TourRequest> RequestedTours
        {
            get { return _requestedTours; }
            set
            {
                _requestedTours = value;
                OnPropertyChanged(nameof(RequestedTours));
            }
        }

        public HomeWindow(int userId)
        {
            InitializeComponent();
            _homeWindowViewModel = new HomeWindowViewModel();
            DataContext = this;
            UserId = userId;
            _tourReservationService = Injector.CreateInstance<ITourReservationService>();
            _tourService = Injector.CreateInstance<ITourService>();
            _tourAttendanceService = Injector.CreateInstance<ITourAttendanceService>();
            _tourRequestService = Injector.CreateInstance<ITourRequestService>();
            _voucherService = Injector.CreateInstance<IVoucherService>();
            Cities = new ObservableCollection<string>();
            Countries = new ObservableCollection<string>();
            Tours = new ObservableCollection<Tour>(_tourService.GetAllNotStartedTours());
            Vouchers = new ObservableCollection<Voucher>(_voucherService.GetAllForUser(UserId));
            _finishedTourViewModel = new FinishedTourViewModel();
            _reservedTourViewModel = new ReservedTourViewModel();
            ReservedTours = new ObservableCollection<Tour>(_tourReservationService.GetAllReservedAndNotFinishedTour(UserId));
            FinishedTours = new ObservableCollection<Tour>(_tourService.GetAllFinished(UserId));
            RequestedTours = new ObservableCollection<TourRequest>(_tourRequestService.GetAllTourRequests(UserId));

            InitializeLanguages();
            InitializeLocations();
            InitializeGuestNumber();
            InitializeDuration();
            InitializeLocalization();
            ReadCitiesAndCountries();
            CheckTourAttendance();
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
            Languages.Insert(0, string.Empty);
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

        private void InitializeLocalization()
        {
            Localization = new ObservableCollection<string>();
            Localization.Add("Srpski");
            Localization.Add("Engleski");
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

        private void InitializeDuration()
        {
            Duration = new ObservableCollection<string>();
            int max = 1;
            foreach (Tour t in Tours)
            {
                if (max < t.Duration)
                {
                    max = t.Duration;
                }
            }
            for (int i = 1; i <= max; i++)
            {
                Duration.Add(i.ToString());
            }
            Duration.Insert(0, string.Empty);
        }
        private void Rezervisi_Click(object sender, RoutedEventArgs e)
        {

            if (Tour.MaxGuestNumber == 0)
            {
                HandleFullTourCapacity();
            }
            else
            {
                Presentation.WPF.View.Guest2.TourReservation tourReservation = new TourReservation(UserId, Tour, NumberOfGuests);
                tourReservation.ShowDialog();
                //SelectedTour.ReduceGuestNumber(tourReservation.NumberOfGuests);
            }

        }


        private void ApplyFilters_Click(object sender, RoutedEventArgs e)
        {
            Tours = new ObservableCollection<Tour>(_tourService.GetAllFiltered(SelectedCity, SelectedCountry, SelectedDurationFrom, SelectedDurationTo, SelectedLanguage, SelectedGuestNumber));
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
                SelectedCity = Cities[0];
            }
        }

        private void HandleFullTourCapacity()
        {
            MessageBoxResult result;
            result = MessageBox.Show(TourViewConstants.MaxGuestNumberIsZero, TourViewConstants.Caption, MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes);
            if (result == MessageBoxResult.Yes)
            {
                Tours = new ObservableCollection<Tour>(_tourService.GetSimilarAsTourHasFullCapacity(SelectedTour.Location.Country, SelectedTour.Location.City));

                if (Tours.Count == 0)
                {
                    MessageBox.Show(TourViewConstants.ViewOtherTours, TourViewConstants.Caption, MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.Yes);
                    Tours = new ObservableCollection<Tour>(_tourService.GetAllNotStartedTours());
                }

            }
        }

        private void CheckTourAttendance()
        {
            TourAttendances = new ObservableCollection<TourAttendance>(_tourAttendanceService.GetAllToCheckByUser(UserId));
            if (!TourAttendances.IsNullOrEmpty())
            {
                foreach (var t in TourAttendances)
                {
                    CheckingTour checkingTour = new CheckingTour(t);
                    checkingTour.ShowDialog();
                }
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PrikaziTuru_Click(object sender, RoutedEventArgs e)
        {
            Tour = SelectedTour;
            OnPropertyChanged(nameof(Tour));
            TureGrid.Visibility = Visibility.Hidden;
            TuraGrid.Visibility = Visibility.Visible;
        }

        private void Detalji_Click(object sender, RoutedEventArgs e)
        {
            _reservedTourViewModel.HandleMessageForDetails(ReservedTour);
        }

        private void Ocijeni_Click(object sender, RoutedEventArgs e)
        {
            _finishedTourViewModel.RateTour(FinishedTour, UserId);
        }

        private void PrikaziStatistiku_Click(object sender, RoutedEventArgs e)
        {
            TourStatistic tourStatistic = new TourStatistic(UserId);
            tourStatistic.Show();
        }

        private void KreirajObicanZahtjev_Click(object sender, RoutedEventArgs e)
        {
            SimpleRequest simpleRequest = new SimpleRequest(UserId);
            simpleRequest.ShowDialog();
        }


        private void KreirajSlozenZahtjev_Click(object sender, RoutedEventArgs e)
        {

        }


        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            TureGrid.Visibility = Visibility.Visible;
            TuraGrid.Visibility = Visibility.Hidden;
        }

        private void Image_MouseLeftButtonDown_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Pocetna.Visibility = Visibility.Visible;
        }
    }
}
