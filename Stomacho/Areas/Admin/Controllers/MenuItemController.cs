using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stomacho.DataAccess.Repository.IRepository;
using Stomacho.Models;
using Stomacho.Models.ViewModels;
using Stomacho.Utility;

namespace Stomacho.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class MenuItemController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public MenuItemController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        List<MenuItem> objMenuItemList = _unitOfWork.MenuItem.GetAll(includeProperties:"Restaurant").ToList();
        
        return View(objMenuItemList);
    }

    public IActionResult Upsert(int? id)
    {
        MenuVM menuVM = new()
        {
            RestaurantList = _unitOfWork.Restaurant
            .GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }),
            MenuItem = new MenuItem()
        };
        if(id == null || id == 0)
        {
            //create
            return View(menuVM);
        }
        else
        {
            //update
            menuVM.MenuItem = _unitOfWork.MenuItem.Get(u => u.Id == id);
            return View(menuVM);
        }
    }

    [HttpPost]
    public IActionResult Upsert(MenuVM menuVM, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string menuItemsPath = Path.Combine(wwwRootPath, @"images\menuitem");

                if (!string.IsNullOrEmpty(menuVM.MenuItem.ImageUrl))
                {
                    //delete the old image
                    var oldImagePath =
                        Path.Combine(wwwRootPath, menuVM.MenuItem.ImageUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using (var fileStream = new FileStream(Path.Combine(menuItemsPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                menuVM.MenuItem.ImageUrl = @"\images\menuitem\" + fileName;
            }

            if (menuVM.MenuItem.Id == 0)
            {
                _unitOfWork.MenuItem.Add(menuVM.MenuItem);
            }
            else
            {
                _unitOfWork.MenuItem.Update(menuVM.MenuItem);
            }

            _unitOfWork.Save();
            TempData["success"] = "Menu item created successfully";
            return RedirectToAction("Index");
        }
        else
        {
            menuVM.RestaurantList = _unitOfWork.Restaurant
            .GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(menuVM);
        }
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        MenuItem? menuItemFromDb = _unitOfWork.MenuItem.Get(u => u.Id == id, includeProperties: "Restaurant");
        if (menuItemFromDb == null)
        {
            return NotFound();
        }
        return View(menuItemFromDb);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePOST(int? id)
    {
        MenuItem? obj = _unitOfWork.MenuItem.Get(u => u.Id == id);

        if (obj == null)
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
        _unitOfWork.MenuItem.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "Menu item deleted successfully";
        return RedirectToAction("Index");
    }
}