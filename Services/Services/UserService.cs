using Services.DTOs;
using Data.Entities;
using Data;

namespace Services.Services
{
    internal class UserService
    {
        private DataContext _dataContext;

        public UserService(DataContext context)
        {
            _dataContext = context;
        }
        
        public string GetEmail(int id)
        {
            User user = new();
            user = _dataContext.Users.SingleOrDefault(c => c.Id == id);

            string email = user.Email;
            return email;
        }

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
