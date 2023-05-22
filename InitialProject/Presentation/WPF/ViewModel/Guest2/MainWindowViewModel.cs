﻿namespace InitialProject.Presentation.WPF.ViewModel.Guest2
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

        private void Execute_NavigateToTourPageCommand(object obj)
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

        public MainWindowViewModel(System.Windows.Navigation.NavigationService navService, int userId)
        {
            this.NavigateToUserPageCommand = new RelayCommand(Execute_NavigateToUserPageCommand);
            this.NavigateToMainPageCommand = new RelayCommand(Execute_NavigateToTourPageCommand);
            this.NavigateToTourPageCommand = new RelayCommand(Execute_NavigateToTourPageCommand);
            this.NavigateToLastMinutePageCommand = new RelayCommand(Execute_NavigateToLastMinutePageCommand);
            this.NavigateToLanguageCommand = new RelayCommand(Execute_SwitchLanguageCommand);
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


    }
}
