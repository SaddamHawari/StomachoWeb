using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stomacho.DataAccess.Data;
using Stomacho.DataAccess.Repository.IRepository;

namespace Stomacho.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IRestaurantRepository Restaurant { get; private set; }
        public IMenuItemRepository MenuItem { get; private set; }
        public ICompanyRepository Company { get; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Restaurant = new RestaurantRepository(_db);
            MenuItem = new MenuItemRepository(_db);
            Company = new CompanyRepository(_db); 
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
