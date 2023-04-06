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
        private int userId;
        private int ownerId;

        public ChangeReservationRequest(int reservationId, DateTime newStartDate, DateTime newEndDate, StatusType requestStatus, int userId, int ownerId)
        {
            this.ReservationId = reservationId;
            this.NewStartDate = newStartDate;
            this.NewEndDate = newEndDate;
            this.RequestStatus = requestStatus;
            this.userId = userId;
            this.ownerId = ownerId; 
        }
        public ChangeReservationRequest()
        {
        }

        public int ReservationId { get => reservationId; set => reservationId = value; }
        public DateTime NewStartDate { get => newStartDate; set => newStartDate = value; }
        public DateTime NewEndDate { get => newEndDate; set => newEndDate = value; }
        public StatusType RequestStatus { get => requestStatus; set => requestStatus = value; }
        public int RequestId { get => requestId; set => requestId = value; }
        public int UserId { get => userId; set => userId = value; }
        public int OwnerId { get => ownerId; set => ownerId = value; }

        public string[] ToCSV()
        {
            string[] csvValues = {
                requestId.ToString(),   
                reservationId.ToString(),
                newStartDate.ToString(),
                newEndDate.ToString(),
                requestStatus.ToString(),
                userId.ToString(),
                ownerId.ToString()
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
            userId = Convert.ToInt32(values[5]);
            ownerId = Convert.ToInt32(values[6]);
        }
    }
}
