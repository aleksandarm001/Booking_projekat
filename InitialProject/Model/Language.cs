using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class Language : ISerializable
    {
        public string Name { get; set; }

        public Language(string language)
        {
            Name = language;
        }
        public Language()
        {
            Name = "";
        }

        public override string ToString()
        {
            return Name;
        }
        
        public string[] ToCSV()
        {
            string[] csvValues = {
                Name
            };

            return csvValues;
        }

        public Language fromStringToLanguage(string s)
        {
            return new Language(s);
        }


        public void FromCSV(string[] values)
        {
            Name = values[0];
        }
    }
}
