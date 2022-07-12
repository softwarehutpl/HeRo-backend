using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Services.Services;
using HeRoBackEnd.ViewModels.User;

namespace HeRoBackEnd.Controllers
{
    [ApiController]
    public class AuthController : BaseController
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

        /// <summary>
        /// Sign in user method
        /// </summary>
        /// <param name="email">E-mail of the user</param>
        /// <param name="password">User password</param>
        /// <returns>Signs in user</returns>
        /// <response code="200">User signs in</response>
        /// <response code="400">Bad credentials</response>
        [HttpGet]
        [Route("Auth/SignIn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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


        /// <summary>
        /// Sign out user method
        /// </summary>
        /// <response code="200">User Logs out</response>
        [HttpGet]
        [Route("Auth/LogOut")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("Cookies");
            return Ok("User logOut");
        }

        /// <summary>
        /// User registration method
        /// </summary>
        /// <param name="newUser">Object containing information about a new user</param>
        /// <response code="200">User created</response>
        /// <response code="400">Invalid Email or Password or User already exist</response>
        [HttpPost]
        [Route("Auth/CreateNewUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNewUser(NewUserViewModel newUser)
        {
            bool created = await _authServices.ValidateAndCreateUserAccount(newUser.Password, newUser.Email);
            
            if(!created) return BadRequest("Invalid Email or Password or User already exist");

            Guid confirmationGuid = Guid.NewGuid();
            _userService.SetUserConfirmationGuid(newUser.Email, confirmationGuid);
            string url = this.Url.Action("ConfirmRegistration", "Auth", new { guid = confirmationGuid }, protocol: "https");
            _emailService.SendConfirmationEmail(newUser.Email, url);

            return Ok("User created");
        }
        [HttpGet]
        [Route("Auth/ConfirmAccount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConfirmAccount(Guid confirmationGuid)
        {
            int userId = GetUserId();
            bool check = _authServices.ConfirmUser(confirmationGuid, userId);
            return Ok();
        }


        /// <summary>
        /// If user exist, sends recovery to mail parameter adress.
        /// </summary>
        /// <param name="email"></param>
        /// <response code="200">Recovery E-Mail send</response>
        /// <response code="400">Account doesn't exist</response>
        [HttpPost]
        [Route("Auth/PasswordRecoveryMail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// If Guid and user email are assign to same entity, change user password
        /// </summary>
        /// <param name="user">object of the UserPasswordRecoveryViewModel class</param>
        /// <response code="200">Password changed</response>
        /// <response code="400">Email and Guid values are assign to different users, try again</response>
        [HttpPost]
        [Route("Auth/RecoverPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RecoverPassword(UserPasswordRecoveryViewModel user)
        {
            bool userGuid = await _authServices.CheckPasswordRecoveryGuid(user.Guid, user.Email);
            if (!userGuid) return BadRequest();

            await _authServices.ChangeUserPassword(user.Email, user.Password);
            return Ok();
        }
    }
}
