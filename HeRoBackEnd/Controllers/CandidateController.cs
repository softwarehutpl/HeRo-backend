using HeRoBackEnd.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System.ComponentModel.DataAnnotations;
//using Data.Entities;

namespace HeRoBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CandidateController : Controller
    {
        //private CandidateService candidateService;

        public CandidateController()
        {
          //  CandidateService candidateService = new CandidateService();
        }
        
        /*
        public IActionResult Index()
        {
            //List<Candidate> candidates = candidateService.GetAllActive();

           // return new JsonResult(candidates);

            return View();
        }
        */
        /// <summary>
        /// Returns a candidate specified by the id
        /// </summary>
        /// <param name="id">Takes the id of a client</param>
        /// <returns>Candidate specified by the id</returns>

        [HttpPost]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
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
        /*
        //public async Task<IActionResult> Create()
        //{
        //    return new JsonResult(new Candidate());
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewCandidateViewModel newCandidate)
        {
            //candidateService.Add(newCandidate);

            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NewCandidateViewModel newCandidate)
        {
            //candidateService.Update(id, newCandidate);

            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            //candidateService.Delete(id);

            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRecruitment(int id, int recruitmentId)
        {
            //candidateService.AddCandidate(id, recruitmentId);

            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNotes(int id, string notes)
        {
            //candidateService.AddNotes(id, notes);

            return RedirectToAction("Index");
        }*/
    }
}
