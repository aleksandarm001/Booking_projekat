using InitialProject.Domen.RepositoryInterfaces;
using InitialProject.Repository;
using InitialProject.Services;
using InitialProject.Services.IServices;

namespace InitialProject.Factory
{
    public static class Injector
    {
        public static ICancelTourService cancelTourService() 
        {
            return new CancelTourService();
        }

        public static ITourRepository tourRepository()
        {
            return new TourRepository();
        }

        public static IReservationRepository reservationRepository()
        {
            return new ReservationRepository();
        }

        public static IVoucherRepository voucherRepository()
        {
            return new VoucherRepository();
        }

        public static ITourStatisticsService tourStatisticsService()
        {
            return new TourStatisticsService();
        }

        public static ITourService tourService()
        {
            return new TourService();
        }

        public static IUserRepository userRepository()
        {
            return new UserRepository();
        }

        public static IReservationService reservationService()
        {
            return new ReservationService();
        }

        public static ITourReservationService tourReservationService()
        {
            return new TourReservationService();
        }

        public static ITourReservationRepository tourReservationRepository()
        {
            return new TourReservationRepository();
        }

        public static ITourPointRepository tourPointRepository()
        {
            return new TourPointRepository();
        }

        public static ITourRateService tourRateService()
        {
            return new TourRateService();
        }
        public static IUserService userService()
        {
            return new UserService();
        }

        public static ITourAttendanceService tourAttendance()
        {
            return new TourAttendanceService();
        }

        public static ITourPointService tourPointService()
        {
            return new TourPointService();
        }
    }
}
