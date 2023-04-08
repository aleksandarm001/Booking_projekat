namespace InitialProject.View.Guest2
{
    using InitialProject.CustomClasses;
    using InitialProject.Model;
    using InitialProject.Repository;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Documents;

    /// <summary>
    /// Interaction logic for FinishedTours.xaml
    /// </summary>
    public partial class FinishedTours : Window
    {
        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }
        private readonly TourRepository _tourRepository;
        private readonly TourAttendanceRepository _tourAttendanceRepository;
        private List<TourAttendance> _tourAttendance;
        public int UserId { get; set; }


        public FinishedTours(int userId)
        {
            InitializeComponent();
            DataContext = this;
            _tourRepository = new TourRepository();
            _tourAttendanceRepository = new TourAttendanceRepository();
            Tours = GetAllFinished(UserId);
            UserId = userId;
        }

        private void Ocijeni_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour!=null)
            {
                TourReview tourReview = new TourReview(SelectedTour.TourId, UserId);
                tourReview.Show();
            }
            
        }

        private ObservableCollection<Tour> GetAllFinished(int userId)
        {
            _tourAttendance = _tourAttendanceRepository.GetAllFinished(userId);
            Tours = new ObservableCollection<Tour>();
            foreach (TourAttendance t in _tourAttendance)
            {
                Tours.Add(_tourRepository.GetByTourId(t.TourId));
            }
            return Tours;
        }
    }
}
