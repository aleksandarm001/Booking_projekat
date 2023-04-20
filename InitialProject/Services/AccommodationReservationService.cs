using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;

namespace InitialProject.Services
{
    public class AccommodationReservationService : IAccommodationReservationService
    {
        private readonly IReservationService reservationService;
        private readonly IAccommodationService accommodationService;
        public AccommodationReservationService()
        {
            reservationService = new ReservationService();
            accommodationService = new AccommodationService();
        }
        public Dictionary<int, string> GetReservationsByUserId(int userId)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            List<Reservation> usersReservations = reservationService.GetReservationsByUserId(userId);
            FilterReservations(usersReservations);
            if (usersReservations.Count > 0)
            {
                foreach (Reservation reservation in usersReservations)
                {
                    int accommodationId = accommodationService.GetAccommodationIdByReservationId(reservation.ReservationId);
                    Reservation founded = reservationService.GetReservationById(reservation.ReservationId);
                    string value = "";
                    string accommodationName = accommodationService.GetNameById(accommodationId);
                    value = value + " " + accommodationName + "; " + founded.ReservationDateRange.SStartDate + "-" + founded.ReservationDateRange.SEndDate;
                    result.Add(reservation.ReservationId, value);
                }
            }
            return result;

        }
        public bool IsCancellingPossible(DateTime currentDate, int ReservationId)
        {
            Accommodation founded = accommodationService.GetAccommodationByReservationId(ReservationId);
            Reservation reservation = reservationService.GetReservationById(ReservationId);
            int daysBeforeCancel = founded.DaysBeforeCancelling;
            DateTime allowedCancellingDate = reservation.ReservationDateRange.EndDate.AddDays(daysBeforeCancel);
            return allowedCancellingDate > currentDate;
        }
        private void FilterReservations(List<Reservation> reservations)
        {
            reservations.RemoveAll(r => r.ReservationDateRange.StartDate <= DateTime.Now);
        }

    }
}
