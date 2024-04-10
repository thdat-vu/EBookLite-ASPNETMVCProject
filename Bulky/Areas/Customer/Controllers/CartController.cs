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
        /// This method plus 1 quantity of selected item and reload Index page
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        public IActionResult Plus(int cartId)
        {
            //retrive the cart
            var cartfromDb = _unitOfWork.ShoppingCart.Get(c => c.Id == cartId);
            //add 1 quantity
            cartfromDb.Count += 1;
            //update cart
            _unitOfWork.ShoppingCart.Update(cartfromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        /// This method minus 1 quantity of selected item and reload Index page
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        public IActionResult Minus(int cartId)
        {   //retrive the cart
            var cartfromDb = _unitOfWork.ShoppingCart.Get(c => c.Id == cartId);

            //be careful of Count of the product is 1 -> Minus 1 means delete the cart
            if (cartfromDb.Count <= 1)
            {
                //remove the product from cart
                _unitOfWork.ShoppingCart.Remove(cartfromDb);

            }
            else
            {
                //minus the cart.Count
                cartfromDb.Count -= 1;
                //update the cart
                _unitOfWork.ShoppingCart.Update(cartfromDb);
            }
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(int cartId)
        {
            //retrieve the cart
            var cartfromDb = _unitOfWork.ShoppingCart.Get(c => c.Id == cartId);
            //remove the cart
            _unitOfWork.ShoppingCart.Remove(cartfromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }


        /// <summary>
        /// This function return price of each item based on quantity
        /// ex: FORTUNE OF TIME cart: quantity from 1-50: 90$, 51-100: 85$, 100+: 80$
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        private double GetPriceBasedOnQuantity(ShoppingCart shoppingCart)
        {
            if (shoppingCart.Count <= 50)
            {
                return shoppingCart.Product.Price;
            }
            else if (shoppingCart.Count <= 100)
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
