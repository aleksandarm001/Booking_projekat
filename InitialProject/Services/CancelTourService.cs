using InitialProject.CustomClasses;
using InitialProject.Factory;
using InitialProject.IRepository;
using InitialProject.Model;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services
{
    public class CancelTourService : ICancelTourService
    {
        private readonly ITourRepository _tourRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IVoucherRepository _voucherRepository;
        public CancelTourService()
        {
            _tourRepository = Injector.tourRepository();
            _reservationRepository = Injector.reservationRepository();
            _voucherRepository = Injector.voucherRepository();
        }

        public List<Tour> GetAll()
        {
            return _tourRepository.GetAll();
        }
        
        //Cancel tour and give Voucher to user
        public void CancelTour(string tourToCancel)
        {
            int tourId = int.Parse(tourToCancel.Split(' ')[0]);
            List<Reservation> reservations = _reservationRepository.GetAll();
            foreach (Reservation reservation in reservations)
            {
                if (reservation.TourId == tourId)
                {
                    _reservationRepository.Delete(reservation);
                }

                Voucher voucher = new Voucher();
                voucher.GuideId = 1;
                voucher.UserId = reservation.UserId;
                voucher.Received = DateTime.Now;
                voucher.Name = "Vaucer";
                voucher.ValidUntil = DateTime.Now.AddYears(1);
                _voucherRepository.Save(voucher);
            }
            
        }
    }
}
