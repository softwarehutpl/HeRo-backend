using HeRoBackEnd.ViewModels.Recruitment;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using Services.DTOs.Recruitment;
using Data.Entities;
using Services.DTOs.Recruitment;
using AutoMapper;
using Common.Enums;

namespace HeRoBackEnd.Controllers
{
    [ApiController]
    public class RecruitmentController : Controller
    {
        private readonly RecruitmentService service;
        private readonly IMapper mapper;

        public RecruitmentController(RecruitmentService service, IMapper map)
        {
            this.service = service;
            mapper = map;
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
        public IActionResult Create(RecruitmentCreateViewModel newRecruitment)
        {
            
            CreateRecruitmentDTO dto = mapper.Map<CreateRecruitmentDTO>(newRecruitment);
            int result=service.AddRecruitment(dto);

            if (result == -1) return BadRequest();
            
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Updates information about a recruitment represented by an id
        /// </summary>
        /// <param name="recruitmentId">Id representing a recruitment</param>
        /// <param name="newRecruitment">Contains new information about a recruitment</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("Recruitment/Edit/{recruitmentId}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int recruitmentId, RecruitmentEditViewModel newRecruitment)
        {
            /*
            UpdateRecruitmentDTO dto = mapper.Map<UpdateRecruitmentDTO>(newRecruitment);
            int result=service.UpdateRecruitment(recruitmentId, dto);

            if (result == -1) return BadRequest();
            */
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Changes the status of the recruitment
        /// </summary>
        /// <param name="newStatus">Contains inormation about a new status of the Recruitment</param>
        /// <returns>IActionResult</returns>
        [HttpPut]
        [Route("Recruitment/ChangeSatus/{recruitmentId}")]
        public IActionResult ChangeStaus(RecruitmentChangeStatusViewModel newStatus)
        {
            ChangeRecruitmentStatusDTO dto = mapper.Map<ChangeRecruitmentStatusDTO>(newStatus);
            dto.LastUpdatedDate = DateTime.Now;

            int result = service.ChangeStatus(dto);

            if (result == -1) return BadRequest();

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
        public IActionResult Finish(int recruitmentId)
        {
            int result=service.EndRecruitment(recruitmentId);

            if (result == -1) return BadRequest();

            return RedirectToAction("Index");
        }
    }
}
