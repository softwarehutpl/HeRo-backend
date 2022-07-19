using System.Security.Claims;
using System.Security.Cryptography;
using Common.Helpers;
using System.Text;
using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Common.Enums;
using System;
using Common.ServiceRegistrationAttributes;
using Services.IServices;

namespace Services.Services
{
    [ScopedRegistrationWithInterface]
    public class AuthService : IAuthService
    {
        private UserRepository _userRepository;

        public AuthService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private bool ValidateUser(string password, string email)
        {
            string passwordInDb = _userRepository.GetUserPassword(email);
            string passwordAfterHash = PasswordHashHelper.GetHash(password);

            if (passwordAfterHash == passwordInDb) return true;

            return false;
        }

        public async Task<ClaimsIdentity> ValidateAndCreateClaim(string password, string email)
        {
            if (ValidateUser(password, email))
            {
                User user = _userRepository.GetUserByEmail(email);

                var claims = new List<Claim>()
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Email", email),
                    new Claim("RoleName", user.RoleName),
                    new Claim("UserStatus", user.UserStatus)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                return claimsIdentity;
            }

            return null;
        }

        public async Task<bool> CheckPasswordRecoveryGuid(Guid guid, string email)
        {
            Guid userGuid = _userRepository.GetUserGuidByEmail(email);
            if (guid == userGuid) return true;
            return false;
        }

        public bool ConfirmUser(Guid guid, string email)
        {
            try
            {
                User user = _userRepository.GetUserByEmail(email);
                if (user.ConfirmationGuid == guid)
                {
                    user.UserStatus = UserStatuses.ACTIVE.ToString();

                    _userRepository.UpdateAndSaveChanges(user);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}