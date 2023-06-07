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
    public class ProposalForRegistrationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly IAccommodationPropositionService _propositionService;
        private readonly IAccommodationService _accommodationService;

        public Accommodation _mostReservationAccommodation;
        public Accommodation MostReservationAccommodation
        {
            get { return _mostReservationAccommodation; }
            set
            {
                _mostReservationAccommodation = value;
                OnPropertyChanged(nameof(MostReservationAccommodation));
            }
        }

        public Accommodation _leastReservationAccommodation;
        public Accommodation LeastReservationAccommodation
        {
            get { return _leastReservationAccommodation; }
            set
            {
                _leastReservationAccommodation = value;
                OnPropertyChanged(nameof(LeastReservationAccommodation));
            }
        }

        public Accommodation _leastDaysOccupied;
        public Accommodation LeastDaysOccupied
        {
            get { return _leastDaysOccupied; }
            set
            {
                _leastDaysOccupied = value;
                OnPropertyChanged(nameof(LeastDaysOccupied));
            }
        }

        public Accommodation _mostDaysOccupied;
        public Accommodation MostDaysOccupied
        {
            get { return _mostDaysOccupied; }
            set
            {
                _mostDaysOccupied = value;
                OnPropertyChanged(nameof(MostDaysOccupied));
            }
        }

        int UserId;

        public ProposalForRegistrationViewModel(int userId) 
        {
            UserId= userId;

            _propositionService = Injector.CreateInstance<IAccommodationPropositionService>();
            _accommodationService= Injector.CreateInstance<IAccommodationService>();

            MostReservationAccommodation = _accommodationService.GetAccommodationById(_propositionService.AccommodationWithMostReservations(UserId));
            LeastReservationAccommodation = _accommodationService.GetAccommodationById(_propositionService.AccommodationWithLeastReservations(UserId));
            MostDaysOccupied = _accommodationService.GetAccommodationById(_propositionService.HotAccommodationStatistics(UserId));
            LeastDaysOccupied = _accommodationService.GetAccommodationById(_propositionService.ColdAccommodationStatistics(UserId));
        
        }
    }
}
