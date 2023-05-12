namespace InitialProject.Presentation.WPF.View.Guest2
{
    using InitialProject.Aplication.Factory;
    using InitialProject.Domen.Model;
    using InitialProject.Services.IServices;
    using System.Collections.ObjectModel;
    using System.Windows;

    /// <summary>
    /// Interaction logic for ComplexTourReview.xaml
    /// </summary>
    public partial class ComplexTourReview : Window
    {
        public ObservableCollection<ComplexTourRequest> ComplexTourRequests { get; set; }
        private readonly IComplexTourRequestService _complexTourRequestService;
        public ComplexTourReview(int tourId)
        {
            InitializeComponent();

            _complexTourRequestService = Injector.CreateInstance<IComplexTourRequestService>();
            ComplexTourRequests = new ObservableCollection<ComplexTourRequest>(_complexTourRequestService.GetTourRequest(tourId));
            DataContext = this;
        }
    }
}
