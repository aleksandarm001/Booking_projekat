using InitialProject.Presentation.WPF.View.Owner.StartWindowPages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InitialProject.Presentation.WPF.ViewModel.Owner
{
    public class HomeViewModel 
    {
        public System.Windows.Navigation.NavigationService NavService { get; set; }

        public RelayCommand NavigateToAllAccommodations { get; set; }
        public RelayCommand NavigateToAddAccommodations { get; set; }
        public RelayCommand NavigateToChangeReservationRequest { get; set; }
        public RelayCommand NavigateToRenovations { get; set; }

        int UserId;
        public HomeViewModel(System.Windows.Navigation.NavigationService navService,int userId) 
        {
            UserId = userId;

            this.NavService = navService;
            this.NavigateToAllAccommodations = new RelayCommand(AllAccommodations_ButtonClick);
            this.NavigateToAddAccommodations = new RelayCommand(AddAccommodation_ButtonClick);
            this.NavigateToRenovations = new RelayCommand(Renovations_ButtonClick);

        }


        private void AllAccommodations_ButtonClick(object obj)
        {
            Page allAccommodations = new AllAccommodations(UserId);
            this.NavService.Navigate(allAccommodations);
            
        }

        private void AddAccommodation_ButtonClick(object obj)
        {
            Page addAccommodations = new AddAccommodation(UserId);
            this.NavService.Navigate(addAccommodations);
        }

        private void Renovations_ButtonClick(object obj)
        {
            Page renovations = new RenovationsPage(UserId);
            this.NavService.Navigate(renovations);
        }
    }
}
