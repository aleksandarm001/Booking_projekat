using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InitialProject.Domen.Model.TourAttendance;

namespace InitialProject.Domen.CustomClasses
{
    public class TourReportsDTO
    {
        private readonly ITourService _tourService;
        public string Name { get; set; }
        public Location Location { get; set; }
        public DateTime StartingDateTime { get; set; }
        public AttendanceStatus Status { get; set; }
        

        public TourReportsDTO(int tourId, AttendanceStatus status, DateTime date)
        {
            _tourService = Injector.CreateInstance<ITourService>();
            Name = _tourService.GetTourById(tourId).Name;
            Status = status;
            StartingDateTime = date;
            Location = _tourService.GetTourById(tourId).Location;
        }
    }
}
