using InitialProject.Model;
using InitialProject.Observer;
using InitialProject.Repository;
using Microsoft.VisualStudio.Services.Common;
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

        public static ObservableCollection<string> _keyPoints { get; set; }


        public List<int> availableOrders;

        public List<int> orders;

        public List<int> usedOrders;

        




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

         
        private string _Name;
        public string NameTourPoint
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(_Name));
            }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                OnPropertyChanged(nameof(_Description));
            }
        }



        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public TourPointForm(int tourID, ObservableCollection<string> KeyPoints)
        {

            DataContext = this;
            InitializeComponent();
            tourId = tourID;

            _tourPointRepository = new TourPointRepository();
            _keyPoints = KeyPoints;

            TourPoints = new ObservableCollection<TourPoint>(_tourPointRepository.getAllTemp());
            availableOrders = availableOrder();


        }

        public List<int> OredersCounter(List<TourPoint> tourPoints)
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

        public List<int> UsedOrder(List<TourPoint> tourPoints)
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
            orders = OredersCounter(_tourPoints.ToList());
            usedOrders = UsedOrder(_tourPoints.ToList());
            availableOrders = orders.Except(usedOrders).ToList();
            return availableOrders;
        }

        private void Edit_ButtonClick(object sender, RoutedEventArgs e)
        {

            if (SelectedTourPoint == null) 
                return; //napraviti window da nije selektovano
            else
            {
                availableOrder();
                EditTourPointForm editTour = new EditTourPointForm(SelectedTourPoint, availableOrders, orders, usedOrders, TourPoints);
                editTour.Show();
            }

            CollectionViewSource.GetDefaultView(_tourPoints).Refresh();

        }

        private void Remove_ButtonClick(object sender, RoutedEventArgs e)
        {

        }

        public TourPoint CreateTourPoint()
        {
            TourPoint tp = new TourPoint();
            tp.TourId = tourId;
            tp.Name = NameTourPoint;
            tp.Description = Description;
            tp.Id = _tourPointRepository.NextIdTemp();
            return tp;
        }

        public void RefreshTourPoints()
        {
            TourPoints.Clear();
            List<TourPoint> list = new List<TourPoint>();
            list = _tourPointRepository.getAllTemp();
            foreach (var tourpoint in list)
            {
                TourPoints.Add(tourpoint);
            }
        }



        private void AddTourPoint_ButtonClick(object sender, RoutedEventArgs e)
        {

            _tourPointRepository.SaveTemp(CreateTourPoint());
            RefreshTourPoints();
            foreach(var TourPoint in TourPoints)
            {
                _keyPoints.Add(TourPoint.Name);
            }
            RefreshTourPoints();


        }

    }
}
