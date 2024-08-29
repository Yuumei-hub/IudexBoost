using IudexBoost.Interface;
using IudexBoost.Models.Classes;
using Microsoft.AspNetCore.Mvc;

namespace IudexBoost.Repository
{
    public class UserRepository : GenericRepository<User>, IUser
    {
        private readonly Context _context;

        public UserRepository(Context context) : base(context)
        {
        }
    }
}
