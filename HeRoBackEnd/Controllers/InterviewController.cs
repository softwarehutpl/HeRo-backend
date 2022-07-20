using Data.DTOs.Interview;
using HeRoBackEnd.ViewModels;
using HeRoBackEnd.ViewModels.Interview;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Listing;
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
        /// <response code="400">string "No Interview with this InterviewId"</response>
        [HttpGet]
        [Route("Interview/Get/{interviewId}")]
        [Authorize(Policy = "AnyRoleRequirment")]
        [ProducesResponseType(typeof(InterviewDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Get(int interviewId)
        {
            InterviewDTO interview = _interviewService.Get(interviewId);

            if (interview == null)
            {
                return BadRequest(new ResponseViewModel("No Interview with this Id"));
            }

            return new JsonResult(interview);
        }

        /// <summary>
        /// Gets all interviews that abide by the filter from the database
        /// </summary>
        /// <param name="interview">An object containing information about the filter</param>
        /// <returns>Object of the JsonResult class representing a list of Interview in the JSON format</returns>
        /// <remarks>
        /// <h2>Format:</h2>
        ///    <h3>Date:</h3>
        ///    yyyy-MM-dd <br />
        ///    yyyy-MM-ddTHH:mm <br />
        ///    yyyy-MM-ddTHH:mm:ss <br />
        ///    yyyy-MM-ddTHH:mm:ss.fff <br />
        /// <h2>Nullable:</h2>
        ///    "fromDate", "toDate", "candidateId", "workerId", "type", "sortOrder"
        /// <h2>Filtring:</h2>
        ///    <h3>Contains:</h3> "name", "description" <br />
        ///    <h3>Equals:</h3> "type" <br />
        /// <h2>Sorting:</h2>
        ///     <h3>Possible keys:</h3> "Date", "Candidateid", "WorkerId", "Type" <br />
        ///     <h3>Value:</h3> "DESC" - sort the result in descending order <br />
        ///                      Another value - sort the result in ascending order <br />
        ///
        /// </remarks>
        /// <response code="200">List of Interviews</response>
        [HttpPost]
        [Route("Interview/GetList")]
        [Authorize(Policy = "AnyRoleRequirment")]
        [ProducesResponseType(typeof(InterviewListing), StatusCodes.Status200OK)]
        public IActionResult GetList(InterviewFiltringViewModel interview)
        {
            InterviewFiltringDTO interviewFiltringDTO =
                new InterviewFiltringDTO
                {
                    FromDate = interview.FromDate,
                    ToDate = interview.ToDate,
                    CandidateId = interview.CandidateId,
                    WorkerId = interview.WorkerId,
                    Type = interview.Type,
                };

            var resutl = _interviewService.GetInterviews(interview.Paging, interview.SortOrder, interviewFiltringDTO);

            return new JsonResult(resutl);
        }

        /// <summary>
        /// Add new interview
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <response code="200">string "Interview created successfully"</response>
        [HttpPost]
        [Route("Interview/Create")]
        [Authorize(Policy = "HrRequirment")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult Create(InterviewCreateViewModel interview)
        {
            InterviewCreateDTO interviewCreate =
                new InterviewCreateDTO(
                    interview.Date,
                    interview.CandidateId,
                    interview.WorkerId,
                    interview.Type);

            int userCreatedId = GetUserId();
            _interviewService.Create(interviewCreate, userCreatedId);

            return Ok(new ResponseViewModel("Interview created successfully"));
        }

        /// <summary>
        /// Updates information about a interview represented by an id
        /// </summary>
        /// <param name="interviewId" example="1">Id of a interview</param>
        /// <response code="200">Interview edited successfully</response>
        /// <response code="404">No interview with this InterviewId</response>
        [HttpPost]
        [Route("Interview/Edit/{interviewId}")]
        [Authorize(Policy = "HrRequirment")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Edit(int interviewId, InterviewEditViewModel interview)
        {
            InterviewEditDTO interviewEdit =
                new InterviewEditDTO(
                    interviewId,
                    interview.Date,
                    interview.WorkerId,
                    interview.Type);

            int userEditId = GetUserId();
            int result = _interviewService.Update(interviewEdit, userEditId);

            if (result == 0)
            {
                return BadRequest(new ResponseViewModel("No interview with this Id"));
            }
            if (result == -1)
            {
                return BadRequest(new ResponseViewModel("Interview edited unsuccessfully"));
            }

            return Ok(new ResponseViewModel("Interview edited successfully"));
        }

        /// <summary>
        /// Deletes a interview represented by an id
        /// </summary>
        /// <param name="interviewId">Id of a interview</param>
        /// <response code="200">Interview deleted successfully</response>
        /// <response code="400">No interview with this interviewId</response>
        [HttpDelete]
        [Route("Interview/Delete/{interviewId}")]
        [Authorize(Policy = "HrRequirment")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int interviewId)
        {
            int loginUserId = GetUserId();

            int result = _interviewService.Delete(interviewId, loginUserId);

            if (result == 0)
            {
                return BadRequest(new ResponseViewModel("No interview with this Id"));
            }
            if (result == -1)
            {
                return BadRequest(new ResponseViewModel("Interview deleted unsuccessfully"));
            }

            return Ok(new ResponseViewModel("Interview deleted successfully"));
        }
    }
}