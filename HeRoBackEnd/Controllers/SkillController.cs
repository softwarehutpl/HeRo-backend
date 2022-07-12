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

        /// <summary>
        /// Adds a skill with the specified name
        /// </summary>
        /// <param name="skillName">Name of the skill</param>
        /// <returns>IActionResult</returns>
        [HttpPut]
        [Route("Skill/Create")]
        public IActionResult Create(string skillName)
        {
            CreateSkillDTO dto = new CreateSkillDTO(skillName, false);
            int result=service.AddSkill(dto);

            if (result == -1) return BadRequest("Wrong data!");

            return Ok("Skill added successfully");
        }

        /// <summary>
        /// Updates a skill specified by an id
        /// </summary>
        /// <param name="skillId">Id of the skill</param>
        /// <param name="newSkillName">New name of the skill</param>
        /// <returns>IActionResult</returns>
        [HttpPut]
        [Route("Skill/Update/{skillId}")]
        public IActionResult Update(int skillId, string newSkillName)
        {
            UpdateSkillDTO dto = new UpdateSkillDTO(skillId, newSkillName);
            int result = service.UpdateSkill(dto);

            if (result == -1) return BadRequest("Wrong data!");

            return Ok("Skill updated successfully");
        }

        /// <summary>
        /// Deletes a skill specified by an id
        /// </summary>
        /// <param name="skillId">Id of the skill</param>
        /// <returns>IAcionResult</returns>
        [HttpDelete]
        [Route("Skill/Delete/{skillId}")]
        public IActionResult Delete(int skillId)
        {
            int result = service.DeleteSkill(skillId);

            if (result == -1) return BadRequest("There is no such skill!");
            if (result == 0) return BadRequest("This skill is currently used in one of the recruitments." +
                " You can't delete it.");

            return Ok("Skill deleted successfully");
        }

        /// <summary>
        /// Returns all the skills
        /// </summary>
        /// <returns>Object of the JsonResult class representing the IEnumerable collection of skills in the JSON format</returns>
        [HttpPost]
        [Route("Skill/GetList")]
        public IActionResult GetList()
        {
            IEnumerable<ReadSkillDTO> skills = service.GetSkills();

            JsonResult result = new JsonResult(skills);

            return result;
        }

        /// <summary>
        /// Returns 5 skills which names contain a string passed as an argument
        /// </summary>
        /// <param name="name">String used to find skills which names contain it</param>
        /// <returns>Object of the JsonResult class representing the IEnumerable collection of skills in the JSON format</returns>
        [HttpGet]
        [Route("Skill/GetListFilteredByName{name}")]
        public IActionResult GetListFilteredByName(string name)
        {
            IEnumerable<ReadSkillDTO> skills = service.GetSkillsFilteredByName(name);

            JsonResult result = new JsonResult(skills);

            return result;
        }

        /// <summary>
        /// Returns a skill specified by an id
        /// </summary>
        /// <param name="skillId">Id of a skill</param>
        /// <returns>Object of the JsonResult class representing the skill in the JSON format</returns>
        [HttpGet]
        [Route("Skill/Get/{skillId}")]
        public IActionResult Get(int skillId)
        {
            ReadSkillDTO skill = service.GetSkill(skillId);

            if (skill == null) return BadRequest("There is no such skill!");

            JsonResult result = new JsonResult(skill);

            return result;
        }
    }
}