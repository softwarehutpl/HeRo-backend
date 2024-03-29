using AutoMapper;
using Common.AttributeRoleVerification;
using Common.Enums;
using Common.Helpers;
using Data.DTOs.Candidate;
using HeRoBackEnd.ViewModels;
using HeRoBackEnd.ViewModels.Candidate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Listing;
using Services.Services;
using System.Text.Json;

namespace HeRoBackEnd.Controllers
{
    [ApiController]
    public class CandidateController : BaseController
    {
        private string _errorMessage;
        private CandidateService _candidateService;
        private UserActionService _userActionService;
        private readonly IMapper _mapper;

        public CandidateController(CandidateService candidateService, IMapper map, UserActionService userActionService)
        {
            this._candidateService = candidateService;
            _mapper = map;
            _userActionService = userActionService;
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
        [RequireUserRole("RECRUITER")]
        [ProducesResponseType(typeof(CandidateProfileDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Get(int candidateId)
        {
            LogUserAction("CandidateController", "Get", candidateId.ToString(), _userActionService);

            CandidateProfileDTO? candDTO = _candidateService.GetCandidateProfileById(candidateId, out _errorMessage);

            if (candDTO == null)
            {
                string message = Translate(_errorMessage);

                return BadRequest(new ResponseViewModel(message));
            }

            return new JsonResult(candDTO);
        }
        /// <summary>
        /// Redirects user to the CV file
        /// </summary>
        /// <param name="candidateId">Id of the candidate whose CV you want to see</param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [Route("Candidate/GetCV/{candidateId}")]
        [RequireUserRole("RECRUITER")]
        public IActionResult GetCV(int candidateId)
        {
            MemoryStream CVstream = _candidateService.GetCandidateCV(candidateId);

            if (CVstream == null)
            {
                string message = Translate(ErrorMessageHelper.ErrorGettingCandidate);

                return BadRequest(new ResponseViewModel(message));
            }

            OkObjectResult result = new OkObjectResult(CVstream);

            return result;
        }

        /// <summary>
        /// Returns a Json result object representing a list of candidates
        /// </summary>
        /// <param name="candidate">An object containing information about the filter</param>
        /// <returns>Json result object representing a list of Candidates</returns>
        /// <remarks>
        /// <h2>Nullable:</h2>
        ///    "recruitmentId", "statuses", "stages", "sortOrder" <br />
        /// <h2>Filtring:</h2>
        ///    <h3>Possible statuses:</h3> "NEW" , "IN_PROCESSING", "DROPPED_OUT", "HIRED" <br />
        ///    <h3>Possible stages:</h3> "EVALUATION", "INTERVIEW", "PHONE_INTERVIEW", "TECH_INTERVIEW", "OFFER" <br />
        /// <h2>Sorting:</h2>
        ///     <h3>Possible keys:</h3> "Id", "Name", "Source", "Status", "Stage" <br />
        ///     <h3>Value:</h3> "DESC" - sort the result in descending order <br />
        ///                      Another value - sort the result in ascending order <br />
        ///
        /// </remarks>
        /// <response code="200">List of Candidates</response>
        [HttpPost]
        [Route("Candidate/GetList")]
        [RequireUserRole("HR_MANAGER", "RECRUITER", "TECHNICIAN")]
        [ProducesResponseType(typeof(CandidateListing), StatusCodes.Status200OK)]
        public IActionResult GetList(CandidateListFilterViewModel candidate)
        {
            LogUserAction("CandidateController", "GetList", JsonSerializer.Serialize(candidate), _userActionService);

            CandidateFilteringDTO candidateFilteringDTO
                = new CandidateFilteringDTO
                {
                    RecruitmentId = candidate.RecruitmentId,
                    Status = candidate.Status,
                    Stages = candidate.Stage
                };

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
        ///       "CV": CV.pdf,
        ///       "recruitmentId": 1
        ///     }
        ///
        ///<h2>Format:</h2>
        ///    <h3>Date:</h3>
        ///    yyyy-MM-dd <br />
        ///    yyyy-MM-ddTHH:mm <br />
        ///    yyyy-MM-ddTHH:mm:ss <br />
        ///    yyyy-MM-ddTHH:mm:ss.fff <br /><br />
        /// <h2>Nullable:</h2>
        ///    "expectedMonthlySalary", "otherExpectations"" <br /><br />
        /// </remarks>
        /// <response code="200">string "Candidate created successfully"</response>
        /// <response code="400">string "Error creating candidate (check parameters)<br />
        /// string "Error creating candidate (invalid recruitmentId)<br />
        /// string "Error saving candidate in database"</response>
        [HttpPost]
        [Route("Candidate/Create")]
        [AllowAnonymous]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromForm]CandidateCreateViewModel newCandidate)
        {
            LogUserAction("CandidateController", "Create", JsonSerializer.Serialize(newCandidate), _userActionService);

            CreateCandidateDTO dto = new CreateCandidateDTO();
            dto.Name = newCandidate.Name;
            dto.LastName = newCandidate.LastName;
            dto.PhoneNumber= newCandidate.PhoneNumber;
            dto.Email = newCandidate.Email;
            dto.AvailableFrom = newCandidate.AvailableFrom;
            dto.ExpectedMonthlySalary = newCandidate.ExpectedMonthlySalary;
            dto.OtherExpectations=newCandidate.OtherExpectations;
            dto.RecruitmentId = newCandidate.RecruitmentId;

            using (Stream stream = newCandidate.CV.OpenReadStream())
            {
                byte[] content = new byte[stream.Length];
                stream.Read(content, 0, content.Length);
                dto.CV = content;

                stream.Close();
            }
            dto.Status = CandidateStatuses.NEW.ToString();
            dto.ApplicationDate = DateTime.Now;
            bool result = _candidateService.CreateCandidate(dto, out _errorMessage);
            string message;

            if (result == false)
            {
                message = Translate(_errorMessage);

                return BadRequest(new ResponseViewModel(message));
            }
            else
            {
                message = Translate(MessageHelper.CandidateCreatedSuccessfully);

                return Ok(new ResponseViewModel(message));
            }
        }

        /// <summary>
        /// Updates information about a candidate
        /// </summary>
        /// <param name="candidate">Object of the CandidateEditViewModel class containing fields used to update informations about the candidate</param>
        /// <returns>IActionResult</returns>
        /// <remarks>
        /// <h2>Format:</h2>
        ///    <h3>Date:</h3>
        ///    yyyy-MM-dd <br />
        ///    yyyy-MM-ddTHH:mm <br />
        ///    yyyy-MM-ddTHH:mm:ss <br />
        ///    yyyy-MM-ddTHH:mm:ss.fff <br /><br />
        /// <h2>Nullable:</h2>
        ///    Everything without "candidateId" and "CV" <br />
        /// </remarks>
        /// <response code="200">string "Candidate updated successfully"</response>
        /// <response code="400">string "User with given Id doesn't exist"<br />
        /// string "Error updating candidate"</response>
        [HttpPost]
        [Route("Candidate/Edit")]
        [RequireUserRole("RECRUITER")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Edit([FromForm] CandidateEditViewModel candidate)
        {
            if(candidate.Stage==".")
            {
                candidate.Stage = "";
            }

            LogUserAction("CandidateController", "Edit", $"{JsonSerializer.Serialize(candidate)}", _userActionService);

            UpdateCandidateDTO dto = new UpdateCandidateDTO();
            dto.CandidateId=candidate.CandidateId;
            dto.Name=candidate.Name;
            dto.LastName=candidate.LastName;
            dto.Status=candidate.Status;
            dto.Stage=candidate.Stage;
            dto.Email=candidate.Email;
            dto.PhoneNumber=candidate.PhoneNumber;
            dto.AvailableFrom=candidate.AvailableFrom;
            dto.ExpectedMonthlySalary=candidate.ExpectedMonthlySalary;
            dto.OtherExpectations=candidate.OtherExpectations;
            dto.LastUpdatedDate = DateTime.Now;
            dto.LastUpdatedBy = GetUserId();

            if(candidate.CV!=null)
            {
                using (Stream stream = candidate.CV.OpenReadStream())
                {
                    byte[] content = new byte[stream.Length];
                    stream.Read(content, 0, content.Length);
                    dto.CV = content;

                    stream.Close();
                }
            }
            
            bool result = _candidateService.UpdateCandidate(dto, out _errorMessage);
            string message;

            if (result == false)
            {
                message = Translate(_errorMessage);

                return BadRequest(new ResponseViewModel(message));
            }

            message = Translate(MessageHelper.CandidateUpdatedSuccessfully);

            return Ok(new ResponseViewModel(message));
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
            LogUserAction("CandidateController", "Delete", candidateId.ToString(), _userActionService);

            DeleteCandidateDTO dto = new DeleteCandidateDTO(candidateId);

            int id = GetUserId();
            dto.LastUpdatedById = id;
            dto.LastUpdatedDate = DateTime.Now;
            dto.DeletedById = id;
            dto.DeletedDate = DateTime.Now;

            bool result = _candidateService.DeleteCandidate(dto, out _errorMessage);
            string message;

            if (result == false)
            {
                message = Translate(_errorMessage);

                return BadRequest(new ResponseViewModel(message));
            }

            message = Translate(MessageHelper.CandidateDeletedSuccessfully);

            return Ok(new ResponseViewModel(message));
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
            LogUserAction("CandidateController", "AddHRNote", $"{candidateId}, {JsonSerializer.Serialize(AddHrNote)}", _userActionService);

            CandidateAddHRNoteDTO dto = _mapper.Map<CandidateAddHRNoteDTO>(AddHrNote);
            dto.RecruiterId = GetUserId();
            bool result = _candidateService.AddHRNote(candidateId, dto, out _errorMessage);
            string message;

            if (result == false)
            {
                message = Translate(_errorMessage);

                return BadRequest(new ResponseViewModel(message));
            }

            message = Translate(MessageHelper.InterviewNoteAdded);

            return Ok(new ResponseViewModel(message));
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
            LogUserAction("CandidateController", "AddTechInterviewNote", $"{candidateId}, {JsonSerializer.Serialize(AddTechNote)}", _userActionService);

            CandidateAddTechNoteDTO dto = _mapper.Map<CandidateAddTechNoteDTO>(AddTechNote);
            dto.TechId = GetUserId();
            bool result = _candidateService.AddTechNote(candidateId, dto, out _errorMessage);
            string message;

            if (result == false)
            {
                message = Translate(_errorMessage);

                return BadRequest(new ResponseViewModel(message));
            }

            message = Translate(MessageHelper.TechInterviewNoteAdded);

            return Ok(new ResponseViewModel(message));
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
            LogUserAction("CandidateController", "AssignTechAndRecruiter", $"{candidateId}, {JsonSerializer.Serialize(assignees)}", _userActionService);

            CandidateAssigneesDTO dto = _mapper.Map<CandidateAssigneesDTO>(assignees);
            dto.LastUpdatedDate = DateTime.Now;
            dto.LastUpdatedBy = GetUserId();

            bool result = _candidateService.AllocateRecruiterAndTech(candidateId, dto, out _errorMessage);
            string message;

            if (result == false)
            {
                message = Translate(_errorMessage);

                return BadRequest(new ResponseViewModel(message));
            }

            message = Translate(MessageHelper.EmployeesAssigned);

            return Ok(new ResponseViewModel(message));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
        [Route("Candidate/GetStageList")]
        [RequireUserRole("HR_MANAGER", "RECRUITER", "TECHNICIAN")]
        public IActionResult GetStageList()
        {
            LogUserAction("CandidateController", "GetStageList", "", _userActionService);

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
        [RequireUserRole("HR_MANAGER", "RECRUITER", "TECHNICIAN")]
        public IActionResult GetStatusList()
        {
            LogUserAction("CandidateController", "GetStatusList()", "", _userActionService);

            var listOfStatus = Enum.GetValues(typeof(CandidateStatuses)).Cast<CandidateStatuses>().Select(v => v.ToString());

            return new JsonResult(listOfStatus);
        }
    }
}