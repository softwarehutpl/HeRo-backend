using Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Data.Repositories
{
    public interface IUserRepository
    {
        void AddUser(int id);
        IdentityUser GetUserById(string id);
        IdentityUser GetUserByEmail(string mail);
        IEnumerable<IdentityUser> GetAllUsers();
        void RemoveUser(int id);
    }
}