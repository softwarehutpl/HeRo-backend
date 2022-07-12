using AutoMapper;
using Common.Enums;
using Common.Listing;
using Data.Entities;
using HeRoBackEnd.ViewModels.Candidate;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.Candidate;
using Services.Services;
using System.ComponentModel.DataAnnotations;
//using Data.Entities;

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
        /// Returns a Json result object representing a list of candidates
        /// </summary>
        /// <returns>Json result object representing a list of Candidates</returns>
        [HttpPost]
        [Route("Candidate/Index")]
        public IActionResult Index(CandidateListFilterViewModel candidateListFilterViewModel)
        {
            Paging paging = candidateListFilterViewModel.Paging;
            SortOrder sortOrder = candidateListFilterViewModel.SortOrder;
            CandidateFilteringDTO candidateFilteringDTO
                = new CandidateFilteringDTO(
                    candidateListFilterViewModel.Status,
                    candidateListFilterViewModel.Stage,
                    candidateListFilterViewModel.RecruiterId,
                    candidateListFilterViewModel.TechId,
                    candidateListFilterViewModel.RecruitmentId);

            IEnumerable<CandidateInfoForListDTO> result = _candidateService.GetCandidates(paging, sortOrder, candidateFilteringDTO);

            return new JsonResult(result);
        }


        /// <summary>
        /// Returns a candidate specified by the id
        /// </summary>
        /// <param name="candidateId">Takes the id of a candidate</param>
        /// <returns>Json string representing a Candidate</returns>

        [HttpGet]
        [Route("Candidate/Get/{candidateId}")]
        public IActionResult Get(int candidateId)
        {
            
            CandidateProfileDTO? candDTO = _candidateService.GetCandidateProfileById(candidateId);

            if (candDTO == null)
            {
                return BadRequest();
            }

            return new JsonResult(candDTO);
        }


        /// <summary>
        /// Creates a candidate
        /// </summary>
        /// <param name="newCandidate">object of the NewCandidateViewModel class
        /// containing information about the new candidate</param>
        /// <param name="RecruiterId">object of the int class
        /// containing ID of assignee (HR)</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("Candidate/Create")]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(CandidateCreateViewModel newCandidate)
        {
            CreateCandidateDTO dto = _mapper.Map<CreateCandidateDTO>(newCandidate);
            dto.Status = CandidateStatusEnum.New.ToString();
            dto.ApplicationDate = DateTime.Now;

            int result = _candidateService.CreateCandidate(dto);

            if (result == -1) return BadRequest();

            return Ok();
        }


        /// <summary>
        /// Updates information about a candidate
        /// </summary>
        /// <param name="candidateId">Id of the candidate</param>
        /// <param name="candidate">Updated information about the candidate</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("Candidate/Edit/{candidateId}")]
        //[ValidateAntiForgeryToken]
        public IActionResult Edit(int candidateId, CandidateEditViewModel candidate)
        {
            UpdateCandidateDTO dto = _mapper.Map<UpdateCandidateDTO>(candidate);
                       
            int result = _candidateService.UpdateCandidate(candidateId, dto);

            if (result == -1) return BadRequest();

            return Ok();
        }


        /// <summary>
        /// Deletes a candidate
        /// </summary>
        /// <param name="candidateId">Id of the candidate</param>
        /// <returns>IActionResult</returns>
        [HttpDelete]
        [Route("Candidate/Delete/{candidateId}")]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int candidateId)
        {
            int result = _candidateService.DeleteCandidate(candidateId);

            if (result == -1) return BadRequest();

            return Ok();
        }
        

        /// <summary>
        /// Adds a note concerning the candidate (given by HR)
        /// </summary>
        /// <param name="candidateId">Id of the candidate</param>
        /// <param name="notes">The content of the note</param>
        /// <param name="score">The score given by HR</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("Candidate/AddHRNote/{candidateId}")]
        //[ValidateAntiForgeryToken]
        public IActionResult AddHRNote(int candidateId, string notes, int score)
        {
            int result = _candidateService.AddHRNote(candidateId, notes, score);
            if (result == -1) return BadRequest();
            else return Ok();
            
        }

        /// <summary>
        /// Adds a note concerning the candidate (given by tech)
        /// </summary>
        /// <param name="candidateId">Id of the candidate</param>
        /// <param name="notes">The content of the note</param>
        /// <param name="score">The score given by tech</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("Candidate/AddInterviewNote/{candidateId}")]
        //[ValidateAntiForgeryToken]
        public IActionResult AddInterviewNote(int candidateId, string notes, int score)
        {
            int result = _candidateService.AddInterviewNote(candidateId, notes, score);
            if (result == -1) return BadRequest();
            else return Ok();
        }


        /// <summary>
        /// Assign tech and recruiter to candidate
        /// </summary>
        /// <param name="candidateId">Takes the id of a candidate</param>
        /// <param name="assignees">Updated information about the candidates assignees</param>
        /// <returns>IActionResult</returns>

        [HttpPost]
        [Route("Candidate/AssignTechAndRecruiter/{candidateId}")]
        public IActionResult AssignTechAndRecruiter(int? candidateId, CandidateAssigneesViewModel assignees)
        {
            if (candidateId == null)
            {
                return BadRequest();
            }
            CandidateAssigneesDTO dto = _mapper.Map<CandidateAssigneesDTO>(assignees);
            int result = _candidateService.AllocateRecruiterAndTech((int)candidateId, dto);

            if (result == -1)
            {
                return BadRequest();
            }

            return Ok();
        }


        /// <summary>
        /// Assigns dates of interviews for the candidate.
        /// </summary>
        /// <param name="candidateId">Id of the candidate</param>
        /// <param name="date">The date of tech interview</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("Candidate/SetInterviewDate/{candidateId}")]
        //[ValidateAntiForgeryToken]
        public IActionResult SetInterviewDate(int candidateId, string? date)
        {
            if (date == null)
                return BadRequest();
            else
            {
                int result = _candidateService.AllocateRecruitmentInterview(candidateId, DateTime.Parse(date));
                if (result == -1) return BadRequest();
                else return Ok();
            }
        }


        /// <summary>
        /// Assigns dates of tech interviews for the candidate.
        /// </summary>
        /// <param name="candidateId">Id of the candidate</param>
        /// <param name="date">The date of tech interiew</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("Candidate/SetTechInterviewDate/{candidateId}")]
        //[ValidateAntiForgeryToken]
        public IActionResult SetTechInterviewDate(int candidateId, string? date)
        {
            if (date == null)
                return BadRequest();
            else
            {
                int result = _candidateService.AllocateTechInterview(candidateId, DateTime.Parse(date));
                if (result == -1) return BadRequest();
                else return Ok();
            }
        }      
    }
}
