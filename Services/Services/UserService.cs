using Common.Listing;
using Data.Entities;
using Data.Repositories;
using PagedList;
using Services.DTOs.User;

namespace Services.Services
{
    public class UserService
    {
        private UserRepository userRepository;

        public UserService(UserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        public UserDTO Get(int? userId)
        {
            User user = userRepository.GetUserById(userId);
            if (user == null)
            {
                return null;
            }

            UserDTO userFiltring = new UserDTO(user.Email, user.UserStatus, user.RoleName);

            return userFiltring;
        }

        public IEnumerable<UserFiltringDTO> GetUsers(Paging paging, SortOrder sortOrder, UserFiltringDTO userFiltringDTO)
        {
            IQueryable<User> users = userRepository.GetAllUsers();

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
                .Select(x => new UserFiltringDTO(x.Id, x.Email, x.UserStatus, x.RoleName))
                .ToPagedList(paging.PageNumber, paging.PageSize);

            return result;
        }

        public int Update(UserEditDTO userEdit)
        {
            User user = userRepository.GetUserById(userEdit.Id);
            if (user == null)
            {
                return 1;
            }

            user.Email = userEdit.Email;
            user.UserStatus = userEdit.UserStatus;
            user.RoleName = userEdit.RoleName;
            user.LastUpdatedDate = DateTime.Now;

            userRepository.Update(user);

            return 0;
        }
    }
}
