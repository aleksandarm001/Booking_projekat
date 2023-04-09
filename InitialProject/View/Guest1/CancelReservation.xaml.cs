using InitialProject.CustomClasses;
using InitialProject.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace InitialProject.View.Guest1
{
    /// <summary>
    /// Interaction logic for CancelReservation.xaml
    /// </summary>
    public partial class CancelReservation : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private ReservationService reservationService;
        private readonly AccommodationService accommodationService;
        private readonly AccommodationReservationService accommodationReservationService;
        private readonly NotificationService notificationService;
        private int _userId;
        private int _ownerId;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ObservableCollection<KeyValuePair<int, string>> Reservations { get; set; }
        private int _selectedReservationId;
        public int SelectedReservationId
        {
            get => _selectedReservationId;
            set
            {
                if(_selectedReservationId != value)
                {
                    _selectedReservationId = value;
                    CancelButton.IsEnabled = (_selectedReservationId != 0);
                }
            }
        }
        public CancelReservation(int userId)
        {
            InitializeComponent();
            DataContext = this;
            _userId = userId;
            reservationService = new ReservationService();
            accommodationService = new AccommodationService();
            accommodationReservationService = new AccommodationReservationService();
            notificationService = new NotificationService();
            CancelButton.IsEnabled = false;
            InitializeReservations();
        }
        private void InitializeReservations()
        {
            Reservations = new ObservableCollection<KeyValuePair<int, string>>(accommodationReservationService.GetReservationsByUserId(_userId));
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelReservation_Click(object sender, RoutedEventArgs e)
        {
            if (accommodationReservationService.IsCancellingPossible(DateTime.Now, SelectedReservationId))
            {
                reservationService.Delete(SelectedReservationId);
                accommodationService.DeleteReservation(SelectedReservationId);
                Notification notification = new Notification(_userId, _ownerId, TypeNotification.ReservationCancelled, SelectedReservationId);
                notificationService.SaveNotification(notification);
                MessageBox.Show("You successfuly cancelled reservation!");
            }
            else
            {
                MessageBox.Show("You cannot cancel this reservation due to owner's accommodation policy");
            }
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
