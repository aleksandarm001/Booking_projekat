using InitialProject.Domen.Model;
using InitialProject.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InitialProject.Presentation.WPF.ViewModel
{
    public class RequestsOverviewViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ChangeReservationRequest> _requests;
        private readonly AccommodationService _accommodationService;
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
        private readonly ChangeReservationRequestService requestsService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public RequestsOverviewViewModel(int userId)
        {
            requestsService = new ChangeReservationRequestService();
            _accommodationService = new AccommodationService();
            Requests = new ObservableCollection<ChangeReservationRequest>(requestsService.GetRequests(userId));
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
