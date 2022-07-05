namespace Data.Repositories
{
    public interface IUserRepository
    {
        void AddUser(int id);
        void GetAllUsers();
        void GetUserById(int id);
        void RemoveUser(int id);
    }
}