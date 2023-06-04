using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.View.Owner;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Presentation.WPF.ViewModel.Owner
{
    public class RenovationViewModel :INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly IRenovationService _renovationService;
        private readonly IAccommodationService _accommodationService;

        public ObservableCollection<Renovation> _renovations;

        public ObservableCollection<Renovation> ScheduledRenovations
        {
            get { return _renovations; }
            set
            {
                _renovations = value;
                OnPropertyChanged(nameof(ScheduledRenovations));
            }
        }

        public ObservableCollection<Renovation> _finishedRenovations;

        public ObservableCollection<Renovation> FinishedRenovations
        {
            get { return _finishedRenovations; }
            set
            {
                _finishedRenovations = value;
                OnPropertyChanged(nameof(FinishedRenovations));
            }
        }

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

        int UserId;

        public Accommodation SelectedAccommodation { get; set; }
        public Renovation SelectedRenovation { get; set; }


        public RelayCommand AddRenovation { get; set; }
        public RelayCommand CancelRenovation { get; set; }


        public RenovationViewModel(int userId) 
        {
            UserId = userId;
            _renovationService = Injector.CreateInstance<IRenovationService>();
            _accommodationService = Injector.CreateInstance<IAccommodationService>();

            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAccommodationsByOwnerId(UserId));
            ScheduledRenovations = new ObservableCollection<Renovation>(_renovationService.GetScheduledRenovationsByOwnerId(UserId));
            FinishedRenovations = new ObservableCollection<Renovation>(_renovationService.GetFinishedRenovationsByOwnerId(UserId));


            AddRenovation = new RelayCommand(AddRenovation_ButtonClick);
            CancelRenovation = new RelayCommand(CancelRenovation_ButtonClick);

        }


        private void AddRenovation_ButtonClick(object parameter)
        {
            if (SelectedAccommodation == null)
            {
                MessageBox.Show("Please select accommodation!");
            }
            else
            {
                AddRenovation addRenovation = new AddRenovation(SelectedAccommodation);
                addRenovation.Show();
            }
        }

        private void CancelRenovation_ButtonClick(object parameter)
        {
            if (SelectedRenovation == null)
            {
                MessageBox.Show("Please select a renovation");
            }
            else
            {
                if (_renovationService.isCancelationPeriodExpired(SelectedRenovation))
                {
                    MessageBoxResult Expired = MessageBox.Show("The cancelation period for this renovation has expired", "Canceling renovation");
                }
                else
                {
                    MessageBoxResult cancel = MessageBox.Show("Are you sure you want to cancel this renovation?", "Cancel renovation", MessageBoxButton.YesNo);
                    if (cancel == MessageBoxResult.Yes)
                    {
                        _renovationService.DeleteRenovation(SelectedRenovation);
                        ScheduledRenovations.Remove(SelectedRenovation);
                    }

                }
            }
        }
    }
}
