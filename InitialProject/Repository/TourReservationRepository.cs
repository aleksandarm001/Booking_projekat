using InitialProject.Model;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace InitialProject.Repository
{
    public class TourReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/tourReservations.csv";

        private readonly Serializer<TourReservation> _serializer;

        private List<TourReservation> _tourReservations;

        public TourReservationRepository()
        {
            _serializer = new Serializer<TourReservation>();
            _tourReservations = _serializer.FromCSV(FilePath);
        }

        public TourReservation GetByTourAndReservationId(int tourId, int reservationId)
        {
            _tourReservations = _serializer.FromCSV(FilePath);
            return _tourReservations.FirstOrDefault(t => t.TourId == tourId && t.ReservationId == reservationId);
        }

        public List<TourReservation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public TourReservation Save(TourReservation tourReservation)
        {
            _tourReservations= _serializer.FromCSV(FilePath);
            _tourReservations.Add(tourReservation);
            _serializer.ToCSV(FilePath, _tourReservations);
            return tourReservation;
        }

        public void Delete(TourReservation tourReservation)
        {
            _tourReservations = _serializer.FromCSV(FilePath);
            TourReservation founded = _tourReservations.Find(c => c.TourId == tourReservation.TourId && c.ReservationId == tourReservation.ReservationId);
            _tourReservations.Remove(founded);
            _serializer.ToCSV(FilePath, _tourReservations);
        }

        public TourReservation Update(TourReservation tourReservation)
        {
            _tourReservations = _serializer.FromCSV(FilePath);
            TourReservation current = _tourReservations.Find(c => c.TourId == tourReservation.TourId);
            _tourReservations.Remove(current);
            _tourReservations.Add(tourReservation);       
            _serializer.ToCSV(FilePath, _tourReservations);
            return tourReservation;
        }



    }
}
