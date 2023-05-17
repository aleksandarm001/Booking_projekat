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

    public class MainWindowViewModel
    {
        public NavigationService NavService { get; set; }
        public RelayCommand NavigateToUserPageCommand { get; set; }
        public RelayCommand NavigateToMainPageCommand { get; set; }
        public RelayCommand NavigateToTourPageCommand { get; set; }

        private void Execute_NavigateToTourPageCommand(object obj)
        {
            Page toursView = new ToursView();
            this.NavService.Navigate(toursView);
        }
        
        private void Execute_NavigateToUserPageCommand(object obj)
        {
            Page usersView = new UserView(1);
            this.NavService.Navigate(usersView);
        }
      
        public MainWindowViewModel(NavigationService navService, int userId)
        {
            this.NavigateToUserPageCommand = new RelayCommand(Execute_NavigateToUserPageCommand);
            this.NavigateToMainPageCommand = new RelayCommand(Execute_NavigateToTourPageCommand);
            this.NavigateToTourPageCommand = new RelayCommand(Execute_NavigateToTourPageCommand);
            this.NavService = navService;

        }


    }
}
