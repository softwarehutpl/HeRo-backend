using Data.DTOs.Language;
using Microsoft.AspNetCore.Mvc;

namespace HeRoBackEnd.Controllers
{
    [ApiController]
    public class LanguageController
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
        [HttpGet]
        [Route("Language/GetList")]
        public IActionResult GetList()
        {
            List<LanguageDTO> result = GetLanguages();

            return null;
            //return Ok(result);
        }
        
    }
}
