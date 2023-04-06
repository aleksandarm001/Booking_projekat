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
    public class AccommodationService
    {
        private readonly AccommodationRepository _repository;
        private readonly AccommodationReservationRepository _accommodationReservationRepository;
        private List<Accommodation> _accommodations;
        private List<AccommodationReservation> _accommodationReservations;
        public AccommodationService()
        {
            _repository = new AccommodationRepository();
            _accommodationReservationRepository = new AccommodationReservationRepository();
            _accommodations = _repository.GetAll();
            _accommodationReservations = _accommodationReservationRepository.GetAll();
        }
        public string getNameById(int accommodationId)
        {
            return _accommodations.Find(a => a.AccommodationID == accommodationId).Name;
        }
        public int getOwnerIdByAccommodationId(int accommodationId)
        {
            return _accommodations.Find(a => a.AccommodationID == accommodationId).UserId;
        }
        public int getOwnerIdByReservationId(int reservationId)
        {
            int accommodationId = GetAccommodationIdByReservationId(reservationId);
            return getOwnerIdByAccommodationId(accommodationId);
        }
        public int GetAccommodationIdByReservationId(int reservationId)
        {
            if(_accommodationReservations.Find(a => a.ReservationId == reservationId) == null) return 0;
            return _accommodationReservations.Find(a => a.ReservationId == reservationId).AccommodationId;
        }
    }
}
