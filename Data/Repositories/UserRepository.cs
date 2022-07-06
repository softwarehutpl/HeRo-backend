using Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
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
            return _dataContext.AspNetUsers.ToList();
        }

        public void AddUser(int id) { }

        public void RemoveUser(int id) { }
    }
}
