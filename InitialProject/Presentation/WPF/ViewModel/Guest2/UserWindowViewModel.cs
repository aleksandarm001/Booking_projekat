namespace InitialProject.Presentation.WPF.ViewModel.Guest2
{
    using InitialProject.Presentation.WPF.View.Guest2.Views;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using System.Windows.Navigation;

    public class UserWindowViewModel
    {
        public NavigationService NavService { get; set; }
        public RelayCommand NavigateToFinishedToursCommand { get; set; }
        public RelayCommand NavigateToReservedToursCommand { get; set; }
        public RelayCommand NavigateToVouchersPageCommand { get; set; }

        /*private void Execute_NavigateToTourPageCommand(object obj)
        {
            Page toursView = new ToursView();
            this.NavService.Navigate(toursView);
        }*/

        private void Execute_NavigateToUserPageCommand(object obj)
        {
            /*Page usersView = new UserView();
            this.NavService.Navigate(usersView);*/
        }

        public UserWindowViewModel(NavigationService navService)
        {
            this.NavigateToFinishedToursCommand = new RelayCommand(Execute_NavigateToUserPageCommand);
            this.NavigateToReservedToursCommand = new RelayCommand(Execute_NavigateToUserPageCommand);
            this.NavigateToVouchersPageCommand = new RelayCommand(Execute_NavigateToUserPageCommand);
            this.NavService = navService;

        }
    }
}
