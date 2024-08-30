using IudexBoost.Models.Classes;

namespace IudexBoost.Repository
{
    public class OrderRepository: GenericRepository<Order>
    {
        public OrderRepository(Context context) : base(context)
        {
        }
        public override Order GetById(int id)
        {
            throw new InvalidOperationException("Use the string id method for this repository");
        }
        public Order GetById(string id)
        {
           return _dbSet.Find(id);
        }
        /*
         * Override if Needed: Override the GetById method in repositories where the ID type is different from the default (e.g., string for OrderRepository).

           Use Default Implementation: For repositories where the ID type is int, use the default implementation provided in GenericRepository.
         */
    }
}
