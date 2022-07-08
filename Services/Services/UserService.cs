using Common.Listing;
using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using PagedList;
using Services.DTOs;
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

        public IEnumerable<User> GetUsers(Paging paging, SortOrder sortOrder, UserFiltringDTO userFiltringDTO)
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
            //if (!String.IsNullOrEmpty(userFiltringDTO.RoleName))
            //{
            //    users = users.Where(s => s.Role.Name.Equals(userFiltringDTO.RoleName));
            //}

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
                //else if (sort.Key == "UserStatus")
                //{
                //    if (sort.Value == "DESC")
                //    {
                //        users = users.OrderByDescending(u => u.UserStatus);
                //    }
                //    else
                //    {
                //        users = users.OrderBy(s => s.UserStatus);
                //    }
                //}
                //else if (sort.Key == "Role")
                //{
                //    if (sort.Value == "DESC")
                //    {
                //        users = users.OrderByDescending(u => u.Role.Name);
                //    }
                //    else
                //    {
                //        users = users.OrderBy(s => s.Role.Name);
                //    }
                //}
            }

            //var result = users
            //    .Select(x => new UserDTO()
            //    { 
            //    Name = x.Name
            //    })
            //    .ToPagedList(paging.PageNumber, paging.PageSize);
            var result = users.ToPagedList(paging.PageNumber, paging.PageSize);
            return result;
        }
    }
}
