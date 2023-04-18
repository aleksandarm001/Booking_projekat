using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace InitialProject.View.Guest1
{
    /// <summary>
    /// Interaction logic for ReservationChange.xaml
    /// </summary>
    public partial class ReservationChange : Window
    {
        private ReservationChangeViewModel viewModel;
        public ReservationChange(int userId, ObservableCollection<ChangeReservationRequest> Requests)
        {
            InitializeComponent();
            viewModel = new ReservationChangeViewModel(userId, Requests);
            DataContext = viewModel;
            Send_Button.IsEnabled = false;
        }
        private void SendRequest_Button(object sender, RoutedEventArgs e)
        {
            viewModel.SendRequest_Button();
            this.Close();
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.ComboBox_SelectionChanged(CheckInPicker, CheckOutPicker);
            Send_Button.IsEnabled = true;
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
