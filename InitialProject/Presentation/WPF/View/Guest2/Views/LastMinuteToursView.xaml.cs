using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.ViewModel.Guest2;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.Presentation.WPF.View.Guest2.Views
{
    /// <summary>
    /// Interaction logic for LastMinuteToursView.xaml
    /// </summary>
    public partial class LastMinuteToursView : Page
    {
        private readonly LastMinuteTourViewModel viewModel;
        public LastMinuteToursView()
        {
            viewModel = new LastMinuteTourViewModel();
            InitializeComponent();
            this.DataContext = viewModel;
        }

        private void TourPreview_Click(object sender, RoutedEventArgs e)
        {
            TourView tourView = new TourView((Tour)DataGridTure.SelectedItem);
            this.NavigationService.Navigate(tourView);
        }
    }
}
