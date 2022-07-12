using Microsoft.AspNetCore.Mvc;
using Services.DTOs.Interview;
using Services.Services;

namespace HeRoBackEnd.Controllers
{
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
        ///        "date": "2012-21-12T12:12:12.127Z",
        ///        "candidateId": "Active",
        ///        "userId": "Admin"
        ///     }
        ///
        /// </remarks>
        [HttpGet]
        [Route("User/Get/{interviewId}")]
        public IActionResult Get(int interviewId)
        {
            InterviewDTO interview = _interviewService.Get(interviewId);

            if (interview == null)
            {
                return NotFound("No interview with this interviewId");
            }

            return new JsonResult(interview);
        }
    }
}
