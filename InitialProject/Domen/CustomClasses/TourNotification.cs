﻿namespace InitialProject.Domen.CustomClasses
{
    using InitialProject.CustomClasses;
    using InitialProject.Domen.Model;
    using Microsoft.VisualStudio.Services.ClientNotification;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TourNotification : ISerializable
    {
        public int TourId { get; set; }
        public int UserId { get; set; }

        public TourNotification()
        {
        }

        public TourNotification(int tourId, int userId)
        {
            TourId = tourId;
            UserId = userId;
        }

        public string[] ToCSV()
        {
            string[] cssValues =
            {
                TourId.ToString(),
                UserId.ToString()
            };
            return cssValues;
        }
        public void FromCSV(string[] values)
        {
            TourId= Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            
        }
    }
}