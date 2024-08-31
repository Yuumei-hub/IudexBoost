using Microsoft.AspNetCore.Mvc;
using IudexBoost.Models.Classes;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Iyzipay.Model;
using Iyzipay.Request;
using Options = Iyzipay.Options;
using CreatePaymentRequest = Iyzipay.Request.CreatePaymentRequest;
using System.Text.RegularExpressions;
using HttpClient = System.Net.Http.HttpClient;
using System.Globalization;
using IudexBoost.ProjectServices.Services;
using IudexBoost.ProjectServices.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace IudexBoost.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly Context _context;
        private readonly OrderService _orderService;

        public CheckoutController(Context context,OrderService orderService)
        {
            _context = context;
            _orderService = orderService;
        }

        private string _conversationId = Guid.NewGuid().ToString();
        Options options = new Options
        {
            ApiKey = "sandbox-Ikx8vXkZKX6YJDDlVWGDlMn4DYnyRNek",
            SecretKey = "sandbox-yhvcanALA1mdYI5mMAUkoizZJ4eSu5Sr",
            BaseUrl = "https://sandbox-api.iyzipay.com"
        };
        public IActionResult PaymentSuccess(Order order)
        {

            return View(order);
        }

        public decimal CalculatePrice()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = _context.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.UserId == userId);

            decimal price = 0;
            foreach (var item in cart.CartItems)
            {
                price += Convert.ToDecimal(item.Price) * item.Quantity;
            }
            return price;
        }
        //----------------------------

        //CF Initiate
        public IActionResult PaymentProcess()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = _context.Users.FirstOrDefault(u => u.UserId == Convert.ToInt32(userId));
            var cart = _context.Carts.Include(c => c.CartItems)
                .FirstOrDefault(c => c.UserId == userId);

            Buyer buyer = new Buyer
            {
                Id = user.UserId.ToString(),
                Name = user.Username,
                Surname = "surname",
                GsmNumber = "+905350000000",
                Email = user.Email,
                IdentityNumber = "12332112332",
                RegistrationAddress = "address",
                City = "cityname",
                Country = "countryname",
                ZipCode = "zipcode"
            };

            Address billingAddress = new Address
            {
                ContactName = "Jane Doe",
                City = "Istanbul",
                Country = "Turkey",
                Description = "description",
                ZipCode = "34742"
            };

            List<BasketItem> basketItems = new List<BasketItem>();

            foreach (var cartItem in cart.CartItems)
            {
                BasketItem basketItemx = new BasketItem
                {
                    Id = cartItem.CartItemId.ToString(),
                    Name = cartItem.GameName,
                    Category1 = "Eloboost",
                    Category2 = "Online Game Items",
                    ItemType = BasketItemType.VIRTUAL.ToString(),
                    Price = cartItem.Price.ToString(),
                };
                basketItems.Add(basketItemx);
            }
            CreatePaymentRequest request = new CreatePaymentRequest
            {
                Locale = Locale.EN.ToString(),
                ConversationId = _conversationId,
                Price = CalculatePrice().ToString(CultureInfo.InvariantCulture),
                PaidPrice = (CalculatePrice() + CalculatePrice() / 10).ToString(CultureInfo.InvariantCulture),
                Currency = Currency.USD.ToString(),
                Installment = 1,
                BasketId = cart.CartId.ToString(),
                PaymentChannel = PaymentChannel.WEB.ToString(),
                PaymentGroup = PaymentGroup.PRODUCT.ToString()
            };
            CreateCheckoutFormInitializeRequest cfrequest = new CreateCheckoutFormInitializeRequest()
            {
                Locale = Locale.EN.ToString(),
                ConversationId = _conversationId,
                Price = CalculatePrice().ToString(CultureInfo.InvariantCulture),
                PaidPrice = (CalculatePrice() + CalculatePrice() / 10).ToString(CultureInfo.InvariantCulture),
                Currency = Currency.USD.ToString(),
                EnabledInstallments = new List<int> { 2, 3, 6, 9 },
                BasketId = cart.CartId.ToString(),
                PaymentGroup = PaymentGroup.PRODUCT.ToString(),
                Buyer = buyer,
                BasketItems = basketItems,
                BillingAddress = billingAddress,
                CallbackUrl = $"https://localhost:44328/Checkout/PaymentCallback?email={user.Email}",
            };

            CheckoutFormInitialize checkoutFormInitialize = CheckoutFormInitialize.Create(cfrequest, options);
            return Redirect(checkoutFormInitialize.PaymentPageUrl);
        }
 

        [HttpPost]
        public async Task<IActionResult> PaymentCallback()
        {
            var email = HttpContext.Request.Query["email"].ToString();

            User user = _context.Users.FirstOrDefault(u => u.Email == email);
            using (var httpClient= new HttpClient())
            {
                var body = await new StreamReader(Request.Body).ReadToEndAsync();
                var token = Regex.Match(body, @"(?<=token=)[a-zA-Z0-9\-]+").Value;
                RetrievePayWithIyzicoRequest pwiRequest = new RetrievePayWithIyzicoRequest();
                pwiRequest.ConversationId = _conversationId;
                pwiRequest.Token = token;
                pwiRequest.Locale=Locale.EN.ToString();

                PayWithIyzico payWithIyzicoResult = PayWithIyzico.Retrieve(pwiRequest, options);

                if (payWithIyzicoResult.Status == "success")
                {
                    
                    Order order = new Order()
                    {
                        OrderId = Guid.NewGuid().ToString(),
                        UserId = user.UserId,
                        User = user,
                        Price = decimal.Parse(payWithIyzicoResult.Price, CultureInfo.InvariantCulture),
                        PaymentMethod = payWithIyzicoResult.CardAssociation + " " + payWithIyzicoResult.CardType,
                        Status=payWithIyzicoResult.Status,
                        OrderDate = DateTime.Now,
                        Description=""
                    };
                    _orderService.CreateOrder(order);
                    _context.SaveChangesAsync();


                    return RedirectToAction("Index","Order");
                }
                else
                    return Redirect("PaymentError");
            }

        }
    }
}
