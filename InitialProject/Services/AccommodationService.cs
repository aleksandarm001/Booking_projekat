using InitialProject.Aplication.Contracts.Repository;
using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Repository;
using InitialProject.Services.IServices;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Services
{
    public class AccommodationService : IAccommodationService
    {
        private readonly IAccommodationRepository _repository;
        private readonly IAccommodationReservationRepository _accommodationReservationRepository;
        private List<Accommodation> _accommodations;
        private List<AccommodationReservation> _accommodationReservations;
        public AccommodationService()
        {
            _repository = new AccommodationRepository();
            _accommodationReservationRepository = new AccommodationReservationRepository();
            _accommodations = _repository.GetAll();
            _accommodationReservations = _accommodationReservationRepository.GetAll();
        }
        public string GetNameById(int accommodationId)
        {
            return _accommodations.Find(a => a.AccommodationID == accommodationId).Name;
        }
        public string GetNameByReservationId(int reservationId)
        {
            int accommodationId = GetAccommodationIdByReservationId(reservationId);
            return _accommodations.Find(a => a.AccommodationID == accommodationId).Name;
        }
        public int GetOwnerIdByAccommodationId(int accommodationId)
        {
            return _accommodations.Find(a => a.AccommodationID == accommodationId).UserId;
        }
        public int GetOwnerIdByReservationId(int reservationId)
        {
            int accommodationId = GetAccommodationIdByReservationId(reservationId);
            return GetOwnerIdByAccommodationId(accommodationId);
        }
        public int GetAccommodationIdByReservationId(int reservationId)
        {
            if(_accommodationReservations.Find(a => a.ReservationId == reservationId) == null) return 0;
            return _accommodationReservations.Find(a => a.ReservationId == reservationId).AccommodationId;
        }
        public Accommodation GetAccommodationByReservationId(int reservationId)
        {
            int accommodationId = GetAccommodationIdByReservationId(reservationId);
            return _accommodations.Find(accommodation => accommodation.AccommodationID == accommodationId);
        }
        public Accommodation GetAccommodationById(int accommodationId)
        {
            return _accommodations.Find(accommodation => accommodation.AccommodationID == accommodationId);
        }
        public int GetReservationIdByAccommodationId(int accommodationId)
        {
            return _accommodationReservations.Find(accommodation => accommodation.AccommodationId == accommodationId).ReservationId;
        }
        public void DeleteReservation(int reservationId)
        {
            _accommodationReservationRepository.DeleteReservation(reservationId);
        }

        public int GetAccommodationIdByAccommodationName(string accommodationName)
        {
            return _accommodations.Find(a => a.Name == accommodationName).AccommodationID; 
        }

        public List<Accommodation>GetAccommodationsByOwnerId(int ownerId)
        {
            return _repository.GetAll().Where(a => a.UserId== ownerId).ToList();
        }
    }
}
