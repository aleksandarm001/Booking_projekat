using InitialProject.Domen.Model;
using InitialProject.Infrastructure.Repository;
using InitialProject.Repository;
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
using System.Windows.Shapes;

namespace InitialProject.Presentation.WPF.View.Owner
{
    /// <summary>
    /// Interaction logic for OwnerStartWindow.xaml
    /// </summary>
    public partial class OwnerStartWindow : Window,INotifyPropertyChanged
    {
        private readonly AccommodationService _accommodationService = new AccommodationService();
        private readonly AccommodationRepository _accommodationRepository= new AccommodationRepository();
        public ObservableCollection<Accommodation> _accommodations;

        public ObservableCollection<Accommodation> Accommodations
        {
            get { return _accommodations; }
            set
            {
                _accommodations = value;
                OnPropertyChanged(nameof(Accommodations));

            }
        }
        public OwnerStartWindow(int userId)
        {
            InitializeComponent();
            DataContext= this;

            Accommodations= new ObservableCollection<Accommodation>(_accommodationService.GetAccommodationsByOwnerId(userId));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
