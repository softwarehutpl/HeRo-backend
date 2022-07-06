using HeRoBackEnd.ViewModels.Recruitment;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
//using Data.Entities;

namespace HeRoBackEnd.Controllers
{
    [ApiController]
    public class RecruitmentController : Controller
    {
        //private RecruitmentService recruitmentService;

        public RecruitmentController()
        {
            //RecruitmentService recruitmentService = new RecruitmentService();
        }

        /// <summary>
        /// Returns a list of recruitments
        /// </summary>
        /// <returns>Json string representing the list of Recruitments</returns>
        [HttpGet]
        [Route("Recruitment/Index")]
        public IActionResult Index()
        {
           // List<Recruitment> recruitments = recruitmentService.GetAllActive();

           // return new JsonResult(recruitments);
           return View();
        }

        /// <summary>
        /// Returns a recruitment specified by an id
        /// </summary>
        /// <param name="recruitmentId">Id of an recruitment</param>
        /// <returns>Json string representing a Recruitment</returns>
        [HttpGet]
        [Route("Recruitment/Get/{recruitmentId}")]
        public async Task<IActionResult> Get(int? recruitmentId)
        {
            if (recruitmentId == null)
            {
                return RedirectToAction("Index");
            }
            return View();
            //Recruitment tempRecruitment = recruitmentService.Get(id);

            //if (tempRecruitment == null)
            //{
            //    return RedirectToAction("Index");
            //}

            //return new JsonResult(tempRecruitment);
        }

        /// <summary>
        /// Creates a new Recruitment
        /// </summary>
        /// <param name="newRecruitment">Contains information about a new recruitment</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("Recruitment/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewRecruitmentViewModel newRecruitment)
        {
            //recruitmentService.Add(newRecruitment);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Updates information about a recruitment represented by an id
        /// </summary>
        /// <param name="recruitmentId">Id representing a recruitment</param>
        /// <param name="newRecruitment">Contains current information about a recriutment</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Recruitment/Edit/{recruitmentId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int recruitmentId, NewRecruitmentViewModel newRecruitment)
        {
            //recruitmentService.Update(recruitmentId, newRecruitment);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Ends a recruitment represented by an id
        /// </summary>
        /// <param name="recruitmentId">Id representing a recruitment</param>
        /// <returns>IActionResult</returns>
        [HttpDelete]
        [Route("Recruitment/Finish/{recruitmentId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Finish(int recruitmentId)
        {
            //RecruitmentService.EndRecruitment(recruitmentId);

            return RedirectToAction("Index");
        }
    }
}
