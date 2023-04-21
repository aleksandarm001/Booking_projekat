namespace InitialProject.Services
{
    using InitialProject.Aplication.Factory;
    using InitialProject.Aplication.Contracts.Repository;
    using InitialProject.Domen.Model;
    using InitialProject.Services.IServices;
    using System.Collections.Generic;

    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        public LocationService()
        {
            _locationRepository = Injector.CreateInstance<ILocationRepository>();
        }

        public List<Location> GetAll()
        {
            return _locationRepository.getAll();
        }
    }
}
