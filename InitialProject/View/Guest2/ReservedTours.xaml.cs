namespace InitialProject.View.Guest2
{
    using InitialProject.Constants;
    using InitialProject.CustomClasses;
    using InitialProject.Model;
    using InitialProject.Repository;
    using InitialProject.Services;
    using System.Collections.ObjectModel;
    using System.Windows;

    /// <summary>
    /// Interaction logic for ReservedTours.xaml
    /// </summary>
    public partial class ReservedTours : Window
    {
        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }
        private ReservationService _reservationService;
        private TourService _tourService;
        private TourPointRepository _tourPointRepository;
        private int UserId { get; set; }

        public ReservedTours(int userId)
        {
            InitializeComponent();
            DataContext = this;
            Tours = new ObservableCollection<Tour>();
            _reservationService = new ReservationService();
            _tourService = new TourService();
            _tourPointRepository = new TourPointRepository();
            UserId = userId;
            InitalizeReservedTours();
        }

        private void Detalji_Click(object sender, RoutedEventArgs e)
        {
            HandleMessage();
        }

        private void InitalizeReservedTours()
        {
            foreach (Reservation r in _reservationService.GetTourReservationByUserId(UserId))
            {
                Tours.Add(_tourService.GetTourById(r.TourId));
            }
        }

        private void HandleMessage()
        {
            if (SelectedTour != null)
            {
                if (SelectedTour.TourStarted)
                {
                    TourPoint tourPoint = _tourPointRepository.GetActiveTourPointOnTour(SelectedTour.TourId);
                    if (tourPoint == null)
                    {
                        MessageBox.Show(TourViewConstants.ActiveTourPointNotFound, TourViewConstants.Caption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes);
                    }
                    else
                    {
                        MessageBox.Show("Trenutno je aktivna " + tourPoint.Name + " tacka!", TourViewConstants.Caption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes);
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
    }
}
