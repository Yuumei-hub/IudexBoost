using IudexBoost.Business.Interfaces;
using IudexBoost.Models.Classes;
using IudexBoost.ProjectServices.Services;
using IudexBoost.Repository;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IudexBoost.Business.Services
{
    public class CartService: IGenericService<Cart>
    {
        private readonly CartRepository _cartRepository;
        private readonly GameService _gameService;
        private readonly CartItemService _cartItemService;
        public CartService(CartRepository cartRepository, GameService gameService, CartItemService cartItemService)
        {
            _cartRepository = cartRepository;
            _gameService = gameService;
            _cartItemService = cartItemService;
        }
        public void Add(Cart cart)
        {
            _cartRepository.Add(cart);
        }

        public void Delete(Cart cart)
        {
            _cartRepository.Delete(cart);
        }

        public Cart GetCartbyCartId(int cartId)
        {
            return _cartRepository.GetById(cartId);
        }
        public void Update(Cart cart)
        {
            _cartRepository.Update(cart);
        }

        public CartItem CreateCartItem(int quantity, decimal price, string fromSkillRating, string toSkillRating, int gameId)
        {
            //just creates a cart item object with the params.
            Game game = _gameService.GetById(gameId);
            if (game == null)
                return null;

            CartItem cartItem = new CartItem
            {
                GameName = game.Title,
                GameImgUrl = game.ImageUrl,
                Quantity = quantity,
                Price = price,
                FromSkillRating = fromSkillRating,
                ToSkillRating = toSkillRating
            };
            return cartItem;
        }
        //should be in cartitemservice

        public void AddCartItemToCart(Cart cart, CartItem cartItem)
        {
            CartItem existingItem = cart.CartItems.FirstOrDefault(item => item.FromSkillRating == cartItem.FromSkillRating &&
                item.ToSkillRating == cartItem.ToSkillRating &&
                item.Price == cartItem.Price &&
                item.GameName == cartItem.GameName &&
                item.GameImgUrl == cartItem.GameImgUrl);
            
            if(existingItem == null)
            {
                //adding cart item to cart.cartitems which is list<cartitem>
                cart.CartItems.Add(cartItem);
                _cartItemService.AddCartItemToDB(cartItem);
            }
            else
                existingItem.Quantity += cartItem.Quantity;
        }
        //should be in cartitemservice

        public void RemoveCartItem(Cart cart, int cartItemId) 
        {
            CartItem cartItem = _cartRepository.GetCartItemById(cartItemId);
            _cartRepository.DeleteCartItem(cart,cartItem);
        }

        //should be in cartitemservice
        public CartItem GetCartItemById(int cartItemId)
        {
            return _cartRepository.GetCartItemById(cartItemId);
        }

        public Cart CreateCart(int userId)
        {
            return new Cart { UserId = userId.ToString() };
        }
        public Cart GetCartByUserId(int userId)
        {
            return _cartRepository.GetCartByUserId(userId);
        }

        public Cart GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
