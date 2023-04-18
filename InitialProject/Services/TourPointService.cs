namespace InitialProject.Services
{
    using InitialProject.Domen.Model;
    using InitialProject.Repository;
    using InitialProject.Services.IServices;
    using System.Collections.Generic;
    using System.Linq;

    public class TourPointService : ITourPointService
    {
        private TourPointRepository _tourPointRepository;

        public TourPointService()
        {
            _tourPointRepository = new TourPointRepository();
        }


        public List<TourPoint> GetAllTourPoints()
        {
            return _tourPointRepository.GetAll();
        }

        public bool TourStartedAndFinished(int tourId)
        {
            return _tourPointRepository.GetAll().Where(t => t.TourId == tourId).Where(t => t.CurrentStatus == TourPoint.Status.Active || t.CurrentStatus == TourPoint.Status.NotActive).Count() == 0;
        }

        public TourPoint GetActiveTourPointOnTour(int tourId)
        {
            return _tourPointRepository.GetAll().Where(t => t.TourId == tourId).Where(t => t.CurrentStatus == TourPoint.Status.Active).FirstOrDefault();
        }
    }
}
