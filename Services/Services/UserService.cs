using Common.Listing;
using Data.Entities;
using Data.Repositories;
<<<<<<< HEAD
using Microsoft.AspNetCore.Identity;
using Services.DTOs;
=======
using PagedList;
>>>>>>> 68a34c719ccdfc66b5fddd9cd28b6a7058bd9de6
using Services.DTOs.User;

namespace Services.Services
{
    public class UserService
    {
        private UserRepository _userRepository;

        public UserService(UserRepository userRepository) { _userRepository = userRepository; }


        public async Task<int> AddUser(UserDTO dto)

        {
            return 0;
        }
        public void UpdateUser(int id, UserDTO dto)
        {

        }
<<<<<<< HEAD
        public async Task<List<IdentityUser>> GetUsers(Paging paging, SortOrder sortOrder, UserFiltringDTO userFiltringDTO)
        {
            return null;
=======

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
>>>>>>> 68a34c719ccdfc66b5fddd9cd28b6a7058bd9de6
        }
    }
}
