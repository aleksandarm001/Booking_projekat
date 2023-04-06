using InitialProject.Model;
using InitialProject.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace InitialProject.View.Guest1
{
    /// <summary>
    /// Interaction logic for ReservationChange.xaml
    /// </summary>
    public partial class ReservationChange : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private ChangeReservationRequestService requestService;
        private ReservationService reservationService;
        private readonly AccommodationService accommodationService;
        private int _userId;
        private int _ownerId;
        public ObservableCollection<KeyValuePair<int, string>> ReservationsForChange { get; set; }
        public int SelectedReservationId { get; set; }
        public DateTime NewCheckInDate { get; set; }
        public DateTime NewCheckOutDate { get; set; }
        public ReservationChange(int userId)
        {
            DataContext = this;
            InitializeComponent();
            InitializeReservationsForChange();
            reservationService = new ReservationService();
            accommodationService = new AccommodationService();
            _userId = userId;
        }
        private void InitializeReservationsForChange()
        {
            ReservationsForChange = new ObservableCollection<KeyValuePair<int, string>>(reservationService.ReservationsForChange(_userId));
        }

        private void SendRequest_Button(object sender, RoutedEventArgs e)
        {
            _ownerId = accommodationService.getOwnerIdByReservationId(SelectedReservationId);
            ChangeReservationRequest request = new ChangeReservationRequest(SelectedReservationId, NewCheckInDate, NewCheckOutDate, StatusType.Pending, _userId, _ownerId);
            requestService.SaveRequest(request);
        }
    }
}
