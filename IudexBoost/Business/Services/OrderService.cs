using IudexBoost.Models.Classes;
using IudexBoost.ProjectServices.Interfaces;
using IudexBoost.Repository;
using Microsoft.EntityFrameworkCore;

namespace IudexBoost.ProjectServices.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        public OrderService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAll().ToList();
        }

        public Order GetOrderById(string orderId)
        {
            return _orderRepository.GetById(orderId);
        }

        public Order UpdateOrder(string orderId, Order order)
        {
            if (orderId != order.OrderId)
                throw new ArgumentException("Order ID does not match the ID of the order to update.");
            _orderRepository.Update(order);
            return order;
        }

        public void DeleteOrder(int orderId)
        {
            Order order = _orderRepository.GetById(orderId);
            if (order == null)
            {
                throw new ArgumentException("Order not found.");
            }

            _orderRepository.Delete(order);
        }
        public void CreateOrder(Order order)
        {
            _orderRepository.Add(order);
        }

        public void UpdateOrderStatus(string orderId, string newStatus)
        {
            var order = _orderRepository.GetById(orderId);
            if (order != null)
            {
                order.Status = newStatus;
            }
            else
            {
                throw new ArgumentException("Order not found.");
            }
        }

    }
}
