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
using static InitialProject.Model.TourPoint;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourForm.xaml
    /// </summary>
    public partial class TourForm : Window, INotifyPropertyChanged
    {
        private readonly LanguageRepository _languageRepository;
        private readonly TourPointRepository _tourPointRepository;
        private readonly LocationRepository _locationRepository;
        private readonly TourRepository _tourRepository;
        private readonly int tourId;
        public static ObservableCollection<Language> Languages { get; set; }
        public static ObservableCollection<string> Countries { get; set; }
        public static ObservableCollection<string> Cities { get; set; }
        public static ObservableCollection<string> KeyPoints { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }


        public TourForm()
        {
            InitializeComponent();
            DataContext = this;
            _languageRepository = new LanguageRepository();
            _tourRepository = new TourRepository();
            _tourPointRepository = new TourPointRepository();
            _locationRepository= new LocationRepository();
            tourId = _tourRepository.NextId();
            Languages = new ObservableCollection<Language>(_languageRepository.GetAll());
            Locations = new ObservableCollection<Location>(_locationRepository.getAll());
            Cities = new ObservableCollection<string>();
            Countries = new ObservableCollection<string>();
            KeyPoints = new ObservableCollection<string>();
            ReadCitiesAndCountries();
            _tourPointRepository.ClearTemp();

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


        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {

        }

        private void SaveTour(object sender, RoutedEventArgs e)
        {

        }

        private void AddKeyPoint_ButtonClick(object sender, RoutedEventArgs e)
        {
            TourPointForm addTourPoint = new TourPointForm(tourId, KeyPoints);
            addTourPoint.Show();
        }

        private void AddDatesAndTimes_ButtonClick(object sender, RoutedEventArgs e)
        {
            DateTimePicker date = new DateTimePicker();
            date.Show();
        }

        private void AddPictures_ButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel_ButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void Save_ButtonClick(object sender, RoutedEventArgs e)
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _language;
        public string Language
        {
            get { return _language; }
            set
            {
                _language = value;
                OnPropertyChanged(nameof(_language));
            }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(_Name));
            }
        }

        

        private string _MaxGuests;
        public string MaxGuests
        {
            get { return _MaxGuests; }
            set
            {
                _MaxGuests = value;
                OnPropertyChanged(nameof(_MaxGuests));
            }
        }

        

        private string _TourDuratation;
        public string TourDuratation
        {
            get { return _TourDuratation; }
            set
            {
                _TourDuratation = value;
                OnPropertyChanged(nameof(_TourDuratation));
            }
        }




    }
}
