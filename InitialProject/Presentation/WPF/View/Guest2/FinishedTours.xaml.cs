namespace InitialProject.View.Guest2
{
    using InitialProject.Domen.Model;
    using InitialProject.Presentation.WPF.Constants;
    using InitialProject.Services;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    /// <summary>
    /// Interaction logic for FinishedTours.xaml
    /// </summary>
    public partial class FinishedTours : Window, INotifyPropertyChanged
    {
        public Tour SelectedTour { get; set; }
        private readonly TourService _tourService;
        private readonly TourAttendanceService _tourAttendanceService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public int UserId { get; set; }
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


        public FinishedTours(int userId)
        {
            InitializeComponent();
            DataContext = this;
            UserId = userId;
            _tourService = new TourService();
            _tourAttendanceService = new TourAttendanceService();
            Tours = new ObservableCollection<Tour>(_tourService.GetAllFinished(UserId));
        }

        private void Ocijeni_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour != null)
            {
                if (_tourAttendanceService.CheckPossibleComment(UserId, SelectedTour.TourId))
                {
                    TourReview tourReview = new TourReview(SelectedTour.TourId, UserId);
                    tourReview.Show();
                }
                else
                {
                    MessageBox.Show(TourViewConstants.TourReviewed, TourViewConstants.Caption, MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.Yes);
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
