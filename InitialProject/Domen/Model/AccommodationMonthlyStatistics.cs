using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domen.Model
{
    public class AccommodationMonthlyStatistics
    {
        //yearID

        private string _month;

        private int _numberOfReservations;

        private int _numberOfCanceledReservations;

        private int _numbeOfRescheduledReservations;

        private int _reccommendationForRenovations;

        public AccommodationMonthlyStatistics()
        {
        }

        public AccommodationMonthlyStatistics(string month, int numberOfReservations, int numberOfCanceledReservations, int numbeOfRescheduledReservations, int reccommendationForRenovations)
        {
            _month = month;
            _numberOfReservations = numberOfReservations;
            _numberOfCanceledReservations = numberOfCanceledReservations;
            _numbeOfRescheduledReservations = numbeOfRescheduledReservations;
            _reccommendationForRenovations = reccommendationForRenovations;
        }

        public string Month { get => _month; set => _month = value; }

        public int NumberOfReservations { get => _numberOfReservations; set => _numberOfReservations = value; }

        public int NumberOfCanceledReservations { get => _numberOfCanceledReservations; set => _numberOfCanceledReservations = value; }

        public int NumberOfRescheduledReservations { get => _numbeOfRescheduledReservations; set => _numbeOfRescheduledReservations = value; }

        public int ReccommendationForRenovations { get => _reccommendationForRenovations; set => _reccommendationForRenovations = value; }



    }
}
