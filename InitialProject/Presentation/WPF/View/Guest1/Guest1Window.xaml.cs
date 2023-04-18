using InitialProject.Domen.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace InitialProject.View.Guest1
{
    /// <summary>
    /// Interaction logic for Guest1Window.xaml
    /// </summary>
    public partial class Guest1Window : Window
    {
        private int _userId;
        public static ObservableCollection<Location> Locations;
        public Guest1Window(int userId, ObservableCollection<Location> locations)
        {
            InitializeComponent();
            _userId = userId;
            Locations = locations;
        }

        private void AccommodationDisplay_Click(object sender, RoutedEventArgs e)
        {
            AccommodationDisplay accommodationDisplay = new AccommodationDisplay(Locations, _userId);
            accommodationDisplay.ShowDialog();
        }

        private void RequestsOverview_Click(object sender, RoutedEventArgs e)
        {
            RequestsOwerview requestsOwerview = new RequestsOwerview(_userId);
            requestsOwerview.ShowDialog();
        }

        private void OwnerRating_Click(object sender, RoutedEventArgs e)
        {
            OwnerRating ownerRating = new OwnerRating(_userId);
            ownerRating.ShowDialog();
        }
    }
}
