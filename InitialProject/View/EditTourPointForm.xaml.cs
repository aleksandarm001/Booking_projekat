using InitialProject.Model;
using InitialProject.Repository;
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
        private readonly TourPointRepository _tourPointRepository;

        public static ObservableCollection<int> Orders { get; set; }

        public List<int> _availableOrders;
        public List<int> _orders;
        public List<int> _usedOrders;


        public EditTourPointForm(TourPoint tourPoint,List<int> availableOrders,List<int> orders,List<int> usedOrders)
        {
            _tourPoint = tourPoint;
            _availableOrders = availableOrders;
            _orders = orders;
            _usedOrders = usedOrders;
            _tourPointRepository = new TourPointRepository();

            InitializeComponent();
            DataContext = this;

            FirstName.Content = _tourPoint.Name;
            Orders = new ObservableCollection<int>(_orders);
        }

        

        

        private void Save_ButtonClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
