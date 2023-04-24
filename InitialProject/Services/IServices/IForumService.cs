﻿using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services.IServices
{
    public interface IForumService
    {
        List<Forum> GetAll();
        Forum Save(Forum forum);
        void Delete(Forum forum);
        void Update(Forum forum);

    }
}
