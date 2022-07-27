using Common.AttributeRoleVerification;
using Common.Enums;
using Common.Helpers;
using Data.DTOs.Email;
using HeRoBackEnd.ViewModels;
using HeRoBackEnd.ViewModels.Email;
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

        //<summary>
        //Adds SMTP account to database
        //</summary>
        //<returns>Adds SMTP account to database</returns>
        //<response code="400">"</response>
        //<response code="200"></response>
        [HttpPost]
        [Route("Email/AddSmtpAccount")]
        [RequireUserRole("Admin")]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status400BadRequest)]
        public IActionResult AddSmtpAccount(SmtpAccountViewModel accountViewModel)
        {
            SmtpAccountDTO dto = new()
            {
                Login = accountViewModel.Login,
                Password = accountViewModel.Password,
                Port = accountViewModel.Port,
                Host = accountViewModel.Host,
                Sender = accountViewModel.Sender,
                Name = accountViewModel.Name
            };

            bool check = _emailService.AddSmtpAccountToDb(dto, out string _errorMessage);

            if (check)
                return Ok(new ResponseViewModel("nie zrobione ale się powiodło"));

            return BadRequest(new ResponseViewModel(_errorMessage));
        }

        //<summary>
        //Adds IMAP account to database
        //</summary>
        //<returns>Adds IMAP account to database</returns>
        //<response code="400">"</response>
        //<response code="200"></response>
        [HttpPost]
        [Route("Email/AddImapAccount")]
        [RequireUserRole("Admin")]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status400BadRequest)]
        public IActionResult AddImapAccount(ImapAccountViewModel accountViewModel)
        {
            ImapAccountDTO dto = new()
            {
                Host = accountViewModel.Host,
                Login = accountViewModel.Login,
                Password = accountViewModel.Password,
                Port = accountViewModel.Port
            };

            bool check = _emailService.AddImapAccountToDb(dto, out string _errorMessage);

            if (check)
                return Ok(new ResponseViewModel("nie zrobione"));

            return BadRequest(new ResponseViewModel(_errorMessage));
        }

        //<summary>
        //Displays SMTP account names
        //</summary>
        //<returns>Displays SMTP account names</returns>
        //<response code="400">"</response>
        //<response code="200"></response>
        [HttpPost]
        [Route("Email/GetAllPossibleSmtpAccountNames")]
        [RequireUserRole("Admin")]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
        public IActionResult GetAllPossibleSmtpAccountNames()
        {
            var listOfStatus = Enum.GetValues(typeof(SmptAccountNames)).Cast<SmptAccountNames>().Select(v => v.ToString());

            return Ok(listOfStatus);
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