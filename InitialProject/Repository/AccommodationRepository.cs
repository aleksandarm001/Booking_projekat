using InitialProject.CustomClasses;
using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InitialProject.Repository
{
    public class AccommodationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodations.txt";

        private readonly Serializer<Accommodation> _serializer;

        private List<Accommodation> _accommodations;

        public AccommodationRepository()
        {
            _serializer = new Serializer<Accommodation>();
            _accommodations = new List<Accommodation>();
        }
        public List<Accommodation> getAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public Accommodation Save(Accommodation accommodation)
        {
            accommodation.AccommodationID = NextId();
            _accommodations.Add(accommodation);
            _serializer.ToCSV(FilePath, _accommodations);
            return accommodation;
        }
        public int NextId()
        {
            _accommodations = _serializer.FromCSV(FilePath);
            if(_accommodations.Count < 1)
            {
                return 1;
            }
            return _accommodations.Max(acm => acm.AccommodationID) + 1;
        }
        public void Delete(Accommodation accommodation)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            Accommodation foundedAccommodation = _accommodations.Find(acm => acm.AccommodationID == accommodation.AccommodationID);
            _accommodations.Remove(foundedAccommodation);
            _serializer.ToCSV(FilePath, _accommodations);
        }
        public Accommodation Update(Accommodation accommodation)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            Accommodation current = _accommodations.Find(acm => acm.AccommodationID == accommodation.AccommodationID);
            int index = _accommodations.IndexOf(current);
            _accommodations.Remove(current);
            _accommodations.Insert(index, accommodation);   
            _serializer.ToCSV(FilePath, _accommodations);
            return accommodation;
        }
    }
}
