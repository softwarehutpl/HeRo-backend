using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Services.DTOs;

namespace Services.Services
{
    public class UserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) { _userRepository = userRepository; }
        public string GetEmailOfUser(string id)
        {
            IdentityUser user = _userRepository.GetUserById(id);

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
