using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services.IServices
{
    public interface IAccommodationReservationService
    {
        Dictionary<int, string> GetReservationsByUserId(int userId);
        bool IsCancellingPossible(DateTime currentDate, int reservationId);
        List<int> GetReservationsIdsByAccommodationId(int accommodationId);
    }
}
