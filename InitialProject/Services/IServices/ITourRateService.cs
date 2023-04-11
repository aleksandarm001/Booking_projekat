using InitialProject.Model;
using System.Collections.Generic;

namespace InitialProject.Services.IServices
{
    public interface ITourRateService
    {
        List<TourRate> GetAllRates();
        void MakeTourRate(TourRate tourRate);

        void Update(TourRate tourRate);
    }
}