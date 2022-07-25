using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System.Globalization;
using System.Resources;
using System.Security.Claims;

namespace HeRoBackEnd.Controllers
{
    public class BaseController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ResourceManager _resourceManager;
        private readonly List<CultureInfo> _supportedLanguages;
        protected CultureInfo culture;

        public BaseController()
        {
            culture = new CultureInfo("en-US");
            _resourceManager = new ResourceManager("Common.LanguageResources.LangResource", typeof(BaseController).Assembly);
            _supportedLanguages = new List<CultureInfo>()
            {
                new CultureInfo("de-DE"),
                new CultureInfo("es-ES"),
                new CultureInfo("fr-FR"),
                new CultureInfo("pl-PL"),
                new CultureInfo("zh-CN")
            };
        }

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
        protected string Translate(string message)
        {
            if(_supportedLanguages.Contains(culture)==false)
            {
                return message;
            }

            string result = _resourceManager.GetString(message, culture);

            return result;
        }
    }
}