using HeRoBackEnd.ViewModels.Candidate;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System.ComponentModel.DataAnnotations;
//using Data.Entities;

namespace HeRoBackEnd.Controllers
{
    [ApiController]
    public class CandidateController : Controller
    {
        //private CandidateService candidateService;
        private ILogger<CandidateController> _logger;
        public CandidateController(ILogger<CandidateController> logger)
        {
            //  CandidateService candidateService = new CandidateService();
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
            //List<Candidate> candidates = candidateService.GetAllActive();

            // return new JsonResult(candidates);

            //return View();
            try
            {
                throw new Exception("Testing logging errors to database with NLog - entering Candidate/Index");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.ToString());
                return BadRequest(exception.ToString());
            }
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

            //Candidate tempCandidate = candidateService.Get(id);

            //if (tempCandidate == null)
            //{
            //    return RedirectToAction("Index");
            //}

            //return new JsonResult(tempCandidate);
            return View();
        }

        /// <summary>
        /// Creates a candidate
        /// </summary>
        /// <param name="newCandidate">object of the NewCandidateViewModel class
        /// containing information about the new candidate</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("Candidate/Create")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewCandidateViewModel newCandidate)
        {
            //candidateService.Add(newCandidate);
            return Ok();
            //return RedirectToAction("Index");
        }
        
        /// <summary>
        /// Updates information about a candidate
        /// </summary>
        /// <param name="candidateId">Id of the candidate</param>
        /// <param name="newCandidate">Updated information about the candidate</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("Candidate/Edit/{candidateId}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int candidateId, NewCandidateViewModel newCandidate)
        {
            //candidateService.Update(id, newCandidate);

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
            //candidateService.Delete(id);

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
            //CandidateService.AddCandidate(id, recruitmentId);

            return RedirectToAction("Index");
        }
        
        /// <summary>
        /// Adds a note concerning the candidate
        /// </summary>
        /// <param name="candidateId">Id of the candidate</param>
        /// <param name="notes">The content of the note</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("Candidate/AddNotes")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNotes(int candidateId, string notes)
        {
            //candidateService.AddNotes(id, notes);

            return RedirectToAction("Index");
        }
    }
}
