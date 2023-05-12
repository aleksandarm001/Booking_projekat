using InitialProject.Aplication.Contracts.Repository;
using InitialProject.Aplication.Factory;
using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Repository;
using InitialProject.Services;
using InitialProject.Services.IServices;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
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

namespace InitialProject.Presentation.WPF.View.Owner
{
    /// <summary>
    /// Interaction logic for OwnerStartWindow.xaml
    /// </summary>
    public partial class OwnerStartWindow : Window,INotifyPropertyChanged
    {
        private readonly IAccommodationService _accommodationService;
        private readonly IAddAccommodationService _addAccommodationService;
        private readonly IGuestReviewService _guestReviewService;
        private readonly IOwnerRateService _ownerRateService;
        private readonly IChangeReservationRequestService _requestService;
        private readonly IRenovationService _renovationService;


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


        public OwnerStartWindow(int userId)
        {
            InitializeComponent();
            DataContext= this;
            UserId = userId;

            _renovationService = Injector.CreateInstance<IRenovationService>();
            _accommodationService = Injector.CreateInstance<IAccommodationService>();
            _addAccommodationService= Injector.CreateInstance<IAddAccommodationService>();
            _guestReviewService = Injector.CreateInstance<IGuestReviewService>();
            _ownerRateService = Injector.CreateInstance<IOwnerRateService>();
            _requestService = Injector.CreateInstance<IChangeReservationRequestService>();
 
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAccommodationsByOwnerId(userId));
            Locations = new ObservableCollection<Location>(_addAccommodationService.GetAllLocations());
            Cities = new ObservableCollection<string>(_addAccommodationService.GetCities(Locations.ToList()));
            Countries = new ObservableCollection<string>(_addAccommodationService.GetCountries(Locations.ToList()));
            Images = new ObservableCollection<AccommodationImage>();

           // Reservations = new ObservableCollection<Reservation>(_reservationRepository.GetAll());
            //GuestReviews = new List<GuestReview>(_guestReviewRepository.GetAll());
            
            UsersToReview = new ObservableCollection<UserToReview>(_guestReviewService.GetUsersByID(UserId));
            OwnerRates = new ObservableCollection<OwnerRate>(_ownerRateService.RatingsFromRatedGuest(UserId));
            Requests = new ObservableCollection<OwnerChangeRequests>(_requestService.OwnerChangeReservationRequest(UserId));

            _renovationService.IsRenovationFinished();
            ScheduledRenovations = new ObservableCollection<Renovation>(_renovationService.GetScheduledRenovationsByOwnerId(UserId));
            FinishedRenovations = new ObservableCollection<Renovation>(_renovationService.GetFinishedRenovationsByOwnerId(UserId));

            AccommodationCancelationDays = "1";

            _guestReviewService.InitializeUsersToReview();
            _guestReviewService.RateNotification(UserId);
            showSuperOwner(UserId);
        }

        private void AddAccommodation_ButtonClick(object sender, RoutedEventArgs e)
        {
            OwnerAccommodations.Visibility= Visibility.Collapsed;
            AddAccommodation.Visibility = Visibility.Visible;
            GuestsToReview.Visibility = Visibility.Collapsed;
        }

        private void AllAccommodations_ButtonClick(object sender, RoutedEventArgs e)
        {
            AddAccommodation.Visibility= Visibility.Collapsed;
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
            OwnerReviews.Visibility= Visibility.Collapsed;
            
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
        private void SaveNewAccommodation_ButtonClick(object sender, RoutedEventArgs e)
        {
            Accommodation newAccommodation = _addAccommodationService.CreateNewAccommodation(UserId,AccommodationName, AccommodationMaxGuests,AccommodationCancelationDays, AccommodationReservationMinDays,CountryComboBox.Text,CityComboBox.Text,TypeComboBox.Text);
            _addAccommodationService.SaveAccommodation(newAccommodation);
            _addAccommodationService.SaveAccommodationImages(Images.ToList());
        }

        private void AddUrl_ButtonClick(object sender, RoutedEventArgs e)
        {
            AccommodationImage newImage = new AccommodationImage();
            newImage.Url = UrlTextBox.Text;
            Images.Add(newImage);
        }
        
        //GOSTI ZA OCENJIVANJE
     
        private void Review_ButtonClick(object sender, RoutedEventArgs e)
        {
            if(SelectedUserToReview == null)
            {
                MessageBox.Show("User to review must be selected");
            }
            else
            {
                GuestReviewForm guestReviewForm = new GuestReviewForm(SelectedUserToReview.Guest1Id,SelectedUserToReview.AccommodationId);
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
                zvjezda.Visibility = Visibility.Visible;
            }
            else
            {
                zvjezda.Visibility = Visibility.Collapsed;
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
        //Renovation
        private void AddRenovation_ButtonClick(object sender, RoutedEventArgs e)
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

        private void CancelRenovation_ButtonClick(object sender, RoutedEventArgs e)
        {
            if(SelectedRenovation == null)
            {
                MessageBox.Show("Please select a renovation");
            }
            else
            {
                if(_renovationService.isCancelationPeriodExpired(SelectedRenovation))
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

    }
}
