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
    /// Interaction logic for RenovationsPage.xaml
    /// </summary>
    public partial class RenovationsPage : Page
    {
        private RenovationViewModel viewModel { get; set; }
        public RenovationsPage(int userId)
        {
            viewModel = new RenovationViewModel(userId);
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
