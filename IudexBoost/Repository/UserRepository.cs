using IudexBoost.Models.Classes;
using Microsoft.AspNetCore.Mvc;

namespace IudexBoost.Repository
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(Context context) : base(context)
        {
        }

        public User GetByUsername(string username)
        {
            return _dbSet.FirstOrDefault(u=>u.Username==username);
        }
        public User GetByEmail(string email)
        {
            return _dbSet.FirstOrDefault(u=>u.Email==email);
        }
    }
}
