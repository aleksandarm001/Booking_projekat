using InitialProject.Domen.Model;
using System.Collections.Generic;

namespace InitialProject.Services.IServices
{
    public interface ITourService
    {
        List<Tour> GetAll();
        List<Tour> GetAllFiltered(string city, string country, string durationFrom, string durationTo, string language, string guestNumber);
        List<Tour> GetAllFinished(int userId);
        List<Tour> GetAllNotStartedTours();
        List<Tour> GetSimilarAsTourHasFullCapacity(string country, string city);
        Tour GetTourById(int id);

        Tour Save(Tour tour);

        List<Tour> GetAllFinishedTours();
        void ReduceMaxGuestNumber(int tourId, int guestNumber);

        public List<Tour> GetAllNotFinishedTour();
    }
}