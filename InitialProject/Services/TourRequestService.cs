namespace InitialProject.Services
{
    using InitialProject.Aplication.Contracts.Repository;
    using InitialProject.Aplication.Factory;
    using InitialProject.Domen.Model;
    using InitialProject.Services.IServices;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TourRequestService : ITourRequestService
    {
        private readonly ITourRequestRepository _repository;

        public TourRequestService()
        {
            _repository = Injector.CreateInstance<ITourRequestRepository>();
        }

        public void CheckRequests()
        {
            foreach (TourRequest tourRequest in  _repository.GetAll()) 
            { 
                if (tourRequest.StartingDate < DateTime.Now.AddDays(2) && tourRequest.RequestStatus == TourRequest.Status.OnHold)
                {
                    tourRequest.RequestStatus = TourRequest.Status.Rejected;
                    _repository.Update(tourRequest);
                }
            }
        }

        public List<TourRequest> GetAllTourRequests(int userId)
        {
            return _repository.GetAll().Where(tourRequest => tourRequest.UserId == userId).ToList();
        }

        public void MakeTourRequest(TourRequest tourRequest)
        {
            _repository.Save(tourRequest);
        }

        public void Update(TourRequest tourRequest)
        {
            _repository.Update(tourRequest);
        }


    }
}
