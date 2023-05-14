using InitialProject.Domen.Model;

namespace InitialProject.Services
{
    public interface IGuideStatusService
    {
        GuideStatus GetStatusByUserId(int id);

        void UpdateToUnemployed(int id);
    }
}