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
    /// Interaction logic for DateTimePicker.xaml
    /// </summary>
    public partial class DateTimePicker : Window, INotifyPropertyChanged
    {
        public static ObservableCollection<string> Hours { get; set; }
        List<string> numbers = new List<string>();


        public DateTimePicker()
        {
            DataContext = this;
            InitializeComponent();
            Hours = new ObservableCollection<string>();

            LoadHoursAndMinutes();
        }

        public void LoadHoursAndMinutes()
        {
            List<string> minutes = MinutesCounter();
            List<string> hours = HourCounter();

            foreach (var hour in hours) 
            {
                Hours.Add(hour.ToString());            
            }
        }
        public List<string> HourCounter()
        {
            List<string> hours = new List<string>();
            for (int i = 0; i < 24; ++i)
            {
                hours.Add(i.ToString("D2"));
            }
            return hours;
        }

        public List<string> MinutesCounter()
        {
            List<string> minutes = new List<string>();
            for (int i = 0; i < 60; ++i)
            {
                minutes.Add(i.ToString("D2"));
            }
            return minutes;
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
