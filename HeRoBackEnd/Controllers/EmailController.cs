using Common.AttributeRoleVerification;
using Common.Helpers;
using Data.DTOs.Email;
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

            // _emailService.SendCustomEmail(userId, to, subject, body, out _errorMessage);

            if (_errorMessage != String.Empty)
                return BadRequest(new ResponseViewModel(_errorMessage));

            return Ok(new ResponseViewModel(MessageHelper.EmailSent));
        }

        ///// <summary>
        ///// Get all email folder names
        ///// </summary>
        ///// <returns>AddUserMailBox</returns>
        ///// <response code="400">"</response>
        ///// <response code="200"></response>
        //[HttpPost]
        //[Route("Email/GetAllEmailFolders")]
        //[RequireUserRole("Admin")]
        //[ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status400BadRequest)]
        //public IActionResult GetAllEmailFolders()
        //{
        //    int userId = GetUserId();
        //    //var folders = _emailService.GetAllFolderNamesList(userId);

        //    if (folders == null)
        //        return BadRequest(new ResponseViewModel(ErrorMessageHelper.FoldersListIsEmpty));

        //    return Ok(folders);
        //}

        ///// <summary>
        ///// Get all emails in folder
        ///// </summary>
        ///// <returns>GetEmailsInFolderList</returns>
        ///// <response code="400">"</response>
        ///// <response code="200"></response>
        //[HttpPost]
        //[Route("Email/GetEmailsInFolderList")]
        //[RequireUserRole("Admin")]
        //[ProducesResponseType(typeof(List<EmailListDataDTO>), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status400BadRequest)]
        //public IActionResult GetEmailsInFolderList(string folderName)
        //{
        //    int userId = GetUserId();

        //    List<EmailListDataDTO>? emails = _emailService.GetAllEmailsInFolder(userId, folderName);

        //    if (emails == null)
        //        return BadRequest(new ResponseViewModel(ErrorMessageHelper.FolderIsEmpty));

        //    return Ok(emails);
        //}

        ///// <summary>
        ///// ReadEmail
        ///// </summary>
        ///// <returns>ReadEmail</returns>
        ///// <response code="400">"</response>
        ///// <response code="200"></response>
        //[HttpPost]
        //[Route("Email/ReadEmail")]
        //[RequireUserRole("Admin")]
        //[ProducesResponseType(typeof(EmailDetailsDTO), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status400BadRequest)]
        //public IActionResult ReadEmail(string messageId, string folderName)
        //{
        //    int userId = GetUserId();

        //    EmailDetailsDTO? email = _emailService.ReadEmailDetails(userId, messageId, folderName);

        //    if (email == null)
        //        return BadRequest(new ResponseViewModel(ErrorMessageHelper.MailNotFound));

        //    return Ok(email);
        //}
    }
}