﻿using InitialProject.CustomClasses;
using System;
using System.Collections.Generic;

namespace InitialProject.Services.IServices
{
    public interface IReservationService
    {
        void Delete(int reservationId);
        List<Reservation> GetAllTourReservations();
        DateTime GetCheckInDate(int userId, int reservationId);
        DateTime GetCheckOutDate(int userId, int reservationId);
        Reservation GetReservationById(int reservationId);
        List<Reservation> GetReservationsByUserId(int userId);
        List<Reservation> GetTourReservationByUserId(int userId);
        void MakeReservation(int userId, int tourId, DateTime startingDateTime, int numberOfGuests, int voucherId);
    }
}