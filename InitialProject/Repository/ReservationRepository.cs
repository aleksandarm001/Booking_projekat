using InitialProject.CustomClasses;
using InitialProject.Model;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace InitialProject.Repository
{
    public class ReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/reservations.txt";

        private readonly Serializer<Reservation> _serializer;

        private List<Reservation> _reservations;

        public ReservationRepository()
        {
            _serializer = new Serializer<Reservation>();
            _reservations = _serializer.FromCSV(FilePath);
        }

        public Reservation GetByReservationId(int reservationId)
        {
            _reservations = _serializer.FromCSV(FilePath);
            return _reservations.FirstOrDefault(t => t.Id == reservationId);
        }

        public List<Reservation> GetReservationsByAccommodationId(int accommodationID)
        {
            List<Reservation> reservations = new List<Reservation>();
            reservations = _reservations.Where(r => r.AccomodationId == accommodationID).ToList();
            return reservations;
        }

        public List<Reservation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Reservation Save(Reservation reservation)
        {
            reservation.Id = NextId();
            _reservations.Add(reservation);
            _serializer.ToCSV(FilePath, _reservations);
            return reservation;
        }
        public int NextId()
        {
            _reservations = _serializer.FromCSV(FilePath);
            if (_reservations.Count < 1)
            {
                return 1;
            }
            return _reservations.Max(t => t.Id) + 1;
        }
        public void Delete(Reservation reservation)
        {
            _reservations = _serializer.FromCSV(FilePath);
            Reservation founded = _reservations.Find(c => c.TourId == reservation.TourId && c.UserId == reservation.UserId);
            _reservations.Remove(founded);
            _serializer.ToCSV(FilePath, _reservations);
        }

        



    }
}
