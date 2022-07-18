using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Services.Services;
using HeRoBackEnd.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Common.Enums;
using Data.Entities;

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
        /// <param name="email" example ="test@gmail.com">E-mail of the user</param>
        /// <param name="password" example = "password">User password</param>
        /// <returns>Signs in user</returns>
        /// <response code="400">string "Cannot log in. Check your credentials."</response>
        /// <response code="200">Returns user email</response>
        [HttpPost]
        [Route("Auth/SignIn")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignIn(string password, string email)
        {
            ClaimsIdentity? claimsIdentity = await _authServices.ValidateAndCreateClaim(password, email);

            if (claimsIdentity != null)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return Ok(email);
            }
            return BadRequest("Wrong Credentials!");
        }

        /// <summary>
        /// Sign out user method
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns user Logs out</response>
        [HttpGet]
        [Route("Auth/LogOut")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("Cookies");
            return Ok();
        }

        /// <summary>
        /// User registration method
        /// </summary>
        /// <returns></returns>
        /// <param name="newUser">Object containing information about a new user</param>
        /// <response code="200">User created successfully</response>
        /// <response code="400">Invalid email or password or user already exist</response>
        [HttpPost]
        [Route("Auth/CreateNewUser")]
        [Authorize(Policy = "AdminRequirment")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNewUser(NewUserViewModel newUser)
        {
            bool check = _userService.CheckIfUserExist(newUser.Email);

            if (check)
                return BadRequest("User already exist");

            Guid confirmationGuid = await _userService.CreateUser(newUser.Password, newUser.Email);

            string url = this.Url.Action("ConfirmRegistration", "Auth", new { guid = confirmationGuid }, protocol: "https");
            _emailService.SendConfirmationEmail(newUser.Email, url);

            return Ok("User created successfully");
        }

        /// <summary>
        /// If user exist, sends recovery to mail parameter adress.
        /// </summary>
        /// <returns></returns>
        /// <param name="confirmationGuid" example="9fb49f98-f169-4316-9737-23b656058c5c"></param>
        /// <response code="200">Account confirmed</response>
        [HttpGet]
        [Route("Auth/ConfirmAccount")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> ConfirmAccount(Guid confirmationGuid)
        {
            int userId = GetUserId();
            bool check = _authServices.ConfirmUser(confirmationGuid, userId);
            return Ok("Account confirmed");
        }

        /// <summary>
        /// If user exist, sends recovery to mail parameter adress.
        /// </summary>
        /// <returns></returns>
        /// <param name="email" example="test@gmail.com"></param>
        /// <response code="200">Recovery e-mail sent</response>
        /// <response code="400">Account doesn't exist</response>
        [HttpPost]
        [Route("Auth/PasswordRecoveryMail")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PasswordRecoveryMail(string email)
        {
            bool changedPassword = _userService.CheckIfUserExist(email);
            if (!changedPassword) return BadRequest($"Account:{email} doesn't exist");

            var recoveryGuid = _userService.SetUserRecoveryGuid(email);

            var fullUrl = this.Url.Action("RecoverPassword", "Auth", new { guid = recoveryGuid }, protocol: "https");

            _emailService.SendPasswordRecoveryEmail(email, fullUrl);
            return Ok("Recovery e-mail sent");
        }

        /// <summary>
        /// If Guid and user email are assign to same entity, change user password
        /// </summary>
        /// <returns></returns>
        /// <param name="user">object of the UserPasswordRecoveryViewModel class</param>
        /// <response code="200">Password changed</response>
        /// <response code="400">Email and Guid values are assigned to different users, try again</response>
        [HttpPost]
        [Route("Auth/RecoverPassword")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RecoverPassword(UserPasswordRecoveryViewModel user)
        {
            bool userGuid = await _authServices.CheckPasswordRecoveryGuid(user.Guid, user.Email);
            if (!userGuid) return BadRequest("User and Guid don't have same owner");

            await _userService.ChangeUserPassword(user.Email, user.Password);
            return Ok("Password Changed");
        }
    }
}