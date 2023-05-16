using InitialProject.Domen.Model;
using System.Collections.Generic;

namespace InitialProject.Infrastructure.Repository
{
    public interface IGuideStatusRepository
    {
        void Delete(GuideStatus status);
        List<GuideStatus> GetAll();
        GuideStatus GetById(int id);
        int NextId();
        GuideStatus Save(GuideStatus status);
        GuideStatus Update(GuideStatus status);

        GuideStatus GetGuideStatusByEmployeeId(int GuideId);
    }
}