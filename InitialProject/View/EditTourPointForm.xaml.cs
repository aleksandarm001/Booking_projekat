using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for EditTourPointForm.xaml
    /// </summary>
    public partial class EditTourPointForm : Window
    {
        private readonly TourPoint _tourPoint;
        private readonly List<int> _availableOrders;
        public static ObservableCollection<int> Orders { get; set; }

        public EditTourPointForm(TourPoint tourPoint,List<int> availableOrders)
        {
            _tourPoint = tourPoint;
            _availableOrders = availableOrders;
            InitializeComponent();
            DataContext = this;
            FirstName.Content = _tourPoint.Name;
            Orders = new ObservableCollection<int>(availableOrders);
        }

        

        private void Save_ButtonClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
