using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services
{
    public interface IAccommodationService
    {
        string GetNameById(int accommodationId);
        int GetOwnerIdByAccommodationId(int accommodationId);
        int GetOwnerIdByReservationId(int reservationId);
        int GetAccommodationIdByReservationId(int reservationId);
        Accommodation GetAccommodationByReservationId(int reservationId);
        int GetReservationIdByAccommodationId(int accommodationId);
        void DeleteReservation(int reservationId);
    }
}
