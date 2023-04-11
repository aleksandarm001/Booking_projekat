using InitialProject.Domen.RepositoryInterfaces;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Services
{
    public class OwnerRateService
    {
       // private readonly OwnerRateRepository _ownerRateRepository;
        private readonly GuestReviewRepository _guestReviewRepository;
        private readonly IOwnerRateRepository _ownerRateRepository;

        public OwnerRateService()
        {
            _ownerRateRepository = new OwnerRateRepository();
            _guestReviewRepository = new GuestReviewRepository();
        }
        public void SaveRate(OwnerRate ownerRate)
        {
            _ownerRateRepository.Save(ownerRate);
        }

        public List<OwnerRate> RatingsFromRatedGuests()
        {
            List<OwnerRate> RatingsFromRatedGuests = new List<OwnerRate>();
            
            
            foreach (OwnerRate ownerRate in _ownerRateRepository.GetAll())
            {
                foreach(GuestReview guestReview in _guestReviewRepository.GetAll())
                {
                    if(ownerRate.userId == guestReview.GuestId && ownerRate.AccommodationId == guestReview.AccommodationId )
                    {
                        RatingsFromRatedGuests.Add(ownerRate);
                    }
                }
            }
            return RatingsFromRatedGuests;
        }
       
        public Boolean isSuperOwner(int ownerId)
        {
            
            List<OwnerRate>OwnerRates = _ownerRateRepository.GetAllRatesByOwner(ownerId);
            if(OwnerRates.Count < 20)
            {
                return false;
            }
            else
            {
                double rateAverage = OwnerRates.Average(o => (o.CleanlinessRate + o.CorrectnessRate)/2);
                if(rateAverage > 4.5)
                {
                    return true;
                }
                return false; 
            }

        }
      

    }
}
