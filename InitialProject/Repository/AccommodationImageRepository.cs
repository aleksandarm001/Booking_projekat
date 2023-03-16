using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace InitialProject.Repository
{
    public  class AccommodationImageRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodationImages.txt";

        private readonly Serializer<AccommodationImages> _serializer;

        private List<AccommodationImages> _accommodationImages;


        public AccommodationImageRepository()
        {
            _serializer = new Serializer<AccommodationImages>();
            _accommodationImages= new List<AccommodationImages>();
        }

        public List<AccommodationImages> getAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public AccommodationImages Save(AccommodationImages accommodationImages,int resourceId)
        {
            accommodationImages.ImageId = NextId();
            accommodationImages.ResourceId = resourceId;
            _accommodationImages.Add(accommodationImages);
            _serializer.ToCSV(FilePath, _accommodationImages);
            return accommodationImages;

        }

        public int NextId()
        {
            _accommodationImages = _serializer.FromCSV(FilePath);
            if (_accommodationImages.Count < 1)
            {
                return 1;
            }
            return _accommodationImages.Max(acm => acm.ImageId) + 1;
        }
    }
}
