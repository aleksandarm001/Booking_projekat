using InitialProject.Model;
using InitialProject.Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourPointForm.xaml
    /// </summary>
    public partial class TourPointForm : Window, INotifyPropertyChanged
    {
        private readonly int tourId;

        private readonly TourPointRepository _tourPointRepository;




        public event PropertyChangedEventHandler PropertyChanged;

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

        

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public TourPointForm(int tourID)
        {

            DataContext = this;
            InitializeComponent();
            tourId = tourID;

            TourPoint tp = new TourPoint();
            tp.Id = tourId;
            tp.TourId = tourId;
            tp.Name= string.Empty;
            tp.Order = 0;
            tp.CurrentActive = 0;
            tp.Description = string.Empty;
            List<TourPoint> tourPoints = new List<TourPoint>() 
            { 
                tp, tp, tp, tp, tp
            };

            _tourPointRepository = new TourPointRepository();
           
            TourPoints = new ObservableCollection<TourPoint>(tourPoints);

           


    }

        
        private void AddTourPoint_ButtonClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
