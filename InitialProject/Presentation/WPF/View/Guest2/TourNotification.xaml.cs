namespace InitialProject.Presentation.WPF.View.Guest2
{
    using InitialProject.Aplication.Factory;
    using InitialProject.Domen.Model;
    using InitialProject.Services.IServices;
    using System;
    using System.Collections.Generic;
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

    /// <summary>
    /// Interaction logic for TourNotification.xaml
    /// </summary>
    public partial class TourNotification : Window
    {
        public TourRequest TourRequest { get; set; }
        private readonly ITourRequestService _tourRequestService;
        public TourNotification(int tourId)
        {
            InitializeComponent();
            DataContext = this;
            _tourRequestService = Injector.CreateInstance<ITourRequestService>();
            TourRequest = _tourRequestService.GetTourRequestById(tourId);
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
