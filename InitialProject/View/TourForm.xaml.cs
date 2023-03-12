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
        private readonly TourRepository _tourRepository;
        private readonly int tourId;
        public static ObservableCollection<Language> Languages { get; set; }

        public TourForm()
        {
            InitializeComponent();
            DataContext = this;
            _languageRepository = new LanguageRepository();
            _tourRepository = new TourRepository();
            _tourPointRepository = new TourPointRepository();
            tourId = _tourRepository.NextId();
            Languages = new ObservableCollection<Language>(_languageRepository.GetAll());
            //_tourPointRepository.ClearTemp();

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



        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {

        }

        
            
            
        private void SaveTour(object sender, RoutedEventArgs e)
        {

        }

        private void AddKeyPoint_ButtonClick(object sender, RoutedEventArgs e)
        {
            TourPointForm addTourPoint = new TourPointForm(tourId);
            addTourPoint.Show();
        }

        private void AddDatesAndTimes_ButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void AddPictures_ButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel_ButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void Save_ButtonClick(object sender, RoutedEventArgs e)
        {
            Language language = new Language();
            language.Name = "srpski";
            Location location = new Location();
            location.City = "zrenjanin";
            location.Country = "serbia";
            Tour tour = new Tour();
            tour.TourId = 1;
            tour.Name = "Test";
            tour.Location = location;
            tour.Duration = 5;
            tour.Description= "TestDesc";
            tour.Language = language;
            tour.MaxGuestNumber= 5;
            DateTime dateTime1 = new DateTime();
            dateTime1 = DateTime.Now;

            tour.StartingDateTime = dateTime1;

            TourPoint pt = new TourPoint();
            pt.TourId = 1;
            pt.Name = "Test";
            pt.Order = 1;
            pt.CurrentActive = 0;
            pt.Description= "Tes2t";
            pt.Id = 1;

            List<TourPoint> points= new List<TourPoint>();
            points.Add(pt);

            tour.KeyPoints = points;

            _tourRepository.Save(tour);

    }

        
    }
}
