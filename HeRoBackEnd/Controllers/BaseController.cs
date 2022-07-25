using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System.Security.Claims;

namespace HeRoBackEnd.Controllers
{
    public class BaseController : Controller
    {
        private readonly IMapper _mapper;

        protected void LogUserAction(string controller, UserService userService, UserActionService userActionService)
        {
            int userId = GetUserId();
            UserAction userAction = new()
            {
                UserId = userId,
                //User = _mapper.Map<User>(userService.Get(userId)),
                ControllerAction = controller,
                Date = DateTime.Now
            };
            userActionService.CreateUserAction(userAction);
        }

        protected int GetUserId()
        {
            List<Claim> claims = HttpContext.User.Claims.ToList();
            Claim idClaim = claims.FirstOrDefault(e => e.Type == "Id");
            int.TryParse(idClaim.Value, out int id);

            return id;
        }
    }
}