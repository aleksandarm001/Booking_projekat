using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Repository;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Services
{
    public class ChangeReservationRequestService : IChangeReservationRequestService
    {
        private readonly ChangeReservationRequestRepository _requestRepository;
        private readonly ReservationService _reservationService;
        public ChangeReservationRequestService()
        {
            _requestRepository = new ChangeReservationRequestRepository();
            _reservationService = new ReservationService();
        }

        public List<ChangeReservationRequest> GetRequests(int userId)
        {
            UpdateRequests(userId);
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

        public void DeleteRequestByReservationId(int reservationId)
        {
            ChangeReservationRequest foundedRequest = _requestRepository.GetAll().Find(r => r.ReservationId == reservationId);
            if (foundedRequest != null)
            {
                _requestRepository.Delete(foundedRequest);
            }
        }
        public ChangeReservationRequest FindRequestByReservationId(int reservationId)
        {
            return _requestRepository.GetAll().First(x => x.ReservationId == reservationId);
        }
        private void UpdateRequests(int userId)
        {
            foreach(Reservation reservation in _reservationService.GetReservationsByUserId(userId))
            {
                if(reservation.ReservationDateRange.StartDate <= DateTime.Now)
                {
                    DeleteRequestByReservationId(reservation.ReservationId);
                }
            }
        }
    }
}
