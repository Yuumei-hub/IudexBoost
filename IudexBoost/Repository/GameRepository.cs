using IudexBoost.Models.Classes;

namespace IudexBoost.Repository
{
    public class GameRepository : GenericRepository<Game>
    {
        public GameRepository(Context context) : base(context)
        {
        }
    }
}
