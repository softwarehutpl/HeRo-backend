using Common.AttributeRoleVerification;
using Common.Helpers;
using HeRoBackEnd.ViewModels;
using HeRoBackEnd.ViewModels.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System.Security.Claims;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace HeRoBackEnd.Controllers
{
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly AuthService _authServices;
        private readonly EmailService _emailService;
        private readonly UserService _userService;
        private readonly UserActionService _userActionService;

        public AuthController(AuthService authServices, EmailService emailService, UserService userService, UserActionService userActionService)
        {
            _authServices = authServices;
            _emailService = emailService;
            _userService = userService;
            _userActionService = userActionService;
        }

        /// <summary>
        /// Sign in user method
        /// </summary>
        /// <param name="user">E-mail of the user</param>
        /// <returns>Signs in user</returns>
        /// <response code="400">string "Cannot log in. Check your credentials."</response>
        /// <response code="200">Returns user email</response>
        [HttpPost]
        [Route("Auth/SignIn")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignIn(SignInViewModel user)
        {
            LogUserAction($"AuthController.SignIn({JsonSerializer.Serialize(user)})", _userService, _userActionService);
            ClaimsIdentity? claimsIdentity = await _authServices.ValidateAndCreateClaim(user.Password, user.Email);

            if (claimsIdentity != null)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return Ok(new ResponseViewModel(user.Email));
            }
            return BadRequest(new ResponseViewModel(ErrorMessageHelper.WrongCredentials));
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
        [RequireUserRole("ADMIN")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNewUser(NewUserViewModel newUser)
        {
            LogUserAction($"AuthController.CreateNewUser({JsonSerializer.Serialize(newUser)})", _userService, _userActionService);
            bool check = _userService.CheckIfUserExist(newUser.Email);

            if (check)
                return BadRequest(new ResponseViewModel(ErrorMessageHelper.UserExists));

            if (!Regex.IsMatch(newUser.Name, @"^[a-zA-Z]+$") || !Regex.IsMatch(newUser.Surname, @"^[a-zA-Z]+$"))
            {
                return BadRequest(new ResponseViewModel(ErrorMessageHelper.ForbiddenSymbol));
            }

            Guid confirmationGuid = await _userService.CreateUser(newUser.Name, newUser.Surname, newUser.Password, newUser.Email);

            string url = this.Url.Action("ConfirmRegistration", "Auth", new { guid = confirmationGuid }, protocol: "https");
            _emailService.SendConfirmationEmail(newUser.Email, url);

            return Ok(new ResponseViewModel(MessageHelper.UserCreated));
        }

        /// <summary>
        /// If user exist, sends recovery to mail parameter adress.
        /// </summary>
        /// <returns></returns>
        /// <param name="confirmationGuid" example="9fb49f98-f169-4316-9737-23b656058c5c"></param>
        /// <response code="200">Account confirmed</response>
        /// <response code="400">Confirmation Failed</response>
        [HttpPost]
        [Route("Auth/ConfirmAccount")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> ConfirmAccount(ConfirmUserViewModel user)
        {
            bool check = _authServices.ConfirmUser(user.ConfirmationGuid, user.Email);

            if (check)
            {
                await _userService.ChangeUserPassword(user.Email, user.Password);
                return Ok(new ResponseViewModel(MessageHelper.AccountConfirmed));
            }

            return BadRequest(new ResponseViewModel(ErrorMessageHelper.ConfirmationFailed));
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
            if (!changedPassword)
            {
                return BadRequest(ErrorMessageHelper.AccountDoesntExist(email));
            }
            var recoveryGuid = _userService.SetUserRecoveryGuid(email);

            var fullUrl = this.Url.Action("RecoverPassword", "Auth", new { guid = recoveryGuid }, protocol: "https");

            _emailService.SendPasswordRecoveryEmail(email, fullUrl);

            return Ok(new ResponseViewModel(MessageHelper.RecoveryEmailSent));
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
            //LogUserAction($"AuthController.RecoverPassword({JsonSerializer.Serialize(user)})", _userService, _userActionService);
            bool userGuid = await _authServices.CheckPasswordRecoveryGuid(user.Guid, user.Email);
            if (!userGuid)
            {
                return BadRequest(ErrorMessageHelper.UserAndGuidDifferentOwner);
            }

            await _userService.ChangeUserPassword(user.Email, user.Password);

            return Ok(new ResponseViewModel(MessageHelper.PasswordChanged));
        }
    }
}