using Data.Entities;

namespace Data.Repositories
{
    public interface IUserRepository
    {
        void AddUser(int id);

        IEnumerable<User> GetAllUsers();

        void GetUserById(int id);

        void RemoveUser(int id);
    }
}