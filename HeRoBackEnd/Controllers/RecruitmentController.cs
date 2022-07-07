using HeRoBackEnd.ViewModels.Recruitment;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using Services.DTOs.Recruitment;
using Data.Entities;

namespace HeRoBackEnd.Controllers
{
    [ApiController]
    public class RecruitmentController : Controller
    {
        private readonly RecruitmentService service;

        public RecruitmentController(RecruitmentService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Returns a list of recruitments
        /// </summary>
        /// <returns>Object of the JsonResult class representing the list of Recruitments in JSON format</returns>
        [HttpGet]
        [Route("Recruitment/Index")]
        public IActionResult Index()
        {
           List<ReadRecruitmentDTO> recruitments = service.GetRecruitments();

           return new JsonResult(recruitments);
        }

        /// <summary>
        /// Returns a recruitment specified by an id
        /// </summary>
        /// <param name="recruitmentId">Id of an recruitment</param>
        /// <returns>Onject of the JsonResult class representing a Recruitment in JSON format</returns>
        [HttpGet]
        [Route("Recruitment/Get/{recruitmentId}")]
        public IActionResult Get(int recruitmentId)
        {
            ReadRecruitmentDTO recruitment = service.GetRecruitment(recruitmentId);

            if (recruitment == null)
            {
                return RedirectToAction("Index");
            }

            return new JsonResult(recruitment);
        }

        /// <summary>
        /// Creates a new Recruitment
        /// </summary>
        /// <param name="newRecruitment">Contains information about a new recruitment</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("Recruitment/Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewRecruitmentViewModel newRecruitment)
        {
            //service.Add(newRecruitment);

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
            int result=service.EndRecruitment(recruitmentId);

            return RedirectToAction("Index");
        }
    }
}
