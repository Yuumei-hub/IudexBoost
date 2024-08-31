using IudexBoost.Business.Services;
using IudexBoost.Models.Classes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IudexBoost.Controllers
{
    public class GameController : Controller
    {
        private readonly GameService _gameService;
        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }
        public IActionResult Index()
        {
            List<Game> games = _gameService.GetAllGames();

            return View(games);
        }

        public IActionResult GameRedirector(int gameId)
        {
            Game game = _gameService.GetById(gameId);
            // Serialize the model into JSON format
            var serializedModel = JsonConvert.SerializeObject(game);

            // Pass the serialized JSON data to the view
            ViewBag.SerializedModel = serializedModel;
            return View("Overwatch",game);
        }
    }
}
