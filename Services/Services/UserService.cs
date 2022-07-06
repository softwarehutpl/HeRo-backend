using Data.Entities;
using Data.Repositories;
using Services.DTOs;
using Services.DTOs.User;
using PagedList;

namespace Services.Services
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;

        public UserService(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        public int AddUser(UserDTO dto)
        {
            return 0;
        }

        public void UpdateUser(int id, UserDTO dto)
        {

        }

        public IEnumerable<User> GetUsers(UserPagingDTO userPagingDTO, UserSortOrderDTO userSortOrderDTO, UserFiltringDTO userFiltringDTO)
        {
            IEnumerable<User> users = userRepository.GetAllUsers();

            if (!String.IsNullOrEmpty(userFiltringDTO.Email))
            {
                users = users.Where(s => s.Email.Contains(userFiltringDTO.Email));
            }
            if (!String.IsNullOrEmpty(userFiltringDTO.UserStatus))
            {
                users = users.Where(s => s.UserStatus.Contains(userFiltringDTO.UserStatus));
            }
            if (!String.IsNullOrEmpty(userFiltringDTO.RoleName))
            {
                users = users.Where(s => s.Role.RoleName.Contains(userFiltringDTO.RoleName));
            }

            foreach (KeyValuePair<string, string> sort in userSortOrderDTO.Sort)
            {
                if (sort.Key == "Email")
                {
                    if (sort.Value == "DESC")
                    {
                        users = users.OrderByDescending(u => u.Email);
                    }
                    else
                    {
                        users = users.OrderBy(s => s.Email);
                    }
                }
                else if (sort.Key == "UserStatus")
                {
                    if (sort.Value == "DESC")
                    {
                        users = users.OrderByDescending(u => u.UserStatus);
                    }
                    else
                    {
                        users = users.OrderBy(s => s.UserStatus);
                    }
                }
                else if (sort.Key == "Role")
                {
                    if (sort.Value == "DESC")
                    {
                        users = users.OrderByDescending(u => u.Role.RoleName);
                    }
                    else
                    {
                        users = users.OrderBy(s => s.Role.RoleName);
                    }
                }
            }

            return users.ToPagedList(userPagingDTO.PageNumber, userPagingDTO.PageSize);
        }
    }
}
