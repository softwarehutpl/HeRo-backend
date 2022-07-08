using Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using Services.Services;


namespace HeRoBackEnd.Controllers
{
    [ApiController]
    public class EmailController : Controller
    {

        private EmailService _emailService;

        public EmailController(EmailService emailService)
        {
            _emailService = emailService;
        }

        /// <summary>
        /// Sends an email with the activation link to the newly registered user
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpPost]
<<<<<<< HEAD
=======
        [Route("Email/SendConfirmation")]
>>>>>>> 68a34c719ccdfc66b5fddd9cd28b6a7058bd9de6
        public IActionResult SendConfirmation(int id)
        {            
            try
            {
                _emailService.SendConfirmationEmail(id);
                return Ok("Mail Wysłany");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
