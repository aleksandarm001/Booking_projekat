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

        public List<ChangeReservationRequest> getRequests()
        {
            return _requestRepository.GetAll();
        }

    }
}
