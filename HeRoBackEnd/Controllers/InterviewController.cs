using Common.Listing;
using HeRoBackEnd.ViewModels.Interview;
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
        ///        "date": "2012-12-12T12:12:12.121Z",
        ///        "candidateId": "Active",
        ///        "userId": "Admin"
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
        ///              "date": "2012-12-12T12:12:12.121Z",
        ///              "candidateId": "1",
        ///              "userId": "2",
        ///              "type": "Tech"
        ///          },
        ///          {
        ///              "date": "2012-12-12T12:12:12.121Z",
        ///              "candidateId": "2",
        ///              "userId": "3",
        ///              "type": "HR"
        ///          }
        ///     ]
        /// </remarks>
        [HttpPost]
        [Route("Interview/GetList")]
        public IActionResult GetList(InterviewListViewModel interview)
        {
            Paging paging = interview.Paging;
            SortOrder sortOrder = interview.SortOrder;
            InterviewFiltringDTO interviewFiltringDTO = new InterviewFiltringDTO(interview.FromDate, interview.ToDate, interview.CandidateId, interview.UserId, interview.Type);

            var resutl = _interviewService.GetInterviews(paging, sortOrder, interviewFiltringDTO);

            return new JsonResult(resutl);
        }
    }
}
