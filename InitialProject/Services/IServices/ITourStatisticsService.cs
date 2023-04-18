using InitialProject.Domen.Model;
using System.Collections.Generic;

namespace InitialProject.Services.IServices
{
    public interface ITourStatisticsService
    {
        List<string> GetAllYears();

        Statistic GetSpecificStatistic(string tour);
        Tour GetMostVisitedTour(string year);
    }
}