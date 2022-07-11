using AutoMapper;
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
    public class CandidateController : Controller
    {
        private CandidateService candidateService;
        private ILogger<CandidateController> _logger;
        private readonly IMapper _mapper;




        public CandidateController(CandidateService candidateService, ILogger<CandidateController> logger, IMapper map)
        {
            this.candidateService = candidateService;
            _mapper = map;
            _logger = logger;
        }





        /// <summary>
        /// Returns a Json string of the list of candidates
        /// </summary>
        /// <returns>Json string representing a list of Candidates</returns>
        [HttpGet]
        [Route("Candidate/Index")]
        public IActionResult Index()
        {
            List<Candidate> candidates = candidateService.GetCandidates();

            return new JsonResult(candidates);
        }
        





        /// <summary>
        /// Returns a candidate specified by the id
        /// </summary>
        /// <param name="candidateId">Takes the id of a candidate</param>
        /// <returns>Json string representing a Candidate</returns>
        
        [HttpGet]
        [Route("Candidate/Get/{candidateId}")]
        public async Task<IActionResult> Get(int? candidateId)
        {
            if (candidateId == null)
            {
                return RedirectToAction("Index");
            }

            Candidate? tempCandidate = candidateService.GetCandidateById((int)candidateId);

            if (tempCandidate == null)
            {
                return RedirectToAction("Index");
            }

            return new JsonResult(tempCandidate);
        }





        /// <summary>
        /// Creates a candidate
        /// </summary>
        /// <param name="newCandidate">object of the NewCandidateViewModel class
        /// containing information about the new candidate</param>
        /// <param name="RecruitmentId">object of the int class
        /// containing ID of recruitment process</param>
        /// <param name="RecruiterId">object of the int class
        /// containing ID of assignee (HR)</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("Candidate/Create")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CandidateCreateViewModel newCandidate, int RecruitmentId, int RecruiterId)
        {
            CreateCandidateDTO dto = _mapper.Map<CreateCandidateDTO>(newCandidate);
            dto.Status = "New";
            

            int result = candidateService.CreateCandidate(dto, RecruitmentId, RecruiterId);

            if (result == -1) return BadRequest();

            return RedirectToAction("Index");
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
            dto.LastUpdatedDate = DateTime.Now;

            int result = candidateService.UpdateCandidate(candidateId, dto);

            if (result == -1) return BadRequest();

            return RedirectToAction("Index");
        }




        /// <summary>
        /// Deletes a candidate
        /// </summary>
        /// <param name="candidateId">Id of the candidate</param>
        /// <returns>IActionResult</returns>
        [HttpDelete]
        [Route("Candidate/Delete/{candidateId}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int candidateId)
        {
            int result = candidateService.DeleteCandidate(candidateId);

            if (result == -1) return BadRequest();

            return RedirectToAction("Index");
        }
        




        /// <summary>
        /// Adds a candidate to the recruitment
        /// </summary>
        /// <param name="candidateId">Id of the candidate</param>
        /// <param name="recruitmentId">Id of the recruitment</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("Candidate/AddRecruitment")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRecruitment(int candidateId, int recruitmentId)
        {
            //CandidateService.AddCandidate(candidateId, recruitmentId);

            return RedirectToAction("Index");
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
        public async Task<IActionResult> AddHRNote(int candidateId, string notes, int score)
        {
            candidateService.AddHRNote(candidateId, notes, score);

            return RedirectToAction("Index");
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
        public async Task<IActionResult> AddInterviewNote(int candidateId, string notes, int score)
        {
            candidateService.AddInterviewNote(candidateId, notes, score);

            return RedirectToAction("Index");
        }
    }
}
