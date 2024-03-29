﻿using InitialProject.Aplication.Contracts.Repository;
using InitialProject.Domen.Model;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Repository
{
    public class TourImagesRepository : ITourImageRepository
    {
        private const string FilePath = "../../../Infrastructure/Resources/Data/tourimages.txt";

        private readonly Serializer<TourImages> _serializer;

        private List<TourImages> _tourImages;


        public TourImagesRepository()
        {
            _serializer = new Serializer<TourImages>();
            _tourImages = new List<TourImages>();
        }

        public List<TourImages> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public TourImages Save(TourImages tourImages)
        {
            tourImages.ImageId = NextId();
            _tourImages.Add(tourImages);
            _serializer.ToCSV(FilePath, _tourImages);
            return tourImages;

        }

        public int NextId()
        {
            _tourImages = _serializer.FromCSV(FilePath);
            if (_tourImages.Count < 1)
            {
                return 1;
            }
            return _tourImages.Max(acm => acm.ImageId) + 1;
        }

        
    }
}
