using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Common.Enums;


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

            if (password == passwordInDb) return true;

            return false;
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
                Password = password,
                CreatedDate = DateTime.Now,
                LastUpdatedDate = DateTime.Now,
                RoleName = RoleNameEnum.Anonymous.ToString(),
                UserStatus = UserStatusEnum.Not_verified.ToString()
            };

            return newUser;
        }

        public async Task<bool> ValidateAndCreateUserAccount(string password, string email)
        {
            bool check = _userRepository.CheckIfUserExist(email);

            if (!check)
            {
                var newUser = await CreateUser(password, email);
                _userRepository.AddUser();
                return true;
            }
            return false;
        }


    }
}
