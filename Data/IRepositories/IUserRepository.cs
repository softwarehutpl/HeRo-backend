using Common.ServiceRegistrationAttributes;
using Data.Entities;

namespace Data.IRepositories
{
    [ScopedRegistrationWithInterface]
    public interface IUserRepository : IBaseRepository<User>
    {
        void ChangeUserPasswordByEmail(string email, string password);

        bool CheckIfUserExist(string email);

        IQueryable<User> GetAllUsers();

        User? GetUserByEmail(string mail);

        string GetUserEmail(int id);

        Guid? GetUserGuidByEmail(string email);

        string? GetUserPassword(string email);

        string GetUserRoleByEmail(string email);
    }
}