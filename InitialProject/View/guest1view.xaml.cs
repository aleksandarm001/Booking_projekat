using InitialProject.CustomClasses;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using System.Windows;
using System.Collections.ObjectModel;
using InitialProject.Model;
using System.Diagnostics.Metrics;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for guest1view.xaml
    /// </summary>
    public partial class Guest1View : Window
    {
        public static ObservableCollection<Location> Locations { get; set; }
        public static ObservableCollection<string> Cities { get; set; }
        public static ObservableCollection<string> Countries { get; set; }
        public static CollectionViewSource CollectionViewLocations { get; set; }
        public static ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }

        private readonly AccommodationRepository _repository;
        private readonly LocationRepository _lrepository;

        public Guest1View()
        {
            InitializeComponent();
            DataContext = this;
            _repository = new AccommodationRepository();
            _lrepository = new LocationRepository();
            Accommodations = new ObservableCollection<Accommodation>((IEnumerable<Accommodation>)_repository.getAll());
            Locations = new ObservableCollection<Location>((IEnumerable<Location>)_lrepository.getAll());
            Cities = new ObservableCollection<string>();
            Countries = new ObservableCollection<string>();
            readCitiesAndCountries();
        }
        private void readCitiesAndCountries()
        {
            Cities.Add(" ");
            Countries.Add(" ");
            foreach (Location l in Locations)
            {
                Cities.Add(l.City);
                if (!Countries.Contains(l.Country))
                {
                    Countries.Add(l.Country);
                }
            }
        }
        private void ApplyAdditionalSearch(object sender, RoutedEventArgs e)
        {

        }

        private void Filter_Cities(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmbx = (ComboBox)sender;
            string country = "";
            try
            {
                if(cmbx.SelectedItem != null)
                {
                    country = cmbx.SelectedItem.ToString();
                }
                else
                {
                    cmbx.SelectedItem = 0;
                }
            }catch(System.NullReferenceException)
            {
                readCitiesAndCountries();
            }
            if (country == " ")
            {
                Cities.Clear();
                readCitiesAndCountries();
            }
            else
            {
                Cities.Clear();
                Cities.Add(" ");
                foreach (Location loc in Locations)
                {
                    if (loc.Country == country)
                    {
                        Cities.Add(loc.City);
                    }
                }
                CityCmbx.SelectedIndex = 1;
            }
        }
        private void Filter_Countries(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
