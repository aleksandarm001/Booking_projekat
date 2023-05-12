using InitialProject.Aplication.Factory;
using InitialProject.Application.Contracts.Repository;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Services
{
    class ComplexTourRequestService : IComplexTourRequestService
    {
        private readonly IComplexTourRequestRepository _repository;

        public ComplexTourRequestService()
        {
            _repository = Injector.CreateInstance<IComplexTourRequestRepository>();
            CheckRequests();
        }

        public void CheckRequests()
        {
            int i = 0;
            foreach (var t in GetAllTourRequests().GroupBy(t => t.TourId))
            {
                if (t.Min(t=> t.StartingDate).AddDays(2)>=DateTime.Now && t.Where(t=> t.RequestStatus==ComplexTourRequest.Status.Accepted).Count() != t.Count())
                {
                    foreach(ComplexTourRequest complexTourRequest in t)
                    {
                        complexTourRequest.RequestStatus = ComplexTourRequest.Status.Rejected;
                        _repository.Update(complexTourRequest);
                    }
                }
            }
        }


        public List<ComplexTourRequest> GetTourRequest(int tourId)
        {
            return _repository.GetAll().Where(complexTourRequest => complexTourRequest.TourId == tourId).ToList();
        }

        public void MakeTourRequest(List<ComplexTourRequest> complexTourRequests)
        {
            int id = _repository.NextId();
            foreach(ComplexTourRequest complexTourRequest in  complexTourRequests)
            {
                complexTourRequest.TourId = id;
                _repository.Save(complexTourRequest);
            }
            
        }

   
        public void Update(ComplexTourRequest complexTourRequests)
        {
            _repository.Update(complexTourRequests);
        }

        public List<ComplexTourRequest> GetAllTourRequests()
        {
            return _repository.GetAll();
        }

        public List<ComplexTourRequest> GetAllTourRequestsByUser(int userId)
        {
            return _repository.GetAll().Where(complexTourRequest => complexTourRequest.UserId == userId).ToList();
        }


    }
}
