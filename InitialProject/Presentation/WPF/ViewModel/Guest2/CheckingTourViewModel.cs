namespace InitialProject.Presentation.WPF.ViewModel.Guest2
{
    using InitialProject.Aplication.Factory;
    using InitialProject.CustomClasses;
    using InitialProject.Services.IServices;

    public class CheckingTourViewModel
    {
        private readonly ITourAttendanceService _tourAttendanceService;

        public CheckingTourViewModel()
        {
            _tourAttendanceService = Injector.CreateInstance<ITourAttendanceService>();
        }

        public void RejectTourAttendance(TourAttendance tourAttendance)
        {
            _tourAttendanceService.RejectTourAttendance(tourAttendance);
        }

        public void ConfirmTourAttendance(TourAttendance tourAttendance)
        {
            _tourAttendanceService.ConfirmTourAttendance(tourAttendance);
        }
    }
}
