using IudexBoost.Models.Classes;

namespace IudexBoost.ProjectServices.Interfaces
{
    public interface IOrderService
    {
        void CreateOrder(Order order);
        void DeleteOrder(string orderId);
        List<Order> GetAllOrders();
        Order GetOrderById(string orderId);
        Order UpdateOrder(string orderId, Order order);
        void UpdateOrderStatus(int orderId, string newStatus);
    }
}
