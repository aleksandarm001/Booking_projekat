using InitialProject.CustomClasses;
using InitialProject.Model;
using InitialProject.Repository;
using System.Windows;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourReservation.xaml
    /// </summary>
    public partial class TourReservation : Window
    {
        public int NumberOfGuests { get; set; }
        public Tour tour { get; set; }
        public int UserId { get; set; }
        private ReservationRepository _reservationRepository { get; set; }

        public TourReservation(int userId, Tour t)
        {
            InitializeComponent();
            DataContext = this;
            tour = t;
            UserId = userId;
            _reservationRepository = new ReservationRepository();

        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (NumberOfGuests > tour.MaxGuestNumber)
            {
                string messageBoxText = "Na ovoj turi nema dovoljno slobodnih mjesta, tj broj slobodnih mjesta je manji od trazenog broja gostiju!";
                string caption = "Rezervacija ture";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;

                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            }
            else
            {
                Reservation reservation = new Reservation(UserId, tour.TourId, tour.StartingDateTimes, NumberOfGuests);
                _reservationRepository.Save(reservation);
                MessageBox.Show("Rezervacija uspjesna");
                this.Close();
            }

        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
