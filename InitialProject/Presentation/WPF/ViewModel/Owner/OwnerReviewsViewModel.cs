using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Presentation.WPF.ViewModel.Owner
{
    public class OwnerReviewsViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly IOwnerRateService _ownerRateService;


        public ObservableCollection<OwnerRate> _ownerRates;
        public ObservableCollection<OwnerRate> OwnerRates
        {
            get { return _ownerRates; }
            set
            {
                _ownerRates = value;
                OnPropertyChanged(nameof(OwnerRates));

            }
        }

        int UserId;

        public OwnerReviewsViewModel(int userId) 
        {
            UserId = userId;

            _ownerRateService = Injector.CreateInstance<IOwnerRateService>();

            OwnerRates = new ObservableCollection<OwnerRate>(_ownerRateService.RatingsFromRatedGuest(UserId));

        }
    }
}
