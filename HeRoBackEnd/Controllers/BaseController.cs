﻿using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Security.Claims;

namespace HeRoBackEnd.Controllers
{
    public class BaseController : Controller
    {
        private readonly ResourceManager _resourceManager;
        private readonly List<CultureInfo> _supportedLanguages;
        protected CultureInfo culture;

        public BaseController()
        {
            _resourceManager = new ResourceManager("HeRoBackEnd.LanguageResources.LangResource", Assembly.GetExecutingAssembly());
            _supportedLanguages = new List<CultureInfo>()
            {
                new CultureInfo("de-DE"),
                new CultureInfo("es-ES"),
                new CultureInfo("fr-FR"),
                new CultureInfo("pl-PL"),
                new CultureInfo("zh-CN")
            };
        }

        protected void LogUserAction(string controller, string controllerAction, string actionParameters, UserActionService userActionService)
        {
            int userId = GetUserId();
            UserAction userAction = new()
            {
                UserId = userId,
                Controller = controller,
                ControllerAction = controllerAction,
                ActionParameters = actionParameters,
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
        protected string Translate(string message)
        {

            if (_supportedLanguages.Contains(culture)==false)
            {
                return message;
            }

            string result = _resourceManager.GetString(message, culture);

            return result;
        }
    }
}