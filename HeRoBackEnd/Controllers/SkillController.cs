using Common.AttributeRoleVerification;
using Common.Helpers;
using Data.DTOs.Skill;
using Data.Entities;
using HeRoBackEnd.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace HeRoBackEnd.Controllers
{
    [ApiController]
    public class SkillController : BaseController
    {
        private readonly SkillService _skillService;
        private string _errorMessage;
        private UserActionService _userActionService;

        public SkillController(SkillService service, UserActionService userActionService)
        {
            _skillService = service;
            _userActionService = userActionService;
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
            LogUserAction($"SkillController.Create({skillName})", _userActionService);
            bool result = _skillService.AddSkill(skillName, out _errorMessage);
            string message;

            if (result == false)
            {
                message = Translate(_errorMessage);

                return BadRequest(new ResponseViewModel(message));
            }

            message = Translate(MessageHelper.SkillAdded);

            return Ok(new ResponseViewModel(message));
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
            LogUserAction($"SkillController.Update({skillId}, {newSkillName})", _userActionService);
            UpdateSkillDTO dto = new UpdateSkillDTO(skillId, newSkillName);
            bool result = _skillService.UpdateSkill(dto, out _errorMessage);
            string message;

            if (result == false)
            {
                message=Translate(_errorMessage);

                return BadRequest(new ResponseViewModel(message));
            }

            message = Translate(MessageHelper.SkillUpdated);

            return Ok(new ResponseViewModel(message));
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
            LogUserAction($"SkillController.Delete({skillId})", _userActionService);

            bool result = _skillService.DeleteSkill(skillId, out _errorMessage);
            string message;

            if (result == false)
            {
                message = Translate(_errorMessage);

                return BadRequest(new ResponseViewModel(message));
            }

            message = Translate(MessageHelper.SkillDeleted);

            return Ok(new ResponseViewModel(message));
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
            LogUserAction($"SkillController.GetList()", _userActionService);
            IEnumerable<Skill> skills = _skillService.GetSkills();

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
            LogUserAction($"SkillController.GetFiltredByName({name})", _userActionService);
            IEnumerable<Skill> skills = _skillService.GetSkillsFilteredByName(name);

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
            LogUserAction($"SkillController.Get({skillId})", _userActionService);
            Skill skill = _skillService.GetSkill(skillId);

            if (skill == null)
            {
                string message = Translate(ErrorMessageHelper.NoSkill);

                return BadRequest(new ResponseViewModel(message));
            }

            return Ok(skill);
        }
    }
}