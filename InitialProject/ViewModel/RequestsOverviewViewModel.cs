using InitialProject.Model;
using InitialProject.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.ViewModel
{
    public class RequestsOverviewViewModel: INotifyPropertyChanged
    {
        public ObservableCollection<ChangeReservationRequest> Requests { get; set; }
        private readonly ChangeReservationRequestService requestsService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public RequestsOverviewViewModel(int userId)
        {
            requestsService = new ChangeReservationRequestService();
            Requests = new ObservableCollection<ChangeReservationRequest>(requestsService.GetRequests(userId));
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
