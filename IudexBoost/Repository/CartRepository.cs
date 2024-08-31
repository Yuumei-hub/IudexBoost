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

    }
}
