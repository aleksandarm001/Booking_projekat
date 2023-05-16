namespace InitialProject.Presentation.WPF.View.Guest2.Views
{
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

    /// <summary>
    /// Interaction logic for TourView.xaml
    /// </summary>
    public partial class TourView : Page
    {
        public TourView(TourViewModel tourViewModel)
        {
            InitializeComponent();
            DataContext = tourViewModel;
        }

    }
}
