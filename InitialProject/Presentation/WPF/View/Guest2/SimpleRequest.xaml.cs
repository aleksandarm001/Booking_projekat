namespace InitialProject.Presentation.WPF.View.Guest2
{
    using InitialProject.Domen.Model;
    using InitialProject.Presentation.WPF.ViewModel.Guest2;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for SimpleRequest.xaml
    /// </summary>
    public partial class SimpleRequest : Window
    {
        private SimpleRequestViewModel _viewModel;

        public SimpleRequest(int userId, ObservableCollection<TourRequest> tourRequests)
        {
            _viewModel = new SimpleRequestViewModel(userId, tourRequests);
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void DatePickerStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePickerEnd.SelectedDate = DatePickerStart.SelectedDate;
            DatePickerEnd.DisplayDateStart = DatePickerStart.SelectedDate;
        }



        private void FilterCities(object sender, SelectionChangedEventArgs e)
        {
            if (sender is not ComboBox cmbx) return;
            string country = cmbx.SelectedItem?.ToString() ?? string.Empty;
            if (string.IsNullOrEmpty(country))
            {
                _viewModel.ReadCitiesAndCountries();
            }
            else
            {
                _viewModel.UpdateCitiesList(country);
            }
        }
    }
}
