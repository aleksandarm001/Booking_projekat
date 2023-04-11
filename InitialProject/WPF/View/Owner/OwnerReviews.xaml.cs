using InitialProject.Model;
using InitialProject.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Media;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.View.Owner
{
    /// <summary>
    /// Interaction logic for OwnerReviews.xaml
    /// </summary>
    public partial class OwnerReviews : Window ,INotifyPropertyChanged
    {
        private readonly OwnerRateService _ownerRateService = new OwnerRateService();
        private ObservableCollection<OwnerRate> _ownerRates;

        public ObservableCollection<OwnerRate> OwnerRates
        {
            get { return _ownerRates; }
            set
            {
                _ownerRates = value;
                OnPropertyChanged(nameof(OwnerRates));
            }
        }
        

        public OwnerReviews()
        {
            InitializeComponent();
            DataContext = this;

            OwnerRates = new ObservableCollection<OwnerRate>(_ownerRateService.RatingsFromRatedGuests());
            showSuperOwner();
            
        }

        public void showSuperOwner()
        {
            if (_ownerRateService.isSuperOwner(0))
            {
                star.Visibility = Visibility.Visible;
            }
            else
            {
                star.Visibility = Visibility.Collapsed;
            }
        }
    
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ReservationRescheduleRequests reservationRescheduleRequests= new ReservationRescheduleRequests();
            reservationRescheduleRequests.Show();
        }
    }
}
