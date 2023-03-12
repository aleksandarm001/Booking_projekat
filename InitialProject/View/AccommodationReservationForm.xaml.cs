using InitialProject.CustomClasses;
using InitialProject.Model;
using InitialProject.Repository;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AccommodationReservationForm.xaml
    /// </summary>
    public partial class AccommodationReservationForm : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DateTime _startDay;
        private DateTime _endDay;
        private int _reservationDays;
        private int _numberOfGuests;
        private readonly ReservationRepository _reservationRepository;
        private ObservableCollection<DateRange> _dateRanges;
        public ObservableCollection<DateRange> DateRanges
        {
            get { return _dateRanges; }
            set
            {
                _dateRanges = value;
                OnPropertyChanged(nameof(DateRanges));
            }
        }
        public DateRange SelectedDateRange { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public string AccommodationName { get; set; }
        public List<Reservation> Reservations { get; set; }
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
                    EndDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, StartDay));
                    OnPropertyChanged("StartDay");
                }
            }
        }
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
        public int ReservationDays
        {
            get
            {
                return _reservationDays;
            }
            set
            {
                if (value != _reservationDays)
                {
                    _reservationDays = value;
                    OnPropertyChanged("ReservationDays");
                }
            }
        }
        public int NumberOfGuests
        {
            get { return _numberOfGuests; }
            set
            {
                _numberOfGuests = value;
            }
        }
        public AccommodationReservationForm(Accommodation accommodation)
        {
            InitializeComponent();
            this.DataContext = this;
            SelectedAccommodation = accommodation;
            AccommodationName = accommodation.Name;
            _reservationRepository = new ReservationRepository();
            Reservations = new List<Reservation>(_reservationRepository.GetReservationsByAccommodationId(SelectedAccommodation.AccommodationID));
            StartDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
            EndDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1))));
            DateRanges = new ObservableCollection<DateRange>();
        }
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private List<DateRange> extractFreeDates(List<Reservation> reservations)
        {
            List<DateRange> result = new List<DateRange>();
            for (var day = StartDay; day.Date <= EndDay; day = day.AddDays(1))
                {
                    if (day.AddDays(ReservationDays).Date > EndDay)
                    {
                        break;
                    }
                    else
                    {
                        DateRange range = new DateRange(day.Date, day.AddDays(ReservationDays).Date);
                        result.Add(range);
                    }
                }
            List<DateRange> tempResult = new List<DateRange>();
            foreach (Reservation reservation in reservations)
            {
                foreach (DateRange range in result)
                {
                    if (reservation.ReservationDateRange.WithinRange(range))
                    {
                        tempResult.Add(range);
                    }
                }
                foreach (DateRange range in tempResult)
                {
                    DateRange dr = result.Find(r => r.StartDate == range.StartDate && r.EndDate == range.EndDate);
                    result.Remove(dr);
                }
            }
            return result;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NoFreeReservation.Visibility = Visibility.Hidden;
            if (ReservationDays < SelectedAccommodation.MinReservationDays)
            {
                MessageBox.Show("Minimum number of days to reserve " + AccommodationName + " is " + SelectedAccommodation.MinReservationDays);
            }
            else
            {
                DateRanges.Clear();
                List<DateRange> freeDates = extractFreeDates(Reservations);
                if (freeDates.Count == 0)
                {
                    NoFreeReservation.Visibility = Visibility.Visible;
                    StartDay = DateTime.Now;
                    EndDay = StartDay.AddDays(90);
                    DateRanges = new ObservableCollection<DateRange>(extractFreeDates(Reservations));
                }
                else
                {
                    DateRanges = new ObservableCollection<DateRange>(freeDates);
                }
            }
        }

        private void DataGritMenuItemClick(object sender, RoutedEventArgs e)
        {
            EnterGuestNumberDialog dlg = new EnterGuestNumberDialog(SelectedAccommodation.MaxGuestNumber);
            dlg.ShowDialog();
            if(dlg.NumberOfGuests != 0)
            {
                NumberOfGuests = dlg.NumberOfGuests;
                Reservation reservation = new Reservation(SelectedAccommodation.AccommodationID, SelectedDateRange, NumberOfGuests);
                SelectedAccommodation.Reservations.Add(reservation);
                _reservationRepository.Save(reservation);
                MessageBox.Show("You successfuly reserved " + ReservationDays.ToString() + " day(s) at " + AccommodationName);
                this.Close();
            }
        }
    }
}
/*
    TO DO:
        U prethodnom prozoru, ucitaj sve rezervacije u svaki Accommodation (U listu accommodation) 
        onda iteriraj u ovom prozoru kroz tu listu a ne koristiti svaki put citanje iz fajla.
*/
