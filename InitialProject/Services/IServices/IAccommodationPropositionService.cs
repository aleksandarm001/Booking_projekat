using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services.IServices
{
    public interface IAccommodationPropositionService
    {
        int AccommodationWithMostReservations(int userId);
        int AccommodationWithLeastReservations(int userId);
        int HotAccommodationStatistics(int userId);
        int ColdAccommodationStatistics(int userId);
    }
}
