using Data.Entities;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Services.DTOs;
using Data.Entities;

namespace Services.Services
{
    public class UserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) { _userRepository = userRepository; }
        public string GetEmailOfUser(int id)
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
