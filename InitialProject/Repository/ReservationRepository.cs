using InitialProject.CustomClasses;
using InitialProject.Model;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace InitialProject.Repository
{
    public class ReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/reservations.txt";

        private readonly Serializer<Reservation> _serializer;

        private TourRepository _tourRepository;

        private List<Reservation> _reservations;

        public ReservationRepository()
        {
            _serializer = new Serializer<Reservation>();
            _reservations = _serializer.FromCSV(FilePath);
            _tourRepository = new TourRepository();
        }

        public Reservation GetByReservationId(int reservationId)
        {
            _reservations = _serializer.FromCSV(FilePath);
            return _reservations.FirstOrDefault(t => t.Id == reservationId);
        }

        public List<Reservation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Reservation Save(Reservation reservation)
        {
            reservation.Id = NextId();
            _reservations = _serializer.FromCSV(FilePath);
            _reservations.Add(reservation);
            _serializer.ToCSV(FilePath, _reservations);
            Tour tour = _tourRepository.GetByTourId(reservation.TourId);
            tour.MaxGuestNumber -= reservation.NumberOfGuests;
            _tourRepository.Update(tour);
            return reservation;
        }

        public void Delete(Reservation reservation)
        {
            _reservations = _serializer.FromCSV(FilePath);
            Reservation founded = _reservations.Find(c => c.TourId == reservation.TourId && c.UserId == reservation.UserId);
            _reservations.Remove(founded);
            _serializer.ToCSV(FilePath, _reservations);
            Tour tour = _tourRepository.GetByTourId(reservation.TourId);
            tour.MaxGuestNumber += reservation.NumberOfGuests;
            _tourRepository.Update(tour);
        }

        public int NextId()
        {
            _reservations = _serializer.FromCSV(FilePath);
            if (_reservations.Count < 1)
            {
                return 1;
            }
            return _reservations.Max(t => t.TourId) + 1;
        }



    }
}
