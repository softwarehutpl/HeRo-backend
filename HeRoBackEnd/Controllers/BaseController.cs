using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System.Security.Claims;

namespace HeRoBackEnd.Controllers
{
    public class BaseController : Controller
    {
        protected void LogUserAction(string controller, UserActionService userActionService)
        {
            int userId = GetUserId();
            UserAction userAction = new()
            {
                UserId = userId,
                ControllerAction = controller,
                Date = DateTime.Now
            };

            userActionService.CreateUserAction(userAction);
        }

        protected int GetUserId()
        {
            List<Claim> claims = HttpContext.User.Claims.ToList();
            Claim? idClaim = claims.FirstOrDefault(e => e.Type == "Id");

            int id;
            if (idClaim == null)
            {
                id = 0;
            }
            else
            {
                int.TryParse(idClaim.Value, out id);
            }
            
            return id;
        }
    }
}