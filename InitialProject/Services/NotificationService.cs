using InitialProject.Aplication.Contracts.Repository;
using InitialProject.CustomClasses;
using InitialProject.Repository;

namespace InitialProject.Services
{
    public class NotificationService
    {
        private readonly INotificationRepository _notificationRespository;

        public NotificationService()
        {
            _notificationRespository = new NotificationRespository();
        }

        public Notification SaveNotification(Notification notificiation)
        {
            return _notificationRespository.Save(notificiation);
        }
    }
}
