using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
using static InitialProject.Model.TourPoint;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourTracking.xaml
    /// </summary>
    public partial class TourTracking : Window, INotifyPropertyChanged
    {

        private readonly TourRepository _tourRepository;
        private readonly TourPointRepository _tourPointRepository;
        
        public TourTracking()
        {
            DataContext = this;
            InitializeComponent();
            _tourRepository = new TourRepository();
            _tourPointRepository = new TourPointRepository();
            Tours = new ObservableCollection<Tour>(_tourRepository.GetAll());
            TourPoints = new ObservableCollection<TourPoint>();
            TourPointGrid.Visibility = Visibility.Hidden;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private TourPoint _selectedTourPoint;
        public TourPoint SelectedTourPoint
        {
            get { return _selectedTourPoint; }
            set
            {
                _selectedTourPoint = value;
                OnPropertyChanged(nameof(SelectedTourPoint));

            }
        }

        private string _currentActive;
        public string CurrentActive
        {
            get { return _currentActive; }
            set
            {
                if (_currentActive != value)
                {
                    _currentActive = value;
                    OnPropertyChanged("CurrentActive");
                }
            }
        }

        private Tour _selectedTour;
        public Tour SelectedTour
        {
            get { return _selectedTour; }
            set
            {
                _selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour));
                TourPoints = new ObservableCollection<TourPoint>(_tourPointRepository.GetTourPointsByTourId(SelectedTour.TourId));
                TourPointGrid.Visibility = Visibility.Hidden;
            }
        }

        private ObservableCollection<Tour> _tours;

        public ObservableCollection<Tour> Tours
        {
            get { return _tours; }
            set
            {
                _tours = value;
                OnPropertyChanged(nameof(Tours));


            }
        }

        private ObservableCollection<TourPoint> _tourPoints;
        public ObservableCollection<TourPoint> TourPoints
        {
            get { return _tourPoints; }
            set
            {
                _tourPoints = value;
                OnPropertyChanged(nameof(TourPoints));
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ZapocniTuru_Button(object sender, RoutedEventArgs e)
        {
            TourPointGrid.Visibility = Visibility.Visible;
            TourGrid.IsEnabled = false;


        }
    }
}
