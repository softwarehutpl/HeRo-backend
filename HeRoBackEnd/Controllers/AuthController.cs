using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;

namespace HeRoBackEnd.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserRepository _userRepository;


        public AuthController(UserRepository userRepo)
        {
            _userRepository = userRepo;
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
            string passwordInDb = _userRepository.GetUserPassword(email).ToString();
            string passwordAfterHash = GetHash(password);

            if (passwordAfterHash == passwordAfterHash) return true;

            return false;
        }
        [HttpGet]
        public async Task<IActionResult> LogIn(string password, string email)
        {
            if (ValidateUser(password, email))
            {
                string role = _userRepository.GetUserRoleByEmail(email);

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, role)
                    
                };
                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
                await HttpContext.SignInAsync("CookieAuthentication", new
                ClaimsPrincipal(claimsIdentity));

                return Ok();
            }
            return BadRequest();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuthentication");
            return Ok();
        }
    }
}
