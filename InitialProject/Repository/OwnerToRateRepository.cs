using InitialProject.CustomClasses;
using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class OwnerToRateRepository
    {
        private const string FileParh = "../../../Resources/Data/ownersToRate.txt";
        private readonly Serializer<OwnerToRate> _serializer;
        private List<OwnerToRate> _ownersToRate;

        public OwnerToRateRepository()
        {
            _serializer = new Serializer<OwnerToRate>();
            _ownersToRate = new List<OwnerToRate>();
        }
        public List<OwnerToRate> GetAll()
        {
            return _serializer.FromCSV(FileParh);
        }
        public OwnerToRate Save(OwnerToRate ownerToRate)
        {
            _ownersToRate.Add(ownerToRate);
            _serializer.ToCSV(FileParh, _ownersToRate);
            return ownerToRate;
        }
        public void Delete(OwnerToRate ownerToRate)
        {
            _ownersToRate = GetAll();
            OwnerToRate foundedRate = _ownersToRate.Find(or => or.OwnerId == ownerToRate.OwnerId);
            _ownersToRate.Remove(foundedRate);
            _serializer.ToCSV(FileParh, _ownersToRate);
        }
    }
}
