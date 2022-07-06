using Common.Listing;
using Data.Entities;
using Services.DTOs;
using Services.DTOs.User;

namespace Services.Services
{
    public interface IUserService
    {
        public int AddUser(UserDTO dto);

        public void UpdateUser(int id, UserDTO dto);

        public IEnumerable<User> GetUsers(Paging paging, SortOrder sortOrder, UserFiltringDTO userFiltringDTO);
    }
}
