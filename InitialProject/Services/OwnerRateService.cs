using InitialProject.Aplication.Contracts.Repository;
using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Repository;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Services
{
    public class OwnerRateService : IOwnerRateService
    {
        private readonly GuestReviewRepository _guestReviewRepository;
        private readonly IOwnerRateRepository _ownerRateRepository;

        public OwnerRateService()
        {
            _ownerRateRepository = Injector.CreateInstance<IOwnerRateRepository>();
            _guestReviewRepository = new GuestReviewRepository();
        }
        public List<OwnerRate> GetAll()
        {
            return _ownerRateRepository.GetAll();
        }
        public void SaveRate(OwnerRate ownerRate)
        {
            _ownerRateRepository.Save(ownerRate);
        }

        public List<OwnerRate> RatingsFromRatedGuest(int ownerId)
        {
            List<OwnerRate> ratingsFromRatedGuests = new List<OwnerRate>();
            /*
            foreach(OwnerRate ownerRate in _ownerRateRepository.GetAllRatesByOwner(ownerId))
            {
                foreach(GuestReview guestReview in _guestReviewRepository.GetAll())
                {
                    if(ownerRate.UserId == guestReview.GuestId && ownerRate.AccommodationId == guestReview.AccommodationId)
                    {
                        ratingsFromRatedGuest.Add(ownerRate);
                        break;
                    }
                }
            }
            return ratingsFromRatedGuest;

            */
            List<OwnerRate> allRatesByOwner = _ownerRateRepository.GetAllRatesByOwner(ownerId);
            List<GuestReview> allGuestReviews = _guestReviewRepository.GetAll();

            foreach (OwnerRate ownerRate in allRatesByOwner)
            {
                bool isRatedByGuest = allGuestReviews.Any(guestReview =>
                    ownerRate.UserId == guestReview.GuestId && ownerRate.AccommodationId == guestReview.AccommodationId);

                if (isRatedByGuest)
                {
                    ratingsFromRatedGuests.Add(ownerRate);
                }
            }
            return ratingsFromRatedGuests;
        }

        

        public bool IsSuperOwner(int ownerId)
        {
            List<OwnerRate> ownerRates = _ownerRateRepository.GetAllRatesByOwner(ownerId);
            /*
            if(ownerRates.Count < 5)
            {
                return false;
            }
            else
            {
                
                double rateAverage = ownerRates.Average(o => (o.CleanlinessRate + o.CorrectnessRate) / 2);
                if(rateAverage > 4.5)
                {
                    return true;
                }
            }
            return false;
            */
            if(ownerRates.Count < 5)
                return false;

            double rateAverage = ownerRates.Average(o => (o.HygieneRate + o.CorrectnessRate) / 2);
            return rateAverage > 4.5;
        }

        public List<OwnerRate> GetRatesByUserId(int userId)
        {
            return GetAll().Where(rate => rate.UserId == userId).ToList();
        }
    }
}
