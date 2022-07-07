using System.Linq;
using Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Data.Repositories
{
    public class UserRepository
    {
        private DataContext _dataContext;

        public UserRepository(DataContext context)
        {
            _dataContext = context;
        }

        public User GetUserById(string id) 
        {
            var result = _dataContext.Users.Find(id);
            return result; 
        }

        public User GetUserByEmail(string mail)
        {
            var result = _dataContext.Users.Find(mail);
            return result;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var result = _dataContext.Users.ToList();

            return result;
        }

        public void AddUser(int id) { }

        public void RemoveUser(int id) { }

        public string GetUserEmail(int id)
        {
            var result = _dataContext.Users.FirstOrDefault(x => x.Id == id).Email;
            return result;
        }

        public string GetUserPassword(string email)
        {
            var result = _dataContext.Users.Where(x => x.Email == email).Select(x => x.Password);
            return result.ToString();
        }

        public string GetUserRoleByEmail(string email)
        {
            var result1 = from user in _dataContext.Users
                          join role in _dataContext.Roles
                          on user.Id equals role.Id
                          select new Role { Name = role.Name };

            return result1.ToString();
        }
    }
}
