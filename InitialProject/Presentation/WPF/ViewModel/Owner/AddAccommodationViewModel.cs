using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
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
    public class AddAccommodationViewModel: INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly IAddAccommodationService _addAccommodationService;

        public static ObservableCollection<string> Countries { get; set; }
        public static ObservableCollection<string> Cities { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        public static ObservableCollection<AccommodationImage> Images { get; set; }


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

        int UserId;

        private string _url;

        public string AccommodationUrl
        {
            get { return _url; }
            set
            {
                if (value != _url)
                {
                    _url = value;
                    OnPropertyChanged("AccommodationUrl");
                }
            }
        }

        public RelayCommand AddUrl { get; set; }
        public RelayCommand SaveNewAccommodation { get; set; }

        public AddAccommodationViewModel(int userId)
        {
            UserId = userId;
            _addAccommodationService = Injector.CreateInstance<IAddAccommodationService>();

            Locations = new ObservableCollection<Location>(_addAccommodationService.GetAllLocations());
            Cities = new ObservableCollection<string>(_addAccommodationService.GetCities(Locations.ToList()));
            Countries = new ObservableCollection<string>(_addAccommodationService.GetCountries(Locations.ToList()));
            Images = new ObservableCollection<AccommodationImage>();

            NewAccommodation.AccommodationCancelationDays = 1;


            AddUrl = new RelayCommand(AddUrl_ButtonClick);
            SaveNewAccommodation = new RelayCommand(SaveNewAccommodation_ButtonClick);
        }

        private void SaveNewAccommodation_ButtonClick(object parameter)
        {
            NewAccommodation.Validate();
            if (NewAccommodation.IsValid)
            {
                Accommodation newAccommodation = _addAccommodationService.CreateNewAccommodation(UserId, NewAccommodation.AccommodationName, NewAccommodation.AccommodationMaxGuests, NewAccommodation.AccommodationCancelationDays, NewAccommodation.MinReservationDays, NewAccommodation.AccommodationCountry, NewAccommodation.AccommodationCity, NewAccommodation.AccommodationType.ToString());
                _addAccommodationService.SaveAccommodation(newAccommodation);
                _addAccommodationService.SaveAccommodationImages(Images.ToList());
                MessageBox.Show("Successfully added a new accommodation", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
    }
}
