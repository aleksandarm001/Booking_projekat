using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Infrastructure.Repository;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services
{
    public class RenovationService :IRenovationService
    {
        private readonly RenovationRepository _renovationRepository;
        private readonly IReservationService _reservationService;
        private readonly IAccommodationReservationService _accommodationReservationService;
        private readonly IAccommodationService _accommodationService;

        public RenovationService()
        {
            _renovationRepository = new RenovationRepository();
            _reservationService = new ReservationService();
            _accommodationReservationService = new AccommodationReservationService();
            _accommodationService = new AccommodationService();

        }

        public List<int> GetReservationId(int accommodationId)
        {
            return _accommodationReservationService.GetReservationsIdsByAccommodationId(accommodationId);
        }

        public List<Reservation> GetReservations(List<int> reservationIds)
        {
            List<Reservation> reservations = new List<Reservation>();
            foreach (int reservationId in reservationIds)
            {
                reservations.Add(_reservationService.GetReservationById(reservationId));
            }
            return reservations;
        }

        public ObservableCollection<DateRange> GetAvailableDates(Accommodation selectedAccommodation, DateTime startDate, DateTime endDate, int Days)
        {
            List<int> reservationIds = GetReservationId(selectedAccommodation.AccommodationID);
            selectedAccommodation.Reservations = GetReservations(reservationIds);

            ObservableCollection<DateRange> availableDates = new ObservableCollection<DateRange>();
            DateTime LastDate = endDate.AddDays(-Days);

            while (startDate <= LastDate)
            {
                DateRange potentialDates = new DateRange(startDate, startDate.AddDays(Days));
                if (CheckIfDatesAvailable(selectedAccommodation.Reservations, potentialDates))
                {
                    availableDates.Add(potentialDates);
                }
                startDate = startDate.AddDays(1);
            }
            return availableDates;
        }

        private bool CheckIfDatesAvailable(List<Reservation> reservations, DateRange dateRange)
        {
            foreach (Reservation reservation in reservations)
            {
                if (DoRangesIntersect(reservation.ReservationDateRange.StartDate, reservation.ReservationDateRange.EndDate, dateRange.StartDate, dateRange.EndDate))
                {
                    return false;
                }
            }
            return true;
        }


        public static bool DoRangesIntersect(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            if ((start1 >= start2 && start1 <= end2) || (end1 >= start2 && end1 <= end2)
                || (start2 >= start1 && start2 <= end1) || (end2 >= start1 && end2 <= end1))
            {
                return true; // If ranges intersect, return true
            }
            else
            {
                return false; // If ranges do not intersect, return false
            }
        }

        public Renovation CreateNewRenovation(string AccommodationName, int AccommodationId, DateRange dateRange, string Description)
        {
            return new Renovation
            {
                AccommodationName = AccommodationName,
                AccommodationId = AccommodationId,
                DateRange = dateRange,
                Description = Description
            };
        }

        public void SaveRenovation(Renovation renovation)
        {
            _renovationRepository.Save(renovation);
        }


        public List<Renovation> GetScheduledRenovationsByOwnerId(int ownerId)
        {
            List<Accommodation> accommodations = _accommodationService.GetAccommodationsByOwnerId(ownerId);
            List<Renovation> renovations = GetScheduledRenovation();

            List<Renovation> scheduledRenovations = new List<Renovation>();

            foreach (Accommodation accommodation in accommodations)
            {
                foreach (Renovation renovation in renovations)
                {
                    if (accommodation.AccommodationID == renovation.AccommodationId)
                    {
                        scheduledRenovations.Add(renovation);
                    }
                }
            }
            return scheduledRenovations;
        }

        public List<Renovation> GetFinishedRenovationsByOwnerId(int ownerId)
        {
            List<Accommodation> accommodations = _accommodationService.GetAccommodationsByOwnerId(ownerId);
            List<Renovation> renovations = GetFinishedRenovations();

            List<Renovation> scheduledRenovations = new List<Renovation>();

            foreach (Accommodation accommodation in accommodations)
            {
                foreach (Renovation renovation in renovations)
                {
                    if (accommodation.AccommodationID == renovation.AccommodationId)
                    {
                        scheduledRenovations.Add(renovation);
                    }
                }
            }
            return scheduledRenovations;
        }
        public List<Renovation> GetScheduledRenovation()
        {
            return _renovationRepository.GetAll().Where(r => r.IsFinished == false).ToList();
        }

        public List<Renovation> GetFinishedRenovations()
        {
            return _renovationRepository.GetAll().Where(r => r.IsFinished == true).ToList();
        }
        public void IsRenovationFinished()
        {
            List<Renovation> renovations = _renovationRepository.GetAll();
            foreach(Renovation renovation in renovations)
            {
                if(renovation.DateRange.EndDate < DateTime.Now)
                {
                    renovation.IsFinished = true;
                    _renovationRepository.Update(renovation);
                }
            }
        }
        public void DeleteRenovation(Renovation renovation)
        {
            _renovationRepository.Delete(renovation);
        }

        public bool isCancelationPeriodExpired(Renovation renovation)
        {
            return renovation.DateRange.StartDate < DateTime.Now.AddDays(-5);
        }
    
    }
}
