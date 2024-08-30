using IudexBoost.Models.Classes;
using IudexBoost.ProjectServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace IudexBoost.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            var orders = _orderService.GetAllOrders();
            return View(orders);
        }

        public IActionResult PaymentSuccessful()
        {
            return View();
        }
        

        //GET
        public IActionResult EditOrder(string orderId)
        {
            var order = _orderService.GetOrderById(orderId);
            if(order==null)
                return NotFound();

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditOrder(string orderId, Order order)
        {
            if (orderId != order.OrderId)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _orderService.UpdateOrderStatus(orderId, order.Status);
                }
                catch (Exception)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        public IActionResult Delete(int orderId)
        {
            try
            {
                _orderService.DeleteOrder(orderId);
                return RedirectToAction(nameof(Index));
            }
            catch(ArgumentException)
            {
                return NotFound();
            }
        }

    }
}
