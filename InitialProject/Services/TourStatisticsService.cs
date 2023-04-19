using InitialProject.Aplication.Contracts.Repository;
using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Services
{
    public class TourStatisticsService : ITourStatisticsService
    {
        private readonly ITourRepository _tourRepository;
        private readonly IUserService userService;
        private readonly ITourReservationService tourReservationService;
        private readonly ITourReservationRepository tourReservationRepository;
        public TourStatisticsService()
        {
            _tourRepository = Injector.CreateInstance<ITourRepository>();
            tourReservationRepository = Injector.CreateInstance<ITourReservationRepository>();
            userService = Injector.CreateInstance<IUserService>();
            tourReservationService = Injector.CreateInstance<ITourReservationService>();
        }

        public List<string> GetAllYears()
        {
            List<Tour> tours = _tourRepository.GetAll();
            List<string> years = new();
            foreach (Tour tour in tours)
            {
                if (tour.TourStarted == true)
                {
                    string year = tour.StartingDateTime.Year.ToString();
                    if (!years.Contains(year))
                    {
                        years.Add(year);
                    }
                }
            }
            //check if same year already exist in years 
            return years;
        }

        public Tour GetMostVisitedTour(string year)
        {
            Dictionary<int, int> tourVisits = new Dictionary<int, int>();
            List<TourReservation> reservations = tourReservationRepository.GetAll().Where(c=> c.TourId > 0).ToList();

            foreach (TourReservation reservation in reservations)
            {
                Tour tour = _tourRepository.GetById(reservation.TourId);
                if (tour.StartingDateTime.Year.ToString() == year)
                {
                    int reservationId = reservation.TourId;
                    int numberOfGuests = reservation.NumberOfGuests;

                    if (tourVisits.ContainsKey(reservationId))
                    {
                        tourVisits[reservationId] += numberOfGuests;
                    }
                    else
                    {
                        tourVisits.Add(reservationId, numberOfGuests);
                    }
                }
            }

            int tourId = tourVisits.FirstOrDefault(x => x.Value == tourVisits.Values.Max()).Key;
            return _tourRepository.GetById(tourId);
            
        }

        public Statistic GetSpecificStatistic(string tour) 
        {
            List<TourReservation> reservations = tourReservationService.GetAllReservations();
            int tourId = int.Parse(tour.Split(' ')[0]);
            int numberOfPeople = 0;
            int numberOfPeopleWithVoucher = 0;
            int numberOfPeopleWithAgeUnder18 = 0;
            int numberOfPeopleWithAgeBetween18And50 = 0;
            int numberOfPeopleWithAgeOver50 = 0;

            foreach (TourReservation reservation in reservations)
            {
                if (reservation.TourId == tourId)
                {
                    numberOfPeople += reservation.NumberOfGuests;
                    if (reservation.VoucherId != 0)
                    {
                        numberOfPeopleWithVoucher += reservation.NumberOfGuests;
                    }

                    int Age = userService.GetById(reservation.UserId).Age;

                        if (Age < 18)
                        {
                            numberOfPeopleWithAgeUnder18 += reservation.NumberOfGuests;
                        }
                        else if (Age >= 18 && Age <= 50)
                        {
                            numberOfPeopleWithAgeBetween18And50 += reservation.NumberOfGuests;
                        }
                        else
                        {
                            numberOfPeopleWithAgeOver50 += reservation.NumberOfGuests;
                        }
                }
            }


           
            return calculateStatistic(numberOfPeople, numberOfPeopleWithVoucher, numberOfPeopleWithAgeUnder18, numberOfPeopleWithAgeBetween18And50, numberOfPeopleWithAgeOver50);
        }

        private Statistic calculateStatistic(int numberOfPeople,int numberOfPeopleWithVoucher,int numberOfPeopleWithAgeUnder18,int numberOfPeopleWithAgeBetween18And50, int numberOfPeopleWithAgeOver50)
        {
            double percentageWithVoucher = 0;
            double percentageWithoutVouchers = 0;
            if (numberOfPeople > 0)
            {
                double percentage = (double)100 / numberOfPeople;
                percentageWithVoucher = percentage * numberOfPeopleWithVoucher;
                percentageWithoutVouchers = 100 - percentageWithVoucher;
            }

            Statistic statistic = new Statistic(numberOfPeopleWithAgeUnder18, numberOfPeopleWithAgeBetween18And50, numberOfPeopleWithAgeOver50, percentageWithVoucher, percentageWithoutVouchers);
            return statistic;


        }





    }
}

