﻿namespace InitialProject.Services.IServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ITourNotificationService
    {
        void NotifyGuest(int userId);
    }
}
