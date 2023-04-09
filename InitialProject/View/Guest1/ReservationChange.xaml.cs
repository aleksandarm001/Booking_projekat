﻿using InitialProject.Model;
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
    /// Interaction logic for ReservationChange.xaml
    /// </summary>
    public partial class ReservationChange : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private ChangeReservationRequestService requestService;
        private ReservationService reservationService;
        private readonly AccommodationService accommodationService;
        private readonly AccommodationReservationService accommodationReservationService;
        private int _userId;
        private int _ownerId;
        public ObservableCollection<KeyValuePair<int, string>> ReservationsForChange { get; set; }
        public int SelectedReservationId { get; set; }
        public DateTime NewCheckInDate { get; set; }
        public DateTime NewCheckOutDate { get; set; }
        private ObservableCollection<ChangeReservationRequest> _requests;
        public ObservableCollection<ChangeReservationRequest> Requests
        {
            get
            {
                return _requests;
            }
            set
            {
                _requests = value;
                OnPropertyChanged(nameof(Requests));
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ReservationChange(int userId, ObservableCollection<ChangeReservationRequest> Requests)
        {
            InitializeComponent();
            DataContext = this;
            reservationService = new ReservationService();
            accommodationService = new AccommodationService();
            requestService = new ChangeReservationRequestService();
            accommodationReservationService = new AccommodationReservationService();
            _userId = userId;
            this.Requests = Requests;
            InitializeReservationsForChange();
        }
        private void InitializeReservationsForChange()
        {
            ReservationsForChange = new ObservableCollection<KeyValuePair<int, string>>(accommodationReservationService.GetReservationsByUserId(_userId));
        }

        private void SendRequest_Button(object sender, RoutedEventArgs e)
        {
            _ownerId = accommodationService.getOwnerIdByReservationId(SelectedReservationId);
            string accommodationName = accommodationService.getNameById(SelectedReservationId);
            ChangeReservationRequest request = new ChangeReservationRequest(SelectedReservationId, accommodationName, NewCheckInDate, NewCheckOutDate, StatusType.Pending, _userId, _ownerId);
            requestService.SaveRequest(request);
            Requests.Add(request);
            this.Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(SelectedReservationId != 0)
            {
                CheckInPicker.SelectedDate = reservationService.GetCheckInDate(_userId, SelectedReservationId);
                CheckOutPicker.SelectedDate = reservationService.GetCheckOutDate(_userId, SelectedReservationId);
            }
        }
    }
}