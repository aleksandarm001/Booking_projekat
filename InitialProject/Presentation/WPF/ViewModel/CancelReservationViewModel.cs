﻿using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
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

namespace InitialProject.Presentation.WPF.ViewModel
{
    public class CancelReservationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private ReservationService _reservationService;
        private readonly AccommodationService _accommodationService;
        private readonly AccommodationReservationService _accommodationReservationService;
        private readonly NotificationService _notificationService;
        private readonly ChangeReservationRequestService _requestService;
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
                if (_selectedReservationId != value)
                {
                    _selectedReservationId = value;
                }
            }
        }
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
        public CancelReservationViewModel(int userId, ObservableCollection<ChangeReservationRequest> requests)
        {
            _userId = userId;
            _reservationService = new ReservationService();
            _accommodationService = new AccommodationService();
            _accommodationReservationService = new AccommodationReservationService();
            _notificationService = new NotificationService();
            _requestService = new ChangeReservationRequestService();
            Requests = requests;
            InitializeReservations();
        }
        public void CancelReservation()
        {
            if (_accommodationReservationService.IsCancellingPossible(DateTime.Now, SelectedReservationId))
            {
                ChangeReservationRequest requestToRemove = Requests.First(request => request.ReservationId == SelectedReservationId);
                Requests.Remove(requestToRemove);
                _reservationService.Delete(SelectedReservationId);
                _accommodationService.DeleteReservation(SelectedReservationId);
                _requestService.DeleteRequestByReservationId(SelectedReservationId);
                Notification notification = new Notification(_userId, _ownerId, TypeNotification.ReservationCancelled, SelectedReservationId);
                _notificationService.SaveNotification(notification);
                MessageBox.Show("You successfully cancelled reservation!");
            }
            else
            {
                MessageBox.Show("You cannot cancel this reservation due to owner's accommodation policy");
            }
        }
        private void InitializeReservations()
        {
            Reservations = new ObservableCollection<KeyValuePair<int, string>>(_accommodationReservationService.GetReservationsByUserId(_userId));
        }
    }
}
