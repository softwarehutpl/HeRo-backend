using Common.ServiceRegistrationAttributes;
using System.Security.Claims;

namespace Services.IServices
{
    [ScopedRegistrationWithInterface]
    public interface IAuthService
    {
        Task<bool> CheckPasswordRecoveryGuid(Guid guid, string email);
        bool ConfirmUser(Guid guid, string email);
        Task<ClaimsIdentity> ValidateAndCreateClaim(string password, string email);
    }
}