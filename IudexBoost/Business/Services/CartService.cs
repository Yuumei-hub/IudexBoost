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
