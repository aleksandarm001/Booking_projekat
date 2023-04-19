namespace InitialProject.Presentation.WPF.ViewModel.Guest2
{
    using InitialProject.Aplication.Factory;
    using InitialProject.Domen.Model;
    using InitialProject.Presentation.WPF.Constants;
    using InitialProject.Services.IServices;
    using System.Windows;

    public class TourReviewViewModel
    {
        private readonly ITourRateService _tourRateService;
        public TourReviewViewModel()
        {
            _tourRateService = Injector.CreateInstance<ITourRateService>();
        }


        public void RateTour(TourRate tourRate)
        {
            _tourRateService.MakeTourRate(tourRate);
            MessageBox.Show(TourViewConstants.CommentNoted, TourViewConstants.Caption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes);
        }

    }
}
