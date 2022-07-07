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

        public IdentityUser GetUserById(string id) 
        {
            var result = _dataContext.AspNetUsers.Find(id);
            return result; 
        }

        public IdentityUser GetUserByEmail(string mail)
        {
            var result = _dataContext.AspNetUsers.Find(mail);
            return result;
        }

        public IEnumerable<IdentityUser> GetAllUsers()
        {
            var result = _dataContext.AspNetUsers;

            return result;
        }

        public void AddUser(int id) { }

        public void RemoveUser(int id) { }

        public IQueryable<string> GetUserEmail(string id)
        {
            var result = _dataContext.AspNetUsers.Where(x => x.Id == id).Select(x => x.Email);
            return result;
        }
    }
}
