using InitialProject.CustomClasses;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services
{
    public class ChangeReservationRequestService
    {
        private readonly ChangeReservationRequestRepository _requestRepository;
        private readonly ReservationService _reservationService;
        private List<ChangeReservationRequest> _changes;
        public ChangeReservationRequestService()
        {
            _requestRepository = new ChangeReservationRequestRepository();
            _reservationService = new ReservationService();
        }

        public List<ChangeReservationRequest> GetRequests(int userId)
        {
            return _requestRepository.GetAll().Where(r => r.UserId == userId).ToList();
        }

        public void SaveRequest(ChangeReservationRequest request)
        {
            if(_requestRepository.GetAll().Find(r => r.ReservationId == request.ReservationId && r.UserId == request.UserId) != null)
            {
                _requestRepository.Update(request);
            }
            else
            {
                _requestRepository.Save(request);
            }
        }
        public ChangeReservationRequest GetRequestById(int requestId)
        {
            return _requestRepository.GetAll().Find(r => r.RequestId == requestId);
        }

        public void Delete(int requestId)
        {
            ChangeReservationRequest request = GetRequestById(requestId);
            _requestRepository.Delete(request);
        }

        public Boolean isDateRangeAvailable(int accommodationId , DateTime newStartDate , DateTime newEndDate)
        {
            List<Reservation> Reservations = new List<Reservation>(_reservationService.GetAll());

            foreach(Reservation reservation in Reservations)
            {
                if(reservation.AccommodationId == accommodationId)
                {
                    DoRangesIntersect(newStartDate, newEndDate, reservation.ReservationDateRange.StartDate, reservation.ReservationDateRange.EndDate);
                }
            }
            return true;
        }

        public static bool DoRangesIntersect(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            // Check if either of the ranges start1 to end1 or start2 to end2 intersect
            if((start1 >= start2 && start1 <= end2) || (end1 >= start2 && end1 <= end2)
                || (start2 >= start1 && start2 <= end1) || (end2 >= start1 && end2 <= end1))
            {
                return false; 
            }
            else
            {
                return true; 
            }
        }
    }
}
