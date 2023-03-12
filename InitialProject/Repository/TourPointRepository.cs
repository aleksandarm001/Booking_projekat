using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class TourPointRepository
    {
        private const string FilePath = "../../../Resources/Data/tourpoints.txt";
        private const string TempFilePath = "../../../Resources/TempData/tourpoints.txt";


        private readonly Serializer<TourPoint> _serializer;

        private List<TourPoint> _tourPoints;

        public TourPointRepository()
        {
            _serializer = new Serializer<TourPoint>();
            _tourPoints = new List<TourPoint>();
        }
        public List<TourPoint> getAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public TourPoint Save(TourPoint tourPoint)
        {
            tourPoint.Id = NextId();
            _tourPoints.Add(tourPoint);
            _serializer.ToCSV(FilePath, _tourPoints);
            return tourPoint;
        }
        public int NextId()
        {
            _tourPoints = _serializer.FromCSV(FilePath);
            if (_tourPoints.Count < 1)
            {
                return 1;
            }
            return _tourPoints.Max(tour => tour.Id) + 1;
        }
        public void Delete(TourPoint tourPoint)
        {
            _tourPoints = _serializer.FromCSV(FilePath);
            TourPoint foundedTourPoint = _tourPoints.Find(tour => tour.Id == tourPoint.Id);
            _tourPoints.Remove(foundedTourPoint);
            _serializer.ToCSV(FilePath, _tourPoints);
        }
        public TourPoint Update(TourPoint tourPoint)
        {
            _tourPoints = _serializer.FromCSV(FilePath);
            TourPoint current = _tourPoints.Find(tour => tour.Id == tourPoint.Id);
            int index = _tourPoints.IndexOf(current);
            _tourPoints.Remove(current);
            _tourPoints.Insert(index, tourPoint);
            _serializer.ToCSV(FilePath, _tourPoints);
            return tourPoint;
        }

        public List<TourPoint> getAllTemp()
        {
            return _serializer.FromCSV(TempFilePath);
        }
        public TourPoint SaveTemp(TourPoint tourPoint)
        {
            tourPoint.Id = NextIdTemp();
            _tourPoints.Add(tourPoint);
            _serializer.ToCSV(TempFilePath, _tourPoints);
            return tourPoint;
        }
        public int NextIdTemp()
        {
            _tourPoints = _serializer.FromCSV(TempFilePath);
            if (_tourPoints.Count < 1)
            {
                return 1;
            }
            return _tourPoints.Max(tour => tour.Id) + 1;
        }
        public void DeleteTemp(TourPoint tourPoint)
        {
            _tourPoints = _serializer.FromCSV(TempFilePath);
            TourPoint foundedTourPoint = _tourPoints.Find(tour => tour.Id == tourPoint.Id);
            _tourPoints.Remove(foundedTourPoint);
            _serializer.ToCSV(TempFilePath, _tourPoints);
        }
        public TourPoint UpdateTemp(TourPoint tourPoint)
        {
            _tourPoints = _serializer.FromCSV(TempFilePath);
            TourPoint current = _tourPoints.Find(tour => tour.Id == tourPoint.Id);
            int index = _tourPoints.IndexOf(current);
            _tourPoints.Remove(current);
            _tourPoints.Insert(index, tourPoint);
            _serializer.ToCSV(TempFilePath, _tourPoints);
            return tourPoint;
        }


    }
}
