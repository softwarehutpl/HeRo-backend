using Data.Entities;
using Services.DTOs;

namespace Services.Services
{
    public interface IUserService
    {
        public int AddUser(UserDTO dto);

        public void UpdateUser(int id, UserDTO dto);

        public IEnumerable<User> GetUsers();
    }
}
