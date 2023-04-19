namespace InitialProject.View.Guest2
{
    using InitialProject.Aplication.Factory;
    using InitialProject.Domen.Model;
    using InitialProject.Presentation.WPF.Constants;
    using InitialProject.Presentation.WPF.ViewModel.Guest2;
    using InitialProject.Services.IServices;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    /// <summary>
    /// Interaction logic for ReservedTours.xaml
    /// </summary>
    public partial class ReservedTours : Window, INotifyPropertyChanged
    {
        private ReservedTourViewModel _reservedTourViewModel;
        public Tour SelectedTour { get; set; }
        private ITourPointService _tourPointService;
        private ITourReservationService _tourReservationService;

        public event PropertyChangedEventHandler? PropertyChanged;

        private ObservableCollection<Tour> _tours { get; set; }
        public ObservableCollection<Tour> Tours
        {
            get { return _tours; }
            set
            {
                _tours = value;
                OnPropertyChanged(nameof(Tours));
            }
        }

        public ReservedTours(int userId)
        {
            InitializeComponent();
            DataContext = this;
            _reservedTourViewModel = new ReservedTourViewModel();
            _tourPointService = Injector.CreateInstance<ITourPointService>();
            _tourReservationService = Injector.CreateInstance<ITourReservationService>();
            Tours = new ObservableCollection<Tour>(_tourReservationService.GetAllReservedAndNotFinishedTour(userId));
        }

        private void Detalji_Click(object sender, RoutedEventArgs e)
        {
            _reservedTourViewModel.HandleMessageForDetails(SelectedTour);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
