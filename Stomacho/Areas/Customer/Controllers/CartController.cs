using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stomacho.DataAccess.Repository.IRepository;
using Stomacho.Models;
using Stomacho.Models.ViewModels;
using Stomacho.Utility;
using System.Security.Claims;

namespace Stomacho.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize  ]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        
        [BindProperty]
        public ShoppingCartVM ShoppingCartVM { get; set; }

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new ()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId 
                == userId, includeProperties: "MenuItem"),
                OrderHeader = new()
            };

            foreach(var cart in ShoppingCartVM.ShoppingCartList)
            {
                if (cart.MenuItem != null)
                {
                    ShoppingCartVM.OrderHeader.OrderTotal += (cart.MenuItem.Price * cart.Count);
                }
            }

            return View(ShoppingCartVM);
        }


        public IActionResult Checkout()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId
                == userId, includeProperties: "MenuItem"),
                OrderHeader = new()
            };

            ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);
            ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
            ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
            ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
            ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
            ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.ApplicationUser.State;
            ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;

            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                if (cart.MenuItem != null)
                {
                    ShoppingCartVM.OrderHeader.OrderTotal += (cart.MenuItem.Price * cart.Count);
                }
            }

            return View(ShoppingCartVM);
        }

        [HttpPost]
        [ActionName("Checkout")]
        public IActionResult CheckoutPOST()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM.ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => 
            u.ApplicationUserId == userId, includeProperties: "MenuItem");

            var OrderHeader = new OrderHeader()
            {
                OrderDate = DateTime.Now,
                ApplicationUserId = userId,
            };
            ShoppingCartVM.OrderHeader = OrderHeader;
            //ShoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
            //ShoppingCartVM.OrderHeader.ApplicationUserId = userId;

            ApplicationUser applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

            ShoppingCartVM.OrderHeader.City = applicationUser.City;
            ShoppingCartVM.OrderHeader.Name = applicationUser.Name;
            ShoppingCartVM.OrderHeader.PhoneNumber = applicationUser.PhoneNumber;
            ShoppingCartVM.OrderHeader.StreetAddress = applicationUser.StreetAddress;
            ShoppingCartVM.OrderHeader.State = applicationUser.State;
            ShoppingCartVM.OrderHeader.PostalCode = applicationUser.PostalCode;

            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                if (cart.MenuItem != null)
                {
                    ShoppingCartVM.OrderHeader.OrderTotal += (cart.MenuItem.Price * cart.Count);
                }
            }

            if(applicationUser.RestaurantId.GetValueOrDefault() == 0)
            {
                //it is a regular customer 
                ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
                ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending;
            }
            else
            {
                //it is a restaurant company account and we need to approve the order
                ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusDelayedPayment;
                ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusApproved;
            }

            _unitOfWork.OrderHeader.Add(ShoppingCartVM.OrderHeader);
            _unitOfWork.Save();

            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                OrderDetail orderDetail = new()
                {
                    MenuItemId = cart.MenuItemId,
                    OrderHeaderId = ShoppingCartVM.OrderHeader.Id,
                    Price = cart.MenuItem.Price,
                    Count = cart.Count
                };
                _unitOfWork.OrderDetail.Add(orderDetail);
                _unitOfWork.Save();
            }

            if (applicationUser.RestaurantId.GetValueOrDefault() == 0)
            {
                //it is a regular customer account and we need to capture payment
                //sripe logic
            }

            return RedirectToAction(nameof(OrderConfirmation), new {id=ShoppingCartVM.OrderHeader.Id});
        }

        public IActionResult OrderConfirmation(int id)
        {
            return View(id);
        }

        public IActionResult Plus(int Id)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == Id);
            cartFromDb.Count += 1;
            _unitOfWork.ShoppingCart.Update(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int Id)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == Id);
            if (cartFromDb.Count == 1)
            {
                _unitOfWork.ShoppingCart.Remove(cartFromDb);
            }
            else
            {
                cartFromDb.Count -= 1;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveFromCart(int Id)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == Id);
            _unitOfWork.ShoppingCart.Remove(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
