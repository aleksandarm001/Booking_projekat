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
    /// Interaction logic for OwnerReviewsPage.xaml
    /// </summary>
    public partial class OwnerReviewsPage : Page
    {
        private OwnerReviewsViewModel viewModel { get; set; }
        public OwnerReviewsPage(int userId)
        {
            viewModel = new OwnerReviewsViewModel(userId);    
            InitializeComponent();
            this.DataContext= viewModel;
        }
    }
}
