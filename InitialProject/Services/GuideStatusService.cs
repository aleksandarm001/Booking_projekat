using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Infrastructure.Repository;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;
using static InitialProject.Domen.Model.GuideStatus;

namespace InitialProject.Services
{
    public class GuideStatusService : IGuideStatusService
    {
        private readonly IGuideStatusRepository guideStatusRepository;
        private readonly ITourService tourService;
        private readonly ITourRateService tourRateService;
        private readonly ICancelTourService cancelTourService;
        public GuideStatusService()
        {
            guideStatusRepository = Injector.CreateInstance<IGuideStatusRepository>();
            tourService = Injector.CreateInstance<ITourService>();
            tourRateService = Injector.CreateInstance<ITourRateService>();
            cancelTourService = Injector.CreateInstance<ICancelTourService>();
        }

        public GuideStatus GetStatusByUserId(int id)
        {
            return guideStatusRepository.GetAll().Where(c=>c.EmployeeId== id).FirstOrDefault();
        }

        public void UpdateToUnemployed(int id)
        {
            GuideStatus guideStatus = guideStatusRepository.GetAll().Where(c => c.EmployeeId == id).FirstOrDefault();
            guideStatus.EmploymentStatus = GuideStatus.Status.Unemployed;
            guideStatusRepository.Update(guideStatus);
        }

        private void UpdateToSuper(int id)
        {
            GuideStatus guideStatus = guideStatusRepository.GetAll().Where(c => c.EmployeeId == id).FirstOrDefault();
            guideStatus.EmploymentStatus = GuideStatus.Status.SuperGuide;
            guideStatus.DateOfPromotion = DateTime.Now;
            guideStatusRepository.Update(guideStatus);
        }

        private string GetMostFrequentLanguage(List<Tour> allFinishedToursByGuide)
        {
            var toursGroupedByLanguage = allFinishedToursByGuide
                .GroupBy(tour => tour.Language.ToString());


            var mostFrequentLanguageGroup = toursGroupedByLanguage
                .OrderByDescending(group => group.Count())
                .FirstOrDefault();

            if (mostFrequentLanguageGroup != null && mostFrequentLanguageGroup.Count() >= 20)
            {
                return mostFrequentLanguageGroup.Key;
            }

            return null;
        }


        private Status GetStatus(int GuideId) 
        {
            return guideStatusRepository.GetAll().Where(c => c.EmployeeId == GuideId).FirstOrDefault().EmploymentStatus;
        }

        private void CheckIsPromotionStillActive(int guideId)
        {
            GuideStatus guideStatus = guideStatusRepository.GetGuideStatusByEmployeeId(guideId);

            if (IsPromotionExpired(guideStatus))
            {
                guideStatus.EmploymentStatus = Status.Employeed;
                guideStatusRepository.Update(guideStatus);
            }
        }

        private bool IsPromotionExpired(GuideStatus guideStatus)
        {
            DateTime promotionExpirationDate = DateTime.Now.AddYears(-1);
            return guideStatus.DateOfPromotion <= promotionExpirationDate;
        }

        public void EvaluateGuideForSuperStatus(int guideId)
        {

            if(GetStatus(guideId) == Status.Unemployed)
                return;

            if(GetStatus(guideId) == Status.SuperGuide)
            {
                CheckIsPromotionStillActive(guideId);
                return;
            }

            List<Tour> allFinishedToursByGuide = tourService.GetAllFinishedInOneYearByGuide(guideId);

            Language? language = new Language(GetMostFrequentLanguage(allFinishedToursByGuide));
            if (language == null)
                return;

            if (tourRateService.GetAverageRateForGuideInPastYearWithSpecificLanguage(guideId, language) > 9)
                UpdateToSuper(guideId);
        }

        public void QuitJob(int guideId)
        {
            try
            {
                cancelTourService.FindAndCancelAllToursByGuide(guideId);
                UpdateToUnemployed(guideId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}
