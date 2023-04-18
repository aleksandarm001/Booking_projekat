namespace InitialProject.Services
{
    using InitialProject.Domen.Model;
    using InitialProject.Repository;
    using InitialProject.Services.IServices;
    using System.Collections.Generic;

    public class TourRateService : ITourRateService
    {
        private readonly TourRateRepository _repository;
        private TourAttendanceService _tourAttendanceService;

        public TourRateService()
        {
            _repository = new TourRateRepository();
            _tourAttendanceService = new TourAttendanceService();
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
