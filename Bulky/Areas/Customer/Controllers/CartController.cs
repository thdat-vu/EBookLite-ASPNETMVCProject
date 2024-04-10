using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Ultility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bulky.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartVM shoppingCartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //get userID of logged in user
            //these 2 methods were developed by Identity engineer in Microsoft
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationId == userId, includeProperties: "Product")
            };

            //Calculate the OderTotal of ShoppingcartList
            foreach (var cart in shoppingCartVM.ShoppingCartList)
            {
                cart.Price = this.GetPriceBasedOnQuantity(cart);
                shoppingCartVM.OrderTotal += (cart.Price * cart.Count);
            }
            return View(shoppingCartVM);
        }
        /// <summary>
        /// This function return price of each cart based on quantity
        /// ex: FORTUNE OF TIME cart: quantity from 1-50: 90$, 51-100: 85$, 100+: 80$
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        private double GetPriceBasedOnQuantity(ShoppingCart shoppingCart)
        {
            if(shoppingCart.Count <= 50)
            {
                return shoppingCart.Product.Price;
            }
            else if(shoppingCart.Count <= 100)
            {
                return shoppingCart.Product.Price50;

            }
            else
            {
                return shoppingCart.Product.Price100;
            }
        }
    }
}
