using InitialProject.Model;
using InitialProject.Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for EditTourPointForm.xaml
    /// </summary>
    public partial class EditTourPointForm : Window
    {
        private readonly TourPoint _tourPoint;
        private readonly TourPointRepository _tourPointRepository;
        
        private ObservableCollection<TourPoint> _tourPoints;
        public static ObservableCollection<int> Orders { get; set; }

        

        public List<int> _availableOrders;
        public List<int> _orders;
        public List<int> _usedOrders;


        public EditTourPointForm(TourPoint tourPoint, List<int> availableOrders, List<int> orders,List<int> usedOrders, ObservableCollection<TourPoint> tourPoints)
        {
            _tourPoint = tourPoint;
            _availableOrders = availableOrders;
            _orders = orders;
            _usedOrders = usedOrders;
            _tourPointRepository = new TourPointRepository();
            _tourPoints = tourPoints;

            InitializeComponent();
            DataContext = this;

            FirstName.Content = _tourPoint.Name;
            Orders = new ObservableCollection<int>(_orders);
        }





        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {

            int broj = int.Parse(OrderComboBox.Text);
            int index = _tourPoints.IndexOf(_tourPoint);
            _tourPointRepository.UpdateTempOrder(_tourPoint, broj);
            _tourPoints[index].Order = broj;
            CollectionViewSource.GetDefaultView(_tourPoints).Refresh();
            Close();

        }
    }
}
