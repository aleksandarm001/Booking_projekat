namespace InitialProject.Services
{
    using InitialProject.Aplication.Contracts.Repository;
    using InitialProject.Aplication.Factory;
    using InitialProject.Domen.CustomClasses;
    using InitialProject.Domen.Model;
    using InitialProject.Repository;
    using InitialProject.Services.IServices;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.TeamFoundation.WorkItemTracking.Process.WebApi.Models;
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
            CheckRequests();
        }

        public void CheckRequests()
        {
            foreach (TourRequest tourRequest in  _repository.GetAll()) 
            { 
                if (tourRequest.StartingDate < DateTime.Now.AddDays(2) && tourRequest.RequestStatus == ComplexTourRequest.Status.OnHold)
                {
                    tourRequest.RequestStatus = ComplexTourRequest.Status.Rejected;
                    _repository.Update(tourRequest);
                }
            }
        }

        public List<TourRequest> GetAllRequests()
        {
            return _repository.GetAll().ToList();
        }


        public List<TourRequest> GetAllTourRequests(int userId)
        {
            return _repository.GetAll().Where(tourRequest => tourRequest.UserId == userId).ToList();
        }

        public List<string> GetAllYearsOfTourReqeusts()
        {
            List<TourRequest> tours = _repository.GetAll();
            List<string> years = new();
            foreach (TourRequest tour in tours)
            {
                if (!years.Contains(tour.StartingDate.Year.ToString()))
                    years.Add(tour.StartingDate.Year.ToString());
            }
            return years;
        }

        public void MakeTourRequest(TourRequest tourRequest)
        {
            _repository.Save(tourRequest);
        }

        public void Update(TourRequest tourRequest)
        {
            _repository.Update(tourRequest);
        }


        public void DeleteTourRequest(TourRequest tourRequst)
        {

        }

        public TourRequest Delete(TourRequest tourRequest)
        {
            throw new NotImplementedException();
        }

        public List<TourRequest> FilterRequests(FilterRequests dataToFilter)
        {
            List<TourRequest> allTourRequests = new List<TourRequest>(_repository.GetAll());
            List<TourRequest> FilteredData = new();
            FilteredData = allTourRequests.Where(c =>(dataToFilter.City == null || c.Location.City == dataToFilter.City) &&
                                      (dataToFilter.Country == null || c.Location.Country == dataToFilter.Country) &&
                                      (dataToFilter.NumberOfGuests == null || c.GuestNumber <= dataToFilter.NumberOfGuests) &&
                                      (dataToFilter.Language == null || c.Language.Name == dataToFilter.Language) &&
                                      (dataToFilter.StartingDate == null || c.StartingDate >= dataToFilter.StartingDate) &&
                                      (dataToFilter.EndingDate == null || c.EndingDate <= dataToFilter.EndingDate)).ToList();
            return FilteredData;

        }
    }
}
