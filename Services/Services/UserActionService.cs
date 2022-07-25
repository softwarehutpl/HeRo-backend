using Common.ServiceRegistrationAttributes;
using Data.Entities;
using Data.Repositories;

namespace Services.Services
{
    [ScopedRegistration]
    public class UserActionService
    {
        private readonly UserActionRepository _userActionRepository;

        public UserActionService(UserActionRepository userActionRepository)
        {
            _userActionRepository = userActionRepository;
        }

        public void CreateUserAction(UserAction userAction)
        {
            _userActionRepository.AddAndSaveChanges(userAction);
        }
    }
}