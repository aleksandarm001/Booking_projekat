namespace InitialProject.Services
{
    using InitialProject.CustomClasses;
    using InitialProject.Repository;
    using System.Collections.Generic;
    using System.Linq;

    public class TourAttendanceService
    {
        private readonly TourAttendanceRepository _tourAttendanceRepository;
        private List<TourAttendance> _tourAttendances;
        public TourAttendanceService()
        {
            _tourAttendanceRepository = new TourAttendanceRepository();
            _tourAttendances = _tourAttendanceRepository.GetAll();
        }

        public List<TourAttendance> GetAll()
        {
            return _tourAttendances;
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
            if (_tourAttendances.FindAll(t => t.UserId == userId && t.TourId == tourId && t.CanGiveReview == true).Count != 0)
            {
                return true;
            }
            return false;
        }

        public void AddedComment(int userId, int tourId)
        {
            foreach (TourAttendance tourAttendance in _tourAttendances.FindAll(t => t.UserId == userId && t.TourId == tourId && t.CanGiveReview == true))
            {
                tourAttendance.CanGiveReview = false;
                _tourAttendanceRepository.Update(tourAttendance);
            }

        }

        public List<TourAttendance> GetAllPresented(int userId)
        {
            return _tourAttendances.Where(tour => tour.UserId == userId && tour.UserAttended == TourAttendance.AttendanceStatus.Present).ToList();
        }

        public List<TourAttendance> GetAllToCheckByUser(int userId)
        {
            return _tourAttendances.Where(tour => tour.UserId == userId && tour.UserAttended == TourAttendance.AttendanceStatus.OnHold).ToList();
        }

    }
}
