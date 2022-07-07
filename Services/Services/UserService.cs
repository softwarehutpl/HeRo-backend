using Common.Listing;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Services.DTOs;
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
        public async Task<List<IdentityUser>> GetUsers(Paging paging, SortOrder sortOrder, UserFiltringDTO userFiltringDTO)
        {
            return null;
        }
    }
}
