using IudexBoost.Models.Classes;

namespace IudexBoost.Repository
{
    public class CartItemRepository: GenericRepository<CartItem>
    {
        public CartItemRepository(Context context):base(context)
        {
        }

    }
}
