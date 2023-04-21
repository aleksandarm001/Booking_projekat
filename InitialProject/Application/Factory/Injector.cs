using InitialProject.Repository;
using InitialProject.Services;
using InitialProject.Services.IServices;
using System.Collections.Generic;
using System;
using InitialProject.Aplication.Contracts.Repository;
using InitialProject.Infrastructure.Repository;

namespace InitialProject.Aplication.Factory
{
    public static class Injector
    {
        private static readonly Dictionary<Type, Lazy<object>> _implementations = new Dictionary<Type, Lazy<object>>
        {
            { typeof(ICancelTourService), new Lazy<object>(() => new CancelTourService()) },
            { typeof(ITourRepository), new Lazy<object>(() => new TourRepository()) },
            { typeof(IReservationRepository), new Lazy<object>(() => new ReservationRepository()) },
            { typeof(IVoucherRepository), new Lazy<object>(() => new VoucherRepository()) },
            { typeof(ITourStatisticsService), new Lazy<object>(() => new TourStatisticsService()) },
            { typeof(ITourService), new Lazy<object>(() => new TourService()) },
            { typeof(IUserRepository), new Lazy<object>(() => new UserRepository()) },
            { typeof(IReservationService), new Lazy<object>(() => new ReservationService()) },
            { typeof(ITourReservationService), new Lazy<object>(() => new TourReservationService()) },
            { typeof(ITourReservationRepository), new Lazy<object>(() => new TourReservationRepository()) },
            { typeof(ITourPointRepository), new Lazy<object>(() => new TourPointRepository()) },
            { typeof(ITourRateService), new Lazy<object>(() => new TourRateService()) },
            { typeof(IUserService), new Lazy<object>(() => new UserService()) },
            { typeof(ITourAttendanceService), new Lazy<object>(() => new TourAttendanceService()) },
            { typeof(ITourAttendanceRepository), new Lazy<object>(() => new TourAttendanceRepository()) },
            { typeof(ITourPointService), new Lazy<object>(() => new TourPointService()) },
            { typeof(IVoucherService), new Lazy<object>(() => new VoucherService()) },
            { typeof(ILocationService), new Lazy<object>(() => new LocationService()) },
            { typeof(ILocationRepository), new Lazy<object>(() => new LocationRepository()) },
            { typeof(ITourRequestRepository), new Lazy<object>(() => new TourRequestRepository()) },
            { typeof(ITourRequestService), new Lazy<object>(() => new TourRequestService()) },
            { typeof(IOwnerToRateService), new Lazy<object>(() => new OwnerToRateService()) },
            { typeof(IAccommodationReservationService), new Lazy<object>(() => new AccommodationReservationService()) },
            { typeof(IAccommodationService), new Lazy<object>(() => new AccommodationService()) },
            { typeof(IUserReservationCounterService), new Lazy<object>(() => new UserReservationCounterService()) },
        };

        public static T CreateInstance<T>()
        {
            Type type = typeof(T);

            if (_implementations.ContainsKey(type))
            {
                return (T)_implementations[type].Value;
            }

            throw new ArgumentException($"No implementation found for type {type.FullName}");
        }
    }
}
