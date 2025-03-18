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
    public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
    {
        private ApplicationDbContext _db;
        public MenuItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(MenuItem obj)
        {
            var objFromDb = _db.MenuItems.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.Description = obj.Description;
                objFromDb.Price = obj.Price;
                objFromDb.IsAvailable = obj.IsAvailable;
                objFromDb.RestaurantId = obj.RestaurantId;

                // Only update ImageUrl if a new image is provided
                if (!string.IsNullOrEmpty(obj.ImageUrl))
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
