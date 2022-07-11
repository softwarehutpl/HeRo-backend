using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HeRoBackEnd.Controllers
{
    [ApiController]
    public class SkillController : Controller
    {
        [HttpPut]
        [Route("Skill/Create")]
        public IActionResult Create(string skillName)
        {
            return RedirectToAction("Index");
        }
        [HttpPut]
        [Route("Skill/Update/{skillId}")]
        public IActionResult Update(int skillId, int skillName)
        {
            return RedirectToAction("Index");
        }
        [HttpDelete]
        [Route("Skill/Delete/{skillId}")]
        public IActionResult Delete(int skillId)
        {
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Route("Skill/GetList")]
        public IActionResult GetList()
        {
            return View();
        }
        [HttpGet]
        [Route("Skill/Get/{skillId}")]
        public IActionResult Get(int skillId)
        {
            return View();
        }
    }
}