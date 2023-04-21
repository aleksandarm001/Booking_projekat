namespace InitialProject.Services.IServices
{
    using InitialProject.Domen.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ITourRequestService
    {
        List<TourRequest> GetAllTourRequests(int userId);
        void MakeTourRequest(TourRequest tourRequest);

        void Update(TourRequest tourRequest);

        void CheckRequests();
    }
}
