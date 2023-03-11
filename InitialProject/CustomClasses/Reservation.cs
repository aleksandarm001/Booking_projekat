﻿using InitialProject.Model;
using System;
using System.Collections.Generic;
using InitialProject.Serializer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.CustomClasses
{
    public class Reservation : ISerializable
    {
        public int Id { get; set; }
        public int AccomodationId { get; set; }
        public int UserId { get; set; }
        public int TourId { get; set; }
        public DateTime StartDate { get; set; }
        public string SStartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string SEndDate { get; set; }
        public float AvgRating { get; set; }
        public DateRange ReservationDateRange { get; set; }
        public int NumberOfGuests { get; set; }


        public Reservation(int accomodationId, int userId, int tourId,DateTime startDate, DateTime endDate)
        {
            AccomodationId = accomodationId;
            UserId = userId;
            TourId = tourId;
            StartDate = startDate;
            EndDate = endDate;
            SStartDate = string.Format("{0:dd.MM.yyyy.}", StartDate);
            SEndDate = string.Format("{0:dd.MM.yyyy.}", EndDate);
        }
        public Reservation(int accomodationId, int userId, DateTime startDate, DateTime endDate)
        {
            AccomodationId = accomodationId;
            UserId = userId;
            TourId = -1;
            StartDate = startDate;
            EndDate = endDate;
            SStartDate = string.Format("{0:dd.MM.yyyy.}", StartDate);
            SEndDate = string.Format("{0:dd.MM.yyyy.}", EndDate);
        }

        public Reservation(int userId, int tourId,DateTime startDate, int numberOfVisitors)
        {
            UserId = userId;
            TourId = tourId;
            StartDate = startDate;
            NumberOfGuests = numberOfVisitors;
        }
        public Reservation(int accomodationId, DateRange dateRange, int guestNumber)
        {
            AccomodationId = accomodationId;
            TourId = -1;
            UserId = -1;
            StartDate = new DateTime();
            EndDate = new DateTime();
            SStartDate = string.Format("{0:dd.MM.yyyy.}", StartDate);
            SEndDate = string.Format("{0:dd.MM.yyyy.}", EndDate);
            ReservationDateRange = dateRange;
            NumberOfGuests = guestNumber;
        }
        public Reservation()
        {
            AccomodationId = -1;
            TourId = -1;
            UserId = -1;
            StartDate = new DateTime();
            EndDate = new DateTime();
            SStartDate = string.Format("{0:dd.MM.yyyy.}", StartDate);
            SEndDate = string.Format("{0:dd.MM.yyyy.}", EndDate);
            ReservationDateRange = new DateRange();
        }
       
        public override string ToString()
        {
            return AccomodationId.ToString() + ";" + StartDate.ToString() + ";" + EndDate.ToString();
        }
       
        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                UserId.ToString(),
                TourId.ToString(),
                AccomodationId.ToString(),
                StartDate.ToString(),
                EndDate.ToString(),
                AvgRating.ToString(),
                NumberOfGuests.ToString(),
                ReservationDateRange.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);    
            UserId = Convert.ToInt32(values[1]);
            TourId = Convert.ToInt32(values[2]);
            AccomodationId = Convert.ToInt32(values[3]);
            StartDate = DateTime.Parse(values[4]);
            SStartDate = string.Format("{0:dd.MM.yyyy.}", StartDate);
            EndDate = DateTime.Parse(values[5]); 
            SEndDate = string.Format("{0:dd.MM.yyyy.}", EndDate);
            AvgRating = float.Parse(values[6]);
            NumberOfGuests = Convert.ToInt32(values[7]);
            ReservationDateRange = ReservationDateRange.fromStringToDateRange(values[8]);
        }


    }
}
