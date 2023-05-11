using InitialProject.Domen.Model;
using System.Collections.Generic;

namespace InitialProject.Services.IServices
{
    public interface ITourPointService
    {
        TourPoint GetActiveTourPointOnTour(int tourId);
        List<TourPoint> GetAllTourPoints();
        bool TourStartedAndFinished(int tourId);
        int FindNextId();

        TourPoint AddTempTourPoint(TourPoint tempTourPoint);
    }
}