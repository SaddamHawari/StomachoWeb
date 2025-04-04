﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stomacho.Models;

namespace Stomacho.DataAccess.Repository.IRepository
{
    public interface IRestaurantRepository : IRepository<Restaurant>
    {
        void Update( Restaurant obj);
    }
}
