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
    /// Interaction logic for FinishedTours.xaml
    /// </summary>
    public partial class FinishedTours : Window, INotifyPropertyChanged
    {
        private FinishedTourViewModel _finishedTourViewModel;
        public Tour SelectedTour { get; set; }
        private readonly ITourService _tourService;

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
            _finishedTourViewModel = new FinishedTourViewModel();
            _tourService = Injector.CreateInstance<ITourService>();
            Tours = new ObservableCollection<Tour>(_tourService.GetAllFinished(UserId));
        }

        private void Ocijeni_Click(object sender, RoutedEventArgs e)
        {
            _finishedTourViewModel.RateTour(SelectedTour, UserId);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
