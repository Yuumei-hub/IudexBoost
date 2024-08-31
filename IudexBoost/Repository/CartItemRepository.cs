using IudexBoost.Models.Classes;

namespace IudexBoost.Repository
{
    public class CartItemRepository: GenericRepository<CartItem>
    {
        public CartItemRepository(Context context):base(context)
        {
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

    }
}
