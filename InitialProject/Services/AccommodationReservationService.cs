using InitialProject.Aplication.Contracts.Repository;
using InitialProject.Aplication.Factory;
using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Repository;
using InitialProject.Services.IServices;
using Microsoft.VisualStudio.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Services
{
    public class AccommodationReservationService : IAccommodationReservationService
    {
        private readonly IReservationService _reservationService;
        private readonly IAccommodationService _accommodationService;
        private readonly IAccommodationReservationRepository _accommodationReservationRepository;
        
        public AccommodationReservationService()
        {
            _reservationService = Injector.CreateInstance<IReservationService>();
            _accommodationService = Injector.CreateInstance<IAccommodationService>();
            _accommodationReservationRepository = Injector.CreateInstance<IAccommodationReservationRepository>();
        }
        public Dictionary<int, string> GetReservationsByUserId(int userId)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            List<Reservation> usersReservations = _reservationService.GetReservationsByUserId(userId);
            FilterReservations(usersReservations);
            if (usersReservations.Count > 0)
            {
                foreach (Reservation reservation in usersReservations)
                {
                    int accommodationId = _accommodationService.GetAccommodationIdByReservationId(reservation.ReservationId);
                    Reservation founded = _reservationService.GetReservationById(reservation.ReservationId);
                    string value = "";
                    string accommodationName = _accommodationService.GetNameById(accommodationId);
                    value = value + " " + accommodationName + "; " + founded.ReservationDateRange.SStartDate + "-" + founded.ReservationDateRange.SEndDate;
                    result.Add(reservation.ReservationId, value);
                }
            }
            return result;

        }
        public bool IsCancellingPossible(DateTime currentDate, int ReservationId)
        {
            Accommodation founded = _accommodationService.GetAccommodationByReservationId(ReservationId);
            Reservation reservation = _reservationService.GetReservationById(ReservationId);
            int daysBeforeCancel = founded.DaysBeforeCancelling;
            DateTime allowedCancellingDate = reservation.ReservationDateRange.EndDate.AddDays(daysBeforeCancel);
            return allowedCancellingDate > currentDate;
        }
        private void FilterReservations(List<Reservation> reservations)
        {
            reservations.RemoveAll(r => r.ReservationDateRange.StartDate <= DateTime.Now);
        }

        public List<int> GetReservationsIdsByAccommodationId(int accommodationId)
        {
            List<int> reservationIds = new List<int>();
            List<AccommodationReservation> reservations = _accommodationReservationRepository.GetAll(); //Pitaj Igora
            foreach(AccommodationReservation ar in reservations)
            {
                if (ar.AccommodationId == accommodationId)
                {
                    reservationIds.Add(ar.ReservationId);
                }
            }
            return reservationIds;
        }
    }
}
