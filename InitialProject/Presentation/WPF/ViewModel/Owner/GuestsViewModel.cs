using InitialProject.Presentation.WPF.View.Owner.StartWindowPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InitialProject.Presentation.WPF.ViewModel.Owner
{
    public class GuestsViewModel
    {
        public System.Windows.Navigation.NavigationService NavService { get; set; }
        int UserId;

        public RelayCommand NavigateToGuestsToReview { get; set; }
        public RelayCommand NavigateToOwnerReviews { get; set; }
        public GuestsViewModel(System.Windows.Navigation.NavigationService navService, int userId) 
        {

            UserId = userId;

            this.NavService = navService;
            this.NavigateToGuestsToReview = new RelayCommand(GuestsToReview_ButtonClick);
            this.NavigateToOwnerReviews = new RelayCommand(OwnerReviews_ButtonClick);


        }


        private void GuestsToReview_ButtonClick(object obj)
        {
            Page guestsToReview = new GuestsToReview(UserId);
            this.NavService.Navigate(guestsToReview);
        }

        private void OwnerReviews_ButtonClick(object obj)
        {
            Page ownerReviews = new OwnerReviewsPage(UserId);
            this.NavService.Navigate(ownerReviews);
        }
    }
}
