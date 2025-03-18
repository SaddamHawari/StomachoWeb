using Microsoft.AspNetCore.Mvc;
using Stomacho.DataAccess.Data;
using Stomacho.DataAccess.Repository.IRepository;
using Stomacho.Models;

namespace Stomacho.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RestaurantController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public RestaurantController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Restaurant> objRestaurantList = _unitOfWork.Restaurant.GetAll().ToList();
            return View(objRestaurantList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Restaurant obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Restaurant.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Restaurant created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            Restaurant? restaurantFromDb = _unitOfWork.Restaurant.Get(u=>u.Id==id);
            /*Restaurant? restaurantFromDb1 = _db.Restaurants.FirstOrDefault(u=>u.Id==id);
            Restaurant? restaurantFromDb2 = _db.Restaurants.Where(u=>u.Id==id).FirstOrDefault();*/

            if(restaurantFromDb == null)
            {
                return NotFound();
            }
            return View(restaurantFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Restaurant obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Restaurant.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Restaurant updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Restaurant? restaurantFromDb = _unitOfWork.Restaurant.Get(u => u.Id == id);
            /*Restaurant? restaurantFromDb1 = _db.Restaurants.FirstOrDefault(u=>u.Id==id);
            Restaurant? restaurantFromDb2 = _db.Restaurants.Where(u=>u.Id==id).FirstOrDefault();*/

            if (restaurantFromDb == null)
            {
                return NotFound();
            }
            return View(restaurantFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Restaurant? obj = _unitOfWork.Restaurant.Get(u => u.Id == id);

            if(obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Restaurant.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Restaurant deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
