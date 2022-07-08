using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Services.DTOs;

namespace Services.Services
{
    public class UserService
    {
        private UserRepository _userRepository;

        public UserService(UserRepository userRepository) { _userRepository = userRepository; }
        public string GetEmailOfUser(string id)
        {
            User user = _userRepository.GetUserById(id);

            return user.Email;
        }

        public async Task<int> AddUser(UserDTO dto)

        {
            return 0;
        }
        public void UpdateUser(int id, UserDTO dto)
        {

        }
        public async Task<List<IdentityUser>> GetUsers()
        {
            return null;
        }
    }
}
