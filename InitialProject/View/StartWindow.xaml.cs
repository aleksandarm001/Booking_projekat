using InitialProject.CustomClasses;
using InitialProject.Model;
using InitialProject.Repository;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        private readonly LocationRepository _locationRepository;
        private readonly TourPointRepository _tourPointRepository;
        private readonly AccommodationRepository _accommodationRepository;
        private readonly GuestReviewRepository _guestReviewRepository;
        private readonly UserToReviewRepository _userToReviewRepository;
        private readonly ReservationRepository _reservationRepository;
        public static ObservableCollection<Location> Locations { get; set; }
        public static ObservableCollection<Reservation> Reservations { get; set; }
        public static List<GuestReview> GuestReviews {get; set;}
        public static ObservableCollection<UserToReview> UsersToReview { get; set;}
        public static ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }
        public static ObservableCollection<Accommodation> Accommodations { get; set; }
        public StartWindow()
        {
            InitializeComponent();
            Height = SystemParameters.PrimaryScreenHeight * 0.66;
            Width = SystemParameters.PrimaryScreenWidth * 0.37;
            DataContext = this;
            _locationRepository = new LocationRepository();
            _accommodationRepository = new AccommodationRepository();
            _guestReviewRepository= new GuestReviewRepository();
            _reservationRepository= new ReservationRepository();
            _userToReviewRepository = new UserToReviewRepository();
            Reservations = new ObservableCollection<Reservation>(_reservationRepository.GetAll());
            GuestReviews = new List<GuestReview>(_guestReviewRepository.GetAll());
            Locations = new ObservableCollection<Location>(_accommodationRepository.GetAllLocationsFromAccommodations());
            Accommodations = new ObservableCollection<Accommodation>(_accommodationRepository.GetAll());
            InitializeUsersToReview();
        }

        private void Guest1_ButtonClick(object sender, RoutedEventArgs e)
        {
            AccommodationDisplay guest1View = new AccommodationDisplay(Locations);
            guest1View.Show();
        }
        private void Guest2_ButtonClick(object sender, RoutedEventArgs e)
        {
            TourView tourView = new TourView(1);
            tourView.Show();
        }
        private void Owner_ButtonClick(object sender, RoutedEventArgs e)
        {
            RegisterNewAccommodation newAccommodation = new RegisterNewAccommodation();
            newAccommodation.Show();
        }
        private void Guide_ButtonClick(object sender, RoutedEventArgs e)
        {
            TourForm tour = new TourForm();
            tour.Show();
        }
        private void InitializeUsersToReview()
        {
            foreach(Reservation reservation in Reservations){
                if (CheckIfLeftReservation(reservation))
                {
                    _reservationRepository.Delete(reservation);
                    UserToReview userToReview = new UserToReview(0, reservation.UserId, reservation.ReservationDateRange.EndDate); //bez sign in forme defaultni ownerId je 0
                    _userToReviewRepository.Save(userToReview);
                }
            }
        }
        private bool CheckIfLeftReservation(Reservation reservation)
        {
            return reservation.ReservationDateRange.EndDate < DateTime.Now;
        }
    }
}
