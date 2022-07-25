using Common.ServiceRegistrationAttributes;
using Data.Entities;

namespace Data.IRepositories
{
    [ScopedRegistrationWithInterface]
    public interface IUserActionRepository : IBaseRepository<UserAction>
    {
        bool AddUserAction(UserAction userAction);

    }
}