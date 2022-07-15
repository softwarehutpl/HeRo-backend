using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Common.Enums;
using System;
using Common.ServiceRegistrationAttributes;

namespace Services.Services
{
    [ScopedRegistration]
    public class AuthService
    {
        private UserRepository _userRepository;

        public AuthService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private static string GetHash(string input)
        {
            SHA256 hashAlgorithm = SHA256.Create();
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            var sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        private bool ValidateUser(string password, string email)
        {
            string passwordInDb = _userRepository.GetUserPassword(email);
            string passwordAfterHash = GetHash(password);

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

        public async Task<User> CreateUser(string password, string email)
        {
            User newUser = new()
            {
                Email = email,
                Password = GetHash(password),
                CreatedDate = DateTime.Now,
                LastUpdatedDate = DateTime.Now,
                RoleName = RoleNames.ANONYMOUS.ToString(),
                UserStatus = UserStatuses.NOT_VERIFIED.ToString()
            };

            return newUser;
        }

        public bool CheckUserExist(string email)
        {
            bool check = _userRepository.CheckIfUserExist(email);
            if (!check) return false;
            return true;
        }

        public async Task<bool> ValidateAndCreateUserAccount(string password, string email)
        {
            bool check = CheckUserExist(email);

            if (!check)
            {
                var newUser = await CreateUser(password, email);
                _userRepository.AddUser(newUser);
                return true;
            }
            return false;
        }

        public async Task<bool> ChangeUserPassword(string email, string password)
        {
            User myUser = _userRepository.GetUserByEmail(email);
            string passwordAfterHash = GetHash(password);
            if (myUser.Password == passwordAfterHash) return false;

            _userRepository.ChangeUserPasswordByEmail(email, passwordAfterHash);
            return true;
        }

        public async Task<bool> CheckPasswordRecoveryGuid(Guid guid, string email)
        {
            Guid userGuid = _userRepository.GetUserGuidByEmail(email);
            if (guid == userGuid) return true;
            return false;
        }

        public bool ConfirmUser(Guid guid, int id)
        {
            User user = _userRepository.GetUserById(id);
            if (user.ConfirmationGuid == guid)
            {
                user.UserStatus = UserStatuses.ACTIVE.ToString();

                _userRepository.UpdateUser(user);
                return true;
            }
            return false;
        }
    }
}