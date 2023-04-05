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
        public ChangeReservationRequestService()
        {
            _requestRepository = new ChangeReservationRequestRepository();
            _reservationService = new ReservationService();
        }

        public List<ChangeReservationRequest> getRequests()
        {
            return _requestRepository.GetAll();
        }

        public void SaveRequest(ChangeReservationRequest request)
        {
            _requestRepository.Save(request);
        }
    }
}
