using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Common.Enums;
using System;

namespace Services.Services
{
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
        public string GenerateNewPassword(string email)
        {                      
            var password = Guid.NewGuid().ToString("N").Substring(0, 8);
            var passwordAfterHash = GetHash(password);
            _userRepository.ChangeUserPassword(email, passwordAfterHash);
            return password;
        }
        public async Task<ClaimsIdentity> ValidateAndCreateClaim(string password, string email)
        {
            if (ValidateUser(password, email))
            {
                string role = _userRepository.GetUserRoleByEmail(email);

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, role)

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
                RoleName = RoleNameEnum.Anonymous.ToString(),
                UserStatus = UserStatusEnum.Not_verified.ToString()
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
    }
}
