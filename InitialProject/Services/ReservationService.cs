﻿using InitialProject.CustomClasses;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ReservationRepository _repository;
        
        public ReservationService()
        {
            _repository = new ReservationRepository();
        }

        public List<Reservation> GetAll()
        {
            return _repository.GetAll();
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

        public void UpdateReservation(int reservationId,DateTime newStart,DateTime newEnd)
        {
           _repository.Update(GetReservationById(reservationId),newStart,newEnd);
        }

    }
}
