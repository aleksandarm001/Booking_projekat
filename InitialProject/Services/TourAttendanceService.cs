﻿namespace InitialProject.Services
{
    using InitialProject.CustomClasses;
    using InitialProject.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TourAttendanceService
    {
        private readonly TourAttendanceRepository _tourAttendanceRepository;
        private List<TourAttendance> _tourAttendances;
        public TourAttendanceService() 
        { 
            _tourAttendanceRepository = new TourAttendanceRepository(); 
            _tourAttendances = _tourAttendanceRepository.GetAll();
        }

        public void ConfirmTourAttendance(TourAttendance tourAttendance)
        {
            tourAttendance.UserAttended = TourAttendance.AttendanceStatus.Present;
            tourAttendance.CanGiveReview = true;
            _tourAttendanceRepository.Update(tourAttendance);
        }

        public void RejectTourAttendance(TourAttendance tourAttendance)
        {
            tourAttendance.UserAttended = TourAttendance.AttendanceStatus.NotPresent;
            _tourAttendanceRepository.Update(tourAttendance);
        }

        public bool CheckPossibleComment(int userId, int tourId) 
        { 
            if (_tourAttendances.FindAll(t => t.UserId == userId && t.TourId==tourId && t.CanGiveReview==true).Count!=0)
            {
                return true;
            }
            return false;
        }

        public void AddedComment(int userId, int tourId)
        {
            TourAttendance tourAttendance = _tourAttendances.Find(t => t.UserId == userId && t.TourId == tourId && t.CanGiveReview == true);
            tourAttendance.CanGiveReview = false;
            _tourAttendanceRepository.Update(tourAttendance);

        }
    }
}
