using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Stomacho.DataAccess.Data;
using Stomacho.DataAccess.Repository.IRepository;
using Stomacho.Models;

namespace Stomacho.DataAccess.Repository
{
    public class RestaurantRepository : Repository<Restaurant>, IRestaurantRepository
    {
        private ApplicationDbContext _db;
        public RestaurantRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Restaurant obj)
        {
            _db.Restaurants.Update(obj);
        }
    }
}
