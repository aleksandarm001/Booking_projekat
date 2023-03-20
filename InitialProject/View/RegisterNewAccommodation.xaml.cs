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
using InitialProject.CustomClasses;
using InitialProject.Model;
using InitialProject.Repository;
using Microsoft.Win32;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for RegisterNewAccommodation.xaml
    /// </summary>
    public partial class RegisterNewAccommodation : Window
    {

        public UrlTable urlTable;

        private readonly AccommodationRepository _accommodationRepository;
        private readonly AccommodationImageRepository _accommodationImageRepository;
        private readonly LocationRepository _locationRepository;
        public static ObservableCollection<string> Countries { get; set; }
        public static ObservableCollection<string> Cities { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        public static ObservableCollection<UserToReview> UsersToReview { get; set; }

        public static ObservableCollection<AccommodationImages>Images { get; set; }

        private string _accommodationName;

        private int _maxGuests;

        private int _minDays;

        private int _cancelationDays;

        private string _imageUrl;

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
        
        public int AccommodationMaxGuests
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

        public int AccomodationReservationMinDays
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

        public int AccomodationCancelationDays
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

        public string AccomodationImageUrl
        {
            get => _imageUrl;
            set
            {
                if(value != _imageUrl)
                {
                    _imageUrl = value;
                    OnPropertyChanged();
                }
            }
        }
/*
        public BitmapImage AccommodationImage
        {
            get => _image;
            set
            {
                if (value != _image)
                {
                    _image = value;
                    OnPropertyChanged(nameof(AccommodationImage));
                }
            }
        }
*/
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RegisterNewAccommodation(ObservableCollection<UserToReview> usersToReview)
        {
            InitializeComponent();
            DataContext = this;
            Images = new ObservableCollection<AccommodationImages>();

            urlTable = new UrlTable(Images);

            _accommodationRepository = new AccommodationRepository();
            _locationRepository = new LocationRepository();
            _accommodationImageRepository= new AccommodationImageRepository();

            Locations = new ObservableCollection<Location>(_locationRepository.getAll());
            Cities = new ObservableCollection<string>();
            Countries = new ObservableCollection<string>();
            UsersToReview = new ObservableCollection<UserToReview>(usersToReview);
            AccomodationCancelationDays = 1;
            RateNotification();
            ReadCitiesAndCountries();
        }
        private void RateNotification()
        {
            foreach(UserToReview userToReview in UsersToReview) 
            { 
                if(checkDateRange(userToReview.LeavingDay) && userToReview.OwnerId == 0) // 0 je defaultni owner id
                {
                    RateUser(userToReview.Guest1Id);

                }
            }
        }
        private void RateUser(int userID)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Rate User", "You can still rate user", MessageBoxButton.YesNo);
            if(dialogResult == MessageBoxResult.Yes)
            {
                GuestReviewForm reviewForm = new GuestReviewForm(userID);
                reviewForm.ShowDialog();
            }
        }
        private bool checkDateRange(DateTime date)
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now.AddDays(-5);
            if(startDate >= date && endDate <= date)
            {
                return true;
            }
            return false;
        }
        private void NewAccommodationRegistration(object sender, RoutedEventArgs e)
        {

            Accommodation newAccommodation = new Accommodation();
            newAccommodation.Name = AccommodationName;
            newAccommodation.MaxGuestNumber = AccommodationMaxGuests;
            newAccommodation.DaysBeforeCancelling = AccomodationCancelationDays;
            newAccommodation.MinReservationDays = AccomodationReservationMinDays;
            newAccommodation.Location = new Location(CityComboBox.Text,CountriesComboBox.Text);
            
            switch (TypeComboBox.Text)
            {
                case "Appartment":
                    newAccommodation.accommodationType = AccommodationType.Appartment;
                    break;

                case "Shack":
                    newAccommodation.accommodationType = AccommodationType.Shack;
                    break;

                case "House":
                    newAccommodation.accommodationType = AccommodationType.House;
                    break;
            }
            
                 _accommodationRepository.Save(newAccommodation);
                 
                 foreach(var image  in Images)
                 {
                _accommodationImageRepository.Save(image, _accommodationRepository.GetLastAccommodationId());

                 }
                
            
            Close();
        }

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

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            Close();
        }

       /* private void Button_Click(object sender, RoutedEventArgs e)
        {
            UrlTable urlTable = new UrlTable();
            urlTable.Show();
        }
       */
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            urlTable.Show();
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AccommodationImages newImage = new AccommodationImages(0, UrlTextBox.Text,-1) ;
            //AccommodationImages savedImage = _accommodationImageRepository.Save(newImage);
            Images.Add(newImage);
           
        }
    }
}
