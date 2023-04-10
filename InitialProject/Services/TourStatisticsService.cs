using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Domen.RepositoryInterfaces;
using InitialProject.Factory;
using InitialProject.Model;
using InitialProject.Services.IServices;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Services
{
    public class TourStatisticsService : ITourStatisticsService
    {
        private readonly ITourRepository _tourRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserService userService;
        private readonly ITourReservationService tourReservationService;
        public TourStatisticsService()
        {
            _tourRepository = Injector.tourRepository();
            _reservationRepository = Injector.reservationRepository();
            userService = Injector.userService();
            tourReservationService = Injector.tourReservationService();
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
            List<Reservation> reservations = _reservationRepository.GetAll().Where(c=> c.TourId > 0).ToList();

            foreach (Reservation reservation in reservations)
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
                            numberOfPeopleWithAgeUnder18++;
                        }
                        else if (Age >= 18 && Age <= 50)
                        {
                            numberOfPeopleWithAgeBetween18And50++;
                        }
                        else
                        {
                            numberOfPeopleWithAgeOver50++;
                        }
                }
            }


            double percentageWithVoucher = 0;
            double percentageWithoutVouchers = 0;
            if (numberOfPeople > 0)
            {
                double percentage = numberOfPeople / 100;
                percentageWithVoucher = percentage * numberOfPeopleWithVoucher;
                percentageWithoutVouchers = 100 - percentageWithVoucher;
            }

                

            Statistic statistic = new Statistic(numberOfPeopleWithAgeUnder18, numberOfPeopleWithAgeBetween18And50, numberOfPeopleWithAgeOver50, percentageWithVoucher, percentageWithoutVouchers);
            return statistic;
        }





    }
}

