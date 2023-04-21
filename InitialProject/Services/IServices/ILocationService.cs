namespace InitialProject.Services.IServices
{
    using InitialProject.Domen.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal interface ILocationService
    {
        public List<Location> GetAll();
    }
}
