using IudexBoost.Business.Interfaces;
using IudexBoost.Models.Classes;
using IudexBoost.Repository;
using Microsoft.AspNetCore.Mvc;

namespace IudexBoost.ProjectServices.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public bool RegisterUser(User user)
        {
            if (UserExists(user.Username))
                return false;

            _userRepository.Add(user);
            return true;
        }

        public bool UserExists(string username)
        {
            return _userRepository.GetByUsername(username) != null;
        }

        public User AuthenticateUser(string email, string password)
        {
            var user= _userRepository.GetByEmail(email);
            if (user == null || user.Password != password)
                return null;
            else
                return user;
        }
    }
}
