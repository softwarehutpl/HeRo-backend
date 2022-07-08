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

        public async Task<int> AddUser(UserDTO dto)
        {
            return 0;
        }

        public void UpdateUser(int id, UserDTO dto)
        {

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
                else if (sort.Key.ToLower() == "role")
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
    }
}
