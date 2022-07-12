using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HeRoBackEnd.Controllers
{
    public class BaseController : Controller
    {

        protected int GetUserId()
        {
            List<Claim> claims = HttpContext.User.Claims.ToList();
            Claim idClaim = claims.FirstOrDefault(e => e.Type == "Id");
            int id = int.Parse(idClaim.Value);

            return id;
        }
    }
}