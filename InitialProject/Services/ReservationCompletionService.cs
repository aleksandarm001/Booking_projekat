using InitialProject.Aplication.Contracts.Repository;
using InitialProject.Aplication.Factory;
using InitialProject.CustomClasses;
using InitialProject.Repository;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services
{
    public class ReservationCompletionService : IReservationCompletionService
    {
        private readonly IReservationService _reservationService;
        private readonly IOwnerToRateService _ownerToRateService;
        private readonly IAccommodationReservationService _accommodationReservationService;
        private readonly IAccommodationService _accommodationService;
        private readonly IUserReservationCounterService _userReservationCounterService;
        public ReservationCompletionService()
        {
            _reservationService = Injector.CreateInstance<IReservationService>();
            _ownerToRateService = Injector.CreateInstance<IOwnerToRateService>();
            _accommodationReservationService = Injector.CreateInstance<IAccommodationReservationService>();
            _accommodationService = Injector.CreateInstance<IAccommodationService>();
            _userReservationCounterService = Injector.CreateInstance<IUserReservationCounterService>();
        }
        public void HandleReservationCompletion(int userId, int reservationId)
        {
            Reservation reservation = _reservationService.GetReservationById(reservationId);
            if (reservation == null)
            {
                return;
            }

            if (CheckIfLeftReservation(reservation))
            {
                int accommodationId = _accommodationService.GetAccommodationIdByReservationId(reservationId);

                // Add owner to OwnerToRate
                int ownerId = _accommodationService.GetOwnerIdByAccommodationId(accommodationId);
                OwnerToRate ownerToRate = new OwnerToRate(ownerId, accommodationId, reservation.UserId, reservation.ReservationDateRange.EndDate);
                _ownerToRateService.Save(ownerToRate);

                // Delete reservation
                _reservationService.Delete(reservationId);
                _accommodationService.DeleteReservation(reservationId);

                //update userReservationCounter
                _userReservationCounterService.UpdateReservationCounter(userId);
                // Add user to UserToReview
                //UserToReview userToReview = new UserToReview(reservation.UserId, accommodationId, reservation.ReservationDateRange.EndDate);
                //_userToReviewRepository.Save(userToReview);
            }
        }
        public bool CheckIfLeftReservation(Reservation reservation)
        {
            return reservation.ReservationDateRange.EndDate < DateTime.Now;
        }
    }
}
