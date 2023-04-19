﻿using InitialProject.Domen.RepositoryInterfaces;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services
{
    public class OwnerRateService
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

        

        public Boolean isSuperOwner(int ownerId)
        {
            List<OwnerRate> ownerRates = _ownerRateRepository.GetAllRatesByOwner(ownerId);

            if(ownerRates.Count < 5)
            {
                return false;
            }
            
                
            double rateAverage = ownerRates.Average(o => (o.CleanlinessRate + o.CorrectnessRate) / 2);
            if(rateAverage > 4.5)
            {
               return true;
            }
            
            return false;
        }
    }
}
