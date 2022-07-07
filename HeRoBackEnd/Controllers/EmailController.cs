using Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using Services.Services;


namespace HeRoBackEnd.Controllers
{
    [ApiController]
    public class EmailController : Controller
    {

        private IEmailHelper _emailHelper;

        public EmailController(IEmailHelper emailHelper)
        {
            _emailHelper = emailHelper;
        }

        /// <summary>
        /// Sends an email with the activation link to the newly registered user
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("Email/SendConfirmation")]
        public IActionResult SendConfirmation()
        {            
            try
            {
                _emailHelper.SendEmail();
                return Ok("Mail Wysłany");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
