using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class TourImages : ISerializable
    {
        public int TourId { get; set; }
        public int ImageId { get; set; }
        public string Url { get; set; }
        public int ResourceId { get; set; }



        public TourImages() { }

        public TourImages(int tourId, int id, string url, int resourceId)
        {
            TourId = tourId;
            ImageId = id;
            Url = url;
            ResourceId = resourceId;


        }

        public void FromCSV(string[] values)
        {
            TourId= Convert.ToInt32(values[0]);
            ImageId = Convert.ToInt32(values[1]);
            ResourceId = Convert.ToInt32(values[2]);
            Url = values[3];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                TourId.ToString(),
                ImageId.ToString(),
                ResourceId.ToString(),
                Url


            };
            return csvValues;
        }
    
    }
}
