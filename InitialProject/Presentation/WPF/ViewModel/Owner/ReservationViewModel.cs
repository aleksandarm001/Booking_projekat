using InitialProject.Presentation.WPF.View.Owner.StartWindowPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InitialProject.Presentation.WPF.ViewModel.Owner
{
    public class ReservationViewModel
    {
        public System.Windows.Navigation.NavigationService NavService { get; set; }
        int UserId;

        public RelayCommand NavigateToChangeReservationRequest { get; set; }

        public ReservationViewModel(System.Windows.Navigation.NavigationService navService, int userId) 
        {
            this.NavigateToChangeReservationRequest = new RelayCommand(ChangeReservationRequest_ButtonClick);
        }


        private void ChangeReservationRequest_ButtonClick(object obj)
        {
            Page changeReservationRequest = new ChangeReservationRequestPage(UserId);
            this.NavService.Navigate(changeReservationRequest);
        }
    }
}
