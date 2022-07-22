using AutoMapper;
using Common.AttributeRoleVerification;
using Common.Enums;
using Data.DTOs.Candidate;
using HeRoBackEnd.ViewModels;
using HeRoBackEnd.ViewModels.Candidate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Listing;
using Services.Services;

namespace HeRoBackEnd.Controllers
{
    [ApiController]
    public class CandidateController : BaseController
    {
        private CandidateService _candidateService;
        private ILogger<CandidateController> _logger;
        private readonly IMapper _mapper;

        public CandidateController(CandidateService candidateService, ILogger<CandidateController> logger, IMapper map)
        {
            this._candidateService = candidateService;
            _mapper = map;
            _logger = logger;
        }

        /// <summary>
        /// Returns a candidate specified by the id
        /// </summary>
        /// <param name="candidateId">Takes the id of a candidate</param>
        /// <returns>Json string representing a Candidate</returns>
        /// <response code="200">Interview object</response>
        /// <response code="400">"Error getting candidate (bad parameters or candidate doesn't exist)"</response>
        [HttpGet]
        [Route("Candidate/Get/{candidateId}")]
        [RequireUserRole(UserRoles = new string[] { "RECRUITER" })]
        [ProducesResponseType(typeof(CandidateProfileDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Get(int candidateId)
        {
            CandidateProfileDTO? candDTO = _candidateService.GetCandidateProfileById(candidateId);

            if (candDTO == null)
            {
                return BadRequest(new ResponseViewModel("Error getting candidate (bad parameters or candidate doesn't exist)"));
            }

            return new JsonResult(candDTO);
        }

        /// <summary>
        /// Returns a Json result object representing a list of candidates
        /// </summary>
        /// <param name="candidate">An object containing information about the filter</param>
        /// <returns>Json result object representing a list of Candidates</returns>
        /// <remarks>
        /// <h2>Nullable:</h2>
        ///    "statuses", "stages", "sortOrder" <br /><br />
        /// <h2>Filtring:</h2>
        ///    <h3>Possible statuses:</h3> "NEW" , "IN_PROCESSING", "DROPPED_OUT", "HIRED" <br />
        ///    <h3>Possible stages:</h3> "EVALUATION", "INTERVIEW", "PHONE_INTERVIEW", "TECH_INTERVIEW", "OFFER" <br />
        /// <h2>Sorting:</h2>
        ///     <h3>Possible keys:</h3> "Name", "Source", "Status", "Stage" <br />
        ///     <h3>Value:</h3> "DESC" - sort the result in descending order <br />
        ///                      Another value - sort the result in ascending order <br />
        ///
        /// </remarks>
        /// <response code="200">List of Candidates</response>
        [HttpPost]
        [Route("Candidate/GetList")]
        [RequireUserRole("HR_MANAGER", "RECRUITER", "TECHNICIAN", "ANONYMOUS")]
        [ProducesResponseType(typeof(CandidateListing), StatusCodes.Status200OK)]
        public IActionResult GetList(CandidateListFilterViewModel candidate)
        {
            CandidateFilteringDTO candidateFilteringDTO
                = new CandidateFilteringDTO(
                    candidate.Status,
                    candidate.Stage);

            var result = _candidateService
                .GetCandidates(
                    candidate.Paging,
                    candidate.SortOrder,
                    candidateFilteringDTO);

            return new JsonResult(result);
        }

        /// <summary> Creates a candidate </summary>
        /// <param name="newCandidate">Object of the CandidateCreateViewModel class containing information about the new candidate</param>
        /// <returns>IActionResult</returns>
        /// <remarks>
        /// Example request:
        ///
        ///     {
        ///       "name": "Grzegorz",
        ///       "lastName": "Brzęczyszczykiewicz",
        ///       "email": "grzesiu@example.com",
        ///       "phoneNumber": "123123123",
        ///       "availableFrom": "2022-07-23T10:37:01.988Z",
        ///       "expectedMonthlySalary": 5000,
        ///       "otherExpectations": "otherExpectationsString",
        ///       "cvPath": "CVPathString",
        ///       "recruitmentId": 1
        ///     }
        /// </remarks>
        /// <response code="200">string "Candidate created successfully"</response>
        /// <response code="400">string "Error creating candidate (check parameters)<br />
        /// string "Error creating candidate (invalid recruitmentId)<br />
        /// string "Error saving candidate in database"</response>
        [HttpPost]
        [Route("Candidate/Create")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Create(CandidateCreateViewModel newCandidate)
        {
            CreateCandidateDTO dto = _mapper.Map<CreateCandidateDTO>(newCandidate);

            dto.Status = CandidateStatuses.NEW.ToString();
            dto.ApplicationDate = DateTime.Now;
            int result = _candidateService.CreateCandidate(dto);
            if (result == -1)
            {
                return BadRequest(new ResponseViewModel("Error creating candidate (one or more invalid parameters)"));
            }
            if (result == -2)
            {
                return BadRequest(new ResponseViewModel("Error creating candidate (invalid recruitmentId)"));
            }
            if (result == -3)
            {
                return BadRequest(new ResponseViewModel("Error saving candidate to database"));
            }

            return Ok(new ResponseViewModel("Candidate created successfully"));
        }

        /// <summary>
        /// Updates information about a candidate
        /// </summary>
        /// <param name="candidateId">Id of the candidate</param>
        /// <param name="candidate">Object of the CandidateEditViewModel class containing fields used to update informations about the candidate</param>
        /// <returns>IActionResult</returns>
        /// <remarks>
        /// Example request:
        ///
        ///     {
        ///       "name": "Grzegorz",
        ///       "lastName": "Brzęczyszczykiewicz",
        ///       "email": "grzesiu@example.com",
        ///       "phoneNumber": "123123123",
        ///       "availableFrom": "2022-07-23T10:37:01.988Z",
        ///       "expectedMonthlySalary": 5000,
        ///       "otherExpectations": "otherExpectationsString",
        ///       "cvPath": "CVPathString",
        ///       "status": "IN_PROCESSING",
        ///       "stage": "PHONE_INTERVIEW"
        ///     }
        /// </remarks>
        /// <response code="200">string "Candidate updated successfully"</response>
        /// <response code="400">string "User with given Id doesn't exist"<br />
        /// string "Error updating candidate"</response>
        [HttpPost]
        [Route("Candidate/Edit/{candidateId}")]
        [RequireUserRole("RECRUITER")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Edit(int candidateId, CandidateEditViewModel candidate)
        {
            UpdateCandidateDTO dto = _mapper.Map<UpdateCandidateDTO>(candidate);
            dto.LastUpdatedDate = DateTime.Now;
            dto.LastUpdatedBy = GetUserId();

            //wyjątek przy złym id kandydata dodać
            int result = _candidateService.UpdateCandidate(candidateId, dto);
            if (result == -1)
            {
                return BadRequest(new ResponseViewModel("User with given Id doesn't exist"));
            }
            if (result == -2)
            {
                return BadRequest(new ResponseViewModel("Error updating candidate"));
            }

            return Ok(new ResponseViewModel("Candidate updated successfully"));
        }

        /// <summary>
        /// Deletes a candidate
        /// </summary>
        /// <param name="candidateId">Id of the candidate</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">string "Candidate deleted successfully"</response>
        /// <response code="400">string "Candidate with given ID doesn't exist"<br />
        /// string "Error deleting candidate"</response>
        [HttpDelete]
        [Route("Candidate/Delete/{candidateId}")]
        [RequireUserRole("RECRUITER")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int candidateId)
        {
            DeleteCandidateDTO dto = new DeleteCandidateDTO(candidateId);

            int id = GetUserId();
            dto.LastUpdatedById = id;
            dto.LastUpdatedDate = DateTime.Now;
            dto.DeletedById = id;
            dto.DeletedDate = DateTime.Now;

            int result = _candidateService.DeleteCandidate(dto);
            if (result == -1)
            {
                return BadRequest(new ResponseViewModel("Candidate with given ID doesn't exist"));
            }
            if (result == -2)
            {
                return BadRequest(new ResponseViewModel("Error deleting candidate"));
            }

            return Ok(new ResponseViewModel("Candidate deleted successfully"));
        }

        /// <summary>
        /// Adds a note concerning the candidate (given by HR)
        /// </summary>
        /// <param name="candidateId">ID of candidate</param>
        /// <param name="AddHrNote">Object of CandidateAddHRNoteViewModel class containing fields for notes and score
        /// given to candidate by HR worker with given recruiterId</param>
        /// <returns>IActionResult</returns>
        /// <remarks>
        /// Example request:
        ///
        ///     {
        ///         "score": 5,
        ///         "note": "Note about candidate given by HR"
        ///     }
        /// </remarks>
        /// <response code="200">string "Interview note added correctly"</response>
        /// <response code="400">string "Candidate with given ID doesn't exist"<br />
        /// string "Error adding note to candidate"</response>
        [HttpPost]
        [Route("Candidate/AddHRNote/{candidateId}")]
        [RequireUserRole("RECRUITER")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult AddHRNote(int candidateId, CandidateAddHRNoteViewModel AddHrNote)
        {
            CandidateAddHRNoteDTO dto = _mapper.Map<CandidateAddHRNoteDTO>(AddHrNote);
            dto.RecruiterId = GetUserId();
            int result = _candidateService.AddHRNote(candidateId, dto);
            if (result == -1)
            {
                return BadRequest(new ResponseViewModel("Candidate with given ID doesn't exist"));
            }
            if (result == -2)
            {
                return BadRequest(new ResponseViewModel("Error adding note to candidate"));
            }

            return Ok(new ResponseViewModel("Interview note added correctly"));
        }

        /// <summary>
        /// Adds a note concerning the candidate (given by tech)
        /// </summary>
        /// <param name="candidateId">ID of candidate</param>
        /// <param name="AddTechNote">Object of CandidateAddTechNoteViewModel class containing fields for notes and score
        /// given to candidate by tech worker with given techId</param>
        /// <returns>IActionResult</returns>
        /// <remarks>
        /// Example request:
        ///
        ///     {
        ///         "score": 5,
        ///         "note": "Note about candidate given by tech"
        ///     }
        /// </remarks>
        /// <response code="200">string "Tech interview note added correctly"</response>
        /// <response code="400">string "Candidate with given ID doesn't exist"<br />
        /// string "Error adding note to candidate"</response>
        [HttpPost]
        [Route("Candidate/AddTechInterviewNote/{candidateId}")]
        [RequireUserRole("TECHNICIAN")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult AddTechInterviewNote(int candidateId, CandidateAddTechNoteViewModel AddTechNote)
        {
            CandidateAddTechNoteDTO dto = _mapper.Map<CandidateAddTechNoteDTO>(AddTechNote);
            dto.TechId = GetUserId();
            int result = _candidateService.AddTechNote(candidateId, dto);
            if (result == -1)
            {
                return BadRequest(new ResponseViewModel("Candidate with given ID doesn't exist"));
            }
            if (result == -2)
            {
                return BadRequest(new ResponseViewModel("Error adding tech note to candidate"));
            }

            return Ok(new ResponseViewModel("Tech interview note added correctly"));
        }

        /// <summary>
        /// Assign tech and recruiter to candidate
        /// </summary>
        /// <param name="candidateId">Takes the id of a candidate</param>
        /// <param name="assignees">Object of CandidateAssigneesViewModel class containing fields for updating information about the candidates assignees</param>
        /// <returns>IActionResult</returns>
        /// <remarks>
        /// Example request (assign technician with Id == techId and HR employee with Id == recruiterId to candidate with Id == candidateId):
        ///
        ///     {
        ///         "techId": 3,
        ///         "recruiterId": 5
        ///     }
        /// </remarks>
        /// <response code="200">string "Employees assigned correctly"</response>
        /// <response code="400">
        /// string "Candidate with given ID doesn't exist"<br />
        /// string "Error assigning employees to candidate"
        /// </response>
        [HttpPost]
        [Route("Candidate/AssignTechAndRecruiter/{candidateId}")]
        [RequireUserRole("RECRUITER")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult AssignTechAndRecruiter(int candidateId, CandidateAssigneesViewModel assignees)
        {
            CandidateAssigneesDTO dto = _mapper.Map<CandidateAssigneesDTO>(assignees);
            dto.LastUpdatedDate = DateTime.Now;
            dto.LastUpdatedBy = GetUserId();

            int result = _candidateService.AllocateRecruiterAndTech(candidateId, dto);
            if (result == -1)
            {
                return BadRequest(new ResponseViewModel("Candidate with given ID doesn't exist"));
            }

            if (result == -2)
            {
                return BadRequest(new ResponseViewModel("Error updating candidate when allocating recruiters"));
            }

            return Ok(new ResponseViewModel("Employees assigned correctly"));
        }

        /// <summary>
        /// Assigns dates of interviews for the candidate.
        /// </summary>
        /// <param name="candidateId">Id of the candidate</param>
        /// <param name="interviewDateViewModel">Object of CandidateAllocateInterviewDateViewModel class used to set candidate interview date to given date</param>
        /// <returns>IActionResult</returns>
        ///  <remarks>
        /// Example request (set candidate interview date to date):
        ///
        ///     {
        ///        "date": "2022-09-23T12:35:00.217Z"
        ///     }
        /// </remarks>
        /// <response code="200">string "Interview date set correctly"</response>
        /// <response code="400">string "User with given ID doesn't exist"<br />
        /// string "Error setting interview date"</response>
        [HttpPost]
        [Route("Candidate/SetInterviewDate/{candidateId}")]
        [RequireUserRole("RECRUITER")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult SetInterviewDate(int candidateId, CandidateAllocateInterviewDateViewModel interviewDateViewModel)
        {
            CandidateAllocateInterviewDateDTO dto = _mapper.Map<CandidateAllocateInterviewDateDTO>(interviewDateViewModel);
            dto.LastUpdatedDate = DateTime.Now;
            dto.LastUpdatedBy = GetUserId();

            int result = _candidateService.AllocateRecruitmentInterview(candidateId, dto);
            if (result == -1)
            {
                return BadRequest(new ResponseViewModel("User with given ID doesn't exist"));
            }
            if (result == -2)
            {
                return BadRequest(new ResponseViewModel("Error setting interview date"));
            }

            return Ok(new ResponseViewModel("Interview date set correctly"));
        }

        /// <summary>
        /// Assigns dates of tech interviews for the candidate.
        /// </summary>
        /// <param name="candidateId">Id of the candidate</param>
        /// <param name="interviewDateViewModel">Object of CandidateAllocateInterviewDateViewModel class used to set candidate tech interview date to given date</param>
        /// <returns>IActionResult</returns>
        ///  <remarks>
        /// Example request (set candidate tech interview date to date):
        ///
        ///     {
        ///        "date": "2022-09-23T12:35:00.217Z"
        ///     }
        /// </remarks>
        /// <response code="200">string "Tech interview date set correctly"</response>
        /// <response code="400">string "Error setting tech interview date"</response>
        [HttpPost]
        [Route("Candidate/SetTechInterviewDate/{candidateId}")]
        [RequireUserRole("RECRUITER")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult SetTechInterviewDate(int candidateId, CandidateAllocateInterviewDateViewModel interviewDateViewModel)
        {
            CandidateAllocateInterviewDateDTO dto = _mapper.Map<CandidateAllocateInterviewDateDTO>(interviewDateViewModel);
            dto.LastUpdatedDate = DateTime.Now;
            dto.LastUpdatedBy = GetUserId();

            int result = _candidateService.AllocateTechInterview(candidateId, dto);
            if (result == -1)
            {
                return BadRequest(new ResponseViewModel("Error setting tech interview date"));
            }

            return Ok(new ResponseViewModel("Tech interview date set correctly"));
        }

        /// <summary>
        /// Shows list of existing stages of recruitment.
        /// </summary>
        /// <returns>JSON list of existing stages of recruitment</returns>

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
        [Route("Candidate/GetStageList")]
        [RequireUserRole("HR_MANAGER", "RECRUITER", "TECHNICIAN", "ANONYMOUS")]
        public IActionResult GetStageList()
        {
            var listOfStages = Enum.GetValues(typeof(StageNames)).Cast<StageNames>().Select(v => v.ToString());

            return new JsonResult(listOfStages);
        }

        /// <summary>
        /// Shows list of existing status values of recruitment.
        /// </summary>
        /// <returns>JSON list of existing status values of recruitment</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
        [Route("Candidate/GetStatusList")]
        [RequireUserRole("HR_MANAGER", "RECRUITER", "TECHNICIAN", "ANONYMOUS")]
        public IActionResult GetStatusList()
        {
            var listOfStatus = Enum.GetValues(typeof(CandidateStatuses)).Cast<CandidateStatuses>().Select(v => v.ToString());

            return new JsonResult(listOfStatus);
        }
    }
}