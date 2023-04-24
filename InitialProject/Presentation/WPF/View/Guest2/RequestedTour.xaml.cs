namespace InitialProject.Presentation.WPF.View.Guest2
{
    using InitialProject.Domen.Model;
    using InitialProject.Services;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    /// <summary>
    /// Interaction logic for RequestedTour.xaml
    /// </summary>
    public partial class RequestedTour : Window, INotifyPropertyChanged
    {
        private int UserId;
        private TourRequestService _tourRequestService;

        public event PropertyChangedEventHandler? PropertyChanged;

        private ObservableCollection<TourRequest> _requestedTours { get; set; }
        public ObservableCollection<TourRequest> RequestedTours
        {
            get { return _requestedTours; }
            set
            {
                _requestedTours = value;
                OnPropertyChanged(nameof(RequestedTours));
            }
        }

        public RequestedTour(int userId)
        {
            InitializeComponent();
            UserId = userId;
            DataContext = this;
            _tourRequestService = new TourRequestService();
            _tourRequestService.CheckRequests();
            RequestedTours = new ObservableCollection<TourRequest>(_tourRequestService.GetAllTourRequests(UserId));

        }

        private void KreirajZahtjev_Click(object sender, RoutedEventArgs e)
        {
            SimpleRequest simpleRequest = new SimpleRequest(UserId);
            simpleRequest.ShowDialog();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
