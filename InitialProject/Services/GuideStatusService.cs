using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services
{
    public class GuideStatusService : IGuideStatusService
    {
        private readonly IGuideStatusRepository guideStatusRepository;
        public GuideStatusService()
        {
            guideStatusRepository = Injector.CreateInstance<IGuideStatusRepository>();
        }

        public GuideStatus GetStatusByUserId(int id)
        {
            return guideStatusRepository.getAll().Where(c=>c.EmployeeId== id).FirstOrDefault();
        }

        public void UpdateToUnemployed(int id)
        {
            GuideStatus guideStatus = guideStatusRepository.getAll().Where(c => c.EmployeeId == id).FirstOrDefault();
            guideStatus.EmploymentStatus = GuideStatus.Status.Unemployed;
            guideStatusRepository.Update(guideStatus);
        }

    }
}
