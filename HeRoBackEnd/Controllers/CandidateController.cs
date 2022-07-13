﻿using AutoMapper;
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


        /// <summary> Returns a Json result object representing a list of candidates </summary>
        /// <returns>Json result object representing a list of Candidates</returns>
        /// <remarks>
        /// Example request (get first 3 candidates from the first page of results where status=="New", sort descending by Name):
        ///
        ///       {
        ///         "name": "",
        ///         "lastName": "",
        ///         "source": "",
        ///         "status": "New",
        ///         "position": "",
        ///         "stage": "",
        ///         "techId": 0,
        ///         "recruiterId": 0,
        ///         "recruitmentId": 0,
        ///         "paging": {
        ///           "pageSize": 3,
        ///           "pageNumber": 1
        ///          },
        ///         "sortOrder": {
        ///         "sort": [
        ///             {
        ///              "key": "name",
        ///              "value": "DESC"
        ///             }
        ///           ]
        ///         }
        ///        }
        /// </remarks>
       


        [ProducesResponseType(typeof(IEnumerable<CandidateInfoForListDTO>),StatusCodes.Status200OK)]
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
        //Return user email
        //string "coś tam"

        /// <summary> Returns a candidate specified by the id </summary>
        /// <param name="candidateId">Takes the id of a candidate</param>
        /// <returns>Json string representing a Candidate</returns>
        /// <response code="400">"Error getting candidate (bad parameters or candidate doesn't exist)"</response>

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CandidateProfileDTO>), StatusCodes.Status200OK)]
        [Route("Candidate/Get/{candidateId}")]
        public IActionResult Get(int candidateId)
        {
            
            CandidateProfileDTO? candDTO = _candidateService.GetCandidateProfileById(candidateId);

            if (candDTO == null)
            {
                return BadRequest("Error getting candidate (bad parameters or candidate doesn't exist)");
            }

            return new JsonResult(candDTO);
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
        /// <response code="400">string "Error creating candidate"</response>

        [HttpPost]
        [Route("Candidate/Create")]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(CandidateCreateViewModel newCandidate)
        {
            CreateCandidateDTO dto = _mapper.Map<CreateCandidateDTO>(newCandidate);
            dto.Status = CandidateStatusEnum.New.ToString();
            dto.ApplicationDate = DateTime.Now;
            
            int result = _candidateService.CreateCandidate(dto);

            if (result == -1) return BadRequest("Error creating candidate");

            return Ok("Candidate created successfully");
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
        ///       "recruitmentId": 1,
        ///       "status": "Hired",
        ///     }
        /// </remarks>
        /// <response code="200">string "Candidate updated successfully"</response>
        /// <response code="400">string "Error updating candidate"</response>
        [HttpPost]
        [Route("Candidate/Edit/{candidateId}")]
        //[ValidateAntiForgeryToken]
        public IActionResult Edit(int candidateId, CandidateEditViewModel candidate)
        {
            UpdateCandidateDTO dto = _mapper.Map<UpdateCandidateDTO>(candidate);
            dto.LastUpdatedDate = DateTime.Now;
            dto.LastUpdatedBy = GetUserId();
            int result = _candidateService.UpdateCandidate(candidateId, dto);

            if (result == -1) return BadRequest("Error updating candidate");

            return Ok("Candidate updated successfully");
        }


        /// <summary>
        /// Deletes a candidate
        /// </summary>
        /// <param name="candidateId">Id of the candidate</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">string "Candidate deleted successfully"</response>
        /// <response code="400">string "Error deleting candidate (or candidate doesn't exist)"</response>
        [HttpDelete]
        [Route("Candidate/Delete/{candidateId}")]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int candidateId)
        {
            DeleteCandidateDTO dto = new DeleteCandidateDTO(candidateId);
            int id = GetUserId();

            dto.LastUpdatedById = id;
            dto.LastUpdatedDate = DateTime.Now;
            dto.DeletedById = id;
            dto.DeletedDate = DateTime.Now;

            int result = _candidateService.DeleteCandidate(dto);

            if (result == -1) return BadRequest("Error deleting candidate (or candidate doesn't exist)");

            return Ok("Candidate deleted successfully");
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
        ///         "note": "Note about candidate given by HR",
        ///         "recruiterId": 5
        ///     }
        /// </remarks>
        /// <response code="200">string "Interview note added correctly"</response>
        /// <response code="400">string "Error adding note to candidate"</response>
        [HttpPost]
        [Route("Candidate/AddHRNote/")]
        //[ValidateAntiForgeryToken]
        public IActionResult AddHRNote(int candidateId, CandidateAddHRNoteViewModel AddHrNote)
        {
            CandidateAddHRNoteDTO dto = _mapper.Map<CandidateAddHRNoteDTO>(AddHrNote);
            int result = _candidateService.AddHRNote(candidateId, dto);
            if (result == -1) return BadRequest("Error adding note to candidate");
            else return Ok("Interview note added correctly");
            
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
        ///         "note": "Note about candidate given by tech",
        ///         "recruiterId": 3
        ///     }
        /// </remarks>
        /// <response code="200">string "Tech interview note added correctly"</response>
        /// <response code="400">string "Error adding tech note to candidate"</response>
        [HttpPost]
        [Route("Candidate/AddTechInterviewNote/{candidateId}")]
        //[ValidateAntiForgeryToken]
        public IActionResult AddTechInterviewNote(int candidateId, CandidateAddTechNoteViewModel AddTechNote)
        {
            CandidateAddTechNoteDTO dto = _mapper.Map<CandidateAddTechNoteDTO>(AddTechNote);
            int result = _candidateService.AddTechNote(candidateId, dto);
            if (result == -1) return BadRequest("Error adding tech note to candidate");
            else return Ok("Tech interview note added correctly");
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
        /// <response code="400">string "Error assigning employees to candidate"</response>
        [HttpPost]
        [Route("Candidate/AssignTechAndRecruiter/{candidateId}")]
        public IActionResult AssignTechAndRecruiter(int candidateId, CandidateAssigneesViewModel assignees)
        {
            
            CandidateAssigneesDTO dto = _mapper.Map<CandidateAssigneesDTO>(assignees);
            dto.LastUpdatedDate = DateTime.Now;
            dto.LastUpdatedBy = GetUserId();
            int result = _candidateService.AllocateRecruiterAndTech(candidateId, dto);

            if (result == -1)
            {
                return BadRequest("Error assigning employees to candidate");
            }

            return Ok("Employees assigned correctly");
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
        /// <response code="400">string "Error setting interview date"</response>
        [HttpPost]
        [Route("Candidate/SetInterviewDate/{candidateId}")]
        //[ValidateAntiForgeryToken]
        public IActionResult SetInterviewDate(int candidateId, CandidateAllocateInterviewDateViewModel interviewDateViewModel)
        {
           CandidateAllocateInterviewDateDTO dto = _mapper.Map<CandidateAllocateInterviewDateDTO>(interviewDateViewModel);
           dto.LastUpdatedDate = DateTime.Now;
           dto.LastUpdatedBy = GetUserId();
                      
           int result = _candidateService.AllocateRecruitmentInterview(candidateId, dto);
           if (result == -1) return BadRequest("Error setting interview date");
           else return Ok("Interview date set correctly");
            
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
        //[ValidateAntiForgeryToken]
        public IActionResult SetTechInterviewDate(int candidateId, CandidateAllocateInterviewDateViewModel interviewDateViewModel)
        {
           CandidateAllocateInterviewDateDTO dto = _mapper.Map<CandidateAllocateInterviewDateDTO>(interviewDateViewModel);
            dto.LastUpdatedDate = DateTime.Now;
            dto.LastUpdatedBy = GetUserId();

            int result = _candidateService.AllocateTechInterview(candidateId, dto);
               if (result == -1) return BadRequest("Error setting tech interview date");
               else return Ok("Tech interview date set correctly");
            
        }      
    }
}
