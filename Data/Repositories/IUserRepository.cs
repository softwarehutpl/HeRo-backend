using Data.Entities;

namespace Data.Repositories
{
    public interface IUserRepository
    {
        void AddUser(int id);
        User GetUserById(int id);
        IEnumerable<User> GetAllUsers();
        void RemoveUser(int id);
    }
}