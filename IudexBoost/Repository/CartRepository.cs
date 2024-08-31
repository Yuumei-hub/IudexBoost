using IudexBoost.Models.Classes;
using Microsoft.EntityFrameworkCore;

namespace IudexBoost.Repository
{
    public class CartRepository: GenericRepository<Cart>
    {
        public CartRepository(Context context):base(context)
        {
        }

        public Cart GetCartByUserId(int userId)
        {
            return _dbSet.Include(c=>c.CartItems)
                .FirstOrDefault(c=>c.UserId==userId.ToString());
        }
        public void DeleteCartItem(Cart cart,CartItem cartItem)
        {
            if(cartItem != null)
            {
                cart.CartItems.Remove(cartItem);
                _context.Remove(cartItem);
                _context.SaveChanges();
            }
        }
        public CartItem GetCartItemById(int cartItemId)
        {
            // Assuming CartItems is a navigation property on the Cart entity
            return _context.CartItems
                           .FirstOrDefault(item => item.CartItemId == cartItemId);
        }
    }
}
