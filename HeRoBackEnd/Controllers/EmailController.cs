using Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using Services.Services;


namespace HeRoBackEnd.Controllers
{
    public class EmailController : Controller
    {

        private EmailService _emailService;

        public EmailController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult SendConfirmation(string id)
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
