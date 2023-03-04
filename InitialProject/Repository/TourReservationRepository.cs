using InitialProject.Model;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;

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


    }
}
