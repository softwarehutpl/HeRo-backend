using Data.Entities;

namespace Data.Repositories
{
    public interface IUserRepository
    {
        void AddUser(int id);
        void GetAllUsers();
        User GetUserById(int id);
        void RemoveUser(int id);
    }
}