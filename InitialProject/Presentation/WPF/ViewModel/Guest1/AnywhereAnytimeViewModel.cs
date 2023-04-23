using InitialProject.Aplication.Factory;
using InitialProject.CustomClasses;
using InitialProject.Domen.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
using MDriven.WebApi.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InitialProject.Presentation.WPF.ViewModel.Guest1
{
    public class AnywhereAnytimeViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<AccommodationReservationDTO> _accommodations;
        private readonly IAccommodationService _accommodationService;
        private readonly IAccommodationReservationService _accommodationReservationService;
        private readonly IReservationService _reservationService;
        private readonly IUserService _userService;
        
        public AccommodationReservationDTO SelectedAccommodation { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public RelayCommand ApplyFiltersCommand { get; set; }
        public RelayCommand ResetFiltersCommand { get; set; }
        public ObservableCollection<AccommodationReservationDTO> Accommodations
        {
            get 
            { 
                return _accommodations; 
            }
            set
            {
                _accommodations = value;
                OnPropertyChanged(nameof(Accommodations));
            }
        }
        private int _accommodationsNumber;
        public int AccommodationsNumber
        {
            get => _accommodationsNumber;
            set
            {
                if (value != _accommodationsNumber)
                {
                    _accommodationsNumber = value;
                    OnPropertyChanged(nameof(AccommodationsNumber));
                }
            }
        }
        public int NumberOfGuests { get; set; }
        private string _strNumberOfGuests;
        public string StrNumberOfGuests
        {
            get => _strNumberOfGuests;
            set
            {
                if (value != _strNumberOfGuests)
                {
                    try
                    {
                        int _numberOfGuests;
                        int.TryParse(value, out _numberOfGuests);
                        NumberOfGuests = _numberOfGuests;
                    }
                    catch (Exception) { }
                    _strNumberOfGuests = value;
                    OnPropertyChanged();
                }
            }
        }
        public int ReservationDays { get; set; }
        private string _strReservationDays;
        public string StrReservationDays
        {
            get => _strReservationDays;
            set
            {
                if (value != _strReservationDays)
                {
                    try
                    {
                        int _reservationDays;
                        int.TryParse(value, out _reservationDays);
                        ReservationDays = _reservationDays;
                    }
                    catch (Exception) { }
                    _strReservationDays = value;
                    OnPropertyChanged();
                }
            }
        }
        private DateTime _startDay;
        public DateTime StartDay
        {
            get
            {
                return _startDay;
            }
            set
            {
                if (value != _startDay)
                {
                    _startDay = value;
                    OnPropertyChanged("StartDay");
                }
            }
        }
        private DateTime _endDay;
        public DateTime EndDay
        {
            get
            {
                return _endDay;
            }
            set
            {
                if (value != _endDay)
                {
                    _endDay = value;
                    OnPropertyChanged("EndDay");
                }
            }
        }
        private ObservableCollection<DateTime> _blackoutedDates;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AnywhereAnytimeViewModel()
        {
            _accommodations = new ObservableCollection<AccommodationReservationDTO>();
            _accommodationService = Injector.CreateInstance<IAccommodationService>();
            _accommodationReservationService = Injector.CreateInstance<IAccommodationReservationService>();
            _reservationService = Injector.CreateInstance<IReservationService>();
            _userService = Injector.CreateInstance<IUserService>();
            ApplyFiltersCommand = new RelayCommand(ApplyFilters);
            ResetFiltersCommand = new RelayCommand(ResetFilters);
            StartDay = DateTime.Now;
            EndDay = DateTime.Now;
            AccommodationsNumber = 0;
        }

        public void ApplyFilters(object parameter)
        {
            Accommodations.Clear();
            List<Accommodation> accommodations = _accommodationService.GetAccommodationsByGuestsAndDaysReserved(NumberOfGuests, ReservationDays);
            foreach(Accommodation accommodation in accommodations)
            {
                List<DateRange> days = _accommodationReservationService.GetAvailableDays(accommodation.AccommodationID, ReservationDays, StartDay, EndDay);
                Make(accommodation, days);
            }
            AccommodationsNumber = Accommodations.Count();
        }
        private void Make(Accommodation accommodation,List<DateRange> days)
        {
            foreach(DateRange day in days)
            {
                AccommodationReservationDTO a = new AccommodationReservationDTO(accommodation.Name, accommodation.Location, accommodation.TypeOfAccommodation, day);
                Accommodations.Add(a);
            }
        }
        public void ResetFilters(object parameter)
        {
            StartDay = DateTime.Now;
            EndDay = DateTime.Now;
            StrReservationDays = string.Empty;
            StrNumberOfGuests = string.Empty;
            Accommodations.Clear();
            AccommodationsNumber = 0;
        }
    }
}
