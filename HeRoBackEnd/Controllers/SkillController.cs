using Common.AttributeRoleVerification;
using Data.DTOs.Skill;
using Data.Entities;
using HeRoBackEnd.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace HeRoBackEnd.Controllers
{
    [ApiController]
    public class SkillController : Controller
    {
        private readonly SkillService _service;

        public SkillController(SkillService service)
        {
            _service = service;
        }

        /// <summary>
        /// Adds a skill with the specified name, you can't create a skill which name already exists in the database
        /// </summary>
        /// <param name="skillName">Name of the skill</param>
        /// <returns>IActionResult</returns>
        [HttpPut]
        [Route("Skill/Create")]
        [RequireUserRole("ADMIN")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Create(string skillName)
        {
            int result = _service.AddSkill(skillName);

            if (result == 0)
            {
                return BadRequest(new ResponseViewModel("Skill with that name already exists!"));
            }
            if (result == -1)
            {
                return BadRequest(new ResponseViewModel("Wrong data!"));
            }

            return Ok(new ResponseViewModel("Skill added successfully"));
        }

        /// <summary>
        /// Updates a skill specified by an id, you can't update a skill with a name that already exists in the database
        /// </summary>
        /// <param name="skillId">Id of the skill</param>
        /// <param name="newSkillName">New name of the skill</param>
        /// <returns>IActionResult</returns>
        [HttpPut]
        [Route("Skill/Update/{skillId}")]
        [RequireUserRole("ADMIN")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Update(int skillId, string newSkillName)
        {
            UpdateSkillDTO dto = new UpdateSkillDTO(skillId, newSkillName);
            int result = _service.UpdateSkill(dto);

            if (result == -2)
            {
                return BadRequest(new ResponseViewModel("There is no such skill!"));
            }
            if (result == 0)
            {
                return BadRequest(new ResponseViewModel("Skill with that name already exists!"));
            }
            if (result == -1)
            {
                return BadRequest(new ResponseViewModel("Wrong data!"));
            }

            return Ok(new ResponseViewModel("Skill updated successfully"));
        }

        /// <summary>
        /// Deletes a skill specified by an id, you can't delete a skill that is used in one of the recruitments
        /// </summary>
        /// <param name="skillId">Id of the skill</param>
        /// <returns>IActionResult</returns>
        [HttpDelete]
        [Route("Skill/Delete/{skillId}")]
        [RequireUserRole("ADMIN")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int skillId)
        {
            int result = _service.DeleteSkill(skillId);

            if (result == 0)
            {
                return BadRequest(new ResponseViewModel("This skill is currently used in one of the recruitments. You can't delete it."));
            }

            if (result == -1)
            {
                return BadRequest(new ResponseViewModel("There is no such skill!"));
            }

            if (result == -2)
            {
                return BadRequest(new ResponseViewModel("Error while deleting skill!"));
            }

            return Ok(new ResponseViewModel("Skill deleted successfully"));
        }

        /// <summary>
        /// Returns all the skills
        /// </summary>
        /// <returns>Object of the JsonResult class representing the IEnumerable collection of skills in the JSON format</returns>
        /// <remarks>
        /// Sample response:
        ///
        ///     [
        ///          {
        ///              "id": 1,
        ///              "name": "General knowledge of the software engineering process"
        ///          },
        ///          {
        ///              "id": 2,
        ///              "name": "Programming in C#"
        ///          },
        ///     ]
        /// </remarks>
        [HttpGet]
        [Route("Skill/GetList")]
        [RequireUserRole("HR_MANAGER", "RECRUITER", "TECHNICIAN", "ANONYMOUS")]
        [ProducesResponseType(typeof(IEnumerable<Skill>), StatusCodes.Status200OK)]
        public IActionResult GetList()
        {
            IEnumerable<Skill> skills = _service.GetSkills();

            return Ok(skills);
        }

        /// <summary>
        /// Returns 5 skills which names contain a string passed as an argument
        /// </summary>
        /// <param name="name">String used to find skills which names contain it</param>
        /// <returns>Object of the JsonResult class representing the IEnumerable collection of skills in the JSON format</returns>
        /// <remarks>
        /// Sample response for the phrase "Program":
        ///
        ///     [
        ///         {
        ///             "id": 1,
        ///             "name": "Programming in C#"
        ///         },
        ///         {
        ///             "id": 2,
        ///             "name": "Programming in Java"
        ///         },
        ///         {
        ///             "id": 4,
        ///             "name": "Programming in Python"
        ///         },
        ///         {
        ///             "id": 5,
        ///             "name": "Programming in C++"
        ///         },
        ///         {
        ///             "id": 6,
        ///             "name": "Programming in C"
        ///         }
        ///     ]
        /// </remarks>
        [HttpGet]
        [Route("Skill/GetListFilteredByName")]
        [RequireUserRole("HR_MANAGER", "RECRUITER", "TECHNICIAN", "ANONYMOUS")]
        [ProducesResponseType(typeof(IEnumerable<Skill>), StatusCodes.Status200OK)]
        public IActionResult GetListFilteredByName(string name)
        {
            IEnumerable<Skill> skills = _service.GetSkillsFilteredByName(name);

            return Ok(skills);
        }

        /// <summary>
        /// Returns a skill specified by an id
        /// </summary>
        /// <param name="skillId">Id of a skill</param>
        /// <returns>Object of the JsonResult class representing the skill in the JSON format</returns>
        /// <remarks>
        /// Sample response:
        ///
        ///     {
        ///         "id": 2,
        ///         "name": "Programming in C#"
        ///     }
        /// </remarks>
        [HttpGet]
        [Route("Skill/Get/{skillId}")]
        [RequireUserRole("HR_MANAGER", "RECRUITER", "TECHNICIAN", "ANONYMOUS")]
        [ProducesResponseType(typeof(Skill), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Get(int skillId)
        {
            Skill skill = _service.GetSkill(skillId);

            if (skill == null)
            {
                return BadRequest(new ResponseViewModel("There is no such skill!"));
            }

            return Ok(skill);
        }
    }
}