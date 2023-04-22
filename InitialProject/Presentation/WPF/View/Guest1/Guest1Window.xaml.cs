using InitialProject.Aplication.Factory;
using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.View.Guest1;
using InitialProject.Presentation.WPF.ViewModel.Guest1;
using InitialProject.Services;
using InitialProject.Services.IServices;
using System.Collections.ObjectModel;
using System.Windows;

namespace InitialProject.View.Guest1
{
    /// <summary>
    /// Interaction logic for Guest1Window.xaml
    /// </summary>
    public partial class Guest1Window : Window
    {
        private int _userId;
        public static ObservableCollection<Location> Locations;
        private readonly IOwnerToRateService _ownerToRateService;
        private readonly IReservationCompletionService _reservationCompletionService;
        private readonly IReservationService _reservationService;
        private readonly IUserReservationCounterService _userReservationCounterService;
        private readonly IUserService _userService;

        public Guest1Window(int userId, ObservableCollection<Location> locations)
        {
            InitializeComponent();
            _reservationCompletionService = new ReservationCompletionService();
            _userReservationCounterService = Injector.CreateInstance<IUserReservationCounterService>();
            _userService = Injector.CreateInstance<IUserService>();
            _reservationService = new ReservationService();
            _userId = userId;
            Locations = locations;
            _ownerToRateService = new OwnerToRateService();
            InitializeReservationCounter();
            HandleReservationCompletion();
            UpdateOwnerToRate();
        }
        private void AccommodationDisplay_Click(object sender, RoutedEventArgs e)
        {
            AccommodationDisplay accommodationDisplay = new AccommodationDisplay(Locations, _userId);
            accommodationDisplay.ShowDialog();
        }
        private void RequestsOverview_Click(object sender, RoutedEventArgs e)
        {
            RequestsOwerview requestsOwerview = new RequestsOwerview(_userId);
            requestsOwerview.ShowDialog();
        }
        private void OwnerRating_Click(object sender, RoutedEventArgs e)
        {
            OwnerRating ownerRating = new OwnerRating(_userId);
            ownerRating.ShowDialog();
        }
        private void HandleReservationCompletion()
        {
            foreach(Reservation reservation in _reservationService.GetReservationsByUserId(_userId))
            {
                _reservationCompletionService.HandleReservationCompletion(_userId, reservation.ReservationId);
            }
        }
        private void InitializeReservationCounter()
        {
            _userReservationCounterService.InitializeReservationCounter(_userId);
        }
        private void UpdateOwnerToRate()
        {
            _ownerToRateService.DeleteIfFiveDaysPassed();
        }

        private void ReviewOverview_Click(object sender, RoutedEventArgs e)
        {
            ReviewsOverview reviewsOverview = new ReviewsOverview(_userId);
            reviewsOverview.ShowDialog();
        }

        private void RenovationRecommendation_Click(object sender, RoutedEventArgs e)
        {
             RenovationRecommendationForm renovationRecommendation = new RenovationRecommendationForm();
            renovationRecommendation.ShowDialog();
        }
    }
}
