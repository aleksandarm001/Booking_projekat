using InitialProject.Model;
using InitialProject.Repository;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourPointForm.xaml
    /// </summary>
    public partial class TourPointForm : Window, INotifyPropertyChanged
    {
        private readonly int tourId;

        private readonly TourPointRepository _tourPointRepository;

        public List<int> availableOrders;




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
            tp.Id = 1;
            tp.TourId = tourId;
            tp.Name= "antonije";
            tp.Order = 1;
            tp.CurrentActive = 0;
            tp.Description = string.Empty;

            TourPoint tp1 = new TourPoint();
            tp1.Id = 2;
            tp1.TourId = tourId;
            tp1.Name = "antonije1";
            tp1.Order = 0;
            tp1.CurrentActive = 0;
            tp1.Description = string.Empty;


            TourPoint tp2 = new TourPoint();
            tp2.Id = 3;
            tp2.TourId = tourId;
            tp2.Name = "antonije2";
            tp2.Order = 0;
            tp2.CurrentActive = 0;
            tp2.Description = string.Empty;



            List<TourPoint> tourPoints = new List<TourPoint>() 
            { 
                tp, tp1, tp2
            };

            _tourPointRepository = new TourPointRepository();

            TourPoints = new ObservableCollection<TourPoint>(_tourPointRepository.getAllTemp());

            availableOrders = availableOrder();


        }

        public List<int> orederCounter(List<TourPoint> tourPoints)
        {
            List<int> list = new List<int>();
            int i = 0;

            foreach (TourPoint tourPoint in _tourPoints)
            {
                i++;
                list.Add(i);
            }

            return list;
        }

        public List<int> usedOrder(List<TourPoint> tourPoints)
        {
            List<int> list = new List<int>();
            foreach(TourPoint tour in tourPoints)
            {
                list.Add(tour.Order);
            }
            return list;
        }

        public List<int> availableOrder()
        {
            List<int> orders = orederCounter(_tourPoints.ToList());
            List<int> usedOrders = usedOrder(_tourPoints.ToList());
            List<int> availableOrders = orders.Except(usedOrders).ToList();
            return availableOrders;
        }

        private void Edit_ButtonClick(object sender, RoutedEventArgs e)
        {

            if (SelectedTourPoint == null) 
                return; //napraviti window da nije selektovano
            else
            {
                EditTourPointForm editTour = new EditTourPointForm(SelectedTourPoint, availableOrders);
                editTour.Show();
            }

            CollectionViewSource.GetDefaultView(_tourPoints).Refresh();

        }

        private void Remove_ButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void AddTourPoint_ButtonClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
