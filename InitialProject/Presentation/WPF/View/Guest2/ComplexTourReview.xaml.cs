namespace InitialProject.Presentation.WPF.View.Guest2
{
    using InitialProject.Aplication.Factory;
    using InitialProject.Domen.Model;
    using InitialProject.Presentation.WPF.ViewModel.Guide;
    using InitialProject.Services.IServices;
    using System.Collections.ObjectModel;
    using System.Windows;

    /// <summary>
    /// Interaction logic for ComplexTourReview.xaml
    /// </summary>
    public partial class ComplexTourReview : Window
    {
        public ComplexTourReview(int tourId)
        {
            ComplexTourRequestsViewModel viewModel = new ComplexTourRequestsViewModel(tourId);
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
