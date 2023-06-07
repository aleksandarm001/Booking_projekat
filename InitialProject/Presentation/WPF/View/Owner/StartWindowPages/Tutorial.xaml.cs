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
using System.Windows.Shapes;

namespace InitialProject.Presentation.WPF.View.Owner.StartWindowPages
{
    /// <summary>
    /// Interaction logic for Tutorial.xaml
    /// </summary>
    public partial class Tutorial : Window
    {

        public Tutorial()
        {
            InitializeComponent();
            this.DataContext= this;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            StopVideo();
        }
        private void StopVideo()
        {
            if (tutorialVideo != null)
            {
                tutorialVideo.Stop();
            }
        }

        private void StartVideo()
        {
            if (tutorialVideo != null)
            {
                tutorialVideo.Play();
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartVideo();
        }
    }
}
