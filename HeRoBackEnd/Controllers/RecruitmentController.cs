using HeRoBackEnd.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using Data.Entities;

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

            return new JsonResult(recruitments);
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

            return new JsonResult(tempRecruitment);
        }

        public async Task<IActionResult> Create()
        {
            return new JsonResult(new Recruitment());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewRecruitmentViewModel newRecruitment)
        {
            recruitmentService.Add(newRecruitment);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NewRecruitmentViewModel newRecruitment)
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
