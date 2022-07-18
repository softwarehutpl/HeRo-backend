using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HeRoBackEnd.Controllers
{
    [EnableCors("corspolicy")]
    public class BaseController : Controller
    {
        protected int GetUserId()
        {
            List<Claim> claims = HttpContext.User.Claims.ToList();
            Claim idClaim = claims.FirstOrDefault(e => e.Type == "Id");
            int.TryParse(idClaim.Value, out int id);

            return id;
        }
    }
}