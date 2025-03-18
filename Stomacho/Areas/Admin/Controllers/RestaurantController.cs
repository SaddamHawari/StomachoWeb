using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Stomacho.DataAccess.Data;
using Stomacho.DataAccess.Repository.IRepository;
using Stomacho.Models;
using Stomacho.Models.ViewModels;
using Stomacho.Utility;

namespace Stomacho.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
    public class RestaurantController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public RestaurantController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
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
        public IActionResult Create(Restaurant obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string restaurantPath = Path.Combine(wwwRootPath, @"images\restaurant");

                    using (var fileStream = new FileStream(Path.Combine(restaurantPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    obj.ImageUrl = @"\images\restaurant\" + fileName;
                }
                _unitOfWork.Restaurant.Add(obj);
                _unitOfWork.Save(); 
                TempData["success"] = "Restaurant created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
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
        public IActionResult Edit(Restaurant obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string restaurantImagesPath = Path.Combine(wwwRootPath, @"images\restaurant");

                    if (!string.IsNullOrEmpty(obj.ImageUrl))
                    {
                        // Delete the old image
                        var oldImagePath = Path.Combine(wwwRootPath, obj.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(restaurantImagesPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    obj.ImageUrl = @"\images\restaurant\" + fileName;
                }
                _unitOfWork.Restaurant.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Restaurant updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
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
            // Delete the image file if it exists
            if (!string.IsNullOrEmpty(obj.ImageUrl))
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                var oldImagePath = Path.Combine(wwwRootPath, obj.ImageUrl.TrimStart('\\'));

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            _unitOfWork.Restaurant.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Restaurant deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
