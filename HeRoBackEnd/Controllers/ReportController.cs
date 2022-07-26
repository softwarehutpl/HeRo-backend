using Common.AttributeRoleVerification;
using Data.DTOs.Report;
using HeRoBackEnd.ViewModels.Report;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System.Text.Json;

namespace HeRoBackEnd.Controllers
{
    [ApiController]
    public class ReportController : BaseController
    {
        private ReportService _reportService;
        private UserActionService _userActionService;

        public ReportController(ReportService reportService, UserActionService userActionService)
        {
            _reportService = reportService;
            _userActionService = userActionService;
        }

        /// <summary>
        /// Returns the number of new candidates
        /// </summary>
        /// <remarks>
        /// <h2>Format:</h2>
        ///    <h3>Date:</h3>
        ///    yyyy-MM-dd <br />
        ///    yyyy-MM-ddTHH:mm <br />
        ///    yyyy-MM-ddTHH:mm:ss <br />
        ///    yyyy-MM-ddTHH:mm:ss.fff <br />
        /// </remarks>
        /// <response code="200">Number of new candidates</response>
        [HttpPost]
        [Route("Report/CountNewCandidates")]
        [RequireUserRole("RECRUITER", "TECHNICIAN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public IActionResult CountNewCandidates(ReportCountViewModel reportViewModel)
        {
            LogUserAction("ReportController", "CountNewCandidates", JsonSerializer.Serialize(reportViewModel), _userActionService);
            ReportCountDTO reportDTO = new ReportCountDTO
            {
                Id = reportViewModel.Id,
                FromDate = reportViewModel.FromDate,
                ToDate = reportViewModel.ToDate
            };

            var result = _reportService.CountNewCandidates(reportDTO);

            return new JsonResult(result);
        }

        /// <summary>
        /// Returns 10 most popular Recruitments
        /// </summary>
        /// <response code="200">Number of new candidates</response>
        [HttpGet]
        [Route("Report/GetPopularRecruitments")]
        [RequireUserRole("RECRUITER", "TECHNICIAN")]
        [ProducesResponseType(typeof(IEnumerable<RaportPopularRecruitmentDTO>), StatusCodes.Status200OK)]
        public IActionResult GetPopularRecruitments()
        {
            LogUserAction("ReportController", "GetPopularRecruitments", "", _userActionService);

            var result = _reportService.GetPopularRecruitments();

            return new JsonResult(result);
        }
    }
}