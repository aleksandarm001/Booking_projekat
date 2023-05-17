using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace InitialProject.Domen.Model
{
    //ovo je dto ali je stavljen ovde pa nisam hteo da prebacujem dan pred u custom classes da nesto ne polupam ,zato nije u dijagramu :)
    public class OwnerChangeRequests 
    {
        private int _requestId;
        private int _reservationId;
        private string _accommodationName;
        private DateTime _newStartDate;
        private DateTime _newEndDate;
        private int _ownerId;
        private bool _isDateAvailable;

        public OwnerChangeRequests()
        {

        }

        public OwnerChangeRequests(int requestId, int reservationId, string accommodationName, DateTime newStartDate, DateTime newEndDate, int ownerId, bool isDateAvailable)
        {
            _requestId = requestId;
            _reservationId = reservationId;
            _accommodationName = accommodationName;
            _newStartDate = newStartDate;
            _newEndDate = newEndDate;
            _ownerId = ownerId;
            _isDateAvailable = isDateAvailable;
        }

        public int RequestId { get => _requestId; set => _requestId = value; }
        public int ReservationId { get => _reservationId; set => _reservationId = value; }
        public string AccommodationName { get => _accommodationName; set => _accommodationName = value; }

        public DateTime NewStartDate { get => _newStartDate; set => _newStartDate = value; }

        public DateTime NewEndDate { get => _newEndDate; set => _newEndDate = value; }

        public int OwnerId { get => _ownerId; set => _ownerId = value; }

        public bool IsDateAvailable { get => _isDateAvailable; set => _isDateAvailable = value;  }

       
    }
}
