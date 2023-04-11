using InitialProject.Model;
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

namespace InitialProject.View.Owner
{
    /// <summary>
    /// Interaction logic for ReservationRescheduleRequests.xaml
    /// </summary>
    public partial class ReservationRescheduleRequests : Window,INotifyPropertyChanged
    {
        private readonly ChangeReservationRequestService _requestService = new ChangeReservationRequestService();
        private ObservableCollection<ChangeReservationRequest> _requests;

        public ObservableCollection<ChangeReservationRequest> Requests
        {
            get { return _requests; }
            set
            {
                _requests = value;
                OnPropertyChanged(nameof(Requests));
            }
        }
        public ReservationRescheduleRequests()
        {
            InitializeComponent();
            DataContext = this;

            Requests = new ObservableCollection<ChangeReservationRequest>(_requestService.GetRequests(1));

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
