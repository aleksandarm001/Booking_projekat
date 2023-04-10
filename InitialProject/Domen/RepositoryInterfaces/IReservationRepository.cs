using InitialProject.CustomClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domen.RepositoryInterfaces
{
    public interface IReservationRepository
    {
        Reservation GetByReservationId(int reservationId);
        List<Reservation> GetAll();
        Reservation Save(Reservation reservation);
        void Delete(Reservation reservation);
    }
}
