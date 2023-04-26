﻿using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.View.Guest1;
using InitialProject.Presentation.WPF.ViewModel.Guest1;
using InitialProject.Services;
using InitialProject.Services.IServices;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace InitialProject.View.Guest1
{
    /// <summary>
    /// Interaction logic for Guest1Window.xaml
    /// </summary>
    public partial class Guest1Window : Window
    {
        private int _userId;
        public static ObservableCollection<Location> Locations;
        private readonly IOwnerToRateService _ownerToRateService;
        private readonly IReservationCompletionService _reservationCompletionService;
        private readonly IReservationService _reservationService;
        private readonly IUserReservationCounterService _userReservationCounterService;
        private readonly IUserService _userService;

        public Guest1Window(int userId, ObservableCollection<Location> locations)
        {
            InitializeComponent();
            _reservationCompletionService = Injector.CreateInstance<IReservationCompletionService>();
            _userReservationCounterService = Injector.CreateInstance<IUserReservationCounterService>();
            _userService = Injector.CreateInstance<IUserService>();
            _reservationService = Injector.CreateInstance<IReservationService>();
            _ownerToRateService = Injector.CreateInstance<IOwnerToRateService>();
            _userId = userId;
            Locations = locations;
            _reservationService.HandleCheckingIn();
            InitializeReservationCounter();
            HandleReservationCompletion();
            UpdateOwnerToRate();
        }
        private void AccommodationDisplay_Click(object sender, RoutedEventArgs e)
        {
            AccommodationDisplay accommodationDisplay = new AccommodationDisplay(Locations, _userId);
            accommodationDisplay.ShowDialog();
        }
        private void RequestsOverview_Click(object sender, RoutedEventArgs e)
        {
            RequestsOwerview requestsOwerview = new RequestsOwerview(_userId);
            requestsOwerview.ShowDialog();
        }
        private void OwnerRating_Click(object sender, RoutedEventArgs e)
        {
            OwnerRating ownerRating = new OwnerRating(_userId);
            ownerRating.ShowDialog();
        }
        private void HandleReservationCompletion()
        {
            foreach(Reservation reservation in _reservationService.GetActiveReservationsByUser(_userId))
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

        private void ReviewOverview_Click(object sender, RoutedEventArgs e)
        {
            ReviewsOverview reviewsOverview = new ReviewsOverview(_userId);
            reviewsOverview.ShowDialog();
        }

        private void ForumComments_Click(object sender, RoutedEventArgs e)
        {
            ForumCommentsOverview commentsOverview = new ForumCommentsOverview();
            commentsOverview.Owner = App.Current.Windows.OfType<Guest1Window>().FirstOrDefault();
            commentsOverview.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            commentsOverview.ShowDialog();
        }

        private void AnywhereAnytime_Click(object sender, RoutedEventArgs e)
        {
            AnywhereAnytimeWindow anywhereAnytimeWindow = new AnywhereAnytimeWindow();
            anywhereAnytimeWindow.Owner = App.Current.Windows.OfType<Guest1Window>().FirstOrDefault();
            anywhereAnytimeWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            anywhereAnytimeWindow.ShowDialog();
        }

        private void OpenForums_Click(object sender, RoutedEventArgs e)
        {
            ForumsOverviewWindow forumsOverviewWindow = new ForumsOverviewWindow();
            forumsOverviewWindow.Owner = App.Current.Windows.OfType<Guest1Window>().FirstOrDefault(); 
            forumsOverviewWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            forumsOverviewWindow.ShowDialog();
        }
    }
}
