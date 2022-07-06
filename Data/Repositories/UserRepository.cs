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
            User user = new();
            user = _dataContext.Users.SingleOrDefault(c => c.Id == id);

            if(user != null) return user;

            return null;
        }

        public void GetAllUsers() { }

        public void AddUser(int id) { }

        public void RemoveUser(int id) { }

    }
}
