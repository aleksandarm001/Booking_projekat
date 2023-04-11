namespace InitialProject.View.Guest2
{
    using InitialProject.Constants;
    using InitialProject.Model;
    using InitialProject.Services;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    /// <summary>
    /// Interaction logic for ReservedTours.xaml
    /// </summary>
    public partial class ReservedTours : Window, INotifyPropertyChanged
    {
 
        public Tour SelectedTour { get; set; }
        private TourPointService _tourPointService;
        private TourReservationService _tourReservationService;

        public event PropertyChangedEventHandler? PropertyChanged;

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

        public ReservedTours(int userId)
        {
            InitializeComponent();
            DataContext = this;
            _tourPointService = new TourPointService();
            _tourReservationService = new TourReservationService();
            Tours = new ObservableCollection<Tour>(_tourReservationService.GetAllReservedAndNotFinishedTour(userId));
        }

        private void Detalji_Click(object sender, RoutedEventArgs e)
        {
            HandleMessage();
        }



        private void HandleMessage()
        {
            if (SelectedTour != null)
            {
                if (SelectedTour.TourStarted)
                {
                    if (_tourPointService.GetActiveTourPointOnTour(SelectedTour.TourId) == null)
                    {
                        MessageBox.Show(TourViewConstants.ActiveTourPointNotFound, TourViewConstants.Caption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes);
                    }
                    else
                    {
                        MessageBox.Show("Trenutno je aktivna " + _tourPointService.GetActiveTourPointOnTour(SelectedTour.TourId).Name + " tacka!", TourViewConstants.Caption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes);
                    }
                }
                else
                {
                    MessageBox.Show(TourViewConstants.TourNotStarted, TourViewConstants.Caption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes);
                }
            }
            else
            {
                MessageBox.Show(TourViewConstants.MustSelectTour, TourViewConstants.Caption, MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.Yes);
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
