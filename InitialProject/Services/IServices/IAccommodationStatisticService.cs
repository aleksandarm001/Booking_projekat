using InitialProject.Domen.CustomClasses;
using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services.IServices
{
    public  interface IAccommodationStatisticService
    {   
        List<StatisticsByYearDTO> YearStatisticsForAccommodation(int accommodationID);
        List<StatisticsByMonthDTO> MonthStatisticsForYear(int accommodationId, int year);
        StatisticsByYearDTO MostOccupiedYear(List<StatisticsByYearDTO> statistics);
        StatisticsByMonthDTO MostOccupiedMonth(List<StatisticsByMonthDTO> statistics);

    }
}
