using Common.Enums;
using Common.Helpers;
using Common.Listing;
using Common.ServiceRegistrationAttributes;
using Data.DTOs.User;
using Data.Entities;
using Data.IRepositories;
using Microsoft.Extensions.Logging;
using PagedList;
using Services.Listing;

namespace Services.Services
{
    [ScopedRegistration]
    public class UserService
    {
        private IUserRepository _userRepository;
        private ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public Guid SetUserRecoveryGuid(string email)
        {
            var user = _userRepository.GetUserByEmail(email);
            user.PasswordRecoveryGuid = Guid.NewGuid();

            try
            {
                _userRepository.UpdateAndSaveChanges(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return user.PasswordRecoveryGuid;
        }

        public UserDTO Get(int userId)
        {
            User? user = _userRepository.GetById(userId);
            if (user == null)
            {
                return null;
            }

            UserDTO userDTO = new UserDTO(user.Id, user.Email, user.UserStatus, user.RoleName);

            return userDTO;
        }

        public UserListing GetUsers(Paging paging, SortOrder? sortOrder, UserFiltringDTO userFiltringDTO)
        {
            IQueryable<User> users = _userRepository.GetAllUsers();
            users = users.Where(u => !u.DeletedDate.HasValue);

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

            if (sortOrder != null && sortOrder.Sort != null)
            {
                users = Sorter<User>.Sort(users, sortOrder.Sort);
            }
            else
            {
                sortOrder = new SortOrder();
                sortOrder.Sort = new List<KeyValuePair<string, string>>();
                sortOrder.Sort.Add(new KeyValuePair<string, string>("Id", ""));

                users = Sorter<User>.Sort(users, sortOrder.Sort);
            }

            UserListing userListing = new UserListing();
            userListing.TotalCount = users.Count();
            userListing.UserFiltringDTO = userFiltringDTO;
            userListing.Paging = paging;
            userListing.SortOrder = sortOrder;
            userListing.UserDTOs = users
                .Select(x => new UserDTO(x.Id, x.Email, x.UserStatus, x.RoleName))
                .ToPagedList(paging.PageNumber, paging.PageSize);

            return userListing;
        }

        public int Update(UserEditDTO userEdit, out string error)
        {
            User user = _userRepository.GetById(userEdit.Id);
            if (user == null)
            {
                error = ErrorMessageHelper.NoUser;
                return -1;
            }

            user.UserStatus = userEdit.UserStatus;
            user.RoleName = userEdit.RoleName;
            user.LastUpdatedDate = DateTime.UtcNow;

            try
            {
                _userRepository.UpdateAndSaveChanges(user);
            }
            catch (Exception ex)
            {
                error = ErrorMessageHelper.ErrorUpdatingUser;
                _logger.LogError(ex.Message);
                return -1;
            }
            error = "";
            return user.Id;
        }

        public int Delete(int userId, int loginUserId, out string error)
        {
            User user = _userRepository.GetById(userId);
            if (user == null)
            {
                error = ErrorMessageHelper.NoUser;
                return -1;
            }
            user.UserStatus = UserStatuses.DELETED.ToString();
            user.DeletedById = loginUserId;
            user.DeletedDate = DateTime.UtcNow;
            try
            {
                _userRepository.UpdateAndSaveChanges(user);
            }
            catch (Exception ex)
            {
                error = ErrorMessageHelper.ErrorDeletingUser;
                _logger.LogError("Error updating user while deleting");
                return -1;
            }
            error = "";
            return user.Id;
        }

        public bool CheckIfUserExist(string email)
        {
            bool check = _userRepository.CheckIfUserExist(email);

            return check;
        }

        public async Task<Guid> CreateUser(string password, string email)
        {
            User newUser = new()
            {
                Email = email,
                Password = PasswordHashHelper.GetHash(password),
                CreatedDate = DateTime.Now,
                LastUpdatedDate = DateTime.Now,
                RoleName = RoleNames.ANONYMOUS.ToString(),
                UserStatus = UserStatuses.NOT_VERIFIED.ToString(),
                ConfirmationGuid = Guid.NewGuid()
            };

            try
            {
                _userRepository.AddAndSaveChanges(newUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return newUser.ConfirmationGuid;
        }

        public async Task<bool> ChangeUserPassword(string email, string password)
        {
            User myUser = _userRepository.GetUserByEmail(email);
            string passwordAfterHash = PasswordHashHelper.GetHash(password);
            if (myUser.Password == passwordAfterHash) return false;

            _userRepository.ChangeUserPasswordByEmail(email, passwordAfterHash);
            return true;
        }
    }
}