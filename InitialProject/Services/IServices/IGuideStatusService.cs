using InitialProject.Domen.Model;

namespace InitialProject.Services
{
    public interface IGuideStatusService
    {
        GuideStatus GetStatusByUserId(int id);

        void EvaluateGuideForSuperStatus(int guideId);
        void UpdateToUnemployed(int id);

        void QuitJob(int guideId);
    }
}