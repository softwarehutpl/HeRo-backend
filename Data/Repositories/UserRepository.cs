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
<<<<<<< HEAD
=======

>>>>>>> 68a34c719ccdfc66b5fddd9cd28b6a7058bd9de6
            return result; 
        }

        public User GetUserByEmail(string mail)
        {
            var result = _dataContext.Users.Find(mail);
<<<<<<< HEAD
            return result;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var result = _dataContext.Users.ToList();
=======

            return result;
        }

        public IQueryable<User> GetAllUsers()
        {
            var result = _dataContext.Users;
>>>>>>> 68a34c719ccdfc66b5fddd9cd28b6a7058bd9de6

            return result;
        }

        public void AddUser(int id) { }

        public void RemoveUser(int id) { }

        public string GetUserEmail(int id)
        {
<<<<<<< HEAD
            var result = _dataContext.Users.Where(x => x.Id == id).Select(x => x.Email).FirstOrDefault();
            return result;
        }

        public string GetUserPassword(string email)
        {
            var result = _dataContext.Users.Where(x => x.Email == email).Select(x => x.Password).FirstOrDefault();
            return result;
        }

        public string GetUserRoleByEmail(string email)
        {
            var result = _dataContext.Users.Where(x => x.Email == email).Select(x => x.Role).FirstOrDefault();
=======

            var result = _dataContext.Users.Where(x => x.Id == id).Select(x => x.Email).FirstOrDefault();
>>>>>>> 68a34c719ccdfc66b5fddd9cd28b6a7058bd9de6

            return result;
        }
    }
}
