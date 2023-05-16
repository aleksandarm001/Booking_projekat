﻿using InitialProject.CustomClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services.IServices
{
    public interface INotificationService
    {
        Notification SaveNotification(Notification notification);

        bool IsReservationCanceled(int reservationId);
    }
}
