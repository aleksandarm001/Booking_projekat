using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace InitialProject.Model
{
    public enum StatusType { Pending, Canceled, Approved}
    public class ChangeReservationRequest: ISerializable
    {
        private int requestId;
        private int reservationId;
        private DateTime newStartDate;
        private DateTime newEndDate;
        private StatusType requestStatus;

        public ChangeReservationRequest(int reservationId, DateTime newStartDate, DateTime newEndDate, StatusType requestStatus)
        {
            this.ReservationId = reservationId;
            this.NewStartDate = newStartDate;
            this.NewEndDate = newEndDate;
            this.RequestStatus = requestStatus;
        }
        public ChangeReservationRequest()
        {
        }

        public int ReservationId { get => reservationId; set => reservationId = value; }
        public DateTime NewStartDate { get => newStartDate; set => newStartDate = value; }
        public DateTime NewEndDate { get => newEndDate; set => newEndDate = value; }
        public StatusType RequestStatus { get => requestStatus; set => requestStatus = value; }
        public int RequestId { get => requestId; set => requestId = value; }

        public string[] ToCSV()
        {
            string[] csvValues = {
                requestId.ToString(),   
                reservationId.ToString(),
                newStartDate.ToString(),
                newEndDate.ToString(),
                requestStatus.ToString()
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            requestId = Convert.ToInt32(values[0]);
            reservationId = Convert.ToInt32(values[1]);
            newStartDate = Convert.ToDateTime(values[2]);
            newEndDate = Convert.ToDateTime(values[3]);
            requestStatus = (StatusType)Enum.Parse(typeof(StatusType), values[4]);
        }
    }
}
