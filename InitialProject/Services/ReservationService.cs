using InitialProject.CustomClasses;
using InitialProject.Repository;
using InitialProject.View.Guest1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services
{
    public class ReservationService
    {
        private readonly ReservationRepository _repository;
        private readonly AccommodationService _accommodationService;
        public ReservationService()
        {
            _repository = new ReservationRepository();
            _accommodationService = new AccommodationService();
        }

        private List<Reservation> GetReservationsByUserId(int userId)
        {
            return _repository.GetAll().Where(r => r.UserId == userId).ToList();
        }
        public Dictionary<int, string> ReservationsForChange(int userId)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            List<Reservation> usersReservations = GetReservationsByUserId(userId);
            if(usersReservations.Count > 0)
            {
                foreach (Reservation reservation in usersReservations)
                {
                    int accommodationId = _accommodationService.GetAccommodationIdByReservationId(reservation.ReservationId);
                    string accommodationName = _accommodationService.getNameById(accommodationId);
                    result.Add(reservation.ReservationId, accommodationName);
                }
                return result;
            }
            return null;
           
        }
    }
}
