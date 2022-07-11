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
        private readonly EmailService _emailService;
        private readonly UserService _userService;

        public AuthController(AuthService authServices, EmailService emailService, UserService userService)
        {
            _authServices = authServices;
            _emailService = emailService;
            _userService = userService;
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

            return Ok("User created");
        }

        [HttpPost]
        [Route("Auth/PasswordRecovery")]
        public async Task<IActionResult> PasswordRecoveryMail(string email)
        {          
            bool changedPassword = _authServices.CheckUserExist(email);
            if (!changedPassword) return BadRequest("Account doesn't exist");

            var recoveryGuid = Guid.NewGuid();
            _userService.SetUserRecoveryGuid(email,recoveryGuid);
            
            var fullUrl = this.Url.Action("RecoverPassword", "Auth", new { guid = recoveryGuid }, protocol: "https");

            _emailService.SendPasswordRecoveryEmail(email, fullUrl);
            return Ok("Recovery E-Mail send"); 
        }
        [HttpPost]
        [Route("Auth/RecoverPassword")]
        public async Task<IActionResult> RecoverPassword(string email, Guid guid)
        {
            bool userGuid = await _authServices.CheckPasswordRecoveryGuid(guid, email);
            if (!userGuid) return BadRequest();

            _emailService.SendRecoveredPassword(email);
            return Ok();
        }

    }
}
