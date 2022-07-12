using Common.Listing;
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
        /// <param name="recruitmentListFilterViewModel">Object containing information about the filtering</param>
        /// <returns>Object of the JsonResult class representing the IEnumerable collection of recruitments in he JSON format</returns>
        [HttpPost]
        [Route("Recruitment/GetList")]
        public IActionResult GetList(RecruitmentListFilterViewModel recruitmentListFilterViewModel)
        {
            Paging paging = recruitmentListFilterViewModel.Paging;
            SortOrder sortOrder = recruitmentListFilterViewModel.SortOrder;
            RecruitmentFiltringDTO recruitmentFiltringDTO 
                = new RecruitmentFiltringDTO(
                    recruitmentListFilterViewModel.Name,  
                    recruitmentListFilterViewModel.Description,
                    recruitmentListFilterViewModel.BeginningDate,
                    recruitmentListFilterViewModel.EndingDate);

            IEnumerable<ReadRecruitmentDTO> recruitments = service.GetRecruitments(paging, sortOrder, recruitmentFiltringDTO);
            JsonResult result = new JsonResult(recruitments);

            return result;
        }

        /// <summary>
        /// Returns a recruitment specified by an id
        /// </summary>
        /// <param name="recruitmentId">Id of an recruitment</param>
        /// <returns>Object of the JsonResult class representing a recruitment in the JSON format</returns>
        [HttpGet]
        [Route("Recruitment/Get/{recruitmentId}")]
        public IActionResult Get(int recruitmentId)
        {
            ReadRecruitmentDTO recruitment = service.GetRecruitment(recruitmentId);

            if (recruitment == null)
            {
                return BadRequest();
            }

            JsonResult result = new JsonResult(recruitment);

            return result;
        }

        /// <summary>
        /// Creates a recruitment
        /// </summary>
        /// <param name="newRecruitment">Contains information about a new recruitment</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("Recruitment/Create")]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(RecruitmentCreateViewModel newRecruitment)
        {
            
            CreateRecruitmentDTO dto = mapper.Map<CreateRecruitmentDTO>(newRecruitment);
            dto.CreatedDate = DateTime.Now;
            dto.LastUpdatedDate = DateTime.Now;

            int result=service.AddRecruitment(dto);

            if (result == -1) return BadRequest();
            
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Updates a recruitment specified by an id
        /// </summary>
        /// <param name="recruitmentId">Id of a recruitment</param>
        /// <param name="recruitment">Contains new information about a recruitment</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("Recruitment/Edit/{recruitmentId}")]
        //[ValidateAntiForgeryToken]
        public IActionResult Edit(int recruitmentId, RecruitmentEditViewModel recruitment)
        {
            UpdateRecruitmentDTO dto = mapper.Map<UpdateRecruitmentDTO>(recruitment);
            dto.LastUpdatedDate = DateTime.Now;

            int result=service.UpdateRecruitment(recruitmentId, dto);

            if (result == -1) return BadRequest();
            
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Ends a recruitment specified by an id
        /// </summary>
        /// <param name="recruitmentId">Id of a recruitment</param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [Route("Recruitment/End/{recruitmentId}")]
        //[ValidateAntiForgeryToken]
        public IActionResult End(int recruitmentId)
        {
            int result=service.EndRecruitment(recruitmentId);

            if (result == -1) return BadRequest();

            return RedirectToAction("Index");
        }
        /// <summary>
        /// Deletes a recruitment represented by an id
        /// </summary>
        /// <param name="recruitmentId">Id representing a recruitment</param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [Route("Recruitment/Delete/{recruitmentId}")]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int recruitmentId)
        {
            int result = service.DeleteRecruitment(recruitmentId);

            if (result == -1) return BadRequest();

            return RedirectToAction("Index");
        }
    }
}
