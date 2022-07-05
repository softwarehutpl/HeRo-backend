using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.DTOs;
using Data.Entities;

namespace Services.Services
{
    public class UserService
    {
        public async Task<int> AddUser(UserDTO dto)
        {
            return 0;
        }
        public void UpdateUser(int id, UserDTO dto)
        {

        }
        public async Task<List<User>> GetUsers()
        {
            return null;
        }
    }
}
