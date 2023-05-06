using InitialProject.Aplication.Factory;
using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Services;
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
    /// Interaction logic for AddRenovation.xaml
    /// </summary>
    public partial class AddRenovation : Window, INotifyPropertyChanged
    {
        private readonly IRenovationService _renovationService;

        public Accommodation _selectedAccommodation = new Accommodation();
        public DateRange SelectedDateRange { get; set; }


        public ObservableCollection<DateRange> _availableDates;


        public ObservableCollection<DateRange> AvailableDates
        {
            get { return _availableDates; }
            set
            {
                _availableDates = value;
                OnPropertyChanged(nameof(AvailableDates));
            }
        }

        int AccommodationID { get; set; }


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
                    EndDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, StartDay));
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

        private int _days;

        public int Days
        {
            get { return _days; }
            set
            {
                _days = value;
                OnPropertyChanged("NumberOfGuests");
            }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public AddRenovation(Accommodation selectedAccommodation)
        {
            InitializeComponent();
            this.DataContext = this;

            _renovationService = Injector.CreateInstance<IRenovationService>();

            _selectedAccommodation = selectedAccommodation;
            AccommodationID = selectedAccommodation.AccommodationID;


            AvailableDates = new ObservableCollection<DateRange>();

            StartDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
            EndDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));


        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SearchDates_ButtonClick(object sender, RoutedEventArgs e)
        {
            AvailableDates.Clear();
            AvailableDates = _renovationService.GetAvailableDates(_selectedAccommodation, StartDay, EndDay, Days);
        }

        private void AddRenovation_ButtonClick(object sender, RoutedEventArgs e)
        {
            Renovation renovation = _renovationService.CreateNewRenovation(_selectedAccommodation.Name, AccommodationID, SelectedDateRange, Description);
            _renovationService.SaveRenovation(renovation);

        }
    }
}
