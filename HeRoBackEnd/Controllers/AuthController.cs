using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Services.Services;
using HeRoBackEnd.ViewModels.User;

namespace HeRoBackEnd.Controllers
{
    [ApiController]
    public class AuthController : Controller
    {
        private readonly AuthService _authServices;

        public AuthController(AuthService authServices)
        {
            _authServices = authServices;
        }


        [HttpGet]
        [Route("Auth/SignIn")]
        public async Task<IActionResult> SignIn(string password, string email)
        {

            ClaimsIdentity? claimsIdentity = await _authServices.ValidateAndCreateClaim(password, email);

            if (claimsIdentity != null)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return Ok();
            }
            return BadRequest("Wrong Credentials");
        }
        [HttpGet]
        [Route("Auth/LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("Cookies");
            return Ok();
        }  
        [HttpPost]
        [Route("Auth/CreateNewUser")]
        public async Task<IActionResult> CreateNewUser(NewUserViewModel newUser)
        {
            bool created = await _authServices.ValidateAndCreateUserAccount(newUser.Password, newUser.Email);
            
            if(!created) return BadRequest("Invalid Email or Password or User already exist");

            return Ok();
        }
    }
}
