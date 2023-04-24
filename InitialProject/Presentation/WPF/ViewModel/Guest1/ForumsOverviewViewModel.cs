using InitialProject.Presentation.WPF.View.Guest1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Presentation.WPF.ViewModel.Guest1
{
    public class ForumsOverviewViewModel
    {
        public RelayCommand CreateForumCommand { get; set; }
        public ForumsOverviewViewModel()
        {
            CreateForumCommand = new RelayCommand(OpenCreateForumForm);
        }
        private void OpenCreateForumForm(object parameter)
        {
            CreatingForumForm creatingForumForm = new CreatingForumForm();
            creatingForumForm.Owner = App.Current.MainWindow;
            creatingForumForm.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            creatingForumForm.ShowDialog();
        }
    }
}
