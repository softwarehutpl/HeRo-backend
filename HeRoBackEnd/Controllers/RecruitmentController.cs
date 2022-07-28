using AutoMapper;
using Common.AttributeRoleVerification;
using Common.Helpers;
using Common.Listing;
using Data.DTOs.Recruitment;
using HeRoBackEnd.ViewModels;
using HeRoBackEnd.ViewModels.Recruitment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Listing;
using Services.Services;
using System.Text.Json;

namespace HeRoBackEnd.Controllers
{
    [ApiController]
    public class RecruitmentController : BaseController
    {
        private readonly RecruitmentService _recruitmentService;
        private readonly IMapper _mapper;
        private string _errorMessage;
        private UserActionService _userActionService;

        public RecruitmentController(RecruitmentService service, IMapper map, UserActionService userActionService)
        {
            this._recruitmentService = service;
            _mapper = map;
            _userActionService = userActionService;
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
            LogUserAction("RecruitmentController", "Get", recruitmentId.ToString(), _userActionService);
            RecruitmentDetailsDTO recruitment = _recruitmentService.GetRecruitment(recruitmentId);

            if (recruitment == null)
            {
                string message = Translate(ErrorMessageHelper.NoRecruitment);

                return BadRequest(new ResponseViewModel(message));
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
        ///     <h3>Possible keys:</h3> "Id", "Name" <br />
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
            LogUserAction("RecruitmentController", "GetPublicList", JsonSerializer.Serialize(recruitmentListFilterViewModel), _userActionService);

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

            RecruitmentListing recruitments = _recruitmentService.GetRecruitments(paging, sortOrder, recruitmentFiltringDTO);

            if (recruitments == null)
            {
                string message = Translate(ErrorMessageHelper.ErrorListRecruitment);

                return BadRequest(new ResponseViewModel(message));
            }

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
        ///     <h3>Possible keys:</h3> "Id", "Name" <br />
        ///     <h3>Value:</h3> "DESC" - sort the result in descending order <br />
        ///                      Another value - sort the result in ascending order <br />
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">Error getting list of recruitments</response>
        /// <response code="200">List of recruitments</response>
        [HttpPost]
        [Route("Recruitment/GetList")]
        [RequireUserRole("HR_MANAGER", "RECRUITER", "TECHNICIAN", "ANONYMOUS")]
        [ProducesResponseType(typeof(RecruitmentListing), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public IActionResult GetList(RecruitmentListFilterViewModel recruitmentListFilterViewModel)
        {
            LogUserAction("RecruitmentController", "GetList", JsonSerializer.Serialize(recruitmentListFilterViewModel), _userActionService);

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

            RecruitmentListing recruitments = _recruitmentService.GetRecruitments(paging, sortOrder, recruitmentFiltringDTO);

            if (recruitments == null)
            {
                string message = Translate(ErrorMessageHelper.ErrorListRecruitment);

                return BadRequest(new ResponseViewModel(message));
            }

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
        [RequireUserRole("HR_MANAGER", "RECRUITER")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Create(RecruitmentCreateViewModel newRecruitment)
        {
            LogUserAction("RecruitmentController", "Create", JsonSerializer.Serialize(newRecruitment), _userActionService);

            CreateRecruitmentDTO dto = _mapper.Map<CreateRecruitmentDTO>(newRecruitment);
            int id = GetUserId();

            dto.CreatedById = id;
            dto.CreatedDate = DateTime.Now;
            dto.LastUpdatedById = id;
            dto.LastUpdatedDate = DateTime.Now;

            bool result = _recruitmentService.AddRecruitment(dto);
            string message;

            if (result == false)
            {
                message = Translate(ErrorMessageHelper.WrongData);

                return BadRequest(new ResponseViewModel(message));
            }

            message = Translate(MessageHelper.RecruitmentCreatedSuccessfully);

            return Ok(new ResponseViewModel(message));
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
        [RequireUserRole("HR_MANAGER", "RECRUITER")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public IActionResult Edit(int recruitmentId, RecruitmentEditViewModel recruitment)
        {
            LogUserAction("RecruitmentController", "Edit", $"{recruitmentId}, {JsonSerializer.Serialize(recruitment)}", _userActionService);

            UpdateRecruitmentDTO dto = _mapper.Map<UpdateRecruitmentDTO>(recruitment);
            int id = GetUserId();

            dto.LastUpdatedById = id;
            dto.LastUpdatedDate = DateTime.Now;

            bool result = _recruitmentService.UpdateRecruitment(recruitmentId, dto, out _errorMessage);
            string message;

            if (result == false)
            {
                message = Translate(_errorMessage);

                return BadRequest(new ResponseViewModel(message));
            }

            message = Translate(MessageHelper.RecruitmentEditedSuccessfully);

            return Ok(new ResponseViewModel(message));
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
        [RequireUserRole("HR_MANAGER", "RECRUITER")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public IActionResult End(int recruitmentId)
        {
            LogUserAction("RecruitmentController", "End", recruitmentId.ToString(), _userActionService);

            EndRecruimentDTO dto = new EndRecruimentDTO(recruitmentId);
            int id = GetUserId();

            dto.LastUpdatedById = id;
            dto.LastUpdatedDate = DateTime.Now;
            dto.EndedById = id;
            dto.EndedDate = DateTime.Now;
            bool result = _recruitmentService.EndRecruitment(dto, out _errorMessage);
            string message;

            if (result == false)
            {
                message = Translate(_errorMessage);

                return BadRequest(new ResponseViewModel(message));
            }

            message = Translate(MessageHelper.RecruitmentEndedSuccessfully);

            return Ok(new ResponseViewModel(message));
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
        [RequireUserRole("HR_MANAGER", "RECRUITER")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int recruitmentId)
        {
            LogUserAction("RecruitmentController", "Delete", recruitmentId.ToString(), _userActionService);

            DeleteRecruitmentDTO dto = new DeleteRecruitmentDTO(recruitmentId);
            int id = GetUserId();

            dto.LastUpdatedById = id;
            dto.LastUpdatedDate = DateTime.Now;
            dto.DeletedById = id;
            dto.DeletedDate = DateTime.Now;

            bool result = _recruitmentService.DeleteRecruitment(dto, out _errorMessage);
            string message;

            if (result == false)
            {
                message = Translate(_errorMessage);

                return BadRequest(new ResponseViewModel(message));
            }

            message = Translate(MessageHelper.RecruitmentDeletedSuccessfully);

            return Ok(new ResponseViewModel(message));
        }
    }
}