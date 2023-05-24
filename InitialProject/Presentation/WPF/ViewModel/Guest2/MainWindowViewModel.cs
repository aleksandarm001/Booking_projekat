namespace InitialProject.Presentation.WPF.ViewModel.Guest2
{
    using InitialProject.Aplication.Factory;
    using InitialProject.Domen.Model;
    using InitialProject.Presentation.WPF.View.Guest2.Views;
    using InitialProject.Services.IServices;
    using InitialProject.View.Guest2;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    public class MainWindowViewModel : ViewModel
    {
        private readonly ITourNotificationService _tourNotificationService;
        private readonly IUserReservationCounterService _userReservationCounterService;
        private readonly ITourAttendanceService _tourAttendanceService;
        private readonly IUserService _userService;
        private int _userId;
        public static ObservableCollection<TourAttendance> TourAttendances { get; set; }
        public System.Windows.Navigation.NavigationService NavService { get; set; }
        public RelayCommand NavigateToUserPageCommand { get; set; }
        public RelayCommand NavigateToLastMinutePageCommand { get; set; }
        public RelayCommand NavigateToMainPageCommand { get; set; }
        public RelayCommand NavigateToTourPageCommand { get; set; }
        public RelayCommand NavigateToLanguageCommand { get; set; }
        public RelayCommand NavigateToDarkModeCommand { get; set; }
        public RelayCommand NavigateToReservedToursCommand { get; set; }
        public RelayCommand NavigateToFinishedToursCommand { get; set; }
        public RelayCommand NavigateToRequestedToursCommand { get; set; }
        public RelayCommand NavigateToVouchersPageCommand { get; set; }
        public string UserName { get; set; }

        private string currentLanguage;

        public string CurrentLanguage
        {
            get { return currentLanguage; }
            set
            {
                currentLanguage = value;
            }
        }

        private bool checker;


        public bool Checker
        {
            get { return checker; }
            set
            {
                checker = value;
                OnPropertyChanged();
            }
        }

        private void Execute_NavigateToMainPageCommand(object obj)
        {
            Page toursView = new ToursView();
            this.NavService.Navigate(toursView);
        }

        private void Execute_NavigateToUserPageCommand(object obj)
        {
            Page usersView = new UserView(_userId);
            this.NavService.Navigate(usersView);
        }

        private void Execute_NavigateToLastMinutePageCommand(object obj)
        {
            Page lastMinute = new LastMinuteToursView();
            this.NavService.Navigate(lastMinute);
        }

        private void Execute_NavigateToFinishedToursCommand(object obj)
        {
            Page finishedTours = new FinishedTours(_userId);
            this.NavService.Navigate(finishedTours);
        }
        private void Execute_NavigateToVouchersViewCommand(object obj)
        {
            Page vouchersView = new VouchersView(_userId);
            this.NavService.Navigate(vouchersView);
        }
        private void Execute_NavigateToRequestedToursCommand(object obj)
        {
            Page tourRequests = new TourRequestsView(_userId);
            this.NavService.Navigate(tourRequests);
        }

        private void Execute_NavigateToReservedToursCommand(object obj)
        {
            Page reservedTours = new ReservedTours(_userId);
            this.NavService.Navigate(reservedTours);
        }

        public MainWindowViewModel(System.Windows.Navigation.NavigationService navService, int userId)
        {
            this.NavigateToUserPageCommand = new RelayCommand(Execute_NavigateToUserPageCommand);
            this.NavigateToMainPageCommand = new RelayCommand(Execute_NavigateToMainPageCommand);
            this.NavigateToLastMinutePageCommand = new RelayCommand(Execute_NavigateToLastMinutePageCommand);
            this.NavigateToLanguageCommand = new RelayCommand(Execute_SwitchLanguageCommand);
            this.NavigateToDarkModeCommand = new RelayCommand(Execute_SwitchModeCommand);
            this.NavigateToReservedToursCommand = new RelayCommand(Execute_NavigateToReservedToursCommand);
            this.NavigateToFinishedToursCommand = new RelayCommand(Execute_NavigateToFinishedToursCommand);
            this.NavigateToRequestedToursCommand = new RelayCommand(Execute_NavigateToRequestedToursCommand);
            this.NavigateToVouchersPageCommand = new RelayCommand(Execute_NavigateToVouchersViewCommand);
            this.NavService = navService;

            this.CurrentLanguage = "sr-LATN";
            this.checker = false;
            _userId = userId;
            _tourNotificationService = Injector.CreateInstance<ITourNotificationService>();
            _userReservationCounterService = Injector.CreateInstance<IUserReservationCounterService>();
            _tourAttendanceService = Injector.CreateInstance<ITourAttendanceService>();
            _userService = Injector.CreateInstance<IUserService>();
            UserName = _userService.GetById(userId).Name;
            _tourNotificationService.NotifyGuest(userId);
            CheckTourAttendance(userId);


        }

        private void CheckTourAttendance(int userId)
        {
            TourAttendances = new ObservableCollection<TourAttendance>(_tourAttendanceService.GetAllToCheckByUser(userId));
            if (TourAttendances.Count() != 0)
            {
                foreach (var t in TourAttendances)
                {
                    CheckingTour checkingTour = new CheckingTour(t);
                    checkingTour.ShowDialog();
                }

                int number = TourAttendances.GroupBy(t => t.TourId).Count();
                _userReservationCounterService.CountTourReservations(userId, number);

            }
        }

        private void Execute_SwitchLanguageCommand(object obj)
        {
            var app = (App)Application.Current;
            if (CurrentLanguage.Equals("en-US"))
            {
                CurrentLanguage = "sr-LATN";
            }
            else
            {
                CurrentLanguage = "en-US";
            }

            app.ChangeLanguage(CurrentLanguage);

        }

        private void Execute_SwitchModeCommand(object obj)
        {

        }


    }
}
