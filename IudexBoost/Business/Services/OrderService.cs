using IudexBoost.Models.Classes;
using IudexBoost.ProjectServices.Interfaces;

namespace IudexBoost.ProjectServices.Services
{
    public class OrderService : IOrderService
    {
        private readonly Context _context;
        public OrderService(Context context)
        {
            _context = context;
        }

        public List<Order> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        public Order GetOrderById(string orderId)
        {
            return _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
        }

        public Order UpdateOrder(string orderId, Order order)
        {
            if (orderId != order.OrderId)
                throw new ArgumentException("Order ID does not match the ID of the order to update.");

            _context.Orders.Update(order);
            _context.SaveChanges();

            return order;
        }

        public void DeleteOrder(string orderId)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
                throw new ArgumentException("Order not found.");

            _context.Orders.Remove(order);
            _context.SaveChanges();
        }
        public void CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void UpdateOrderStatus(int orderId, string newStatus)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                order.Status = newStatus;
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Order not found.");
            }
        }
    }
}
