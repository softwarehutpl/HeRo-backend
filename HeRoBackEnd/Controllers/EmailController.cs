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

        public EmailController(EmailService emailService)
        {
            _emailService = emailService;
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

            bool check = _emailService.AddSmtpAccountToDb(dto, out _errorMessage);

            if (check)
                return Ok(new ResponseViewModel(MessageHelper.SMTPAccountAdded));

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

            bool check = _emailService.AddImapAccountToDb(dto, out _errorMessage);

            if (check)
                return Ok(new ResponseViewModel(MessageHelper.IMAPAccountAdded));

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

        //<summary>
        //Get one mail form list
        //</summary>
        //<returns>Get one mail form list</returns>
        //<response code="400">"</response>
        //<response code="200"></response>
        [HttpPost]
        [Route("Email/GetSingleMail")]
        [RequireUserRole("Admin")]
        [ProducesResponseType(typeof(EmailDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status400BadRequest)]
        public IActionResult GetSingleMail(string messageId)
        {
            EmailDetailsDTO? dto = _emailService.GetEmailDetails(messageId);

            if (dto == null)
                return BadRequest(new ResponseViewModel(ErrorMessageHelper.MailNotFound));

            return Ok(dto);
        }

        //<summary>
        //Return email list
        //</summary>
        //<returns>Get mail list</returns>
        //<response code="400">"</response>
        //<response code="200"></response>
        [HttpPost]
        [Route("Email/GetAllMails")]
        [RequireUserRole("Admin")]
        [ProducesResponseType(typeof(List<EmailListDataDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status400BadRequest)]
        public IActionResult GetAllMails()
        {
            List<EmailListDataDTO>? dtoList = _emailService.GetAllEmailsList();

            if (dtoList == null)
                return BadRequest(new ResponseViewModel(ErrorMessageHelper.EmailDataBaseEmpty));

            return Ok(dtoList);
        }
    }
}