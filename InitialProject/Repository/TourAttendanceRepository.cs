﻿using InitialProject.CustomClasses;
using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class TourAttendanceRepository
    {
        private const string FilePath = "../../../Resources/Data/tourattendances.txt";

        private readonly Serializer<TourAttendance> _serializer;

        private List<TourAttendance> _tourAttendances;

        public TourAttendanceRepository()
        {
            _serializer = new Serializer<TourAttendance>();
            _tourAttendances = _serializer.FromCSV(FilePath);
        }


        public List<TourAttendance> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public TourAttendance Save(TourAttendance tourAttendance)
        {
            tourAttendance.TourAttendanceId = NextId();
            _tourAttendances.Add(tourAttendance);
            _serializer.ToCSV(FilePath, _tourAttendances);
            return tourAttendance;
        }
        public int NextId()
        {
            _tourAttendances = _serializer.FromCSV(FilePath);
            if (_tourAttendances.Count < 1)
            {
                return 1;
            }
            return _tourAttendances.Max(t => t.TourAttendanceId) + 1;
        }
    }
}
