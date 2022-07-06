using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.DTOs;

namespace Services.Services
{
    public class UserService
    {
        private UserRepository userRepository;

        public UserService(UserRepository _userRepository)
        {
            return 0;
        }
        public void UpdateUser(int id, UserDTO dto)
        {

        }
        public async Task<List<User>> GetUsers()
        {
            IEnumerable<User> users = userRepository.GetAllUsers();

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
                users = users.Where(s => s.Role.RoleName.Equals(userFiltringDTO.RoleName));
            }

            foreach (KeyValuePair<string, string> sort in sortOrder.Sort)
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

            var result = users.ToPagedList(paging.PageNumber, paging.PageSize);

            return result;
        }
    }
}
