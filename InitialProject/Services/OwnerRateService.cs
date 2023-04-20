using InitialProject.Aplication.Contracts.Repository;
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
        private readonly GuestReviewRepository guestReviewRepository;
        private readonly IOwnerRateRepository _ownerRateRepository;

        public OwnerRateService()
        {
            _ownerRateRepository = new OwnerRateRepository();
            guestReviewRepository = new GuestReviewRepository();
        }
        public void SaveRate(OwnerRate ownerRate)
        {
            _ownerRateRepository.Save(ownerRate);
        }

        public List<OwnerRate> RatingsFromRatedGuest(int ownerId)
        {
            List<OwnerRate> RatingsFromRatedGuest = new List<OwnerRate>();

            foreach(OwnerRate ownerRate in _ownerRateRepository.GetAllRatesByOwner(ownerId))
            {
                foreach(GuestReview guestReview in guestReviewRepository.GetAll())
                {
                    if(ownerRate.userId == guestReview.GuestId && ownerRate.AccommodationId == guestReview.AccommodationId)
                    {
                        RatingsFromRatedGuest.Add(ownerRate);
                        break;
                    }
                }
            }
            return RatingsFromRatedGuest;
        }

        

        public bool IsSuperOwner(int ownerId)
        {
            List<OwnerRate> ownerRates = _ownerRateRepository.GetAllRatesByOwner(ownerId);

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
        }
    }
}
