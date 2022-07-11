using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Services.Services;
using Services.DTOs.Skill;

namespace HeRoBackEnd.Controllers
{
    [ApiController]
    public class SkillController : Controller
    {
        private readonly SkillService service;
        public SkillController(SkillService service)
        {
            this.service = service;
        }

        [HttpPut]
        [Route("Skill/Create")]
        public IActionResult Create(string skillName)
        {
            SkillDTO dto = new SkillDTO(skillName);
            int result=service.AddSkill(dto);

            if (result == -1) return BadRequest();

            return RedirectToAction("Index");
        }

        [HttpPut]
        [Route("Skill/Update/{skillId}")]
        public IActionResult Update(int skillId, string skillName)
        {
            SkillDTO dto = new SkillDTO(skillId, skillName);
            int result = service.UpdateSkill(dto);

            if (result == -1) return BadRequest();

            return RedirectToAction("Index");
        }

        [HttpDelete]
        [Route("Skill/Delete/{skillId}")]
        public IActionResult Delete(int skillId)
        {
            int result = service.DeleteSkill(skillId);

            if (result == -1) return BadRequest();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("Skill/GetList")]
        public IActionResult GetList()
        {
            IEnumerable<SkillDTO> skills = service.GetSkills();

            JsonResult result = new JsonResult(skills);

            return result;
        }

        [HttpGet]
        [Route("Skill/Get/{skillId}")]
        public IActionResult Get(int skillId)
        {
            SkillDTO skill = service.GetSkill(skillId);

            if (skill == null) return BadRequest();

            JsonResult result = new JsonResult(skill);

            return result;
        }
    }
}