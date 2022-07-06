using Data.Entities;

namespace Data.Repositories
{
    public class UserRepository
    {
        private DataContext dataContext;

        public UserRepository(DataContext _dataContext)
        {
            dataContext = _dataContext;
        }

        public void GetUserById(int id) { }

        public IEnumerable<User> GetAllUsers()
        {
            return dataContext.Users;
        }

        public void AddUser(int id) { }

        public void RemoveUser(int id) { }
    }
}
