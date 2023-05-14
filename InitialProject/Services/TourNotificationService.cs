namespace InitialProject.Services
{
    using InitialProject.Aplication.Contracts.Repository;
    using InitialProject.Aplication.Factory;
    using InitialProject.Application.Contracts.Repository;
    using InitialProject.Presentation.WPF.View.Guest2;
    using InitialProject.Services.IServices;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TourNotificationService : ITourNotificationService
    {
        private readonly ITourNotificationRepository _repository;

        public TourNotificationService()
        {
            _repository = Injector.CreateInstance<ITourNotificationRepository>();
        }

        public void NotifyGuest(int userId)
        {
            var lista = _repository.GetAllForUser(userId);
            foreach(var notification in lista) 
            {
                TourNotification tourNotification = new TourNotification(notification.TourId);
                _repository.Delete(notification);
                tourNotification.ShowDialog();
                
            }
        }

        public void MakeNotification(int userId, int tourId, Domen.CustomClasses.TourNotification.NotificationType type)
        {
            Domen.CustomClasses.TourNotification notification = new Domen.CustomClasses.TourNotification(userId, tourId, type);
            _repository.Save(notification);
        }
    }
}
