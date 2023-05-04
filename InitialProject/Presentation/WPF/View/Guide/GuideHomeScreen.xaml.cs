using InitialProject.Presentation.WPF.ViewModel.Guide;
using InitialProject.View;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace InitialProject.Presentation.WPF.View.Guide
{
    /// <summary>
    /// Interaction logic for GuideHomeScreen.xaml
    /// </summary>
    public partial class GuideHomeScreen : Window
    {
        private readonly GuideHomeScreenViewModel guideHomeScreenViewModel;
        public GuideHomeScreen()
        {
            guideHomeScreenViewModel = new GuideHomeScreenViewModel(this);
            DataContext = guideHomeScreenViewModel;
            InitializeComponent();
        }

        private void CreateTourButton(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void BurgerMenuButton_Click(object sender, RoutedEventArgs e)
        {

        }




    }
}
