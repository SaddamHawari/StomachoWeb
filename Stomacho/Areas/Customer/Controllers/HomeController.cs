using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Stomacho.DataAccess.Repository.IRepository;
using Stomacho.Models;

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
        var restaurant = _unitOfWork.Restaurant.Get(
        u => u.Id == restaurantId,
        includeProperties: "MenuItems"
    );

        if (restaurant == null)
        {
            return NotFound();
        }

        return View(restaurant);
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
