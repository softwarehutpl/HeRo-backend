using Data.Entities;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DataContext _dataContext;

        public UserRepository(DataContext context)
        {
            _dataContext = context;
        }

        public User GetUserById(int id) 
        {
            return _dataContext.Users.SingleOrDefault(c => c.Id == id);
        }

        public void GetAllUsers() { }

        public void AddUser(int id) { }

        public void RemoveUser(int id) { }

    }
}
