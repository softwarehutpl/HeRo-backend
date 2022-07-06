using HeRoBackEnd.ViewModels.Candidate;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
//using Data.Entities;

namespace HeRoBackEnd.Controllers
{
    public class CandidateController : Controller
    {
        //private CandidateService candidateService;

        public CandidateController()
        {
          //  CandidateService candidateService = new CandidateService();
        }

        public IActionResult Index()
        {
            //List<Candidate> candidates = candidateService.GetAllActive();

           // return new JsonResult(candidates);

            return View();
        }

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
        }
    }
}
