using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InitialProject.Serializer;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class Images : ISerializable
    {
        public string Url { get; set; }
        public int ImageId { get; set; }
        public int ResourceId { get; set; }



        public Images(){ }

        public Images(string url,int imageId, int resourceId)
        {
            Url = url;
            ImageId = imageId;
            ResourceId = resourceId;

            
        }

        public void FromCSV(string[] values)
        {
            ImageId = int.Parse(values[0]);
            ResourceId = int.Parse(values[1]);
            Url = values[2];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                ImageId.ToString(),
                ResourceId.ToString(),
                Url

                
            };
            return csvValues;
        }
    }
}
