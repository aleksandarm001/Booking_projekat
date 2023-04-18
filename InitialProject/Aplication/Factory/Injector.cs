using InitialProject.Repository;
using InitialProject.Services;
using InitialProject.Services.IServices;
using System.Collections.Generic;
using System;
using InitialProject.Aplication.Contracts.Repository;

namespace InitialProject.Aplication.Factory
{
    public static class Injector
    {
        private static Dictionary<Type, object> _implementations = new Dictionary<Type, object>
        {
            { typeof(ICancelTourService), new CancelTourService() },
            { typeof(ITourRepository), new TourRepository() },
            { typeof(IReservationRepository), new ReservationRepository() },
            { typeof(IVoucherRepository), new VoucherRepository() },
            { typeof(ITourStatisticsService), new TourStatisticsService() },
            { typeof(ITourService), new TourService() },
            { typeof(IUserRepository), new UserRepository() },
            { typeof(IReservationService), new ReservationService() },
            { typeof(ITourReservationService), new TourReservationService() },
            { typeof(ITourReservationRepository), new TourReservationRepository() },
            { typeof(ITourPointRepository), new TourPointRepository() },
            { typeof(ITourRateService), new TourRateService() },
            { typeof(IUserService), new UserService() },
            { typeof(ITourAttendanceService), new TourAttendanceService() },
            { typeof(ITourPointService), new TourPointService() },
        };

        public static T CreateInstance<T>()
        {
            Type type = typeof(T);

            if (_implementations.ContainsKey(type))
            {
                return (T)_implementations[type];
            }

            throw new ArgumentException($"No implementation found for type {type}");
        }
    }
}
