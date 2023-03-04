using InitialProject.Model;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Repository
{

    public class TourRepository

    {

        private const string FilePath = "../../../Resources/Data/tours.csv";

        private readonly Serializer<Tour> _serializer;

        private List<Tour> _tours;

        public TourRepository()
        {
            _serializer = new Serializer<Tour>();
            _tours = _serializer.FromCSV(FilePath);
        }
        public Tour GetByTourId(int tourId)
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours.FirstOrDefault(t => t.TourId == tourId);
        }

    }
}
