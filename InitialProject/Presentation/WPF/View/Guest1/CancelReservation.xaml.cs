using InitialProject.Domen.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace InitialProject.Presentation.WPF.ViewModel.Guest1
{
    /// <summary>
    /// Interaction logic for CancelReservation.xaml
    /// </summary>
    public partial class CancelReservation : Window
    {
        private CancelReservationViewModel viewModel;
        public CancelReservation(ObservableCollection<ChangeReservationRequest> requests)
        {
            InitializeComponent();
            viewModel = new CancelReservationViewModel(requests);
            DataContext = viewModel;
        }
    }
}
