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

        public IQueryable<User> GetAllUsers()
        {
            var result = _dataContext.Users;

            return result;
        }

        public void AddUser(int id) { }

        public void RemoveUser(int id) { }

        public string GetUserEmail(int id)
        {

            var result = _dataContext.Users.Where(x => x.Id == id).Select(x => x.Email).FirstOrDefault();

            return result;
        }
    }
}
