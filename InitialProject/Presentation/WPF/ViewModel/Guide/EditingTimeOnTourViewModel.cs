using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InitialProject.Presentation.WPF.ViewModel.Guide
{
    public class EditingTimeOnTourViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<DateTime> Dates { get; set; }
        public ObservableCollection<Dates> ReservedDates { get; set; }

        public ICommand DeleteCommand { get; set; }
        public ICommand AddDateCommand { get; set; }
        public EditingTimeOnTourViewModel()
        {
            AddDateCommand = new RelayCommand(AddDate);
            DeleteCommand = new RelayCommand(DeleteDate);

            Dates = new ObservableCollection<DateTime>();
            Hours = new ObservableCollection<string>();
            Minutes = new ObservableCollection<string>();
            ReservedDates = new ObservableCollection<Dates>();
            Dates dateTime = new Dates();
            dateTime.Date = DateTime.Now.ToString();
            ReservedDates.Add(dateTime);
            LoadHoursAndMinutes();
        }

        public void AddDate(object obj)
        {
            if (SelectedDate.HasValue)
            {
                DateTime combinedDateTime = new DateTime(SelectedDate.Value.Year,
                                                         SelectedDate.Value.Month,
                                                         SelectedDate.Value.Day,
                                                         SelectedHour,
                                                         SelectedMinute,
                                                         0);
                Dates dateTime = new Dates();
                dateTime.Date = combinedDateTime.ToString();
                ReservedDates.Add(dateTime);
            }
            else
            {
            }
        }

        public void DeleteDate(object obj)
        {
            Dates dateToDelete = (Dates)obj;
            if (dateToDelete != null)
            {
                ReservedDates.Remove(dateToDelete);
            }
        }


        public void LoadHoursAndMinutes()
        {
            List<string> minutes = MinutesCounter();
            List<string> hours = HourCounter();

            // clear existing lists
            Hours.Clear();
            Minutes.Clear();

            // add hours and minutes to respective lists
            foreach (var hour in hours)
            {
                Hours.Add(hour);
            }
            foreach (var minute in minutes)
            {
                Minutes.Add(minute);
            }

        }

        public List<string> HourCounter()
        {
            List<string> hours = new List<string>();
            for (int i = 0; i < 24; i++)
            {
                hours.Add(i.ToString("D2"));
            }
            return hours;
        }

        public List<string> MinutesCounter()
        {
            List<string> minutes = new List<string>();
            for (int i = 0; i < 60; i++)
            {
                minutes.Add(i.ToString("D2"));
            }
            return minutes;
        }

        private ObservableCollection<string> _hours;
        public ObservableCollection<string> Hours
        {
            get => _hours;
            set
            {
                _hours = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _minutes;


        public ObservableCollection<string> Minutes
        {
            get => _minutes;
            set
            {
                _minutes = value;
                OnPropertyChanged();
            }
        }

        private int _selectedHour;
        public int SelectedHour
        {
            get => _selectedHour;
            set
            {
                _selectedHour = value;
                OnPropertyChanged();
            }
        }

        private int _selectedMinute;
        public int SelectedMinute
        {
            get => _selectedMinute;
            set
            {
                _selectedMinute = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _selectedDate;
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
