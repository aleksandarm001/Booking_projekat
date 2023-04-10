using InitialProject.CustomClasses;
using InitialProject.Domen.RepositoryInterfaces;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services
{
    public class OwnerToRateService
    {
        private readonly IOwnerToRateRepository _ownerToRateRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IAccommodationReservationRepository _accommodationReservationRepository;
        private readonly IAccommodationRepository _accommodationRepository;
        private List<OwnerToRate> _ownersToRate;

        public OwnerToRateService()
        {
            _ownerToRateRepository = new OwnerToRateRepository();
            _reservationRepository = new ReservationRepository();
            _accommodationReservationRepository = new AccommodationReservationRepository();
            _accommodationRepository = new AccommodationRepository();
            DeleteIfFiveDaysPassed();
            UpdateOwnersToRateList();
            _ownersToRate = _ownerToRateRepository.GetAll();
        }
        public List<OwnerToRate> GetOwnersToRate()
        {
            return _ownerToRateRepository.GetAll();
        }
        public void UpdateOwnersToRateList()
        {
            foreach(Reservation r in _reservationRepository.GetAll())
            {
                if (CheckIfLeftReservation(r))
                {
                    _reservationRepository.Delete(r);
                    int accommodationId = GetAccommodationIdByReservationId(r.ReservationId);
                    OwnerToRate ownerToRate = new OwnerToRate(GetOwnerIdByAccommodationId(accommodationId), accommodationId, r.UserId, r.ReservationDateRange.EndDate);
                    _ownerToRateRepository.Save(ownerToRate);
                }
            }
        }
        private void DeleteIfFiveDaysPassed()
        {
            List<OwnerToRate> tempList = _ownerToRateRepository.GetAll();
            foreach(OwnerToRate ownerToRate in tempList)
            {
                if(DateTime.Now.AddDays(-5) > ownerToRate.LeavingDay)
                {
                    _ownerToRateRepository.Delete(ownerToRate);
                }
            }
        }
        private bool CheckIfLeftReservation(Reservation reservation)
        {
            return reservation.ReservationDateRange.EndDate < DateTime.Now;
        }
        //PITAJ IMA LI POTREBE DA SE OVO STAVI U REPOZITORIUJM
        private int GetAccommodationIdByReservationId(int reservationId)
        {
            return (_accommodationReservationRepository.GetAll().Find(a => a.ReservationId == reservationId)).AccommodationId;
        }
        public int GetOwnerIdByAccommodationId(int accommodationId)
        {
            return (_accommodationRepository.GetAll().Find(a => a.AccommodationID == accommodationId)).UserId;
        }
        public Dictionary<int, string> GetAccommodationNamesByUser(int userId)
        {
            Dictionary<int, string> accommodationNames = new Dictionary<int, string>();
            foreach (OwnerToRate o in (_ownersToRate))
            {
                if (o.UserId == userId)
                {
                    Accommodation accommodation = _accommodationRepository.GetAll().Find(a => a.AccommodationID == o.AccommodationId);
                    accommodationNames.Add(accommodation.AccommodationID, accommodation.Name);
                }
            }
            return accommodationNames;
        }
        public void DeleteOwnerToRate(int accommodationId)
        {
            OwnerToRate founded = _ownersToRate.Find(or => or.AccommodationId == accommodationId);
            _ownerToRateRepository.Delete(founded);
        }
    }
}
