namespace InitialProject.Services
{
    using InitialProject.Aplication.Factory;
    using InitialProject.Aplication.Contracts.Repository;
    using InitialProject.Domen.Model;
    using InitialProject.Repository;
    using InitialProject.Services.IServices;
    using System.Collections.Generic;

    public class TourRateService : ITourRateService
    {
        private readonly ITourRateRepository _repository;
        private readonly ITourAttendanceService _tourAttendanceService;

        public TourRateService()
        {
            _repository = Injector.CreateInstance<ITourRateRepository>();
            _tourAttendanceService = Injector.CreateInstance<ITourAttendanceService>();
        }

        public List<TourRate> GetAllRates()
        {
            return _repository.GetAll();
        }

        public void MakeTourRate(TourRate tourRate)
        {
            _repository.Save(tourRate);
            _tourAttendanceService.AddedComment(tourRate.GuestId, tourRate.TourId);
        }

        public void Update(TourRate tourRate)
        {
            _repository.Update(tourRate);
        }
    }
}
