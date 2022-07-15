using Common.Enums;
using Common.Listing;
using Common.ServiceRegistrationAttributes;
using Data.Entities;
using Data.Repositories;
using Microsoft.Extensions.Logging;
using PagedList;
using Services.DTOs.User;

namespace Services.Services
{
    [ScopedRegistration]
    public class UserService
    {
        private UserRepository _userRepository;
        private ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger, UserRepository userRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public Guid GetUserGuid(string email)
        {
            Guid result;
            try
            {
                result = _userRepository.GetUserGuidByEmail(email);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return default;
            }
            return result;
        }

        public void SetUserRecoveryGuid(string email, Guid guid)
        {
            var user = _userRepository.GetUserByEmail(email);
            user.PasswordRecoveryGuid = guid;
            try
            {
                _userRepository.UpdateUser(user);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            
        }

        public void SetUserConfirmationGuid(string email, Guid guid)
        {
            var user = _userRepository.GetUserByEmail(email);
            user.ConfirmationGuid = guid;
            _userRepository.UpdateUser(user);
        }

        public UserDTO Get(int userId)
        {
            User user = _userRepository.GetUserById(userId);
            if (user == null)
            {
                return null;
            }

            UserDTO userDTO = new UserDTO(user.Id, user.Email, user.UserStatus, user.RoleName);

            return userDTO;
        }

        public IEnumerable<UserDTO> GetUsers(Paging paging, SortOrder sortOrder, UserFiltringDTO userFiltringDTO)
        {
            IQueryable<User> users = _userRepository.GetAllUsers();

            if (!String.IsNullOrEmpty(userFiltringDTO.Email))
            {
                users = users.Where(s => s.Email.Contains(userFiltringDTO.Email));
            }
            if (!String.IsNullOrEmpty(userFiltringDTO.UserStatus))
            {
                users = users.Where(s => s.UserStatus.Equals(userFiltringDTO.UserStatus));
            }
            if (!String.IsNullOrEmpty(userFiltringDTO.RoleName))
            {
                users = users.Where(s => s.RoleName.Equals(userFiltringDTO.RoleName));
            }

            foreach (KeyValuePair<string, string> sort in sortOrder.Sort)
            {
                if (sort.Key.ToLower() == "email")
                {
                    if (sort.Value.ToUpper() == "DESC")
                    {
                        users = users.OrderByDescending(u => u.Email);
                    }
                    else
                    {
                        users = users.OrderBy(s => s.Email);
                    }
                }
                else if (sort.Key.ToLower() == "userstatus")
                {
                    if (sort.Value.ToUpper() == "DESC")
                    {
                        users = users.OrderByDescending(u => u.UserStatus);
                    }
                    else
                    {
                        users = users.OrderBy(s => s.UserStatus);
                    }
                }
                else if (sort.Key.ToLower() == "rolename")
                {
                    if (sort.Value.ToUpper() == "DESC")
                    {
                        users = users.OrderByDescending(u => u.RoleName);
                    }
                    else
                    {
                        users = users.OrderBy(s => s.RoleName);
                    }
                }
            }

            var result = users
                .Select(x => new UserDTO(x.Id, x.Email, x.UserStatus, x.RoleName))
                .ToPagedList(paging.PageNumber, paging.PageSize);

            return result;
        }

        public int Update(UserEditDTO userEdit)
        {
            User user = _userRepository.GetUserById(userEdit.Id);
            if (user == null)
            {
                return 0;
            }

            user.UserStatus = userEdit.UserStatus;
            user.RoleName = userEdit.RoleName;
            user.LastUpdatedDate = DateTime.UtcNow;

            _userRepository.UpdateAndSaveChanges(user);

            return user.Id;
        }

        public int Delete(int userId, int loginUserId)
        {
            User user = _userRepository.GetUserById(userId);
            if (user == null)
            {
                return 0;
            }
            user.UserStatus = UserStatuses.DELETED.ToString();
            user.DeletedById = loginUserId;
            user.DeletedDate = DateTime.UtcNow;
            try 
            {
                _userRepository.UpdateAndSaveChanges(user);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error updating user while deleting");
                return -1;
            }
            

            return user.Id;
        }
    }
}