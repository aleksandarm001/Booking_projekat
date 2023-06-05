using InitialProject.Presentation.WPF.ViewModel.Owner;
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

namespace InitialProject.Presentation.WPF.View.Owner.StartWindowPages
{
    /// <summary>
    /// Interaction logic for GuestsSideBarPage.xaml
    /// </summary>
    public partial class GuestsSideBarPage : Page
    {

        private GuestsViewModel viewModel { get; set; }
        public GuestsSideBarPage(System.Windows.Navigation.NavigationService navService, int UserId)
        {
            InitializeComponent();
            this.viewModel = new GuestsViewModel(navService, UserId);
            this.DataContext = viewModel;
        }
    }
}
