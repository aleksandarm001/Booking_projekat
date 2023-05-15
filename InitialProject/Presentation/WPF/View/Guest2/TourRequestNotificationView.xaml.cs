namespace InitialProject.Presentation.WPF.View.Guest2
{
    using InitialProject.Aplication.Factory;
    using InitialProject.Domen.CustomClasses;
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
    /// Interaction logic for TourRequestNotificationView.xaml
    /// </summary>
    public partial class TourRequestNotificationView : Window
    {
        public TourRequest TourRequest { get; set; }
        private readonly ITourRequestService _tourRequestService;
        public TourRequestNotificationView(TourNotification notification)
        {
            InitializeComponent();
            DataContext = this;
            _tourRequestService = Injector.CreateInstance<ITourRequestService>();
            TourRequest = _tourRequestService.GetTourRequestById(notification.TourId);
        }

        

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
