﻿using InitialProject.CustomClasses;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Services
{
    public class ReservationService
    {
        private readonly ReservationRepository _repository;
        public ReservationService()
        {
            _repository = new ReservationRepository();
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

        public void MakeReservation(int userId, int tourId, DateTime startingDateTime, int numberOfGuests)
        {
            Reservation reservation = new Reservation(userId, tourId, startingDateTime, numberOfGuests);
            _repository.Save(reservation);
        }

        public List<Reservation> GetTourReservationByUserId(int userId)
        {
            return _repository.GetAll().Where(r => r.UserId == userId && r.TourId != -1).ToList();
        }
    }
}
