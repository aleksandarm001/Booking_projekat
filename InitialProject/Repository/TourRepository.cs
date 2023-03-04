using InitialProject.Model;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

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

        public List<Tour> GetAll() 
        {
            return _serializer.FromCSV(FilePath);
        }

        public Tour Save(Tour tour)
        {
            tour.TourId = NextId();
            _tours = _serializer.FromCSV(FilePath);
            _tours.Add(tour);
            _serializer.ToCSV(FilePath, _tours);
            return tour;
        }

        public void Delete(Tour tour)
        {
            _tours = _serializer.FromCSV(FilePath);
            Tour founded = _tours.Find(t => t.TourId == tour.TourId);
            _tours.Remove(founded);
            _serializer.ToCSV(FilePath, _tours);
        }

        public int NextId()
        {
            _tours = _serializer.FromCSV(FilePath);
            if (_tours.Count < 1)
            {
                return 1;
            }
            return _tours.Max(t => t.TourId) + 1;
        }

        public Tour Update(Tour tour)
        {
            _tours = _serializer.FromCSV(FilePath);
            Tour current = _tours.Find(t => t.TourId == tour.TourId);
            int index = _tours.IndexOf(current);
            _tours.Remove(current);
            _tours.Insert(index, tour);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _tours);
            return tour;
        }
    }
}
