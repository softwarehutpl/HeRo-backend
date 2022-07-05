using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Data.Repositories;
using Services.DTOs;

namespace Services.Services
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;

        public UserService(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        public int AddUser(UserDTO dto)
        {
            return 0;
        }

        public void UpdateUser(int id, UserDTO dto)
        {

        }

        public IEnumerable<User> GetUsers()
        {
            return userRepository.GetAllUsers();
        }
    }
}
