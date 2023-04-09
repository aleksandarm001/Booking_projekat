using InitialProject.IRepository;
using InitialProject.Repository;
using InitialProject.Services;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Factory
{
    public static class Injector
    {
        public static ICancelTourService cancelTourService() 
        {
            return new CancelTourService();
        }

        public static ITourRepository tourRepository()
        {
            return new TourRepository();
        }

        public static IReservationRepository reservationRepository()
        {
            return new ReservationRepository();
        }

        public static IVoucherRepository voucherRepository()
        {
            return new VoucherRepository();
        }
    }
}
