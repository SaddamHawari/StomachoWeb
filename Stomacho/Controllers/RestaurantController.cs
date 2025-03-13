using Microsoft.AspNetCore.Mvc;
using Stomacho.DataAccess.Data;
using Stomacho.Models;

namespace Stomacho.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly ApplicationDbContext _db;
        public RestaurantController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Restaurant> objRestaurantList = _db.Restaurants.ToList();
            return View(objRestaurantList);
        }
    }
}
