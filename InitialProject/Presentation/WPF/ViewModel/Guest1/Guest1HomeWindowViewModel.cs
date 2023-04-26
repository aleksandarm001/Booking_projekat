using InitialProject.Aplication.Factory;
using InitialProject.Domen.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.View.Guest1;
using InitialProject.Services;
using InitialProject.Services.IServices;
using InitialProject.View;
using InitialProject.View.Guest1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Presentation.WPF.ViewModel.Guest1
{

    public class Guest1HomeWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ReservationDTO> _reservations;
        private int _userId;
        private string _superguest;
        private string _username;
        private int _points;
        private readonly IReservationCompletionService _reservationCompletionService;
        private readonly IUserService _userService;
        private readonly IUserReservationCounterService _userReservationCounterService;
        private readonly IOwnerToRateService _ownerToRateService;
        private readonly IReservationService _reservationService;
        private readonly IAccommodationService _accommodationService;
        public event PropertyChangedEventHandler? PropertyChanged;
        public RelayCommand OpenAccommodationDisplay_Command { get; set; }
        public RelayCommand OpenForums_Command { get; set; }
        public RelayCommand OpenMyRatings_Command { get; set; }
        public RelayCommand OpenOwnerRating_Command { get; set; }
        public RelayCommand OpenChangeReservation_Command { get; set; }
        public RelayCommand OpenCancelReservation_Command { get; set; }
        public RelayCommand OpenAnywhereAnytime_Command { get; set; }
        public RelayCommand OpenForumCreate_Command { get; set; }
        public RelayCommand LogUserOut_Commend { get; set; }
        public ObservableCollection<ReservationDTO> Reservations
        {
            get
            {
                return _reservations;
            }
            set
            {
                _reservations = value;
                OnPropertyChanged(nameof(Reservations));
            }
        }
        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }
        public string Superguest
        {
            get => _superguest;
            set
            {
                if (_superguest != value)
                {
                    _superguest = value;
                    OnPropertyChanged(nameof(Superguest));
                }
            }
        }
        public int Points
        {
            get => _points;
            set
            {
                if (_points != value)
                {
                    _points = value;
                    OnPropertyChanged(nameof(Points));
                }
            }
        }

        public Guest1HomeWindowViewModel()
        {
            _userService = Injector.CreateInstance<IUserService>();
            _reservationCompletionService = Injector.CreateInstance<IReservationCompletionService>();
            _userReservationCounterService = Injector.CreateInstance<IUserReservationCounterService>();
            _ownerToRateService = Injector.CreateInstance<IOwnerToRateService>();
            _reservationService = Injector.CreateInstance<IReservationService>();
            _accommodationService = Injector.CreateInstance<IAccommodationService>();
            _userId = _userService.GetUserId();
            OpenAccommodationDisplay_Command = new RelayCommand(OpenAccommodationDisplay);
            OpenForums_Command = new RelayCommand(OpenForums);
            OpenMyRatings_Command = new RelayCommand(OpenMyRatings);
            OpenOwnerRating_Command = new RelayCommand(OpenOwnerRating);
            OpenChangeReservation_Command = new RelayCommand(OpenChangeReservation);
            OpenCancelReservation_Command = new RelayCommand(OpenCancelReservation);
            OpenAnywhereAnytime_Command = new RelayCommand(OpenAnywhereAnytime);
            OpenForumCreate_Command = new RelayCommand(OpenForumCreate);
            LogUserOut_Commend = new RelayCommand(LogUserOut);
            Reservations = new ObservableCollection<ReservationDTO>();
            SetupReservationSystem();
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void InitializePoints()
        {
            Points = _userService.GetUserPoints();
        }
        private void InitializeUsername()
        {
            Username = _userService.GetUsername();
        }
        private void InitializeSuperGuest()
        {
            if (_userService.GetSuperGuest())
            {
                Superguest = "Yes";
            }
            else
            {
                Superguest = "No";
            }
        }
        private void InitializeReservations()
        {
            Reservations.Clear();
            List<Reservation> reservations = _reservationService.GetUpcomingReservationsByUser(_userId);
            foreach(Reservation reservation in reservations)
            {
                Accommodation accommodation = _accommodationService.GetAccommodationByReservationId(reservation.ReservationId);
                ReservationDTO reservationDTO = new ReservationDTO(accommodation.Name, accommodation.Location, reservation.ReservationDateRange);
                Reservations.Add(reservationDTO);
            }
            Reservations = new ObservableCollection<ReservationDTO>(Reservations.OrderBy(r => r.DateTimeCheckIn).ToList());
        }
        private void HandleCheckingIn()
        {
            _reservationService.HandleCheckingIn();
        }
        private void HandleReservationCompletion()
        {
            foreach (Reservation reservation in _reservationService.GetActiveReservationsByUser(_userId))
            {
                _reservationCompletionService.HandleReservationCompletion(_userId, reservation.ReservationId);
            }
        }
        private void InitializeReservationCounter()
        {
            _userReservationCounterService.InitializeReservationCounter(_userId);
        }
        private void UpdateOwnerToRate()
        {
            _ownerToRateService.DeleteIfFiveDaysPassed();
        }
        private void SetupReservationSystem()
        {
            HandleCheckingIn();
            InitializeReservationCounter();
            HandleReservationCompletion();
            UpdateOwnerToRate();
            InitializeReservations();
            InitializeUsername();
            InitializeSuperGuest();
            InitializePoints();
        }
        private void OpenAccommodationDisplay(object parameter)
        {
            AccommodationDisplay accommodationDisplay = new AccommodationDisplay(_userService.GetUserId());
            AdjustWindow(accommodationDisplay);
            accommodationDisplay.ShowDialog();
            InitializeReservations();
            InitializePoints();
        }
        private void LogUserOut(object parameter)
        {
            CloseWindow();
            UserLogIn logInWindow = new UserLogIn();
            logInWindow.Show();
        }
        private void OpenForums(object parameter)
        {
            ForumsOverviewWindow forumsOverviewWindow = new ForumsOverviewWindow();
            AdjustWindow(forumsOverviewWindow);
            forumsOverviewWindow.ShowDialog();
        }

        private void OpenMyRatings(object parameter)
        {
            ReviewsOverview reviewsOverview = new ReviewsOverview(_userId);
            AdjustWindow(reviewsOverview);
            reviewsOverview.ShowDialog();
        }
        private void OpenOwnerRating(object parameter)
        {
            OwnerRating ownerRating = new OwnerRating(_userId);
            AdjustWindow(ownerRating);
            ownerRating.ShowDialog();
        }
        private void OpenChangeReservation(object parameter)
        {
            RequestsOwerview requestsOwerview = new RequestsOwerview(_userId);
            AdjustWindow(requestsOwerview);
            requestsOwerview.ShowDialog();
            InitializeReservations();
        }
        private void OpenCancelReservation(object parameter)
        {
            RequestsOwerview requestsOwerview = new RequestsOwerview(_userId);
            AdjustWindow(requestsOwerview);
            requestsOwerview.ShowDialog();
            InitializeReservations();
        }
        private void OpenAnywhereAnytime(object parameter)
        {
            AnywhereAnytimeWindow anywhereAnytimeWindow = new AnywhereAnytimeWindow();
            AdjustWindow(anywhereAnytimeWindow);
            anywhereAnytimeWindow.ShowDialog();
            InitializeReservations();
        }
        private void OpenForumCreate(object parameter)
        {
            ForumsOverviewWindow forumsOverviewWindow = new ForumsOverviewWindow();
            AdjustWindow(forumsOverviewWindow);
            forumsOverviewWindow.ShowDialog();
        }
        private void AdjustWindow(Window anywhereAnytimeWindow)
        {
            anywhereAnytimeWindow.Owner = App.Current.Windows.OfType<Guest1HomeWindow>().FirstOrDefault();
            anywhereAnytimeWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }
        private void CloseWindow()
        {
            App.Current.MainWindow = App.Current.Windows.OfType<Guest1HomeWindow>().FirstOrDefault();
            App.Current.MainWindow.Close();
        }
    }
}
