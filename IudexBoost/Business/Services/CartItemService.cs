using IudexBoost.Models.Classes;
using IudexBoost.Repository;

namespace IudexBoost.Business.Services
{
    public class CartItemService
    {
       private readonly CartItemRepository _cartItemRepository;
        private readonly GameService _gameService;
        public CartItemService(CartItemRepository cartItemRepository, GameService gameService)
        {
            _cartItemRepository = cartItemRepository;
            _gameService = gameService;
        }

        public void AddCartItemToDB(CartItem cartItem)
        {
            _cartItemRepository.Add(cartItem);
        }

        public CartItem GetCartItemById(int cartItemId)
        {
            return _cartItemRepository.GetById(cartItemId);
        }
        public void RemoveCartItem(Cart cart, int cartItemId)
        {
            CartItem cartItem = _cartItemRepository.GetById(cartItemId);
            _cartItemRepository.DeleteCartItem(cart, cartItem);
        }
        public CartItem CreateCartItem(int quantity, decimal price, string fromSkillRating, string toSkillRating, int gameId)
        {
            //just creates a cart item object with the params.
            Game game = _gameService.GetById(gameId);
            if (game == null)
                return null;

            CartItem cartItem = new CartItem
            {
                GameName = game.Title,
                GameImgUrl = game.ImageUrl,
                Quantity = quantity,
                Price = price,
                FromSkillRating = fromSkillRating,
                ToSkillRating = toSkillRating
            };
            return cartItem;
        }

    }
}
