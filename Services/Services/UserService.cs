using Common.Listing;
using Data.Entities;
using Data.Repositories;
using PagedList;
using Services.DTOs.User;

namespace Services.Services
{
    public class UserService
    {
        private UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        { 
            _userRepository = userRepository; 
        }

        public Guid GetUserGuid(string email)
        {
            var result = _userRepository.GetUserGuidByEmail(email);
            return result;
        }
        public void SetUserRecoveryGuid(string email, Guid guid)
        {
            var user = _userRepository.GetUserByEmail(email);
            user.PasswordRecoveryGuid = guid;
            _userRepository.UpdateUser(user);
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

            UserDTO userDTO = new UserDTO(user.Email, user.UserStatus, user.RoleName);

            return userDTO;
        }

        public IEnumerable<UserFiltringDTO> GetUsers(Paging paging, SortOrder sortOrder, UserFiltringDTO userFiltringDTO)
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
                .Select(x => new UserFiltringDTO(x.Email, x.UserStatus, x.RoleName))
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

            user.DeletedById = loginUserId;
            user.DeletedDate = DateTime.UtcNow;

            _userRepository.UpdateAndSaveChanges(user);

            return user.Id;
        }
    }
}
