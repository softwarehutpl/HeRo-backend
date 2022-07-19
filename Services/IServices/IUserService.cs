using Common.Listing;
using Common.ServiceRegistrationAttributes;
using Data.DTOs.User;
using Services.Listing;

namespace Services.IServices
{
    [ScopedRegistrationWithInterface]
    public interface IUserService
    {
        Task<bool> ChangeUserPassword(string email, string password);
        bool CheckIfUserExist(string email);
        Task<Guid> CreateUser(string password, string email);
        int Delete(int userId, int loginUserId);
        UserDTO Get(int userId);
        Guid GetUserGuid(string email);
        UserListing GetUsers(Paging paging, SortOrder? sortOrder, UserFiltringDTO userFiltringDTO);
        Guid SetUserRecoveryGuid(string email);
        int Update(UserEditDTO userEdit);
    }
}