using Microsoft.AspNetCore.Mvc;

namespace HeRoBackEnd.Controllers
{
    public class CandidateController : Controller
    {
        private candidateService; 

        public CandidateController()
        {
            CandidateService candidateService = new CandidateService();
        }

        public IActionResult Index()
        {
            List<Candidate> candidates = candidateService.GetAllActive();
            return View(candidates);
        }

        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Candidate tempCandidate = candidateService.Get(id);

            if (tempCandidate == null)
            {
                return RedirectToAction("Index");
            }

            return View(tempCandidate);
        }

        public async Task<IActionResult> Create()
        {
            return View(new Candidate());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Candidate newCandidate)
        {
            candidateService.Add(newCandidate);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Candidate tempCandidate = candidateService.Get(id);

            if (tempCandidate == null)
            {
                return RedirectToAction("Index");
            }

            return View(tempCandidate);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Candidate tempCandidate = candidateService.Get(id);

            if (tempCandidate == null)
            {
                return RedirectToAction("Index");
            }

            return View(tempCandidate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Candidate newCandidate)
        {
            candidateService.Update(id, newCandidate);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Candidate tempCandidate = candidateService.Get(id);

            if (tempCandidate == null)
            {
                return RedirectToAction("Index");
            }

            return View(tempCandidate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Candidate user)
        {
            candidateService.Delete(id, user);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRecruitment(int id, int idRecruitment)
        {
            candidateService.AddCandidate(id, idRecruitment);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNotes(int id, string notes)
        {
            candidateService.AddNotes(id, notes);

            return RedirectToAction("Index");
        }
    }
}
