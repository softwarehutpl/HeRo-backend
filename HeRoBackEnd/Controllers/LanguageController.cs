using Common.Helpers;
using Data.DTOs.Language;
using HeRoBackEnd.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace HeRoBackEnd.Controllers
{
    [ApiController]
    public class LanguageController : BaseController
    {
        private List<LanguageDTO> GetLanguages()
        {
            List<LanguageDTO> result = new List<LanguageDTO>()
            {
                new LanguageDTO("English", "en-GB"),
                new LanguageDTO("German", "de-DE"),
                new LanguageDTO("Spanish", "es-ES"),
                new LanguageDTO("French", "fr-FR"),
                new LanguageDTO("Polish", "pl-PL"),
                new LanguageDTO("Chinese", "zh-CN")
            };

            return result;
        }
        /// <summary>
        /// Returns a list of supported languages
        /// </summary>
        /// <remarks>
        /// Sample response:
        /// 
        ///     [
        ///         {
        ///             "name": "English",
        ///             "language": "en-GB"
        ///         },
        ///         {
        ///             "name": "German",
        ///             "language": "de-DE"
        ///         },
        ///         {
        ///             "name": "Spanish",
        ///             "language": "es-ES"
        ///         },
        ///         {
        ///             "name": "French",
        ///             "language": "fr-FR"
        ///         },
        ///         {
        ///             "name": "Polish",
        ///             "language": "pl-PL"
        ///         },
        ///         {
        ///             "name": "Chinese",
        ///             "language": "zh-CN"
        ///         }
        ///     ]
        /// </remarks>
        /// <returns>Object of the JsonResult class representing the list of supported languages in JSON format</returns>
        [HttpGet]
        [Route("Language/GetList")]
        public IActionResult GetList()
        {
            List<LanguageDTO> result = GetLanguages();

            return Ok(result);
        }
        /// <summary>
        /// Sets the language of the site on the one passed as an argument
        /// </summary>
        /// <param name="language">The preferred language</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("Language/ChooseLanguage")]
        public IActionResult ChooseLanguage(string? language)
        {
            string message;

            if (language == null)
            {
                message = Translate(ErrorMessageHelper.NoLanguage);

                return BadRequest(new ResponseViewModel(message));
            }

            HttpContext.Session.SetString("Language", language);

            message = Translate(MessageHelper.LanguageChangeSuccess);

            return Ok(new ResponseViewModel(message));
        }

    }
}
