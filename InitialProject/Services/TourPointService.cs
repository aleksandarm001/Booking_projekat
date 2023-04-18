﻿namespace InitialProject.Services
{
    using InitialProject.Domen.Model;
    using InitialProject.Repository;
    using InitialProject.Services.IServices;
    using System.Collections.Generic;
    using System.Linq;

    public class TourPointService : ITourPointService
    {
        private TourPointRepository _tourPointRepository;
        private List<TourPoint> _tourPoints;

        public TourPointService()
        {
            _tourPointRepository = new TourPointRepository();
            _tourPoints = new List<TourPoint>(_tourPointRepository.GetAll());
        }


        public List<TourPoint> GetAllTourPoints()
        {
            return _tourPoints;
        }

        public bool TourStartedAndFinished(int tourId)
        {
            return _tourPoints.Where(t => t.TourId == tourId).Where(t => t.CurrentStatus == TourPoint.Status.Active || t.CurrentStatus == TourPoint.Status.NotActive).Count() == 0;
        }

        public TourPoint GetActiveTourPointOnTour(int tourId)
        {
            return _tourPoints.Where(t => t.TourId == tourId).Where(t => t.CurrentStatus == TourPoint.Status.Active).FirstOrDefault();
        }
    }
}
