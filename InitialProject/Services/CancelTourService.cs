using InitialProject.Aplication.Contracts.Repository;
using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Services
{
    public class CancelTourService : ICancelTourService
    {
        private readonly ITourRepository _tourRepository;
        private readonly ITourReservationRepository _tourReservationRepository;
        private readonly IVoucherRepository _voucherRepository;
        private readonly ITourPointRepository _tourPointRepository;
        public CancelTourService()
        {
            _tourRepository = Injector.CreateInstance<ITourRepository>();
            _tourReservationRepository = Injector.CreateInstance<ITourReservationRepository>();
            _voucherRepository = Injector.CreateInstance<IVoucherRepository>();
            _tourPointRepository = Injector.CreateInstance<ITourPointRepository>();
        }

        public List<Tour> GetAll()
        {
            return _tourRepository.GetAll();
        }

        public List<Tour> GetAllTwoDaysFromNow()
        {
            List<Tour> tours = _tourRepository.GetAll();
            List<Tour> toursToCancel = new();
            foreach (Tour tour in tours)
            {
                if (tour.StartingDateTime > DateTime.Now.AddDays(2).Date && tour.TourStarted == false)
                {
                    toursToCancel.Add(tour);
                }
            }
            return toursToCancel;
        }

        //Cancel tour and give Voucher to user
        public void CancelTour(string tourToCancel)
        {
            int tourId = int.Parse(tourToCancel.Split(' ')[0]);
            _tourRepository.Delete(_tourRepository.GetById(tourId));
            List<TourReservation> reservations = _tourReservationRepository.GetAll();
            foreach (TourReservation reservation in reservations)
            {
                if (reservation.TourId == tourId)
                {
                    _tourReservationRepository.Delete(reservation);
                    foreach (var TourPoint in _tourPointRepository.GetAll().Where(c => c.TourId == tourId).ToList())
                    {
                        _tourPointRepository.Delete(TourPoint);
                    }
                    CreateVoucher(reservation.UserId);
                }

            }

        }

        private void CreateVoucher(int userID)
        {
            Voucher voucher = new();
            voucher.GuideId = 1;//hardcoded because of no login form
            voucher.UserId = userID;
            voucher.Received = DateTime.Now;
            voucher.Name = "Vaucer";
            voucher.ValidUntil = DateTime.Now.AddYears(1);
            _voucherRepository.Save(voucher);
        }
    }
}
