using IudexBoost.Business.Interfaces;
using IudexBoost.Models.Classes;
using IudexBoost.Repository;

namespace IudexBoost.Business.Services
{
    public class GameService: IGenericService<Game>
    {
        private readonly GameRepository _gameRepository;
        public GameService(GameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }
        public void Add(Game game)
        {
            _gameRepository.Add(game);
        }

        public void Delete(Game game)
        {
            throw new NotImplementedException();
        }

        public Game GetById(int gameId)
        {
            return _gameRepository.GetById(gameId);
        }

        public void Update(Game entity)
        {
            throw new NotImplementedException();
        }

        public List<Game> GetAllGames()
        {
            return _gameRepository.GetAll().ToList();
        }
    }
}
