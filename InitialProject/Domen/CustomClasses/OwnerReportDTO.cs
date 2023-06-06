using InitialProject.CustomClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domen.CustomClasses
{
    public class OwnerReportDTO
    {
        public string UserName;
        public DateRange ReservationDateRange { get; set; }
        public int NumberOfGuests { get; set; }

        public OwnerReportDTO() { }

        public OwnerReportDTO(string userName, DateRange reservationDateRange, int numberOfGuests)
        {
            UserName = userName;
            ReservationDateRange = reservationDateRange;
            NumberOfGuests = numberOfGuests;
        }
    }
}
