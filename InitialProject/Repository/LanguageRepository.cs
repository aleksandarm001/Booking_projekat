using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class LanguageRepository
    {
        private const string FilePath = "../../../Resources/Data/languages.txt";

        private readonly Serializer<Language> _serializer;


        public LanguageRepository()
        {
            _serializer = new Serializer<Language>();
        }


        public List<Language> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

    }
}
