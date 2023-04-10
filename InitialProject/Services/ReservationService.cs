using InitialProject.CustomClasses;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Services
{
    public class ReservationService
    {
        private readonly ReservationRepository _repository;
        private readonly TourService _tourService;
        private readonly VoucherService _voucherService;
        public ReservationService()
        {
            _repository = new ReservationRepository();
            _tourService = new TourService();
            _voucherService = new VoucherService();
        }
        public List<Reservation> GetReservationsByUserId(int userId)
        {
            return _repository.GetAll().Where(r => r.UserId == userId).ToList();
        }
        public DateTime GetCheckInDate(int userId, int reservationId)
        {
            List<Reservation> reservations = GetReservationsByUserId(userId);
            return reservations.Find(r => r.ReservationId == reservationId).ReservationDateRange.StartDate;
        }
        public DateTime GetCheckOutDate(int userId, int reservationId)
        {
            List<Reservation> reservations = GetReservationsByUserId(userId);
            return reservations.Find(r => r.ReservationId == reservationId).ReservationDateRange.EndDate;
        }
        public Reservation GetReservationById(int reservationId)
        {
            return _repository.GetAll().Find(r => r.ReservationId == reservationId);
        }
        public void Delete(int reservationId)
        {
            Reservation reservation = GetReservationById(reservationId);
            _repository.Delete(reservation);
        }

        public void MakeReservationWithVoucher(int userId, int tourId, DateTime startingDateTime, int numberOfGuests, Voucher voucher)
        {
            Reservation reservation = new Reservation(userId, tourId, startingDateTime, numberOfGuests, voucher.Id);
            _repository.Save(reservation);
            _tourService.ReduceMaxGuestNumber(tourId,numberOfGuests);
            _voucherService.Delete(voucher);
        }

        public void MakeReservationWithoutVoucher(int userId, int tourId, DateTime startingDateTime, int numberOfGuests)
        {
            Reservation reservation = new Reservation(userId, tourId, startingDateTime, numberOfGuests, 0);
            _repository.Save(reservation);
            _tourService.ReduceMaxGuestNumber(tourId, numberOfGuests);
        }

        public List<Reservation> GetTourReservationByUserId(int userId)
        {
            return _repository.GetAll().Where(r => r.UserId == userId && r.TourId != -1).ToList();
        }
    }
}
