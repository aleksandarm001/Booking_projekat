﻿namespace InitialProject.Services.IServices
{
    using InitialProject.Domen.Model;
    using System.Collections.Generic;

    public interface ILocationService
    {
        public List<Location> GetAll();
    }
}