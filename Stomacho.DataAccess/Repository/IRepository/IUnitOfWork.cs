﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stomacho.DataAccess.Data;

namespace Stomacho.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IRestaurantRepository Restaurant { get; }
        IMenuItemRepository MenuItem { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailRepository OrderDetail { get; }


        void Save();
    }
}
