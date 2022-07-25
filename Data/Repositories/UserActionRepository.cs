using Common.ServiceRegistrationAttributes;
using Data.Entities;

namespace Data.Repositories
{
    [ScopedRegistration]
    public class UserActionRepository : BaseRepository<UserAction>
    {
        public UserActionRepository(DataContext context) : base(context)
        {
        }
    }
}