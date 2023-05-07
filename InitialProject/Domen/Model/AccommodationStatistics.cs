using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domen.Model
{
    public class AccommodationStatistics
    {
        private int _accommodationId;

        private List<AccommodationYearStatistics> _yearlyStatistic;

        public AccommodationStatistics() { }

        public AccommodationStatistics(int accommodationId, List<AccommodationYearStatistics> AccommodationStatistics)
        {
            _accommodationId = accommodationId;
            _yearlyStatistic = AccommodationStatistics;
        }

        public int AccommodationId { get => _accommodationId; set => _accommodationId = value; }
        public List<AccommodationYearStatistics> YearlyStatistics { get => _yearlyStatistic; set => _yearlyStatistic = value; }

    }
}
