using InitialProject.CustomClasses;
using System.Collections.Generic;

namespace InitialProject.IRepository
{
    public interface IReservationRepository
    {
        void Delete(Reservation reservation);
        List<Reservation> GetAll();
        Reservation GetByReservationId(int reservationId);
        int NextId();
        Reservation Save(Reservation reservation);
    }
}