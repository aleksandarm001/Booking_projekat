using InitialProject.Model;
using System.Collections.Generic;

namespace InitialProject.IRepository
{
    public interface ITourRepository
    {
        List<Tour> GetAll();
        int NextId();
        Tour Save(Tour tour);
        Tour Update(Tour tour);
    }
}