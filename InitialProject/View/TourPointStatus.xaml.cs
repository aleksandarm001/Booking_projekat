using InitialProject.CustomClasses;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for TourPointStatus.xaml
    /// </summary>
    public partial class TourPointStatus : Window, INotifyPropertyChanged
    {
        public ObservableCollection<TourPoint> _tourPoints;
        private readonly TourPoint _selectedTourPoint;
        public static ObservableCollection<Reservation> Reservations;

        public TourPointStatus(ObservableCollection<TourPoint> tourPoints, TourPoint selectedTourPoint)
        {
            DataContext = this;
            InitializeComponent();
            TourPoints = tourPoints;
            _selectedTourPoint = selectedTourPoint;


        }

        private ObservableCollection<TourPoint> _points;
        public ObservableCollection<TourPoint> TourPoints
        {
            get { return _points; }
            set
            {
                _points = value;
                OnPropertyChanged(nameof(TourPoints));
            }
        }

        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            TourPoints.Remove(_selectedTourPoint);
            Status status = (Status)Enum.Parse(typeof(Status), StatusComboBox.Text);
            if(_selectedTourPoint.CurrentStatus == Status.Active)
            {
                if(status == Status.Finished) 
                {
                    _selectedTourPoint.CurrentStatus = Status.Finished;
                }
            }
            else
            {
                if (_selectedTourPoint.CurrentStatus == Status.NotActive)
                {
                    foreach(var tourPoint in TourPoints)
                    {
                        if(tourPoint.CurrentStatus == Status.Active)
                            tourPoint.CurrentStatus = Status.Finished;
                    }

                    _selectedTourPoint.CurrentStatus = Status.Active;
                }
            }
            TourPoints.Add(_selectedTourPoint);
        }

        

    }
}
