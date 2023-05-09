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

namespace InitialProject.Presentation.WPF.ViewModel.Owner
{ 
    
    public class OwnerStartViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private readonly IAccommodationService _accommodationService;
        private readonly IAddAccommodationService _addAccommodationService;
        private readonly IGuestReviewService _guestReviewService;
        private readonly IOwnerRateService _ownerRateService;
        private readonly IChangeReservationRequestService _requestService;
        private readonly IRenovationService _renovationService;

        //Za dodavanje
        // private readonly AccommodationRepository _accommodationRepository;
        //private readonly LocationRepository _locationRepository;
        //private readonly AccommodationImageRepository _accommodationImageRepository;


        //Za Guest review
        // private readonly ReservationRepository _reservationRepository;
        // private readonly GuestReviewRepository _guestReviewRepository;
        //private readonly UserToReviewRepository _userToReviewRepository;
        //private readonly AccommodationReservationRepository _accommodationReservationRepository;

        //Za owner review
        //private readonly OwnerRateService _ownerRateService = new OwnerRateService();

        //Za change reserv req
       // private readonly ChangeReservationRequestService _requestService = new ChangeReservationRequestService();
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

        public static ObservableCollection<string> Countries { get; set; }
        public static ObservableCollection<string> Cities { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        public static ObservableCollection<AccommodationImage> Images { get; set; }

        public static ObservableCollection<Reservation> Reservations { get; set; }
        public static List<GuestReview> GuestReviews { get; set; }
        public static ObservableCollection<UserToReview> UsersToReview { get; set; }
        public static ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }


        int UserId;

        private string _accommodationName;

        private string _maxGuests;

        private string _minDays;

        private string _cancelationDays;


        public string AccommodationName
        {
            get => _accommodationName;
            set
            {
                if (value != _accommodationName)
                {
                    _accommodationName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string AccommodationMaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        public string AccommodationReservationMinDays
        {
            get => _minDays;
            set
            {
                if (value != _minDays)
                {

                    _minDays = value;
                    OnPropertyChanged();
                }
            }
        }

        public string AccommodationCancelationDays
        {
            get => _cancelationDays;
            set
            {
                if (value != _cancelationDays)
                {
                    _cancelationDays = value;
                    OnPropertyChanged();
                }
            }
        }
        public UserToReview SelectedUserToReview { get; set; }
        public OwnerChangeRequests SelectedRequest { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public Renovation SelectedRenovation { get; set; }

        //public RelayCommand AddAccommodationCommand { get; set; }


        public OwnerStartViewModel(int userId)
        {
            UserId = userId;

            _accommodationService = Injector.CreateInstance<IAccommodationService>();
            _addAccommodationService = Injector.CreateInstance<IAddAccommodationService>();
            _guestReviewService = Injector.CreateInstance<IGuestReviewService>();
            _ownerRateService = Injector.CreateInstance<IOwnerRateService>();
            _requestService = Injector.CreateInstance<IChangeReservationRequestService>();
            _renovationService = Injector.CreateInstance<IRenovationService>();

            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAccommodationsByOwnerId(userId));
            Locations = new ObservableCollection<Location>(_addAccommodationService.GetAllLocations());
            Cities = new ObservableCollection<string>(_addAccommodationService.GetCities(Locations.ToList()));
            Countries = new ObservableCollection<string>(_addAccommodationService.GetCountries(Locations.ToList()));
            Images = new ObservableCollection<AccommodationImage>();

            // Reservations = new ObservableCollection<Reservation>(_reservationRepository.GetAll());
            //GuestReviews = new List<GuestReview>(_guestReviewRepository.GetAll());
            //AccommodationReservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationRepository.GetAll());
            UsersToReview = new ObservableCollection<UserToReview>(_guestReviewService.GetUsersByID(UserId));
            OwnerRates = new ObservableCollection<OwnerRate>(_ownerRateService.RatingsFromRatedGuest(UserId));
            Requests = new ObservableCollection<OwnerChangeRequests>(_requestService.OwnerChangeReservationRequest(UserId));

            AccommodationCancelationDays = "1";

            _renovationService.IsRenovationFinished();
            ScheduledRenovations = new ObservableCollection<Renovation>(_renovationService.GetScheduledRenovationsByOwnerId(UserId));
            FinishedRenovations = new ObservableCollection<Renovation>(_renovationService.GetFinishedRenovationsByOwnerId(UserId));

            //AddAccommodationCommand = new RelayCommand(AddAccommodation_ButtonClick);


            //ReadCitiesAndCountries();
            _guestReviewService.InitializeUsersToReview();
            _guestReviewService.RateNotification(UserId);
            // showSuperOwner(UserId);
        }


        /*
        private void AddAccommodation_ButtonClick(object parameter)
        {
            OwnerAccommodations.Visibility = Visibility.Collapsed;
            AddAccommodation.Visibility = Visibility.Visible;
            GuestsToReview.Visibility = Visibility.Collapsed;
        }
        */

        /*

        private void AllAccommodations_ButtonClick(object sender, RoutedEventArgs e)
        {
            AddAccommodation.Visibility = Visibility.Collapsed;
            OwnerAccommodations.Visibility = Visibility.Visible;
        }

        private void Guests_ButtonClick(object sender, RoutedEventArgs e)
        {
            HomeButtons.Visibility = Visibility.Collapsed;
            GuestsButtons.Visibility = Visibility.Visible;
            GuestsToReview.Visibility = Visibility.Visible;
            ReservationButtons.Visibility = Visibility.Collapsed;
            AddAccommodation.Visibility = Visibility.Collapsed;
            OwnerAccommodations.Visibility = Visibility.Collapsed;
        }
        private void GuestsToReview_ButtonClick(object sender, RoutedEventArgs e)
        {
            OwnerAccommodations.Visibility = Visibility.Collapsed;
            GuestsToReview.Visibility = Visibility.Visible;
        }

        private void Home_ButtonClick(object sender, RoutedEventArgs e)
        {
            GuestsButtons.Visibility = Visibility.Collapsed;
            HomeButtons.Visibility = Visibility.Visible;
            AddAccommodation.Visibility = Visibility.Collapsed;
            OwnerAccommodations.Visibility = Visibility.Visible;
            GuestsToReview.Visibility = Visibility.Collapsed;
            OwnerReviews.Visibility = Visibility.Collapsed;

        }

        private void Request_ButtonClick(object sender, RoutedEventArgs e)
        {

        }
        private void OwnerReviews_ButtonClick(object sender, RoutedEventArgs e)
        {

        }

        

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        /*      //DODAVANJE AKOMODACIJA
              private void ReadCitiesAndCountries()
              {
                  Cities.Clear();
                  Countries.Clear();
                  Cities.Add("");
                  Countries.Add("");
                  foreach (Location l in Locations)
                  {
                      Cities.Add(l.City);
                      if (!Countries.Contains(l.Country))
                      {
                          Countries.Add(l.Country);
                      }
                  }
          }
        /*
              /*
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
              
        private void SaveNewAccommodation_ButtonClick(object sender, RoutedEventArgs e)
        {
            Accommodation newAccommodation = _addAccommodationService.CreateNewAccommodation(UserId, AccommodationName, AccommodationMaxGuests, AccommodationCancelationDays, AccommodationReservationMinDays, CountryComboBox.Text, CityComboBox.Text, TypeComboBox.Text);
            _addAccommodationService.SaveAccommodation(newAccommodation);
            _addAccommodationService.SaveAccommodationImages(Images.ToList());
        }

        private void AddUrl_ButtonClick(object sender, RoutedEventArgs e)
        {
            AccommodationImage newImage = new AccommodationImage();
            newImage.Url = UrlTextBox.Text;
            Images.Add(newImage);
        }
        /*
                private Accommodation CreateNewAccommodation(int _userId)
                {
                    return new Accommodation
                    {
                        UserId = _userId,
                        Name = AccommodationName,
                        MaxGuestNumber = Convert.ToInt32(AccommodationMaxGuests),
                        DaysBeforeCancelling = Convert.ToInt32(AccommodationCancelationDays),
                        MinReservationDays = Convert.ToInt32(AccommodationReservationMinDays),
                        Location = new Location(CountryComboBox.Text, CityComboBox.Text),
                        TypeOfAccommodation = GetAccommodationType()

                    };
                }

                private AccommodationType GetAccommodationType()
                {
                    switch (TypeComboBox.Text)
                    {
                        case "Appartment":
                            return AccommodationType.Apartment;
                        case "Shack":
                            return AccommodationType.Shack;
                        default:
                            return AccommodationType.House;

                    }
                }

                private void SaveAccommodation(Accommodation accommodation)
                {
                    _accommodationRepository.Save(accommodation);
                    //Accommodations.Add(accommodation);
                }

                private void SaveAccommodationImages(ObservableCollection<AccommodationImage> images)
                {
                    foreach (var image in images)
                    {
                        _accommodationImageRepository.Save(image, _accommodationRepository.GetLastAccommodationId());
                    }
                }
        */

        //GOSTI ZA OCENJIVANJE

        /*   private void InitializeUsersToReview()
           {
               foreach (Reservation reservation in Reservations)
               {
                   if (CheckIfLeftReservation(reservation))
                   {
                       int accommodation_id = ReservationAccommodationId(reservation);
                       int owner_id = OwnerReservationId(accommodation_id);
                       UserToReview userToReview = new UserToReview(owner_id, accommodation_id, reservation.UserId, reservation.ReservationDateRange.EndDate); 
                       _userToReviewRepository.Save(userToReview);
                       _reservationRepository.Delete(reservation);
                       _accommodationReservationRepository.DeleteReservation(reservation.ReservationId);
                      // UsersToReview.Add(userToReview); napravis if funkciju koja dodaje

                   }
               }
           }

           private bool CheckIfLeftReservation(Reservation reservation)
           {
               if (reservation.ReservationDateRange.EndDate < DateTime.Now)
               {
                   return true;
               }
               return false;
           }

           private int ReservationAccommodationId(Reservation reservation)
           {
               foreach (AccommodationReservation accommodationReservation in AccommodationReservations)
               {
                   if (accommodationReservation.ReservationId == reservation.ReservationId)
                   {
                       return accommodationReservation.AccommodationId;
                   }
               }
               return -1;
           }

           private int OwnerReservationId(int accommodationId)
           {
               foreach (Accommodation accommodation in AllAccommodations)
               {
                   if (accommodation.AccommodationID == accommodationId)
                   {
                       return accommodation.UserId;
                   }
               }
               return -1;
           }

           private void RateNotification()
           {
               foreach (UserToReview userToReview in UsersToReview)
               {
                   if (CheckDateRange(userToReview.LeavingDay) && userToReview.OwnerId == UserId)
                   {
                       RateUser(userToReview.Guest1Id, userToReview.AccommodationId, userToReview.LeavingDay);
                   }
                   else
                   {
                       _userToReviewRepository.DeleteByIdAndDate(userToReview.Guest1Id, userToReview.LeavingDay);
                   }
               }
           }

           private void RateUser(int userID, int accommodationId, DateTime date)
           {
               MessageBoxResult dialogResult = MessageBox.Show("Rate User", "You can still rate user", MessageBoxButton.YesNo);
               if (dialogResult == MessageBoxResult.Yes)
               {
                   GuestReviewForm reviewForm = new GuestReviewForm(userID, accommodationId);
                   reviewForm.ShowDialog();
                   if (reviewForm.IsReviewd)
                   {
                       _userToReviewRepository.DeleteByIdAndDate(userID, date);

                   }
               }
           }

           private bool CheckDateRange(DateTime date)
           {
               DateTime startDate = DateTime.Now;
               DateTime endDate = DateTime.Now.AddDays(-5);
               if (startDate >= date && endDate <= date)
               {
                   return true;
               }
               return false;
           }
        
        private void Review_ButtonClick(object sender, RoutedEventArgs e)
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
                }
            }
        }

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

        //Za ChangereservationRequest
        private void AcceptChangeReservation_Button_Click(object sender, RoutedEventArgs e)
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

        private void DeclineChangeReservation_Button_Click(object sender, RoutedEventArgs e)
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
       */
    }
}
        