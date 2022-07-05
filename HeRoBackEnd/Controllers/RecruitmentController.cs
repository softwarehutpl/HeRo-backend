using Microsoft.AspNetCore.Mvc;

namespace HeRoBackEnd.Controllers
{
    public class RecruitmentController : Controller
    {
        private recruitmentService;

        public RecruitmentController()
        {
            RecruitmentService recruitmentService = new RecruitmentService();
        }
        public IActionResult Index()
        {
            List<Recruitment> recruitments = recruitmentService.GetAllActive();
            return View(recruitments);
        }

        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Recruitment tempRecruitment = recruitmentService.Get(id);

            if (tempRecruitment == null)
            {
                return RedirectToAction("Index");
            }

            return View(tempRecruitment);
        }

        public async Task<IActionResult> Create()
        {
            return View(new Recruitment());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewRecruitmentView newRecruitment)
        {
            recruitmentService.Add(newRecruitment);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NewRecruitmentView newRecruitment)
        {
            recruitmentService.Update(id, newRecruitment);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Finish(int id)
        {
            recruitmentService.Finish(id);

            return RedirectToAction("Index");
        }
    }
}
