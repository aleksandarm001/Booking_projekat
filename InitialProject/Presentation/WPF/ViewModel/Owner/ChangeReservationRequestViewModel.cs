using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.View.Owner;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Presentation.WPF.ViewModel.Owner
{
    public class ChangeReservationRequestViewModel : INotifyPropertyChanged
    {



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly IChangeReservationRequestService _requestService;


        public ObservableCollection<OwnerChangeRequests> _requests;
        public ObservableCollection<OwnerChangeRequests> Requests
        {
            get { return _requests; }
            set
            {
                _requests = value;
                OnPropertyChanged(nameof(Requests));
            }
        }

        int UserId;

        public OwnerChangeRequests SelectedRequest { get; set; }


        public RelayCommand AcceptChangeReservation { get; set; }
        public RelayCommand DeclineChangeReservation { get; set; }

        public ChangeReservationRequestViewModel(int userId) 
        {
            UserId= userId;
            _requestService = Injector.CreateInstance<IChangeReservationRequestService>();

            Requests = new ObservableCollection<OwnerChangeRequests>(_requestService.OwnerChangeReservationRequest(UserId));

            AcceptChangeReservation = new RelayCommand(AcceptChangeReservation_Button_Click);
            DeclineChangeReservation = new RelayCommand(DeclineChangeReservation_Button_Click);


        }


        public void AcceptChangeReservation_Button_Click(object parameter)
        {
            if (SelectedRequest == null)
            {
                MessageBox.Show("Please select request");
            }
            else
            {
                _requestService.AcceptRequest(SelectedRequest.RequestId);
                _requestService.ChangeReservationDateRange(SelectedRequest.NewStartDate, SelectedRequest.NewEndDate, SelectedRequest.ReservationId);
                Requests.Remove(SelectedRequest);
            }
        }

        private void DeclineChangeReservation_Button_Click(object parameter)
        {
            if (SelectedRequest == null)
            {
                MessageBox.Show("Please select request");
            }
            else
            {
                DeclineRequest declineRequest = new DeclineRequest(SelectedRequest.RequestId);
                declineRequest.Show();

            }
        }
    }
}
