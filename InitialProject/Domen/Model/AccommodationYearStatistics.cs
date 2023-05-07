using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domen.Model
{
    public class AccommodationYearStatistics
    {
        private int _accommodationStatisticsId;
        private List<AccommodationMonthlyStatistics> _monthlyStatistics;

        public AccommodationYearStatistics()
        {
        }

        public AccommodationYearStatistics(int id,List<AccommodationMonthlyStatistics> monthlyStatistics)
        {
            _accommodationStatisticsId = id;
            _monthlyStatistics = monthlyStatistics;
        }

        public List<AccommodationMonthlyStatistics> MonthlyStatistics { get => _monthlyStatistics; set => _monthlyStatistics = value; }
        public int AccommodationStatisticsId { get => _accommodationStatisticsId; set => _accommodationStatisticsId = value; }
    }
}
