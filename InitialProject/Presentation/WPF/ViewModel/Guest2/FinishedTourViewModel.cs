namespace InitialProject.Presentation.WPF.ViewModel.Guest2
{
    using InitialProject.Aplication.Factory;
    using InitialProject.Domen.Model;
    using InitialProject.Presentation.WPF.Constants;
    using InitialProject.Services.IServices;
    using InitialProject.View.Guest2;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    public class FinishedTourViewModel
    {
        private readonly ITourAttendanceService _tourAttendanceService;

        public FinishedTourViewModel()
        {
            _tourAttendanceService = Injector.CreateInstance<ITourAttendanceService>();
        }

        public void RateTour(Tour tour, int userId)
        {
            if (tour != null)
            {
                if (_tourAttendanceService.CheckPossibleComment(userId, tour.TourId))
                {
                    TourReview tourReview = new TourReview(tour.TourId, userId);
                    tourReview.Show();
                }
                else
                {
                    MessageBox.Show(TourViewConstants.TourReviewed, TourViewConstants.Caption, MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.Yes);
                }
            }
        }
    }
}
