﻿using InitialProject.Aplication.Factory;
using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.View.Owner;
using InitialProject.Services.IServices;
using InitialProject.Services;
using InitialProject.View.Owner;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.TeamFoundation.Build.WebApi;
using InitialProject.Presentation.WPF.View.Owner.StartWindowPages;
using System.Windows.Controls;

namespace InitialProject.Presentation.WPF.ViewModel.Owner
{ 
    
    public class OwnerStartViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public System.Windows.Navigation.NavigationService NavService { get; set; }
        public System.Windows.Navigation.NavigationService SideBarNavigationService { get; set; }

        private readonly IAccommodationService _accommodationService;
        //private readonly IAddAccommodationService _addAccommodationService;
        private readonly IGuestReviewService _guestReviewService;
        private readonly IOwnerRateService _ownerRateService;
        //private readonly IChangeReservationRequestService _requestService;
        private readonly IRenovationService _renovationService;

       
        /*
        public ObservableCollection<OwnerChangeRequests> _requests;
        public ObservableCollection<OwnerChangeRequests> Requests
        {
            get { return _requests; }
            set
            {
                _requests = value;
                OnPropertyChanged(nameof(Requests));
            }
        }
        */
        /*
        public ObservableCollection<OwnerRate> _ownerRates;
        public ObservableCollection<OwnerRate> OwnerRates
        {
            get { return _ownerRates; }
            set
            {
                _ownerRates = value;
                OnPropertyChanged(nameof(OwnerRates));

            }
        }
        */
        public ObservableCollection<Accommodation> _accommodations;
        public ObservableCollection<Accommodation> Accommodations
        {
            get { return _accommodations; }
            set
            {
                _accommodations = value;
                OnPropertyChanged(nameof(Accommodations));

            }
        }
        /*
        public ObservableCollection<Renovation> _renovations;

        public ObservableCollection<Renovation> ScheduledRenovations
        {
            get { return _renovations; }
            set
            {
                _renovations = value;
                OnPropertyChanged(nameof(ScheduledRenovations));
            }
        }

        public ObservableCollection<Renovation> _finishedRenovations;

        public ObservableCollection<Renovation> FinishedRenovations
        {
            get { return _finishedRenovations; }
            set
            {
                _finishedRenovations = value;
                OnPropertyChanged(nameof(FinishedRenovations));
            }
        }
        */

        //public static ObservableCollection<string> Countries { get; set; }
       // public static ObservableCollection<string> Cities { get; set; }
        //public static ObservableCollection<Location> Locations { get; set; }
        //public static ObservableCollection<AccommodationImage> Images { get; set; }
        public static ObservableCollection<Reservation> Reservations { get; set; }
       // public static List<GuestReview> GuestReviews { get; set; }
        //public static ObservableCollection<UserToReview> UsersToReview { get; set; }
        public static ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }


        int UserId;

        /*

        private string _url;

        
        public string AccommodationUrl
        {
            get  { return _url; }
            set
            {
                if (value != _url)
                {
                    _url = value;
                    OnPropertyChanged("AccommodationUrl");
                }
            }
        }
        */
       
        /*
        private Accommodation _accommodation = new Accommodation();
        public Accommodation NewAccommodation
        {
            get { return _accommodation; }
            set
            {
                if (value != _accommodation)
                {
                    _accommodation = value;
                    OnPropertyChanged("Accommodation");
                }
            }
        }
        */
       // public UserToReview SelectedUserToReview { get; set; }
        //public OwnerChangeRequests SelectedRequest { get; set; }
        //public Accommodation SelectedAccommodation { get; set; }
        public Accommodation StartSelectedAccommodation { get; set; }
       // public Renovation SelectedRenovation { get; set; }


       // public RelayCommand AddUrl { get; set; }
       // public RelayCommand SaveNewAccommodation { get; set; }
       // public RelayCommand AddRenovation { get; set; }
        //public RelayCommand CancelRenovation { get; set; }
       // public RelayCommand ReviewGuest { get; set; }
        //public RelayCommand AcceptChangeReservation { get; set; }
        //public RelayCommand DeclineChangeReservation { get; set; }

        public RelayCommand AccommodationStatistics { get; set; }





        //public RelayCommand NavigateToAllAccommodations { get; set; }
        //public RelayCommand NavigateToAddAccommodations { get; set; }
        //public RelayCommand NavigateToGuestsToReview { get; set; } 
        //public RelayCommand NavigateToOwnerReviews { get; set; }
        //public RelayCommand NavigateToChangeReservationRequest { get; set; }
        //public RelayCommand NavigateToRenovations { get; set; }
        public RelayCommand NavigateToHome { get;set; }

        public RelayCommand NavigateToGuests { get; set; }

        public RelayCommand NavigateToReservations { get; set; }


        public OwnerStartViewModel(System.Windows.Navigation.NavigationService navService,System.Windows.Navigation.NavigationService sideBarFrameNavigation, int userId)
        {
            UserId = userId;

            _accommodationService = Injector.CreateInstance<IAccommodationService>();
            //_addAccommodationService = Injector.CreateInstance<IAddAccommodationService>();
            _guestReviewService = Injector.CreateInstance<IGuestReviewService>();
            _ownerRateService = Injector.CreateInstance<IOwnerRateService>();
            //_requestService = Injector.CreateInstance<IChangeReservationRequestService>();
            _renovationService = Injector.CreateInstance<IRenovationService>();

            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAccommodationsByOwnerId(userId));
            //Locations = new ObservableCollection<Location>(_addAccommodationService.GetAllLocations());
            //Cities = new ObservableCollection<string>(_addAccommodationService.GetCities(Locations.ToList()));
            //Countries = new ObservableCollection<string>(_addAccommodationService.GetCountries(Locations.ToList()));
            //Images = new ObservableCollection<AccommodationImage>();

            // Reservations = new ObservableCollection<Reservation>(_reservationRepository.GetAll());
            //GuestReviews = new List<GuestReview>(_guestReviewRepository.GetAll());
            //AccommodationReservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationRepository.GetAll());
           //UsersToReview = new ObservableCollection<UserToReview>(_guestReviewService.GetUsersByID(UserId));
           //OwnerRates = new ObservableCollection<OwnerRate>(_ownerRateService.RatingsFromRatedGuest(UserId));
            //Requests = new ObservableCollection<OwnerChangeRequests>(_requestService.OwnerChangeReservationRequest(UserId));

            //NewAccommodation.AccommodationCancelationDays = 1;

            _renovationService.IsRenovationFinished();
            //ScheduledRenovations = new ObservableCollection<Renovation>(_renovationService.GetScheduledRenovationsByOwnerId(UserId));
            //FinishedRenovations = new ObservableCollection<Renovation>(_renovationService.GetFinishedRenovationsByOwnerId(UserId));
            _renovationService.RecentlyRenovated(); // PRoveri gde treba



            //ReadCitiesAndCountries();
            _guestReviewService.InitializeUsersToReview();
            _guestReviewService.RateNotification(UserId);
            // showSuperOwner(UserId);

            this.SideBarNavigationService = sideBarFrameNavigation;
            this.NavService = navService;
            //this.NavigateToAllAccommodations = new RelayCommand(AllAccommodations_ButtonClick);
            //this.NavigateToAddAccommodations = new RelayCommand(AddAccommodation_ButtonClick);
            //this.NavigateToGuestsToReview = new RelayCommand(GuestsToReview_ButtonClick);
            //this.NavigateToOwnerReviews = new RelayCommand(OwnerReviews_ButtonClick);
            //this.NavigateToChangeReservationRequest = new RelayCommand(ChangeReservationRequest_ButtonClick);
            //this.NavigateToRenovations = new RelayCommand(Renovations_ButtonClick);
            this.NavigateToHome = new RelayCommand(Home_ButtonClick);
            this.NavigateToGuests = new RelayCommand(Guests_ButtonClick);
            this.NavigateToReservations = new RelayCommand(Reservations_ButtonClick);

            //AddUrl = new RelayCommand(AddUrl_ButtonClick);
            //ReviewGuest = new RelayCommand(Review_ButtonClick);
            //AcceptChangeReservation = new RelayCommand(AcceptChangeReservation_Button_Click);
            //DeclineChangeReservation = new RelayCommand(DeclineChangeReservation_Button_Click);
            //SaveNewAccommodation = new RelayCommand(SaveNewAccommodation_ButtonClick);
            //AddRenovation = new RelayCommand(AddRenovation_ButtonClick);
            //CancelRenovation = new RelayCommand(CancelRenovation_ButtonClick);
            
            AccommodationStatistics = new RelayCommand(Statistics_ButtonClick);

        }

       // private void AllAccommodations_ButtonClick(object obj)
       // {
        //    Page allAccommodations = new AllAccommodations(UserId);
        //    this.NavService.Navigate(allAccommodations);
       // }

       // private void AddAccommodation_ButtonClick(object obj)
       // {
        //    Page addAccommodations = new AddAccommodation(UserId);
        //    this.NavService.Navigate(addAccommodations);
       // }
       /*
        private void GuestsToReview_ButtonClick(object obj)
        {
            Page guestsToReview = new GuestsToReview(UserId);
            this.NavService.Navigate(guestsToReview);
        }
       */
       /*
        private void OwnerReviews_ButtonClick(object obj)
        {
            Page ownerReviews = new OwnerReviewsPage(UserId);
            this.NavService.Navigate(ownerReviews);
        }
       */
       /*
        private void ChangeReservationRequest_ButtonClick(object obj)
        {
            Page changeReservationRequest = new ChangeReservationRequestPage(UserId);
            this.NavService.Navigate(changeReservationRequest);
        }
       */
        /*
        private void Renovations_ButtonClick(object obj)
        {
            Page renovations = new RenovationsPage(UserId);
            this.NavService.Navigate(renovations);
        }
       */
        private void Home_ButtonClick(object obj)
        {
            Page home = new HomeSideBarPage(NavService,UserId);
            this.SideBarNavigationService.Navigate(home);
            Page allAccommodations = new AllAccommodations(UserId);
            this.NavService.Navigate(allAccommodations);

        }
        private void Guests_ButtonClick(object obj)
        {
            Page guests = new GuestsSideBarPage(NavService, UserId);
            this.SideBarNavigationService.Navigate(guests);
            Page guestsToReview = new GuestsToReview(UserId);
            this.NavService.Navigate(guestsToReview);
        }
        
        private void Reservations_ButtonClick(object obj)
        {
            Page reservations = new ReservationSideBarPage(NavService, UserId);
            this.SideBarNavigationService.Navigate(reservations);
            Page changeReservations = new ChangeReservationRequestPage(UserId);
            this.NavService.Navigate(changeReservations);
        }

        /*
    //ADD ACCOMMODATION

   private void FilterCities(object sender, SelectionChangedEventArgs e)
   {
       ComboBox cmbx = (ComboBox)sender;
       string country = "";
       try
       {
           if (cmbx.SelectedItem != null)
           {
               country = cmbx.SelectedItem.ToString();
           }
           else
           {
               cmbx.SelectedItem = 0;
           }
       }
       catch (System.NullReferenceException)
       {
           ReadCitiesAndCountries();
       }
       if (country == "")
       {

           ReadCitiesAndCountries();
       }
       else
       {
           Cities.Clear();
           Cities.Add("");
           foreach (Location loc in Locations)
           {
               if (loc.Country == country)
               {
                   Cities.Add(loc.City);
               }
           }
           CityComboBox.SelectedIndex = 1;
       }
   }
   */

        /*
        private void SaveNewAccommodation_ButtonClick(object parameter)
        {
            NewAccommodation.Validate();
            if (NewAccommodation.IsValid)
            {
                Accommodation newAccommodation = _addAccommodationService.CreateNewAccommodation(UserId, NewAccommodation.AccommodationName, NewAccommodation.AccommodationMaxGuests, NewAccommodation.AccommodationCancelationDays,NewAccommodation.MinReservationDays,NewAccommodation.AccommodationCountry, NewAccommodation.AccommodationCity, NewAccommodation.AccommodationType.ToString());
                _addAccommodationService.SaveAccommodation(newAccommodation);
                _addAccommodationService.SaveAccommodationImages(Images.ToList());
            }
            else
            {
                OnPropertyChanged(nameof(NewAccommodation));
            }
        }

        public void AddUrl_ButtonClick(object parameter)
        {
            AccommodationImage newImage = new AccommodationImage();
            newImage.Url = AccommodationUrl;
            Images.Add(newImage);
        }

    */

        //GOSTI ZA OCENJIVANJE
        /*
        private void Review_ButtonClick(object parameter)
        {
            if (SelectedUserToReview == null)
            {
                MessageBox.Show("User to review must be selected");
            }
            else
            {
                GuestReviewForm guestReviewForm = new GuestReviewForm(SelectedUserToReview.Guest1Id, SelectedUserToReview.AccommodationId);
                guestReviewForm.ShowDialog();
                if (guestReviewForm.IsReviewd)
                {
                    _guestReviewService.DeleteByIdAndDate(SelectedUserToReview.Guest1Id, SelectedUserToReview.LeavingDay);
                    UsersToReview.Remove(SelectedUserToReview);
                }
            }
        }
        */
        //Za owner review
        public void showSuperOwner(int ownerId)
        {
            if (_ownerRateService.IsSuperOwner(ownerId))
            {
                //zvjezda.Visibility = Visibility.Visible;
            }
            else
            {
                // zvjezda.Visibility = Visibility.Collapsed;
            }
        }

        /*Za ChangereservationRequest
        public void AcceptChangeReservation_Button_Click(object parameter)
        {
            if (SelectedRequest == null)
            {
                MessageBox.Show("Please select request");
            }
            else
            {
                _requestService.AcceptRequest(SelectedRequest.RequestId);
                _requestService.ChangeReservationDateRange(SelectedRequest.NewStartDate, SelectedRequest.NewEndDate, SelectedRequest.ReservationId);
                Requests.Remove(SelectedRequest);
            }
        }

        private void DeclineChangeReservation_Button_Click(object parameter)
        {
            if (SelectedRequest == null)
            {
                MessageBox.Show("Please select request");
            }
            else
            {
                DeclineRequest declineRequest = new DeclineRequest(SelectedRequest.RequestId);
                declineRequest.Show();

            }
        }
        */
        /*
        Renovation
        private void AddRenovation_ButtonClick(object parameter)
        {
            if (SelectedAccommodation == null)
            {
                MessageBox.Show("Please select accommodation!");
            }
            else
            {
                AddRenovation addRenovation = new AddRenovation(SelectedAccommodation);
                addRenovation.Show();
            }
        }

        private void CancelRenovation_ButtonClick(object parameter)
        {
            if (SelectedRenovation == null)
            {
                MessageBox.Show("Please select a renovation");
            }
            else
            {
                if (_renovationService.isCancelationPeriodExpired(SelectedRenovation))
                {
                    MessageBoxResult Expired = MessageBox.Show("The cancelation period for this renovation has expired", "Canceling renovation");
                }
                else
                {
                    MessageBoxResult cancel = MessageBox.Show("Are you sure you want to cancel this renovation?", "Cancel renovation", MessageBoxButton.YesNo);
                    if (cancel == MessageBoxResult.Yes)
                    {
                        _renovationService.DeleteRenovation(SelectedRenovation);
                        ScheduledRenovations.Remove(SelectedRenovation);
                    }

                }
            }
        }
        */
        //Statistics
        private void Statistics_ButtonClick(object parameter)
        {
            if (StartSelectedAccommodation == null)
            {
                MessageBox.Show("Please select accommodation!");
            }
            else
            {
                Statistics statistics = new Statistics(StartSelectedAccommodation.AccommodationID);
                statistics.Show();
            }
        }

    }
}
        
