namespace InitialProject.Presentation.WPF.View.Guest2.Views
{
    using InitialProject.Domen.Model;
    using InitialProject.Presentation.WPF.ViewModel.Guest2;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for ToursView.xaml
    /// </summary>
    public partial class ToursView : Page
    {
        private ToursViewModel viewModel;
        public ToursView()
        {
            viewModel = new ToursViewModel();
            InitializeComponent();
            this.DataContext = viewModel;
            

        }

        private void TourPreview_Click(object sender, RoutedEventArgs e)
        {
            TourViewModel tourViewModel = new TourViewModel((Tour)DataGridTure.SelectedItem);
            TourView tourView = new TourView(tourViewModel);
            this.NavigationService.Navigate(tourView);
        }
    }
}
