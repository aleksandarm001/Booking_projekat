using InitialProject.Model;
using InitialProject.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.View.Owner
{
    /// <summary>
    /// Interaction logic for OwnerReviews.xaml
    /// </summary>
    public partial class OwnerReviews : Window,INotifyPropertyChanged
    {
        private readonly OwnerRateService _ownerRateService = new OwnerRateService();
        public ObservableCollection<OwnerRate> _ownerRates;
        public int OwnerId { get; set; }

        public ObservableCollection<OwnerRate> OwnerRates
        {
            get { return _ownerRates; }
            set 
            {
                _ownerRates = value;
                OnPropertyChanged(nameof(OwnerRates));  
            
            }
        }
        public OwnerReviews(int userId)
        {
            InitializeComponent();
            DataContext= this;
            OwnerId = userId;

            OwnerRates = new ObservableCollection<OwnerRate>(_ownerRateService.RatingsFromRatedGuest(OwnerId));
            showSuperOwner(OwnerId);
        }

        public void showSuperOwner(int ownerId)
        {
            if(_ownerRateService.isSuperOwner(ownerId))
            {
                zvjezda.Visibility = Visibility.Visible;
            }
            else
            {
                zvjezda.Visibility = Visibility.Collapsed;
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
