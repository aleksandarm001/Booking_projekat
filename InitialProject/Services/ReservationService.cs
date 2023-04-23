using InitialProject.Aplication.Contracts.Repository;
using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Repository;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repository;
        public ReservationService()
        {
            _repository = Injector.CreateInstance<IReservationRepository>();
        }
        public List<Reservation> GetReservationsByUserId(int userId)
        {
            return _repository.GetAll().Where(r => r.UserId == userId && r.Status != ReservationStatus.Finished).ToList();
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
        public void DeleteLogical(int reservationId)
        {
            Reservation reservation = GetReservationById(reservationId);
            reservation.Status = ReservationStatus.Finished;
            _repository.Update(reservation);
        }
        public List<Reservation> GetAllWithoutFinished()
        {
            return _repository.GetAll().Where(r => r.Status != ReservationStatus.Finished).ToList();
        }
        public void HandleCheckingIn()
        {
            List<Reservation> reservations = GetAllWithoutFinished();
            foreach(Reservation reservation in reservations)
            {
                if (reservation.ReservationDateRange.StartDate <= DateTime.Now && reservation.ReservationDateRange.EndDate > DateTime.Now)
                {
                    reservation.Status = ReservationStatus.CheckedIn;
                    _repository.Update(reservation);
                }
            }

        }
    }
}
