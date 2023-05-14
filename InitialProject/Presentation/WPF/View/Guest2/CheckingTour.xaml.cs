namespace InitialProject.View.Guest2
{
    using InitialProject.Aplication.Factory;
    using InitialProject.Domen.Model;
    using InitialProject.Presentation.WPF.ViewModel.Guest2;
    using InitialProject.Services.IServices;
    using System.Windows;

    /// <summary>
    /// Interaction logic for CheckingTour.xaml
    /// </summary>
    public partial class CheckingTour : Window
    {
        public Tour Tour { get; }
        private readonly CheckingTourViewModel _checkingTourViewModel;
        private readonly ITourService _tourService;
        private TourAttendance _tourAttendance;

        public CheckingTour(TourAttendance tourAttendance)
        {
            _checkingTourViewModel = new CheckingTourViewModel();
            _tourService = Injector.CreateInstance<ITourService>();
            Tour = _tourService.GetTourById(tourAttendance.TourId);
            _tourAttendance = tourAttendance;
            InitializeComponent();
            DataContext = this;
        }



        private void Odbij_Click(object sender, RoutedEventArgs e)
        {
            _checkingTourViewModel.RejectTourAttendance(_tourAttendance);
            this.Close();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            _checkingTourViewModel.ConfirmTourAttendance(_tourAttendance);
            this.Close();

        }
    }
}
