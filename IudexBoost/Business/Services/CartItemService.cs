using IudexBoost.Models.Classes;
using IudexBoost.Repository;

namespace IudexBoost.Business.Services
{
    public class CartItemService
    {
       private readonly CartItemRepository _cartItemRepository;
        public CartItemService(CartItemRepository cartItemRepository)
        {
                _cartItemRepository = cartItemRepository;
        }

        public void AddCartItemToDB(CartItem cartItem)
        {
            _cartItemRepository.Add(cartItem);
        }
    }
}
