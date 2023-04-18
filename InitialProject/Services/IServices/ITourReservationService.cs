using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;

namespace InitialProject.Services.IServices
{
    public interface ITourReservationService
    {
        List<TourReservation> GetAllReservations();
        List<Tour> GetAllReservedAndNotFinishedTour(int userId);
        List<TourReservation> GetReservationsByUserId(int userId);
        void MakeReservationWithoutVoucher(int userId, int tourId, DateTime startingDateTime, int numberOfGuests);
        void MakeReservationWithVoucher(int userId, int tourId, DateTime startingDateTime, int numberOfGuests, Voucher voucher);
    }
}