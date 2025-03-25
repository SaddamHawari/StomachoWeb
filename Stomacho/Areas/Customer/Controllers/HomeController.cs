using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stomacho.DataAccess.Repository.IRepository;
using Stomacho.Models;
using Stomacho.Models.ViewModels;

namespace Stomacho.Areas.Customer.Controllers;
[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;

    }

    public IActionResult Index()
    {
        IEnumerable<Restaurant> restaurantList = _unitOfWork.Restaurant.GetAll();
        return View(restaurantList);
    }

    public IActionResult SeeMenu(int restaurantId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var restaurant = _unitOfWork.Restaurant.Get(
        u => u.Id == restaurantId,
        includeProperties: "MenuItems"
    );

        if (restaurant == null)
        {
            return NotFound();
        }

        // Create a view model to pass both restaurant and shopping cart items
        var viewModel = new SeeMenuViewModel
        {
          
            Restaurant = restaurant/*,
            ShoppingCartItems = shoppingCartItems*/
        };

        return View(viewModel);
    }

    [HttpPost]
    [Authorize]
    public IActionResult SeeMenu(ShoppingCart shoppingCart)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
        shoppingCart.ApplicationUserId = userId;

        ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId ==
        userId && u.MenuItemId == shoppingCart.MenuItemId);

        if(cartFromDb != null)
        {
            cartFromDb.Count += shoppingCart.Count;
            _unitOfWork.ShoppingCart.Update(cartFromDb);
        }
        else
        {
            _unitOfWork.ShoppingCart.Add(shoppingCart);
        }

        TempData["Success"] = "Cart updated successfully";
        _unitOfWork.Save();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
