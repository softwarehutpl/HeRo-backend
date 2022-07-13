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
        /// <param name="interviewId">Id of the interviewId</param>
        /// <returns>Json string representing an object of the User class</returns>
        /// <remarks>
        /// Sample Responses:
        ///
        ///     {
        ///        "interviewId" : 1,
        ///        "date": "2012-12-12T12:12:12.121Z",
        ///        "candidateId": 1,
        ///        "candidateName": "Jan"
        ///        "candidateLastName": "Naj"
        ///        "candidateEmail": "test@da.com",
        ///        "workerId": 1,
        ///        "workerEmail": "testHR@da.com",
        ///        "type": "HR"
        ///     }
        ///
        /// </remarks>
        [HttpGet]
        [Route("Interview/Get/{interviewId}")]
        public IActionResult Get(int interviewId)
        {
            InterviewDTO interview = _interviewService.Get(interviewId);

            if (interview == null)
            {
                return NotFound("No interview with this interviewId");
            }

            return new JsonResult(interview);
        }

        /// <summary>
        /// Gets all interviews that abide by the filter from the database
        /// </summary>
        /// <param name="interview">An object containing information about the filter</param>
        /// <returns>Object of the JsonResult class representing a list of Users in the JSON format</returns>
        /// <remarks>
        /// Sample Responses:
        ///
        ///     [
        ///          {
        ///              "interviewId" : 1,
        ///              "date": "2012-12-12T12:12:12.121Z",
        ///              "candidateId": 1,
        ///              "candidateName": "Jan"
        ///              "candidateLastName": "Naj"
        ///              "candidateEmail": "test@da.com",
        ///              "workerId": 1,
        ///              "workerEmail": "testHR@da.com",
        ///              "type": "HR"
        ///          },
        ///          {
        ///             "interviewId" : 2,
        ///              "date": "2013-13-13T13:13:13.131Z",
        ///              "candidateId": 3,
        ///              "candidateName": "Maja"
        ///              "candidateLastName": "Listopad"
        ///              "candidateEmail": "test33@da.com",
        ///              "workerId": 2,
        ///              "workerEmail": "testTech@da.com",
        ///              "type": "Tech"
        ///          }
        ///     ]
        /// </remarks>
        [HttpPost]
        [Route("Interview/GetList")]
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
        /// <param name="interviewId">Id of a interview</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("User/Edit/{interviewId}")]
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
                return NotFound("No interview with this Id");
            }

            return Ok("Editing was successful");
        }

        [HttpDelete]
        [Route("Interview/Delete/{interviewId}")]
        [Authorize(Policy = "AdminRequirment")]
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