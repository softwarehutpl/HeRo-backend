using Common.AttributeRoleVerification;
using Common.Helpers;
using HeRoBackEnd.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace HeRoBackEnd.Controllers
{
    [ApiController]
    public class EmailController : BaseController
    {
        private readonly EmailService _emailService;
        private string _errorMessage;
        private readonly UserService _userService;

        public EmailController(EmailService emailService, UserService userService)
        {
            _emailService = emailService;
            _userService = userService;
        }

        /// <summary>
        /// AddUserMailBox.
        /// </summary>
        /// <remarks>
        /// U need to allow less secure apps or two step verification on your mailbox account
        /// for Email functions to work;
        /// </remarks>
        /// <param name="mailBoxLogin">Email</param>
        /// <param name="mailBoxPassword">MailBox Password</param>
        /// <response code="400">"</response>
        /// <response code="200"></response>
        [HttpPost]
        [Route("Email/AddUserMailBox")]
        [RequireUserRole("Admin")]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status400BadRequest)]
        public IActionResult AddUserMailBox(string mailBoxLogin, string mailBoxPassword)
        {
            int userId = GetUserId();

            _userService.SetUserMailBox(userId, mailBoxLogin, mailBoxPassword, out _errorMessage);

            if (_errorMessage != String.Empty) return BadRequest(new ResponseViewModel(_errorMessage));

            return Ok(new ResponseViewModel(MessageHelper.AccountConfirmed));
        }

        /// <summary>
        /// Send Custom Email
        /// </summary>
        /// <param name="to">Reciver</param>
        /// <param name="subject">Mail subject</param>
        /// <param name="body">Mail Body</param>
        /// <returns>AddUserMailBox</returns>
        /// <response code="400">"</response>
        /// <response code="200"></response>
        [HttpPost]
        [Route("Email/SendCustomEmail")]
        [RequireUserRole("Admin")]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status400BadRequest)]
        public IActionResult SendCustomEmail(string to, string subject, string body)
        {
            int userId = GetUserId();

            _emailService.SendCustomEmail(userId, to, subject, body, out _errorMessage);

            if (_errorMessage != String.Empty) return BadRequest(new ResponseViewModel(_errorMessage));
            return Ok(new ResponseViewModel(MessageHelper.EmailSent));
        }

        //public IActionResult GetAllEmailsList()
        //{
        //    return Ok();
        //}

        //public IActionResult GetEmailDetails()
        //{
        //    return Ok();
        //}
    }
}