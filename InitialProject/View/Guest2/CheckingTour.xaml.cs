namespace InitialProject.View.Guest2
{
    using InitialProject.CustomClasses;
    using InitialProject.Model;
    using InitialProject.Repository;
    using System.Windows;

    /// <summary>
    /// Interaction logic for CheckingTour.xaml
    /// </summary>
    public partial class CheckingTour : Window
    {
        public Tour Tour { get; }
        private readonly TourRepository _tourRepository;
        private readonly TourAttendanceRepository _tourAttendanceRepository;
        private TourAttendance _tourAttendance;

        public CheckingTour(TourAttendance tourAttendance)
        {

            _tourRepository = new TourRepository();
            _tourAttendanceRepository = new TourAttendanceRepository();
            Tour = _tourRepository.GetByTourId(tourAttendance.TourId);
            _tourAttendance = _tourAttendanceRepository.GetById(tourAttendance.TourAttendanceId);
            InitializeComponent();
            DataContext = this;
        }



        private void Odbij_Click(object sender, RoutedEventArgs e)
        {
            _tourAttendance.UserAttended = TourAttendance.AttendanceStatus.NotPresent;
            _tourAttendanceRepository.Update(_tourAttendance);
            this.Close();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            _tourAttendance.UserAttended = TourAttendance.AttendanceStatus.Present;
            _tourAttendance.CanGiveReview = true;
            _tourAttendanceRepository.Update(_tourAttendance);
            this.Close();

        }
    }
}
