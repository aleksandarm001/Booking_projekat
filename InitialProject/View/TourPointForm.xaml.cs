using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourPointForm.xaml
    /// </summary>
    public partial class TourPointForm : Window
    {
        private readonly int tourId;
        public TourPointForm(int tourID)
        {
            DataContext = this;
            tourId = tourID;
            InitializeComponent();
        }

        
        private void AddTourPoint_ButtonClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
