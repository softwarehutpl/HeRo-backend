using Common.Listing;
using HeRoBackEnd.ViewModels.Interview;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.Interview;
using Services.Services;

namespace HeRoBackEnd.Controllers
{
    [ApiController]
    public class InterviewController : BaseController
    {
        private InterviewService _interviewService;

        public InterviewController(InterviewService interviewService)
        {
            _interviewService = interviewService;
        }

        /// <summary>
        /// Gets a interview specified by an id
        /// </summary>
        /// <param name="interviewId">Id of the user</param>
        /// <returns>Json string representing an object of the Interview class</returns>
        /// <response code="200">Interview object</response>
        /// <response code="400">No Interview with this InterviewId</response>
        [HttpGet]
        [Route("Interview/Get/{interviewId}")]
        [ProducesResponseType(typeof(InterviewDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public IActionResult Get(int interviewId)
        {
            InterviewDTO interview = _interviewService.Get(interviewId);

            if (interview == null)
            {
                return BadRequest();
            }

            return new JsonResult(interview);
        }

        /// <summary>
        /// Gets all interviews that abide by the filter from the database
        /// </summary>
        /// <param name="interview">An object containing information about the filter</param>
        /// <returns>Object of the JsonResult class representing a list of Interview in the JSON format</returns>
        /// <response code="200">List of Interview</response>
        [HttpPost]
        [Route("Interview/GetList")]
        [ProducesResponseType(typeof(IEnumerable<InterviewDTO>), StatusCodes.Status200OK)]
        public IActionResult GetList(InterviewFiltringViewModel interview)
        {
            InterviewFiltringDTO interviewFiltringDTO = new InterviewFiltringDTO(interview.FromDate, interview.ToDate, interview.CandidateId, interview.WorkerId, interview.Type);

            var resutl = _interviewService.GetInterviews(interview.Paging, interview.SortOrder, interviewFiltringDTO);

            return new JsonResult(resutl);
        }

        /// <summary>
        /// Add new interview
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("User/Create")]
        public IActionResult Create(InterviewCreateViewModel interview)
        {
            InterviewCreateDTO interviewCreate =
                new InterviewCreateDTO(
                    interview.Date,
                    interview.CandidateId,
                    interview.UserId,
                    interview.Type);

            int userCreatedId = GetUserId();
            _interviewService.Create(interviewCreate, userCreatedId);

            return Ok("Creating was successful");
        }

        /// <summary>
        /// Updates information about a interview represented by an id
        /// </summary>
        /// <param name="interviewId" example="1">Id of a interview</param>
        /// <response code="200">Interview edited</response>
        /// <response code="404">No interview with this InterviewId</response>
        [HttpPost]
        [Route("User/Edit/{interviewId}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Edit(int interviewId, InterviewEditViewModel interview)
        {
            InterviewEditDTO interviewEdit =
                new InterviewEditDTO(
                    interviewId,
                    interview.Date,
                    interview.CandidateId,
                    interview.UserId,
                    interview.Type);

            int userEditId = GetUserId();
            int result = _interviewService.Update(interviewEdit, userEditId);

            if (result == 0)
            {
                return BadRequest("No interview with this Id");
            }

            return Ok("Editing was successful");
        }

        /// <summary>
        /// Deletes a interview represented by an id
        /// </summary>
        /// <param name="interviewId">Id of a interview</param>
        /// <response code="200">Interview deleted</response>
        /// <response code="400">No interview with this interviewId</response>
        [HttpDelete]
        [Route("Interview/Delete/{interviewId}")]
        [Authorize(Policy = "AdminRequirment")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int interviewId)
        {
            int loginUserId = GetUserId();

            int result = _interviewService.Delete(interviewId, loginUserId);

            if (result == 0)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}