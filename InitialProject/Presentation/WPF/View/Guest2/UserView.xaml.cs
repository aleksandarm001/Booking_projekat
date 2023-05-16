namespace InitialProject.Presentation.WPF.View.Guest2.Views
{
    using InitialProject.Presentation.WPF.ViewModel.Guest2;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : Page
    {
        public UserWindowViewModel _ViewModel { get; set; }
        public UserView()
        {
            InitializeComponent();
            this._ViewModel = new UserWindowViewModel(this.frame.NavigationService);
            this.DataContext = this._ViewModel;
        }
    }
}
