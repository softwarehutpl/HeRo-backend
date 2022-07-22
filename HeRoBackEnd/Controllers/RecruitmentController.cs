using AutoMapper;
using Common.AttributeRoleVerification;
using Common.Listing;
using Data.DTOs.Recruitment;
using HeRoBackEnd.ViewModels;
using HeRoBackEnd.ViewModels.Recruitment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Listing;
using Services.Services;

namespace HeRoBackEnd.Controllers
{
    [ApiController]
    public class RecruitmentController : BaseController
    {
        private readonly RecruitmentService service;
        private readonly IMapper mapper;

        public RecruitmentController(RecruitmentService service, IMapper map)
        {
            this.service = service;
            mapper = map;
        }

        /// <summary>
        /// Returns a recruitment specified by an id
        /// </summary>
        /// <param name="recruitmentId" example="1">Id of an recruitment</param>
        /// <returns>Onject of the JsonResult class representing a Recruitment in JSON format</returns>
        /// <response code="400">There is no recruitment with such Id</response>
        /// <response code="200">Recruitment with given Id</response>
        [HttpGet]
        [Route("Recruitment/Get/{recruitmentId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RecruitmentDetailsDTO), StatusCodes.Status200OK)]
        public IActionResult Get(int recruitmentId)
        {
            RecruitmentDetailsDTO recruitment = service.GetRecruitment(recruitmentId);

            if (recruitment == null)
            {
                return BadRequest(new ResponseViewModel("There is no recruitment with such Id"));
            }

            return Ok(recruitment);
        }

        /// <summary>
        /// Returns a list of public recruitments
        /// </summary>
        /// <param name="recruitmentListFilterViewModel">Object containing information about the filtering</param>
        /// <returns>Object of the JsonResult class representing the list of Recruitments in JSON format</returns>
        /// <remarks>
        /// <h2>Format:</h2>
        ///    <h3>Date:</h3>
        ///    yyyy-MM-dd <br />
        ///    yyyy-MM-ddTHH:mm <br />
        ///    yyyy-MM-ddTHH:mm:ss <br />
        ///    yyyy-MM-ddTHH:mm:ss.fff <br />
        ///<h2>Nullable:</h2>
        ///    "name", "description", "beginningDate", "endingDate", "sortOrder" <br /><br />
        /// <h2>Filtring:</h2>
        ///    <h3>Contains:</h3> "name", "description" <br />
        ///    <h3>Bool:</h3> "showOpen", "showClosed" <br /><br />
        /// <h2>Sorting:</h2>
        ///     <h3>Possible keys:</h3> "Name" <br />
        ///     <h3>Value:</h3> "DESC" - sort the result in descending order <br />
        ///                      Another value - sort the result in ascending order <br />
        /// </remarks>
        /// <response code="400">Something went wrong!</response>
        /// <response code="200"></response>
        [HttpPost]
        [Route("Recruitment/GetPublicList")]
        [ProducesResponseType(typeof(RecruitmentListing), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public IActionResult GetPublicList(RecruitmentListFilterViewModel recruitmentListFilterViewModel)
        {
            Paging paging = recruitmentListFilterViewModel.Paging;
            SortOrder sortOrder = recruitmentListFilterViewModel.SortOrder;
            RecruitmentFiltringDTO recruitmentFiltringDTO
                = new RecruitmentFiltringDTO(
                    recruitmentListFilterViewModel.Name,
                    recruitmentListFilterViewModel.Description,
                    recruitmentListFilterViewModel.BeginningDate,
                    recruitmentListFilterViewModel.EndingDate,
                    false,
                    recruitmentListFilterViewModel.ShowOpen,
                    recruitmentListFilterViewModel.ShowClosed);

            RecruitmentListing recruitments = service.GetRecruitments(paging, sortOrder, recruitmentFiltringDTO);

            if (recruitments == null) return BadRequest();

            return Ok(recruitments);
        }

        /// <summary>
        /// Returns a list of recruitments
        /// </summary>
        /// <param name="recruitmentListFilterViewModel">Object containing information about the filtering</param>
        /// <returns>Object of the JsonResult class representing the list of Recruitments in JSON format</returns>
        /// <remarks>
        /// <h2>Format:</h2>
        ///    <h3>Date:</h3>
        ///    yyyy-MM-dd <br />
        ///    yyyy-MM-ddTHH:mm <br />
        ///    yyyy-MM-ddTHH:mm:ss <br />
        ///    yyyy-MM-ddTHH:mm:ss.fff <br /><br />
        /// <h2>Nullable:</h2>
        ///    "name", "description", "beginningDate", "endingDate", "sortOrder" <br /><br />
        /// <h2>Filtring:</h2>
        ///    <h3>Contains:</h3> "name", "description" <br />
        ///    <h3>Bool:</h3> "showOpen", "showClosed" <br /><br />
        /// <h2>Sorting:</h2>
        ///     <h3>Possible keys:</h3> "Name" <br />
        ///     <h3>Value:</h3> "DESC" - sort the result in descending order <br />
        ///                      Another value - sort the result in ascending order <br />
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">Error getting list of recruitments</response>
        /// <response code="200">List of recruitments</response>
        [HttpPost]
        [Route("Recruitment/GetList")]
        [RequireUserRole(UserRoles = new string[] { "HR_MANAGER", "RECRUITER", "TECHNICIAN", "ANONYMOUS" })]
        [ProducesResponseType(typeof(RecruitmentListing), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public IActionResult GetList(RecruitmentListFilterViewModel recruitmentListFilterViewModel)
        {
            Paging paging = recruitmentListFilterViewModel.Paging;
            SortOrder sortOrder = recruitmentListFilterViewModel.SortOrder;
            RecruitmentFiltringDTO recruitmentFiltringDTO
                = new RecruitmentFiltringDTO(
                    recruitmentListFilterViewModel.Name,
                    recruitmentListFilterViewModel.Description,
                    recruitmentListFilterViewModel.BeginningDate,
                    recruitmentListFilterViewModel.EndingDate,
                    true,
                    recruitmentListFilterViewModel.ShowOpen,
                    recruitmentListFilterViewModel.ShowClosed);

            RecruitmentListing recruitments = service.GetRecruitments(paging, sortOrder, recruitmentFiltringDTO);

            if (recruitments == null) return BadRequest("Error getting list of recruitments");

            return Ok(recruitments);
        }

        /// <summary>
        /// Creates a recruitment
        /// </summary>
        /// <param name="newRecruitment">Contains information about a new recruitment</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Recruitment created successfully</response>
        /// <response code="400">Error creating recruitment</response>
        [HttpPost]
        [Route("Recruitment/Create")]
        [RequireUserRole(UserRoles = new string[] { "HR_MANAGER", "RECRUITER" })]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Create(RecruitmentCreateViewModel newRecruitment)
        {
            CreateRecruitmentDTO dto = mapper.Map<CreateRecruitmentDTO>(newRecruitment);
            int id = GetUserId();

            dto.CreatedById = id;
            dto.CreatedDate = DateTime.Now;
            dto.LastUpdatedById = id;
            dto.LastUpdatedDate = DateTime.Now;

            int result = service.AddRecruitment(dto);

            if (result == -1)
            {
                return BadRequest(new ResponseViewModel("Wrong data!"));
            }

            return Ok(new ResponseViewModel("Recruitment created successfully"));
        }

        /// <summary>
        /// Updates a recruitment specified by an id
        /// </summary>
        /// <param name="recruitmentId" example="1">Id representing a recruitment</param>
        /// <param name="recruitment">Contains new information about a recruitment</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Recruitment updated successfully</response>
        /// <response code="400">Error updating recruitment or there is no recruitment with such Id</response>
        [HttpPost]
        [Route("Recruitment/Edit/{recruitmentId}")]
        [RequireUserRole(UserRoles = new string[] { "HR_MANAGER", "RECRUITER" })]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public IActionResult Edit(int recruitmentId, RecruitmentEditViewModel recruitment)
        {
            UpdateRecruitmentDTO dto = mapper.Map<UpdateRecruitmentDTO>(recruitment);
            int id = GetUserId();

            dto.LastUpdatedById = id;
            dto.LastUpdatedDate = DateTime.Now;

            int result = service.UpdateRecruitment(recruitmentId, dto);

            if (result == 0)
            {
                return BadRequest(new ResponseViewModel("No recruitment with this Id"));
            }

            if (result == -1)
            {
                return BadRequest(new ResponseViewModel("Wrong data!"));
            }

            return Ok(new ResponseViewModel("Recruitment updated successfully"));
        }

        /// <summary>
        /// Ends a recruitment specified by an id
        /// </summary>
        /// <param name="recruitmentId" example="1">Id representing a recruitment</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Recruitment ended successfully</response>
        /// <response code="400">Error ending recruitment or there is no recruitment with such Id</response>
        [HttpGet]
        [Route("Recruitment/End/{recruitmentId}")]
        [RequireUserRole(UserRoles = new string[] { "HR_MANAGER", "RECRUITER" })]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public IActionResult End(int recruitmentId)
        {
            EndRecruimentDTO dto = new EndRecruimentDTO(recruitmentId);
            int id = GetUserId();

            dto.LastUpdatedById = id;
            dto.LastUpdatedDate = DateTime.Now;
            dto.EndedById = id;
            dto.EndedDate = DateTime.Now;
            int result = service.EndRecruitment(dto);

            if (result == 0)
            {
                return BadRequest(new ResponseViewModel("No recruitment with this Id"));
            }

            if (result == -1)
            {
                return BadRequest(new ResponseViewModel("Something went wrong!"));
            }

            return Ok(new ResponseViewModel("Recruitment ended successfully"));
        }

        /// <summary>
        /// Deletes a recruitment represented by an id
        /// </summary>
        /// <param name="recruitmentId" example="1">Id representing a recruitment</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Recruitment deleted successfully</response>
        /// <response code="400">Error deleting recruitment or there is no recruitment with such Id</response>
        [HttpGet]
        [Route("Recruitment/Delete/{recruitmentId}")]
        [RequireUserRole(UserRoles = new string[] { "HR_MANAGER", "RECRUITER" })]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int recruitmentId)
        {
            DeleteRecruitmentDTO dto = new DeleteRecruitmentDTO(recruitmentId);
            int id = GetUserId();

            dto.LastUpdatedById = id;
            dto.LastUpdatedDate = DateTime.Now;
            dto.DeletedById = id;
            dto.DeletedDate = DateTime.Now;

            int result = service.DeleteRecruitment(dto);

            if (result == 0)
            {
                return BadRequest(new ResponseViewModel("No recruitment with this Id"));
            }

            if (result == -1)
            {
                return BadRequest(new ResponseViewModel("Something went wrong!"));
            }

            return Ok(new ResponseViewModel("Recruitment deleted successfully"));
        }
    }
}