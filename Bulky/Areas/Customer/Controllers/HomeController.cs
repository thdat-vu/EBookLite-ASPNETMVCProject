using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Ultility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Bulky.Areas.Customer.Controllers
{
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
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties:"Category");
            return View(productList);
        }

        public IActionResult Details(int productId)
        {
            ShoppingCart cart = new()
            {
                Product = _unitOfWork.Product.Get(u => u.Id == productId, includeProperties: "Category"),
                Count = 1,
                ProductId = productId

            };
            
            return View(cart);
        }
        [HttpPost]
        [Authorize]//must be  authorize user

        public IActionResult Details(ShoppingCart shoppingCart)
        {
            //get userID of logged in user
            //these 2 methods were developed by Identity engineer in Microsoft
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            //pass userId to shopping cart ApplicationId
            shoppingCart.ApplicationId = userId;

            //check cart of this user for this product has already existed or not
            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(c => c.ApplicationId == userId  && c.ProductId == shoppingCart.ProductId);
            if(cartFromDb != null)
            {
                //shopping cart exist -> Update Number
                cartFromDb.Count += shoppingCart.Count;
                //update count.
                _unitOfWork.ShoppingCart.Update(cartFromDb);    
            }
            else
            {
                //add shopping cart
                _unitOfWork.ShoppingCart.Add(shoppingCart);
            }

            TempData["success"] = "Cart updated successfully";
            _unitOfWork.Save();

            //return RedirectToAction("Index");
            //instead of write a magic string action name here. We can write name of with corresponding action name.
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
}
