using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Services;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace InitialProject.Presentation.WPF.ViewModel.Guest1
{
    public class ReservationChangeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private IChangeReservationRequestService _requestService;
        private IReservationService _reservationService;
        private readonly IAccommodationService _accommodationService;
        private readonly IAccommodationReservationService _accommodationReservationService;
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
        public ReservationChangeViewModel(int userId, ObservableCollection<ChangeReservationRequest> Requests)
        {
            _reservationService = Injector.CreateInstance<IReservationService>();
            _accommodationService = Injector.CreateInstance<IAccommodationService>();
            _requestService = Injector.CreateInstance<IChangeReservationRequestService>();
            _accommodationReservationService = Injector.CreateInstance<IAccommodationReservationService>();
            _userId = userId;
            this.Requests = Requests;

            InitializeReservationsForChange();
        }
        private void InitializeReservationsForChange()
        {
            ReservationsForChange = new ObservableCollection<KeyValuePair<int, string>>(_accommodationReservationService.GetReservationsByUserId(_userId));
        }
        public void SendRequest_Button()
        {
            _ownerId = _accommodationService.GetOwnerIdByReservationId(SelectedReservationId);
            string accommodationName = _accommodationService.GetNameByReservationId(SelectedReservationId);
            ChangeReservationRequest request = new ChangeReservationRequest(SelectedReservationId, accommodationName, NewCheckInDate, NewCheckOutDate, StatusType.Pending, _userId, _ownerId);
            _requestService.SaveRequest(request);
            UpdateRequests(request);
        }
        public void ComboBox_SelectionChanged(DatePicker CheckInPicker, DatePicker CheckOutPicker)
        {
            if (SelectedReservationId != 0)
            {
                CheckInPicker.SelectedDate = _reservationService.GetCheckInDate(_userId, SelectedReservationId);
                CheckOutPicker.SelectedDate = _reservationService.GetCheckOutDate(_userId, SelectedReservationId);
            }
        }
        private void UpdateRequests(ChangeReservationRequest request)
        {
            bool requestExists = Requests.Any(r => r.ReservationId == request.ReservationId);
            if (!requestExists)
            {
                Requests.Add(request);
            }
        }
    }
}
