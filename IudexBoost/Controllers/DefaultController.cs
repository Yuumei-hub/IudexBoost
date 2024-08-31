using IudexBoost.Business.Services;
using IudexBoost.Models.Classes;
using Microsoft.AspNetCore.Mvc;

namespace IudexBoost.Controllers
{
    public class DefaultController : Controller
    {
        private readonly GameService _gameService;
        private readonly TestimonialService _testimonialService;
        public DefaultController(Context context, GameService gameService,TestimonialService testimonialService)
        {
            _context = context;
            _gameService = gameService;
            _testimonialService = testimonialService;
        }
        public IActionResult Index()
        {
            var games = _gameService.GetAllGames();
            var testimonials = _testimonialService.GetAllTestimonials();
            var viewModel = new Tuple<List<Game>, List<Testimonial>>(games, testimonials);

            return View(viewModel);
        }
        public IActionResult ToLogin()
        {
            return RedirectToAction("Index","Login");
        }

    }
}
