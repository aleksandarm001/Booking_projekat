using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services.IServices
{
    public interface IAccommodationService
    {
        string GetNameById(int accommodationId);
        string GetNameByReservationId(int reservationId);
        int GetOwnerIdByAccommodationId(int accommodationId);
        int GetOwnerIdByReservationId(int reservationId);
        int GetAccommodationIdByReservationId(int reservationId);
        Accommodation GetAccommodationByReservationId(int reservationId);
        Accommodation GetAccommodationById(int accommodationId);
        int GetReservationIdByAccommodationId(int accommodationId);
        void DeleteReservation(int reservationId);
    }
}
