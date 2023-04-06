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

namespace InitialProject.View.Guest1
{
    /// <summary>
    /// Interaction logic for RequestsOwerview.xaml
    /// </summary>
    public partial class RequestsOwerview : Window
    {
        private int userId;
        public RequestsOwerview(int userId)
        {
            InitializeComponent();
            this.userId = userId;   
        }

        private void CancelReservation_Click(object sender, RoutedEventArgs e)
        {
            CancelReservation cancelReservationWindow = new CancelReservation(userId);
            cancelReservationWindow.ShowDialog();
        }
    }
}
