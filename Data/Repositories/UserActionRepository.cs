using Common.ServiceRegistrationAttributes;
using Data.Entities;
using Data.IRepositories;

namespace Data.Repositories
{
    [ScopedRegistrationWithInterface]
    public class UserActionRepository : BaseRepository<UserAction>, IUserActionRepository
    {
        private DataContext _dataContext;

        public UserActionRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }

        public bool AddUserAction(UserAction userAction)
        {
            AddAndSaveChanges(userAction);
            return true;
        }
    }
}