using InitialProject.Aplication.Factory;
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
using System.Windows.Media.Imaging;
using System.Windows.Media;

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
        private readonly IGuestReviewService _guestReviewService;
        private readonly IOwnerRateService _ownerRateService;
        private readonly IRenovationService _renovationService;

       
        
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

        private ImageSource _imageSource;
        public ImageSource ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }

        

        private Visibility _imageVisibility;
        public Visibility ImageVisibility
        {
            get { return _imageVisibility; }
            set
            {
                _imageVisibility = value;
                OnPropertyChanged(nameof(ImageVisibility));
            }
        }

        



        int UserId;

       
        public RelayCommand NavigateToHome { get;set; }

        public RelayCommand NavigateToGuests { get; set; }

        public RelayCommand NavigateToReservations { get; set; }
        public RelayCommand NavigateToReport { get; set; }
        public RelayCommand OpenTutorial { get; set; }
        public RelayCommand AboutUsCommand { get; set; }

        public RelayCommand ZoomInCommand { get; set; }
        public RelayCommand ZoomOutCommand { get; set; }


        private double _zoomLevel = 1.0;
        public double ZoomLevel
        {
            get { return _zoomLevel; }
            set
            {
                _zoomLevel = value;
                OnPropertyChanged(nameof(ZoomLevel));
            }
        }

        public OwnerStartViewModel(System.Windows.Navigation.NavigationService navService,System.Windows.Navigation.NavigationService sideBarFrameNavigation, int userId)
        {
            UserId = userId;

            _accommodationService = Injector.CreateInstance<IAccommodationService>();
            _guestReviewService = Injector.CreateInstance<IGuestReviewService>();
            _ownerRateService = Injector.CreateInstance<IOwnerRateService>();
            _renovationService = Injector.CreateInstance<IRenovationService>();

            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAccommodationsByOwnerId(userId));
            _renovationService.IsRenovationFinished();
            _renovationService.RecentlyRenovated(); // PRoveri gde treba

            ImageSource = new BitmapImage(new Uri("/Infrastructure/Resources/Images/Zvjezda.png", UriKind.Relative));
            ImageVisibility = Visibility.Collapsed;

            //ReadCitiesAndCountries();
            _guestReviewService.InitializeUsersToReview();
            _guestReviewService.RateNotification(UserId);
             showSuperOwner(UserId);

            this.SideBarNavigationService = sideBarFrameNavigation;
            this.NavService = navService;
            this.NavigateToHome = new RelayCommand(Home_ButtonClick);
            this.NavigateToGuests = new RelayCommand(Guests_ButtonClick);
            this.NavigateToReservations = new RelayCommand(Reservations_ButtonClick);
            this.NavigateToReport = new RelayCommand(Report_ButtonClick);
            this.OpenTutorial = new RelayCommand(Tutorial_ButtonClick);
            this.AboutUsCommand = new RelayCommand(AboutUs_ButtonClick);
            this.ZoomInCommand = new RelayCommand(ZoomIn);
            this.ZoomOutCommand = new RelayCommand(ZoomOut);

        }

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

        private void Report_ButtonClick(object obj)
        {
            Page report = new GeneratingReport(UserId);
            this.NavService.Navigate(report);
            Page reservations = new ReservationSideBarPage(NavService, UserId);
            this.SideBarNavigationService.Navigate(reservations);
        }

        private void Tutorial_ButtonClick(object obj)
        {
            Tutorial tutorial = new Tutorial();
            tutorial.Show();

        }

        private void AboutUs_ButtonClick(object obj)
        {
            AboutUs aboutUs = new AboutUs();
            aboutUs.Show();
        }

        private void ZoomIn(object parameter)
        {
            ZoomLevel += 0.1;
        }

        private void ZoomOut(object parameter)
        {
            if (ZoomLevel > 0.1)
            {
                ZoomLevel -= 0.1;
            }
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
                ImageVisibility = Visibility.Visible;
            }
        }

        
    }
}
        
