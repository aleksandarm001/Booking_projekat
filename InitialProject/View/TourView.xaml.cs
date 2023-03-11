namespace InitialProject.View
{
    using InitialProject.Model;
    using InitialProject.Repository;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Transactions;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for TourView.xaml
    /// </summary>
    public partial class TourView : Window
    {
        private int UserId { get; }
        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }
        
        private TourRepository _tourRepository;

        public TourView(int userId)
        {
            InitializeComponent();
            DataContext = this;
            UserId = userId;
            _tourRepository = new TourRepository();
           // SelectedTour = new Tour();
            Tours = new ObservableCollection<Tour>(_tourRepository.GetAll());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Rezervisi_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour == null) 
            {
                string messageBoxText = "Morate prvo izabrati turu!";
                string caption = "Rezervacija ture";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;

                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            }
            else
            {
                TourReservation tourReservation = new TourReservation(UserId, SelectedTour);
                tourReservation.Show();
                //Close();
            }
        }

     
    }
}
