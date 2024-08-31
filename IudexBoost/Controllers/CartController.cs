using IudexBoost.Models.Classes;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using IudexBoost.Business.Services;

namespace IudexBoost.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;
        private readonly GameService _gameService;
        private readonly RankPriceService _rankPriceService;
        public CartController(CartService cartService, GameService gameService, RankPriceService rankPriceService)
        {
            _cartService = cartService;
            _gameService = gameService;
            _rankPriceService = rankPriceService;
        }

        [HttpPost]
        public JsonResult AddCartItem(int quantity,string fromSkillRating, string toSkillRating, int gameId)
        {
            decimal price= _rankPriceService.GetPrice(fromSkillRating, toSkillRating);
            CartItem cartItem=_cartService.CreateCartItem(quantity, price, fromSkillRating, toSkillRating, gameId);

            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if(userId==null)
                return Json(new {success=false, message="Error. User id null"});

            if(cartItem==null)
                return Json(new {success=false, message="Error. Cart item null"});
            Cart cart= _cartService.GetCartByUserId(userId);

            if (ModelState.IsValid)
            {
                if (cart!=null)//if user has a cart already
                {
                    //add cartitem to cart
                    _cartService.AddCartItemToCart(cart, cartItem);
                }
                else
                {
                    //create cart first then add the item
                    cart = _cartService.CreateCart(Convert.ToInt32(userId));
                    _cartService.AddCartItemToCart(cart, cartItem);
                }
                return Json(new { success = true, message = "Successful data" });
            }
            return Json(new { success = false, message = "Invalid data" });
        }
        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Cart cart;
            try
            {
                cart = _cartService.GetCartByUserId(Convert.ToInt32(userId));
            }
            catch (InvalidOperationException ex)
            {
                return RedirectToAction("Index", "Login");
            }

            return View(cart);
        }

        public IActionResult DeleteCartItem(int cartItemid)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Cart cart= _cartService.GetCartByUserId(Convert.ToInt32(userId));
            CartItem cartItem = _cartService.GetCartItemById(cartItemid);
            
            if(cartItem != null)
            {
                _cartService.RemoveCartItem(cart, cartItemid);
            }
            return RedirectToAction("Index");
        }

    }
}
