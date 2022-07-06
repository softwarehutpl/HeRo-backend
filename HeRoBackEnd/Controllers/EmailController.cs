using Common.Helpers;
using Microsoft.AspNetCore.Mvc;


namespace HeRoBackEnd.Controllers
{
    public class EmailController : Controller
    {

        private IEmailHelper _emailHelper;

        public EmailController(IEmailHelper emailHelper)
        {
            _emailHelper = emailHelper;
        }

        [HttpPost]
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
