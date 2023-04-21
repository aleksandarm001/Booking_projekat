using InitialProject.Aplication.Factory;
using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Services;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Presentation.WPF.ViewModel.Guest1
{
    public class CancelReservationViewModel : INotifyPropertyChanged
    {
        private int _userId;
        private int _ownerId;
        public ObservableCollection<KeyValuePair<int, string>> Reservations { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        private IReservationService _reservationService;
        private readonly IAccommodationService _accommodationService;
        private readonly IAccommodationReservationService _accommodationReservationService;
        private readonly INotificationService _notificationService;
        private readonly IChangeReservationRequestService _requestService;
        private readonly IUserService _userService;
        private int _selectedReservationId;
        public int SelectedReservationId
        {
            get => _selectedReservationId;
            set
            {
                if (_selectedReservationId != value)
                {
                    _selectedReservationId = value;
                    CanCancelReservation = true;
                    OnPropertyChanged(nameof(SelectedReservationId));
                }
            }
        }
        public bool CanCancelReservation { get; set; }
        public RelayCommand CancelReservationCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }

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
        public CancelReservationViewModel()
        {
            _reservationService = Injector.CreateInstance<IReservationService>();
            _accommodationService = Injector.CreateInstance<IAccommodationService>();
            _accommodationReservationService = Injector.CreateInstance<IAccommodationReservationService>();
            _notificationService = Injector.CreateInstance<INotificationService>();
            _requestService = Injector.CreateInstance<IChangeReservationRequestService>();
            _userService = Injector.CreateInstance<IUserService>();
            MessageBox.Show(_userService.GetUserId().ToString());
            InitializeReservations();
            _userId = _userService.GetUserId();
            CanCancelReservation = false;
            CancelReservationCommand = new RelayCommand(CancelReservation);
            CloseCommand = new RelayCommand(Close);
            Requests = new ObservableCollection<ChangeReservationRequest>(_requestService.GetRequests(_userService.GetUserId()));
        }
        public void CancelReservation(object parameter)
        {
            if (_accommodationReservationService.IsCancellingPossible(DateTime.Now, SelectedReservationId))
            {
                ChangeReservationRequest requestToRemove = Requests.First(request => request.ReservationId == SelectedReservationId);
                Requests.Remove(requestToRemove);

                _reservationService.Delete(SelectedReservationId);
                _accommodationService.DeleteReservation(SelectedReservationId);
                _requestService.DeleteRequestByReservationId(SelectedReservationId);

                _ownerId = _accommodationService.GetOwnerIdByReservationId(SelectedReservationId);

                Notification notification = new Notification(_userId, _ownerId, TypeNotification.ReservationCancelled, SelectedReservationId);
                _notificationService.SaveNotification(notification);
                MessageBox.Show("You successfully cancelled reservation!");
            }
            else
            {
                MessageBox.Show("You cannot cancel this reservation due to owner's accommodation policy");
            }
        }
        public void Close(object parameter)
        {
            var window = parameter as Window;
            window.Close();
        }
        private void InitializeReservations()
        {
            Reservations = new ObservableCollection<KeyValuePair<int, string>>(_accommodationReservationService.GetReservationsByUserId(_userId));
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
