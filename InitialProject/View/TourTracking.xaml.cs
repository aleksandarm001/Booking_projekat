using InitialProject.CustomClasses;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
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
        private readonly ReservationRepository _reservationRepository;
        private readonly UserRepository _userRepository;
        private readonly TourAttendanceRepository _tourAttendanceRepository;
        
        public TourTracking()
        {
            DataContext = this;
            InitializeComponent();
            _tourRepository = new TourRepository();
            _tourPointRepository = new TourPointRepository();
            _reservationRepository = new ReservationRepository();
            _userRepository = new UserRepository();
            _tourAttendanceRepository = new TourAttendanceRepository();

            Tours = CreateTours();

            TourPoints = new ObservableCollection<TourPoint>();
            TourPointGrid.Visibility = Visibility.Hidden;
            ChangeStatusButton.Visibility = Visibility.Hidden;
        }

        public ObservableCollection<Tour> CreateTours()
        {
            return new ObservableCollection<Tour>(_tourRepository.GetAll().Where(s => s.TourStarted == false)
                                                                           .Where(d => DateOnly.FromDateTime(d.StartingDateTime) == DateOnly.FromDateTime(ParseDateInDDMMYYYY(DateTime.Today)))
                                                                           .ToList());
        }

        public static DateTime ParseDateInDDMMYYYY(DateTime dt)
        {
            string dateString = dt.ToString("dd/MM/yyyy");
            return DateTime.ParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
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

        

        private Tour _selectedTour;
        public Tour SelectedTour
        {
            get { return _selectedTour; }
            set
            {
                _selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour));
                TourPoints = new ObservableCollection<TourPoint>(_tourPointRepository.GetTourPointsByTourId(SelectedTour.TourId));
                if (TourPoints[0].CurrentStatus != Status.Finished)
                {
                    TourPoints[0].CurrentStatus = Status.Active;
                }
                TourPointGrid.Visibility = Visibility.Hidden;
                TourPointGrid.IsEnabled = true;
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
            ChangeStatusButton.Visibility = Visibility.Visible;
            TourGrid.IsEnabled = false;


        }

        public List<User> GetUsersOnTour(int tourId)
        {
            List<User> users = new(); 
            foreach(var reservation in _reservationRepository.GetAll())
            {
                if(reservation.TourId == tourId)
                {
                    users.Add(_userRepository.GetById(reservation.UserId));
                }
            }

            return users;
        }

        public void CheckIfExistAndSave(TourAttendance tourAttendance)
        {
            if(_tourAttendanceRepository.GetAll().Contains(tourAttendance) == true)
            {
                _tourAttendanceRepository.Save(tourAttendance);
            }
        }

        public List<TourAttendance> GetUsersOnTourPoint()
        {
            List<User> users = GetUsersOnTour(SelectedTour.TourId);
            for(int i=0; i<users.Count; i++)
            {
                TourAttendance tourAttendance = new();
                tourAttendance.TourId = SelectedTour.TourId;
                tourAttendance.TourPointId = SelectedTourPoint.Id;
                tourAttendance.UserId = users[i].Id;
                tourAttendance.Username = users[i].Name;
                tourAttendance.UserAttended = TourAttendance.AttendanceStatus.OnHold;
                CheckIfExistAndSave(tourAttendance);
            }

            return _tourAttendanceRepository.GetAll().Where(t=>t.TourId == SelectedTour.TourId)
                                                     .Where(tp=>tp.TourPointId == SelectedTourPoint.Id)
                                                     .ToList();
        }

        
        private void ChageStatus_Button(object sender, RoutedEventArgs e)
        {
           List<TourAttendance> tourAttendances = GetUsersOnTourPoint();
           TourPointStatus tourPointStatus = new TourPointStatus(TourPoints, SelectedTourPoint, tourAttendances);
           tourPointStatus.ShowDialog();
           TourPoints = new ObservableCollection<TourPoint>(_tourPointRepository.GetTourPointsByTourId(SelectedTour.TourId));
            if (TourPoints.Last().CurrentStatus == Status.Finished)
            {
                TourGrid.IsEnabled = true;
                TourPointGrid.IsEnabled = false;
                SelectedTour.TourStarted = true;
                _tourRepository.Update(SelectedTour);


            }
        }


        

        private void StopTour_Button(object sender, RoutedEventArgs e)
        {
            foreach(var tourPoint in TourPoints)
            {
                if(tourPoint.CurrentStatus != Status.Finished)
                {
                    tourPoint.CurrentStatus = Status.ForceFisnihed;
                    _tourPointRepository.Update(tourPoint);
                }
            }
            TourGrid.IsEnabled = true;
            TourPointGrid.IsEnabled = false;
            TourPoints = new ObservableCollection<TourPoint>(_tourPointRepository.GetTourPointsByTourId(SelectedTour.TourId));
        }

    }
}
