namespace InitialProject.View.Guest2
{
    using InitialProject.Constants;
    using InitialProject.CustomClasses;
    using InitialProject.Model;
    using InitialProject.Repository;
    using InitialProject.Services;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;

    /// <summary>
    /// Interaction logic for ReservedTours.xaml
    /// </summary>
    public partial class ReservedTours : Window
    {
        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }
        private TourService _tourService;
        private TourPointService _tourPointService;


        public ReservedTours(int userId)
        {
            InitializeComponent();
            DataContext = this;
            Tours = new ObservableCollection<Tour>(_tourService.GetAllReservedAndNotFinishedTour(userId));
            _tourService = new TourService();
            _tourPointService = new TourPointService();
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
                    if (_tourPointService.GetActiveTourPointOnTour(SelectedTour.TourId)== null)
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
    }
}
