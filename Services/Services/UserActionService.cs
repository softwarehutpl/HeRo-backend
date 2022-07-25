using AutoMapper;
using Common.Enums;
using Common.Helpers;
using Common.Listing;
using Common.ServiceRegistrationAttributes;
using Data.DTOs.Candidate;
using Data.Entities;
using Data.IRepositories;
using Microsoft.Extensions.Logging;
using PagedList;
using Services.Listing;

namespace Services.Services
{
    [ScopedRegistration]
    public class UserActionService
    {
        private readonly IUserActionRepository _userActionRepository;
        private readonly IMapper _mapper;

        public UserActionService(IMapper map, IUserActionRepository userActionRepository)
        {
            _mapper = map;
            _userActionRepository = userActionRepository;
        }

        public bool CreateUserAction(UserAction userAction)
        {
            _userActionRepository.AddUserAction(userAction);
            
            return true;
        }
    }
}